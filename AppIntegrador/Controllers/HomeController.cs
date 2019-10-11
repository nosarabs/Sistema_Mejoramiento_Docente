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
using System.Threading.Tasks;

namespace AppIntegrador.Controllers
{
    public class HomeController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        /*5 minutes timeout when an user fails to login 3 times in a row.*/
        private const int LOGIN_TIMEOUT = 300000;

        private const int MAX_FAILED_ATTEMPTS = 3;

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
        public async Task<ActionResult> Login(Usuario objUser)
        {
            /*User story TAM-1.3 Brute-force attack prevention.*/
            int failedAttempts = 0;
            if (System.Web.HttpContext.Current.Application[objUser.Username] != null)
            {
                failedAttempts = (int)System.Web.HttpContext.Current.Application[objUser.Username];
                if (failedAttempts == MAX_FAILED_ATTEMPTS)
                {
                    System.Web.HttpContext.Current.Application.Remove(objUser.Username);
                    await Task.Delay(LOGIN_TIMEOUT).ConfigureAwait(false);
                    return RedirectToAction("Login");
                }
            }
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
                            if (System.Web.HttpContext.Current.Application[objUser.Username] == null)
                            {
                                System.Web.HttpContext.Current.Application[objUser.Username] = 1;
                                ModelState.AddModelError("Password", "Contraseña incorrecta");
                            }
                            else /*User story TAM-1.3 Brute-force attack prevention.*/
                            {
                                failedAttempts = (int)System.Web.HttpContext.Current.Application[objUser.Username] + 1;

                                if (failedAttempts == MAX_FAILED_ATTEMPTS)
                                {
                                    ModelState.AddModelError("Password", "¡Ha excedido el límite de intentos fallidos!\nDebe esperar" +
                                        " 5 minutos antes de intentar de nuevo.");         
                                }
                                else
                                {          
                                    ModelState.AddModelError("Password", "Contraseña incorrecta");
                                }

                                System.Web.HttpContext.Current.Application[objUser.Username] = failedAttempts;
                            }
                            
                        }
                        /*End of user story.*/

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