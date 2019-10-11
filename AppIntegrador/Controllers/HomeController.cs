using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppIntegrador.Models;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Objects;

namespace AppIntegrador.Controllers
{
    public class HomeController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.HTMLCheck = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario objUser)
        {
            ViewBag.HTMLCheck = true;
            if (ModelState.IsValid)
            {
                /* Historia: Página de login -> Refactor del código para utilizar el modelo en el llamado al procedimiento almacenado */
                try
                {
                    ObjectParameter loginResult = new ObjectParameter("result", typeof(Int32));

                    // Se ejecuta el procedimiento almacenado
                    db.LoginUsuario(objUser.Username.ToString(), objUser.Password.ToString(), loginResult);
                    int result = (int)loginResult.Value;

                    // Credenciales correctos
                    if (result == 0)
                    {
                        Session["Username"] = objUser.Username.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // No se encontró el usuario
                        if (result == 1)
                        {
                            ModelState.AddModelError("Username", "Nombre de usuario incorrecto");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Contraseña incorrecta");
                        }

                        return View(objUser);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }

            }
            return View(objUser);
        }

        public ActionResult UserDashboard()
        {
            ViewBag.HTMLCheck = true;
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}