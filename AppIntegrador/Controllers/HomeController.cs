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
using System.Web.Security;
using System.Threading.Tasks;

namespace AppIntegrador.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        /*5 minutes timeout when an user fails to login 3 times in a row.*/
        private const int LOGIN_TIMEOUT = 300000;

        private const int MAX_FAILED_ATTEMPTS = 3;

        /*User story TAM-1.6.1 is implemented in each function: returns the requested view if the user is logged in,
         redirects to login page otherwise.*/
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult About()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else 
            {
                ViewBag.HTMLCheck = true;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Usuario objUser)
        {

            ViewBag.HTMLCheck = true;
            if (ModelState.IsValid)
            {
                if (IsUserLocked(objUser)) 
                {
                    ModelState.AddModelError("Password", "Este usuario está bloqueado.\nDebe esperar 5 minutos antes de intentar de nuevo.");
                    return View(objUser);
                }
                /* Historia: Página de login -> Refactor del código para utilizar el modelo en el llamado al procedimiento almacenado */
                try
                {
                    ObjectParameter loginResult = new ObjectParameter("result", typeof(Int32));

                    // Se ejecuta el procedimiento almacenado
                    db.LoginUsuario(objUser.Username, objUser.Password, loginResult);
                    int result = (int)loginResult.Value;

                    // Credenciales correctos
                    if (result == 0)
                    {
                        FormsAuthentication.SetAuthCookie(objUser.Username, false);
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
                            _ = WrongPassword(objUser);
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
        private async Task<ActionResult> WrongPassword(Usuario objUser) {
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
                    await DeactivateUserTemporarily(objUser).ConfigureAwait(false);
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

        private async Task<Usuario> DeactivateUserTemporarily(Usuario objUser) {
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

        private static bool IsUserLocked(Usuario objUser) {
            bool locked = false;
            using (var context = new DataIntegradorEntities())
            {
                var query = context.Usuario
                    .Where(u => u.Username == objUser.Username)
                    .FirstOrDefault<Usuario>();
                if(query != null)
                    locked = !query.Activo;
            }
            return locked;
        }
        /*End of user story.*/

        [Authorize]
        public ActionResult Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}