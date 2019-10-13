using System;
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
        public ActionResult Index()
        {
            List<Usuario> Usuarios = db.Usuario.ToList();
            List<Persona> Personas = db.Persona.ToList();

            var usuarioPersona = from u in Usuarios
                                 join p in Personas on u.Username equals p.Correo into table1
                                 from p in table1.ToList()
                                 select new UsuarioPersona
                                 {
                                     Usuario = u,
                                     Persona = p
                                 };

            //var persona = db.Persona.Include(p => p.Estudiante).Include(p => p.Funcionario).Include(p => p.Usuario1);
            return View(usuarioPersona);
        }

        // GET: Users/Details/5
        public ActionResult Details(string username, string domain)
        {
            string id = username + domain;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne");
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo");
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Correo,CorreoAlt,Identificacion,Nombre1,Nombre2,Apellido1,Apellido2,Usuario,TipoIdentificacion")] Persona persona)
        {
            if (ModelState.IsValid && persona != null)
            {
                ObjectParameter createResult = new ObjectParameter("estado", typeof(Boolean));
                db.AgregarUsuario(persona.Correo, persona.Nombre1, true, createResult);
                persona.Usuario = persona.Correo;
                db.Persona.Add(persona);
                
                bool status = (bool)createResult.Value;
                if (status)
                {
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("Correo", "¡Ya existe un usuario en el sistema con ese correo!");
                    return View(persona);
                }
                return RedirectToAction("Index");
            }

            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", persona.Usuario);
            return View(persona);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string username, string domain)
        {
            string id = username + domain;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", persona.Usuario);
            return View(persona);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Correo,CorreoAlt,Identificacion,Nombre1,Nombre2,Apellido1,Apellido2,Usuario,TipoIdentificacion")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Correo = new SelectList(db.Estudiante, "Correo", "Carne", persona.Correo);
            ViewBag.Correo = new SelectList(db.Funcionario, "Correo", "Correo", persona.Correo);
            ViewBag.Usuario = new SelectList(db.Usuario, "Username", "Password", persona.Usuario);
            return View(persona);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string username, string domain)
        {
            string id = username + domain;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Users/Delete/5
        
        public ActionResult DeleteConfirmed(string username, string domain, bool confirmed)
        {
            if(!confirmed)
                return RedirectToAction("Index");
            string id = username + domain;
            Persona persona = db.Persona.Find(id);
            Usuario usuario = db.Usuario.Find(persona.Correo);
            if (usuario == null)
                usuario = db.Usuario.Find(persona.Nombre1);

            if (usuario != null && persona != null)
            {
                db.Usuario.Remove(usuario);
                db.Persona.Remove(persona);
                db.SaveChanges();
            }
            else {
                TempData["Message"] = "No se pudo borrar el usuario!";
            }
            
            return RedirectToAction("Index");
        }

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
