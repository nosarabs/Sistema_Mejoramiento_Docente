﻿using AppIntegrador.Models;
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

        [AllowAnonymous]
        public ActionResult RedireccionSeguraAnonymous(string urlHash)
        {
            return Redirigir(urlHash, true);
        }

        public ActionResult RedireccionSegura(string urlHash)
        {
            return Redirigir(urlHash);
        }

        [AllowAnonymous]
        private ActionResult Redirigir(string urlHash, bool anonimo = false)
        {
            // Busca tupla en la base de datos
            EnlaceSeguro enlaceSeguro = db.EnlaceSeguro.SingleOrDefault(u => u.Hash == urlHash);
            if (enlaceSeguro != null)
            {
                // Podría mejorarse para asegurarse de que la hora esté sincronizada con la base de datos
                DateTime momentoActual = DateTime.Now;
                DateTime expira = (DateTime)enlaceSeguro.Expira;
                int usos = enlaceSeguro.Usos;
                int fechaValida = DateTime.Compare(momentoActual, expira);
                // Enlace válido
                if (fechaValida < 0)
                {
                    // no se utiliza "> 0" ya que se desea que -1 haga que el enlace no tenga limite de uso 
                    if (usos != 0) {
                        // Revisar usuario válido
                        if (!anonimo && enlaceSeguro.UsuarioAsociado != null)
                        {
                            if (CurrentUser.getUsername() == enlaceSeguro.UsuarioAsociado)
                            {
                                //se envia a borrar el enlace, esto decrementa su valor de "usos" disponibles.
                                db.EnlaceSeguro.Remove(enlaceSeguro);
                                return Redirect(enlaceSeguro.UrlReal);
                            }
                        }
                        // Enlace no relacionado a un solo usuario o anónimos permitidos
                        else
                        {
                            //se envia a borrar el enlace, esto decrementa su valor de "usos" disponibles.
                            db.EnlaceSeguro.Remove(enlaceSeguro);
                            return Redirect(enlaceSeguro.UrlReal);
                        }
                    }
                }   
            }
            TempData["alertmessage"] = "Enlace no válido.";
            return RedirectToAction("Index", "Home");
        }

        public string ObtenerEnlaceSeguro(string urlReal, string usuario = null, DateTime? expira = null, int usos = 0)
        {
            return GenerarEnlaceSeguro(urlReal, usuario, expira, usos);
        }

        public string ObtenerEnlaceSeguroAnonimo(string urlReal, DateTime? expira = null, int usos = 0)
        {
            return GenerarEnlaceSeguro(urlReal, null, expira, usos, true);
        }

        private string GenerarEnlaceSeguro(string urlReal, string usuario = null, DateTime? expira = null, int usos = 0, bool anonimo = false)
        {
            // Almacenar los datos necesitados
            ObjectParameter resultadoHash = new ObjectParameter("resultadohash", typeof(string));
            ObjectParameter estado = new ObjectParameter("estado", typeof(string));
            db.AgregarEnlaceSeguro(usuario, urlReal, expira, usos, resultadoHash, estado);

            // Obtener los datos que debe recibir quien solicitó el enlace
            string urlHash = (string) resultadoHash.Value;
            var request = System.Web.HttpContext.Current.Request;
            string domain = request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Authority;

            if (anonimo)
            {
                return domain + "/EnlaceSeguro/RedireccionSeguraAnonymous?urlHash=" + urlHash;
            }
            else
            {
                return domain + "/EnlaceSeguro/RedireccionSegura?urlHash=" + urlHash;
            }
        }
    }
}