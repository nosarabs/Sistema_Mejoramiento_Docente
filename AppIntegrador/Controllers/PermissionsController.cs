using AppIntegrador.Models;
using AppIntegrador.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class PermissionsController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        // GET: Permissions
        public ActionResult Index()
        {
            if (!PermissionManager.IsAuthorized(PermissionManager.Permission.EDITAR_USUARIOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            PermissionsViewHolder model = new PermissionsViewHolder();
            
            return View(model);
        }

        /* Se llama cuando se quieren guardar los cambios */
        [HttpPost]
        public ActionResult Index(PermissionsViewHolder model, bool updateView, string enfasis)
        {
            if (!PermissionManager.IsAuthorized(PermissionManager.Permission.EDITAR_USUARIOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }

            // Obtener nombre de Perfil y Énfasis
            var profileName = model.Perfiles.Where(p => p.Codigo.Equals(model.PerfilesSeleccionados[0])).ElementAt(0);
            // Debe cambiarse para utilizar el código, no el nombre
            var emphCode = model.EnfasisView.Where(e => e.CodCarrera.Equals(model.CarrerasSeleccionadas[0]) && e.Nombre.Equals(enfasis)).ElementAt(0);
            // Actualizar los checkboxes con la selección de énfasis.
            if (updateView)
            {
                ObjectParameter tienePerfil = new ObjectParameter("tienePerfil", typeof(bool));
                ObjectParameter tieneActivo = new ObjectParameter("tieneActivo", typeof(bool));
                // Para revisar si el usuario tiene todos esos perfiles
                int total = 0;
                int correct = 0;

                // Revisa los checks de las personas
                foreach (Persona persona in model.Personas)
                {
                    total = 0;
                    correct = 0;
                    for (int i = 0; i < model.PerfilesSeleccionados.Length; ++i)
                    {
                        ++total;
                        // Se asume una sola carrera y un solo énfasis
                        db.TienePerfilEnElEnfasis(persona.Correo, profileName.NombrePerfil, model.CarrerasSeleccionadas[0], emphCode.Codigo, tienePerfil);

                        if ((bool)tienePerfil.Value)
                        {
                            // Si tiene el perfil asignado, aumente contador
                            ++correct;
                        }
                    }

                    // Tiene al menos un perfil
                    if (correct > 0)
                    {
                        // Tiene todos los perfiles
                        if (total == correct)
                        {
                            persona.HasProfileInEmph = true;
                        }
                        // No tiene todos
                        /*else
                        {
                            // TO-DO: Cambiar con Javascript checkbox a "indeterminado"
                        }*/
                    }
                }

                // Revisa los checks de los permisos
                foreach (Permiso permiso in model.Permisos)
                {
                    total = 0;
                    correct = 0;
                    for (int i = 0; i < model.PerfilesSeleccionados.Length; ++i)
                    {
                        ++total;
                        // Se asume una sola carrera y un solo énfasis
                        db.TienePermisoActivoEnEnfasis(permiso.Id, profileName.NombrePerfil, model.CarrerasSeleccionadas[0], emphCode.Codigo, tieneActivo);

                        if ((bool)tieneActivo.Value)
                        {
                            // Si está activado en el perfil, aumente contador
                            ++correct;
                        }
                    }

                    // Activo en al menos un perfil
                    if (correct > 0)
                    {
                        // Activo en todos los perfiles
                        if (total == correct)
                        {
                            permiso.ActiveInProfileEmph = true;
                        }
                        // No tiene todos
                        /*else
                        {
                            // TO-DO: Cambiar con Javascript checkbox a "indeterminado"
                        }*/
                    }
                }
            }
            //TO-DO: Guardar aquí la selección de perfil, permisos, usuarios, carrera 
            //y énfasis en la base de datos.
            
            // Guardar cambios en la base de datos
            else
            {

            }
            return View(model);
        }

        [HttpGet]
        public JsonResult CargarEnfasisDeCarrera(string value)
        {
            List<string> carreras = new List<string>();
            using (var context = new DataIntegradorEntities())
            {
                var listaCarreras = from Carrera in db.EnfasisXCarrera(value)
                                    select Carrera;
                foreach (var codigoEnfasis in listaCarreras)
                    carreras.Add(codigoEnfasis + "," + (db.Enfasis.Find(value, codigoEnfasis.codEnfasis)).Nombre);
                carreras.Add("0" + "," + "Tronco común");
            }
            return Json(new { data = carreras }, JsonRequestBehavior.AllowGet);
        }

    }
    
}