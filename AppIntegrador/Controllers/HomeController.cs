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
using AppIntegrador.Utilities;

namespace AppIntegrador.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        /*5 minutes timeout when an user fails to login 3 times in a row.*/
        private const int LOGIN_TIMEOUT = 300000;

        /*Max number of failed login attempts before temporarily locking the account.*/
        private const int MAX_FAILED_ATTEMPTS = 3;

        public ActionResult Index()
        {
            //Si hay informacion de alerta en TempData entonces pasarla al viewbag
            if (TempData["alertmessage"] != null)
            {
                ViewBag.typeMessage = "alert";
                ViewBag.AlertMessage = TempData["alertmessage"].ToString();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");

            }
            List<string> perfiles = new List<string>();
            using (var context = new Entities())
            {
                var profileList = from Profile in db.PerfilesXUsuario("andres@mail.com")
                                  select Profile;
                foreach (var profileName in profileList)
                    perfiles.Add(profileName.NombrePefil);
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
            ViewBag.EnableBS4NoNavBar = true;
            //Verificamos si hay un mensaje de alerta de alguna de las operanciones realizadas, si lo hay lo desplegamos con javascript
            if (TempData["successMessage"] != null)
            {
                ViewBag.typeMessage = "success";
                ViewBag.NotifyMessage = TempData["successMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Usuario objUser)
        {

            ViewBag.EnableBS4NoNavBar = true;
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
                            ModelState.AddModelError("Password", "Usuario y/o contraseña incorrectos");
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
                ModelState.AddModelError("Password", "Usuario y/o contraseña incorrectos");
            }
            else 
            {
                /*If this user has already made failed login attempts, increment the counter.*/
                failedAttempts = (int)System.Web.HttpContext.Current.Application[objUser.Username] + 1;

                /*If the counter reached the max failed attempts count, lock the account, except for the admin.*/
                if (failedAttempts == MAX_FAILED_ATTEMPTS && objUser.Username != "admin@mail.com")
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
                    ModelState.AddModelError("Password", "Usuario y/o contraseña incorrectos");
                }

                /*Save the failed attempts count back to somewhere in the system.*/
                System.Web.HttpContext.Current.Application[objUser.Username] = failedAttempts;
            }
            return null;
        }

        private async Task<Usuario> DeactivateUserTemporarily(Usuario objUser) {

            /*To lock the user, first fetch it from the database.*/
            using (var context = new Entities())
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
            using (var context = new Entities())
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
            using (var context = new Entities())
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
            /*TAM 1.1.1: Modificado para que no guarde la sesión del usuario la siguiente vez que se ingrese al sistema.*/
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("Index");
        }

        /* User story TAM-1.5 */
        public ActionResult PasswordReset()
        {
            ViewBag.EnableBS4NoNavBar = true;
            return View();
        }

        public ActionResult SendPasswordRequest(string correo)
        {
            var user = db.Usuario.Where(a => a.Username == correo).FirstOrDefault();

            if (user != null)
            {
                /*Prevencion del bloqueo de Adminsitrador*/
                if (correo == "admin@mail.com")
                {
                    ViewBag.ErrorMessage = "Este correo solo es para fines de desarrollo, no puede recibir un correo de recuperacion";
                    ViewBag.EnableBS4NoNavBar = true;
                    return View("PasswordReset");
                }


                /* Only for demo purposes, will be changed for a password reset link */
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var newPassword = new string(Enumerable.Repeat(chars, 16)
                  .Select(s => s[random.Next(s.Length)]).ToArray());                

                db.ChangePassword(correo, newPassword);
                db.SaveChanges();

                EmailNotification notification = new EmailNotification();

                List<string> users = new List<string>();
                users.Add(correo);
                notification.SendNotification(users, "Recuperación de contraseña", "Su nueva contraseña es " + newPassword + " .", "Su nueva contraseña es " + newPassword + " .");

                TempData["successMessage"] = "Se ha enviado un correo con su nueva contraseña a la dirección indicada.";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Correo no encontrado";
            }
            ViewBag.EnableBS4NoNavBar = true;
            return View("PasswordReset");
        }
    }
}