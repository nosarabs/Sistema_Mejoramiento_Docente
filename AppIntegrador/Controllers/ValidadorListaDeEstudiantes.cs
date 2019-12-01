﻿using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Controllers
{
    public class ValidadorListaDeEstudiantes : Validador
    {

        public bool Validar(ListaEstudiante lista)
        {
            System.Diagnostics.Debug.WriteLine("validor");

            //Correo Persona
            if (!ValidarEmail(lista.CorreoPersona) || checkForSQLInjection(lista.CorreoPersona))
            {

                return false; //email invalidao
            }
            //Correo Estudiante
            if (!ValidarEmail(lista.CorreoEstudiante) || checkForSQLInjection(lista.CorreoEstudiante))
            {
                System.Diagnostics.Debug.WriteLine("emailP");
                return false; //email invalidao
            }
            //Correo estudiante empadronado
            if (!ValidarEmail(lista.CorreoEstudianteEmpadronado) || checkForSQLInjection(lista.CorreoEstudianteEmpadronado))
            {
                System.Diagnostics.Debug.WriteLine("emailP");
                return false; //email invalidao
            }
            //Id
            if (!ValidarTamanoText(lista.IdPersona, 30) || checkForSQLInjection(lista.IdPersona))
            {
                System.Diagnostics.Debug.WriteLine("id");
                return false;
            }
            //Tipo Id
            if (!ValidarTamanoText(lista.TipoIdPersona, 30) || checkForSQLInjection(lista.TipoIdPersona))
            {
                System.Diagnostics.Debug.WriteLine("tipo");

                return false;
            }
            //Nombre
            if (!ValidarTamanoText(lista.NombrePersona, 15) || checkForSQLInjection(lista.NombrePersona))
            {
                System.Diagnostics.Debug.WriteLine("nombre");

                return false;
            }
            //Apellido
            if (!ValidarTamanoText(lista.ApellidoPersona, 15) || checkForSQLInjection(lista.ApellidoPersona))
            {
                System.Diagnostics.Debug.WriteLine("apellido");

                return false;
            }
            //borrado
            if (checkForSQLInjection(lista.Borrado) || ValidaBool(lista.Borrado))
            {
                System.Diagnostics.Debug.WriteLine("borrado");

                return false;
            }
            //Codigo Enfasis
            if (!ValidarTamanoText(lista.CodigoEnfasisEmpadronado, 15) || checkForSQLInjection(lista.CodigoEnfasisEmpadronado))
            {
                System.Diagnostics.Debug.WriteLine("apellido");

                return false;
            }
            //Codigo Carrera
            if (!ValidarTamanoText(lista.CodigoCarreraEmpadronado, 15) || checkForSQLInjection(lista.CodigoCarreraEmpadronado))
            {
                System.Diagnostics.Debug.WriteLine("apellido");

                return false;
            }
            return true;
        }

    }
}