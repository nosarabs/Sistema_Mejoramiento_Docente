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

        /*Max number of failed login attempts before temporarily locking the account.*/
        private const int MAX_FAILED_ATTEMPTS = 3;

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
                /*When loggin in, first checks whether the user's account is locked or deactivated.*/
                if (IsUserLocked(objUser)) 
                {
                    ModelState.AddModelError("Password", "Este usuario está bloqueado temporalmente.\nIntente de nuevo más tarde o contacte al adminstrador del sitio.");
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
                        /*If user's account exists in the database but the password is wrong.*/
                        else
                        {
                            _ = WrongPassword(objUser);
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

        /*User story TAM-1.3 Brute-force attack prevention.*/
        private async Task<ActionResult> WrongPassword(Usuario objUser) {
            int failedAttempts = 0;
            
            /*If it's this user first failed login attempt, store it somewhere in the system to keep watching 
             this user's activity.*/
            if (System.Web.HttpContext.Current.Application[objUser.Username] == null)
            {
                /*Sets the count of failed login attempts to 1.*/
                System.Web.HttpContext.Current.Application[objUser.Username] = 1;
                ModelState.AddModelError("Password", "Contraseña incorrecta");
            }
            else 
            {
                /*If this user has already made failed login attempts, increment the counter.*/
                failedAttempts = (int)System.Web.HttpContext.Current.Application[objUser.Username] + 1;

                /*If the counter reached the max failed attempts count, lock the account, except for the admin.*/
                if (failedAttempts == MAX_FAILED_ATTEMPTS && objUser.Username != "admin")
                {
                    ModelState.AddModelError("Password", "¡Ha excedido el límite de intentos fallidos!\nDebe esperar" +
                        " 5 minutos antes de intentar de nuevo.");

                    /*Removes the failed attempts count from the system for this user.*/
                    System.Web.HttpContext.Current.Application.Remove(objUser.Username);

                    /*Deactivates the user temporarily, asynchronously so that the system doesn't stall.*/
                    await DeactivateUserTemporarily(objUser).ConfigureAwait(false);

                    return View(objUser);
                }
                else
                {
                    ModelState.AddModelError("Password", "Contraseña incorrecta");
                }

                /*Save the failed attempts count back to somewhere in the system.*/
                System.Web.HttpContext.Current.Application[objUser.Username] = failedAttempts;
            }
            return null;
        }

        private async Task<Usuario> DeactivateUserTemporarily(Usuario objUser) {

            /*To lock the user, first fetch it from the database.*/
            using (var context = new DataIntegradorEntities())
            {
                var user = db.Usuario.SingleOrDefault(u => u.Username == objUser.Username);
                if (user != null)
                {
                    /*Sets the active/inactive bit to 0.*/
                    user.Activo = false;
                    db.SaveChanges();
                }
            }

            /*Waits for the blocking time specified in the constant.*/
            await Task.Delay(LOGIN_TIMEOUT).ConfigureAwait(false);

            /*Then reactivates the user.*/
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

        /*Function to tell whether a given user account is locked or not.*/
        private static bool IsUserLocked(Usuario objUser) {
            bool locked = false;
            using (var context = new DataIntegradorEntities())
            {
                var query = context.Usuario
                    .Where(u => u.Username == objUser.Username)
                    .FirstOrDefault<Usuario>();
                /*Just return the user account's active/inactive bit.*/
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