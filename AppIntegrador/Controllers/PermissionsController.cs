using AppIntegrador.Models;
using AppIntegrador.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

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

        public ActionResult ConfigIndex()
        {
            return View("SeleccionarPerfil");
        }

        /* Se llama cuando se selecciona un énfasis en la página, para cargar los checkboxes según la configuración seleccionada.*/
        [HttpPost]
        public JsonResult CargarCheckboxes(string[] profileCodes, string[] profileNames, string majorCode, string emphCode)
        {
            if ((profileCodes == null) || (profileNames == null) || (majorCode == null) || (emphCode == null)) {
                TempData["alertmessage"] = "Algo salió mal. Intente de nuevo.";
                return Json(new { persons = "", permissions = "" });
            }
            PermissionsViewHolder model = new PermissionsViewHolder();
            // Obtener nombre de Perfil y Énfasis
            var profileName = profileNames[0];
            // Actualizar los checkboxes con la selección de énfasis.

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
                for (int i = 0; i < profileCodes.Length; ++i)
                {
                    ++total;
                    // Se asume una sola carrera y un solo énfasis
                    db.TienePerfilEnElEnfasis(persona.Correo, profileName, majorCode, emphCode, tienePerfil);

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
                for (int i = 0; i < profileCodes.Length; ++i)
                {
                    ++total;
                    // Se asume una sola carrera y un solo énfasis
                    db.TienePermisoActivoEnEnfasis(permiso.Id, profileName, majorCode, emphCode, tieneActivo);

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
            return Json( new { persons = PermissionManagerViewBuilder.ListPersonProfiles(model.Personas), permissions = PermissionManagerViewBuilder.ListProfilePermissions(model.Permisos) });
        }

        [HttpGet]
        public JsonResult CargarEnfasisDeCarrera(string value)
        {
            List<string> enfasis = new List<string>();
            using (var context = new DataIntegradorEntities())
            {
                var listaEnfasis = from Carrera in db.EnfasisXCarrera(value)
                                    select Carrera;
                foreach (var codigoEnfasis in listaEnfasis)
                {
                    string nombreEnfasis = db.Enfasis.Find(value, codigoEnfasis.codEnfasis).Nombre;
                    enfasis.Add(codigoEnfasis.codEnfasis + "," + nombreEnfasis);
                }
                enfasis.Add("0" + "," + "Tronco común");
            }
            return Json(new { data = enfasis }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CargarPerfil()
        {
            string username = CurrentUser.Username;
            List<string> perfiles = new List<string>();
            using (var context = new DataIntegradorEntities())
            {
                var listaPerfiles = from Perfil in db.PerfilesXUsuario(username)
                                    select Perfil;
                foreach (var nombrePerfil in listaPerfiles)
                    perfiles.Add(nombrePerfil.NombrePefil);
            }
            return Json(new { data = perfiles }, JsonRequestBehavior.AllowGet);
        }


    }
    
}