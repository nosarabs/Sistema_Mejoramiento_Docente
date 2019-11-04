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
            return View(new PermissionsViewHolder());
        }

        /* Se llama cuando se quieren guardar los cambios */
        [HttpPost]
        public ActionResult Index(PermissionsViewHolder Data, bool updateView)
        {
            if (!PermissionManager.IsAuthorized(PermissionManager.Permission.EDITAR_USUARIOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            // Actualizar los checkboxes con la selección de énfasis.
            if (updateView)
            {
                ObjectParameter tienePerfil = new ObjectParameter("tienePerfil", typeof(bool));
                ObjectParameter tieneActivo = new ObjectParameter("tieneActivo", typeof(bool));
                // Para revisar si el usuario tiene todos esos perfiles
                int total = 0;
                int correct = 0;

                // Revisa los checks de las personas
                foreach (Persona persona in Data.Personas)
                {
                    total = 0;
                    correct = 0;
                    for (int i = 0; i < Data.PerfilesSeleccionados.Length; ++i)
                    {
                        ++total;
                        // Se asume una sola carrera y un solo énfasis
                        db.TienePerfilEnElEnfasis(persona.Correo, Data.PerfilesSeleccionados[i], Data.CarrerasSeleccionadas[0], Data.EnfasisSeleccionados[0], tienePerfil);

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
                foreach (Permiso permiso in Data.Permisos)
                {
                    total = 0;
                    correct = 0;
                    for (int i = 0; i < Data.PerfilesSeleccionados.Length; ++i)
                    {
                        ++total;
                        // Se asume una sola carrera y un solo énfasis
                        db.TienePermisoActivoEnEnfasis(permiso.Id, Data.PerfilesSeleccionados[i], Data.CarrerasSeleccionadas[0], Data.EnfasisSeleccionados[0], tieneActivo);

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
            return View(Data);
        }

        /* Se llama cuando se han elegido los perfiles y énfasis */
        private void UpdateCheckboxes(PermissionsViewHolder Data)
        {
            if (Data == null)
            {
                return;
            }
            ObjectParameter result = new ObjectParameter("result", typeof(bool));
            // Para revisar si el usuario tiene todos esos perfiles
            int total = 0;
            int correct = 0;
            
            // Revisa los checks de las personas
            foreach(Persona persona in Data.Personas)
            {
                total = 0;
                correct = 0;
                for (int i = 0; i < Data.PerfilesSeleccionados.Length; ++i)
                {
                    ++total;
                    // Se asume una sola carrera y un solo énfasis
                    db.TienePerfilEnElEnfasis(persona.Correo, Data.PerfilesSeleccionados[i], Data.CarrerasSeleccionadas[0], Data.EnfasisSeleccionados[0], result);

                    if ((bool)result.Value)
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
            foreach (Permiso permiso in Data.Permisos)
            {
                total = 0;
                correct = 0;
                for (int i = 0; i < Data.PerfilesSeleccionados.Length; ++i)
                {
                    ++total;
                    // Se asume una sola carrera y un solo énfasis
                    db.TienePermisoActivoEnEnfasis(permiso.Id, Data.PerfilesSeleccionados[i], Data.CarrerasSeleccionadas[0], Data.EnfasisSeleccionados[0], result);

                    if ((bool)result.Value)
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

            // TO-DO: Cargar la página con los cambios
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