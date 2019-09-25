using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace AppIntegrador.Controllers
{
    public class UsuarioLoginController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario objUser)
        {
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
                                View(objUser);
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
