﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador
{
    public class UsersController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Users
        /*Shows the list of users and persons in the database. Only displays users whose associated person exists.
         Users created without a person won't be shown.*/

         /*Responds to User Story TAM-2.1.*/
        public ActionResult Index()
        {
            //Verificamos si hay un mensaje de alerta de alguna de las operanciones realizadas, si lo hay lo desplegamos con javascript
            if (TempData["alertmessage"] != null)
            {
                ViewBag.AlertMessage = TempData["alertmessage"].ToString();
            }

            string username = HttpContext.User.Identity.Name;
            if (username != "admin@mail.com")
            {
                TempData["alertmessage"] = "Solo el administrador puede accesar esta pagina";
                return RedirectToAction("../Home/Index");
            }
            /*To show the list of all users first fetch all the users and persons in the database, and join them 
             by the key: mail address.*/
            List<Usuario> Usuarios = db.Usuario.ToList();
            List<Persona> Personas = db.Persona.ToList();

            /*Creates a list with the joiner entity "usuarioPersona", and then sends them to the view.*/
            var usuarioPersona = from u in Usuarios
                                 join p in Personas on u.Username equals p.Correo into table1
                                 from p in table1.ToList()
                                 select new UsuarioPersona
                                 {
                                     Usuario = u,
                                     Persona = p
                                 };
            usuarioPersona.OrderBy(up => up.Persona.Identificacion);
            return View(usuarioPersona);
        }
        /*End of User Story TAM-2.1.*/

        // GET: Users/Details/5
        /*Shows the details of a selected user.*/
        /*Responds to User Story TAM-2.9.*/
        public ActionResult Details(string username, string domain)
        {
            /* Full query string is splitted into username and domain to avoid problems while sending the string to 
             * the controller. Ex: username: john.doe@mail, domain: .com*/
            string id = username + domain;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /*To view the details of a selected user, first fetch the user and its associated person in the database.*/
            Persona persona = db.Persona.Find(id);
            Usuario usuario = db.Usuario.Find(id);

            /*Then, join these entities into "usuarioPersona" and send this object to the view.*/
            UsuarioPersona usuarioPersona = new UsuarioPersona();
            usuarioPersona.Usuario = usuario;
            usuarioPersona.Persona = persona;

            if (persona == null || usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuarioPersona);
        }
        /*End of user story TAM-2.9.*/

        // GET: Users/Create
        /*Two functions corresponding to User Story TAM-2.2.*/
        public ActionResult Create()
        {
            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne");
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo");
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Persona persona)
        {
            if (ModelState.IsValid && persona != null)
            {
                /*To create a new user-person, first create the user using the stored procedure "AgregarUsuario"
                 using as username the principal mail provided, and as password the first name.*/
                /*TO-DO: modify the view to allow custom password setting.*/

                List<Persona> Personas = db.Persona.ToList();

                //Primero confirmamos si alguna persona existe con ese correo
                if (db.Persona.Find(persona.Correo) == null)
                {
                    ObjectParameter result = new ObjectParameter("result", typeof(bool));
                    db.CheckID(persona.Identificacion, result);
                    bool checkResult = (bool)result.Value;
                    //Una vez confirmado verificamos si existe otra persona con ese mismo numero de identificacion
                    if (checkResult == false)
                    {
                        //Ahora verificamos si el usuario introdujo un Carne, si si lo introdujo entonces agregamos el correo a los datos que van a ser insertados en Estudiante, si no
                        //borramos todos los datos de estudiante para que el framework no itente añadirlo
                        if(persona.Estudiante.Carne != null)
                        {
                            persona.Estudiante.Correo = persona.Correo;
                        }
                        else
                        {                            
                            persona.Estudiante = null;
                        }
                        //Nadie repetido, añadir a la BD
                        db.Persona.Add(persona);
                        db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Identificacion", "Ya existe un usuario en el sistema con esta identificación.");
                        return View(persona);
                    }
                }
                else
                {
                    ModelState.AddModelError("Correo", "Ya existe un usuario en el sistema con este correo.");
                    return View(persona);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", persona.Usuario);
 
            return View(persona);
        }
        /*End of User Story TAM-2.2.*/
        // GET: Users/Edit/5

        /*Functions to edit a selected user account.*/
        /*Respond to User Story 2.4.*/
        public ActionResult Edit(string username, string domain)
        {
            string id = username + domain;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /*To create a joint view of User and Person, first is necessary to look for the person and the user separately.*/
            Persona persona = db.Persona.Find(id);
            Usuario usuario = db.Usuario.Find(id);

            /*Then these entities get together in the model "UsuarioPersona" to make the CRUD operations easier over 
             these objects.*/

            UsuarioPersona usuarioPersona = new UsuarioPersona();
            usuarioPersona.Persona = persona;
            usuarioPersona.Usuario = usuario;
            if (persona == null || usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", persona.Usuario);

            /*Saves the current user being edited's mail, to search in the database with this key in case of changing it.*/
            System.Web.HttpContext.Current.Application["CurrentEditingUser"] = usuarioPersona.Usuario.Username;

            /*Return the joint view of the selected User and Person as one plain entity.*/
            return View(usuarioPersona);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioPersona usuarioPersona)
        {
            if (ModelState.IsValid && 
                usuarioPersona != null && 
                usuarioPersona.Persona != null || 
                usuarioPersona.Usuario != null )
            {
                /*To edit an user, first fetch it from the database using the stored email in the other edit function.*/
                using (var db = new DataIntegradorEntities()) {

                    string formerUserMail = (string)System.Web.HttpContext.Current.Application["CurrentEditingUser"];
                    var originalUser = db.Usuario.SingleOrDefault(u => u.Username == formerUserMail);

                    if (originalUser != null && usuarioPersona != null && usuarioPersona.Usuario != null)
                    {
                        originalUser.Activo = usuarioPersona.Usuario.Activo;
                        db.SaveChanges();
                        /*TO-DO: Stored procedure to change the password of a given user. Need to recalculate the "salt" and the SHA 256.*/
                    }

                    /*To edit a person, first fetch him from the database using the email passed by the view.*/
                    var originalPerson = db.Persona.SingleOrDefault(p => p.Correo == formerUserMail);

                    if (originalPerson != null && usuarioPersona != null && usuarioPersona.Persona != null)
                    {
                        /*Stored procedure to change the mail of a given person*/
                        ObjectParameter modResult = new ObjectParameter("resultado", typeof(bool));
                        db.ModificarCorreo(originalPerson.Correo, usuarioPersona.Persona.Correo, modResult);
                        bool modificationResult = (bool)modResult.Value;
                        
                        /*No pudo modificarse el correo por ya estar en la base de datos*/
                        if (modificationResult == false)
                        {
                            ModelState.AddModelError("Correo", "Ya existe un usuario en el sistema con este correo.");
                            return View(usuarioPersona);
                        }
                        
                        if (originalUser != null)
                        {
                            /*Stored procedure to change the mail of a given user*/
                            //db.ModificarUsername(formerUserMail, usuarioPersona.Persona.Correo);
                        }
                        
                        /*Updates each editable field of the selected user, and then stores the data back to the DB.*/
                        originalPerson.Nombre1 = usuarioPersona.Persona.Nombre1;
                        originalPerson.Nombre2 = usuarioPersona.Persona.Nombre2;
                        originalPerson.Apellido1 = usuarioPersona.Persona.Apellido1;
                        originalPerson.Apellido2 = usuarioPersona.Persona.Apellido2;
                        originalPerson.CorreoAlt = usuarioPersona.Persona.CorreoAlt;
                        originalPerson.TipoIdentificacion = usuarioPersona.Persona.TipoIdentificacion;
                        originalPerson.Identificacion= usuarioPersona.Persona.Identificacion;
                        if (originalPerson.Estudiante == null)
                            originalPerson.Estudiante = new Estudiante();
                        originalPerson.Estudiante.Carne = usuarioPersona.Persona.Estudiante.Carne;

                        db.SaveChanges();
                    }
                }
            }

            /*Since the joint view "UsuarioPersona" is not a database entity, we have to rebuild the view, to show 
             the changes made in the view.*/

            string originalMail = (string)System.Web.HttpContext.Current.Application["CurrentEditingUser"];
            string mailToSearch = usuarioPersona.Persona.Correo == null ? originalMail : usuarioPersona.Persona.Correo;
            
            /*Searches the user and person tuples associated to the edited user.*/
            Usuario usuarioEdited = db.Usuario.Find(mailToSearch);
            Persona personaEdited = db.Persona.Find(mailToSearch);

            /*Joins the tuples in the UsuarioPersona object to be shown in the view.*/
            UsuarioPersona usuarioPersonaRefreshed = new UsuarioPersona();
            usuarioPersonaRefreshed.Persona = personaEdited;
            usuarioPersonaRefreshed.Usuario = usuarioEdited;

            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", usuarioPersona.Persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", usuarioPersona.Persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", usuarioPersona.Persona.Usuario);

            /*Removes the temporal stored mail, saved in the first Edit() funcion.*/
            System.Web.HttpContext.Current.Application.Remove("CurrentEditingUser");

            return View(usuarioPersonaRefreshed);
        }
        /*End of User Story TAM-2.4.*/

        // POST: Users/Delete/5
        
        /*Deletes a selected user if the action was confirmed in the view.*/
        /*Responds to User Story TAM-2.3.*/
        public ActionResult DeleteConfirmed(string username, string domain, bool confirmed)
        {
            /*Before deleting permanently the user, first checks whether the action was confirmed in the view or not.*/
            if (!confirmed)
                return RedirectToAction("Index");

            /*If it was confirmed, then delete the user and its related person.*/
            string id = username + domain;
            Usuario usuario = db.Usuario.Find(id);

            if (usuario != null)
            {
                if(id != "admin@mail.com")
                {
                    /*Both user and person tuples are deleted from the database.*/
                    /*TO-DO: make the person deletion optional, user able to choose deleting only the user.*/
                    db.Usuario.Remove(usuario);
                    db.SaveChanges();
                }
                else
                {
                    TempData["alertmessage"] = "No se puede borrar el Administrador";
                }

            }
            else {
                /*Error message to be shown at page footer.*/
                /*TO-DO: find a better way of showing errors.*/
                TempData["alertmessage"] = "No se pudo borrar el usuario";
            }
            
            return RedirectToAction("Index");
        }
        /*End of User Story TAM-2.4.*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
