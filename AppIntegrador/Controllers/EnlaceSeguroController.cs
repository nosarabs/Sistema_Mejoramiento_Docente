using AppIntegrador.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class EnlaceSeguroController : Controller
    {
        private DataIntegradorEntities db;
        
        // GET: EnlaceSeguro
        public EnlaceSeguroController()
        {
            db = new DataIntegradorEntities();
        }

        public EnlaceSeguroController(DataIntegradorEntities db)
        {
            this.db = db;
        }

        public ActionResult RedireccionSegura(string urlHash)
        {
            // Busca tupla en la base de datos
            EnlaceSeguro enlaceSeguro = db.EnlaceSeguro.SingleOrDefault(u => u.Hash == urlHash);
            if (enlaceSeguro != null)
            {
                // Podría mejorarse para asegurarse de que la hora esté sincronizada con la base de datos
                DateTime momentoActual = DateTime.Now;
                DateTime expira = (DateTime)enlaceSeguro.Expira;
                int fechaValida = DateTime.Compare(momentoActual, expira);
                // Enlace válido
                if (fechaValida < 0)
                {
                    // Revisar usuario válido
                    if (enlaceSeguro.UsuarioAsociado != null)
                    {
                        if (CurrentUser.getUsername() == enlaceSeguro.UsuarioAsociado)
                        {
                            return Redirect(enlaceSeguro.UrlReal);
                        }
                    }
                    else
                    {
                        // Enlace no relacionado a un solo usuario
                        return Redirect(enlaceSeguro.UrlReal);
                    }
                }
            }
            TempData["alertmessage"] = "Enlace no válido.";
            return RedirectToAction("Index", "Home");
        }

        public string ObtenerEnlaceSeguro(string urlReal, string usuario = null, DateTime? expira = null)
        {
            ObjectParameter resultadoHash = new ObjectParameter("resultadohash", typeof(string));
            ObjectParameter estado = new ObjectParameter("estado", typeof(string));
            db.AgregarEnlaceSeguro(usuario, urlReal, expira, resultadoHash, estado);
            string urlHash = (string) resultadoHash.Value;
            return "/EnlaceSeguro/RedireccionSegura?urlHash=" + urlHash.Split('/')[0];
        }
    }
}