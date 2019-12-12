﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Controllers.PlanesDeMejoraBI;
using AppIntegrador.Models;
using AppIntegrador.Utilities;

namespace AppIntegrador.Controllers
{
    public class AccionablesController : Controller
    {
        private DataIntegradorEntities db;
        private readonly IPerm permissionManager;
        private PlanesDeMejoraUtil util;

        public AccionablesController()
        {
            db = new DataIntegradorEntities();
            permissionManager = new PermissionManager();
            util = new PlanesDeMejoraUtil();
        }

        public AccionablesController(DataIntegradorEntities db)
        {
            this.db = db;
            permissionManager = new PermissionManager();
        }


        [HttpPost]
        public ActionResult AnadirAccionables(List<Accionable> accionables)
        {
            List<Accionable> misAccionables = accionables;
            List<String> FuncionariosNombreLista = new List<String>();
            ViewBag.FuncionariosLista = db.Funcionario.ToList();
            String name = "NombreCompleto";
            ObjectParameter name_op;
            foreach (var funcionario in ViewBag.FuncionariosLista)
            {
                name_op = new ObjectParameter(name, "");
                db.GetTeacherName(funcionario.Correo, name_op);
                FuncionariosNombreLista.Add(name_op.Value.ToString());
            }
            ViewBag.FuncionariosNombreLista = FuncionariosNombreLista;
            if (misAccionables == null)
            {
                misAccionables = new List<Accionable>();
            }
            return Json(new { error = true, message = util.getView(PartialView("_TablaAccionables", misAccionables), this.ControllerContext) });
        }

        //Requiere refactorización para eliminar el .Where de aquí
        public ActionResult TablaAccionables(int codPlan, string nombObj, string descripAcMej, bool edit = true)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.IdPlan = codPlan;
            ViewBag.nomObj = nombObj;
            ViewBag.descripAcMej = descripAcMej;

            IEnumerable<AppIntegrador.Models.Accionable> accionables = db.Accionable.Where(o => o.codPlan == codPlan && o.nombreObj == nombObj && o.descripAcMej == descripAcMej);
            if( edit == false)
            {
                return PartialView("_listarAccionables", accionables);
            }
            return PartialView("_Tabla", accionables);
        }

        [HttpPost]
        public ActionResult ObtenerFuncionarios(List<String> SeccionSeleccionado)
        {
            List<Funcionario> funcionarios = new List<Funcionario>();
            funcionarios = db.Funcionario.ToList();
            List<String> FuncionariosNombreLista = new List<String>();
            ViewBag.FuncionariosLista = db.Funcionario.ToList();
            String name = "NombreCompleto";
            ObjectParameter name_op;
            foreach (var funcionario in ViewBag.FuncionariosLista)
            {
                name_op = new ObjectParameter(name, "");
                db.GetTeacherName(funcionario.Correo, name_op);
                FuncionariosNombreLista.Add(name_op.Value.ToString());
            }
            ViewBag.FuncionariosNombreLista = FuncionariosNombreLista;
            string result = util.getView(PartialView("_AnadirResponsables", funcionarios), this.ControllerContext);
            return Json(new { error = true, message = result });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
