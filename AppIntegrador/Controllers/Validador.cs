/*using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AppIntegrador.Controllers
{
    public class Validador
    {

        public bool ValidaCSVRow(ArchivoCSV fila, int tipo)
        {
            switch (tipo)
            {
                case 1: //Unidad Academica
                    break;
                case 2: //Carrera
                    break;
                case 3://Enfasis
                    break;
                case 4://Curso
                    break;
                case 5: //Grupo
                    break;
                case 6: //Profesor
                    break;
                case 7: //Estudiante
                    break;
                case 8: //Imparten
                    break;
                case 9: //Matriculados
                    break;
            }
            return true;
        }

        private bool ValidarUnidad (String codigoUnidad, string Nombre)
        {
            bool valido=false;
            if(ValidarTamanoText(codigoUnidad, 10) && ValidarTamanoText(codigoUnidad, 50)){ //Si son textos validos
                valido = true;
            }
            return valido;
        }

        private bool ValidarTamanoText(String hilera, int tamano)
        {
            if (hilera.Length <= tamano && !String.IsNullOrEmpty(hilera))
            {
                return true;
            }
            return false;
        }

        private bool ValidarNumero(string valor)
        {
            if(int.TryParse(valor, out int integer) && !String.IsNullOrEmpty(valor))
            {
                return true;
            }
            return false;
        }
        private bool ValidarEmail(string email)
        {
            // source: http://thedailywtf.com/Articles/Validating_Email_Addresses.aspx
            Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            if( rx.IsMatch(email) && !String.IsNullOrEmpty(email))
            {
                return true;
            }
            return false;
        }

    }
}*/