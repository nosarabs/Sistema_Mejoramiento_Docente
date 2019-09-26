using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AppIntegrador.Models;
using System.Web;
using System.Web.Mvc;

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
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoginIntegrador"].ToString()))
                {
                    /*El sqlCommand recibe como primer parámetro el nombre del
                    procedimiento almacenado,
                    * de segundo parámetro recibe el sqlConnection
                    */
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.loginUsuario(@pLoginName, @pPassword)", con))
                    {
                        try
                        {
                            //Se preparan los parámetros que recibe el procedimiento almacenado
                            SqlParameter loginName = new SqlParameter("@pLoginName",
                            SqlDbType.VarChar);
                            loginName.Value = objUser.Username;
                            loginName.Direction = ParameterDirection.Input;
                            SqlParameter userPassword = new
                            SqlParameter("@pPassword", SqlDbType.VarChar);
                            userPassword.Value = objUser.Password;
                            userPassword.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(loginName);
                            cmd.Parameters.Add(userPassword);
                            /*Se abre la conexión*/
                            con.Open();
                            //Se ejecuta el procedimiento almacenado
                            bool success = (bool)cmd.ExecuteScalar();
                            if (success)
                            {
                                Session["Username"] = objUser.Username.ToString();
                                return RedirectToAction("UserDashboard");
                            }
                            else
                            {
                                ModelState.AddModelError("Password", "Contraseña o nombre de usuario incorrecto");
                                return View(objUser);
                            }
                        }
                        catch (SqlException ex)
                        {
                            return View(objUser);
                        }

                    }
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