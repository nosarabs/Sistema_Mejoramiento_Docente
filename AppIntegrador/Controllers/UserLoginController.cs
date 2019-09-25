using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador
{
    public class UserLoginController : Controller
    {

        private DataIntegradorEntities db = new DataIntegradorEntities();

        public ActionResult UserLoginView()
        {
            ViewBag.HTMLCheck = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult UserLoginView(UserProfile objUser)
        {
            ViewBag.HTMLCheck = true;
            if (ModelState.IsValid)
            {
                using (DataIntegradorEntities db = new DataIntegradorEntities())
                {
                    var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            ViewBag.HTMLCheck = true;
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("UserLoginView");
            }
        }
    }
}