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

            ViewBag.HTMLCheck = true;
            if (ModelState.IsValid)
            {
                if (isUserLocked(objUser)) 
                {
                    ModelState.AddModelError("Password", "Este usuario está bloqueado.\nDebe esperar 5 minutos antes de intentar de nuevo.");
                    return View(objUser);
                }
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
                            wrongPassword(objUser);
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

        /*User story TAM-1.3 Brute-force attack prevention.*/
        private async Task<ActionResult> wrongPassword(Usuario objUser) {
            int failedAttempts = 0;
            
            if (System.Web.HttpContext.Current.Application[objUser.Username] == null)
            {
                System.Web.HttpContext.Current.Application[objUser.Username] = 1;
                ModelState.AddModelError("Password", "Contraseña incorrecta");
            }
            else 
            {
                failedAttempts = (int)System.Web.HttpContext.Current.Application[objUser.Username] + 1;

                if (failedAttempts == MAX_FAILED_ATTEMPTS && objUser.Username != "admin")
                {
                    ModelState.AddModelError("Password", "¡Ha excedido el límite de intentos fallidos!\nDebe esperar" +
                        " 5 minutos antes de intentar de nuevo.");
                    System.Web.HttpContext.Current.Application.Remove(objUser.Username);
                    await deactivateUserTemporarily(objUser).ConfigureAwait(false);
                    return View(objUser);
                }
                else
                {
                    ModelState.AddModelError("Password", "Contraseña incorrecta");
                }

                System.Web.HttpContext.Current.Application[objUser.Username] = failedAttempts;
            }
            return null;
        }

        public async Task<Usuario> deactivateUserTemporarily(Usuario objUser) {
            using (var context = new DataIntegradorEntities())
            {
                var user = db.Usuario.SingleOrDefault(u => u.Username == objUser.Username);
                if (user != null)
                {
                    user.Activo = false;
                    db.SaveChanges();
                }
            }
            await Task.Delay(LOGIN_TIMEOUT).ConfigureAwait(false);
            using (var context = new DataIntegradorEntities())
            {
                var user = db.Usuario.SingleOrDefault(u => u.Username == objUser.Username);
                if (user != null)
                {
                    user.Activo = true;
                    db.SaveChanges();
                }
            }
            return null;
        }

        private bool isUserLocked(Usuario objUser) {
            bool locked = false;
            using (var context = new DataIntegradorEntities())
            {
                var query = context.Usuario
                    .Where(u => u.Username == objUser.Username)
                    .FirstOrDefault<Usuario>();
                locked = !query.Activo;
            }
            return locked;
        }
        /*End of user story.*/

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