using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AppIntegrador.Controllers
{
    public class Validador
    {

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

        public bool checkForSQLInjection(string userInput) // Basado en el codigo encontrado en http://aspdotnetmyblog.blogspot.com/2013/10/how-to-check-string-against-sql.html
        {
            bool isSQLInjection = false;
            string[] sqlCheckList = { //palabras reservadas SQL injection
                "--",
                ";--",
                ";",
                "/*",
                "*/",
                "@@",
                "@",
                "char",
                "nchar",
                "varchar",
                "nvarchar",
                "alter",
                "begin",
                "cast",
                "create",
                "cursor",
                "declare",
                "delete",
                "drop",
                "end",
                "exec",
                "execute",
                "fetch",
                "insert",
                "kill",
                "select",
                "sys",
                "sysobjects",
                "syscolumns",
                "table",
                "update"
                };
            string CheckString = userInput.Replace("'", "''");
            for (int i = 0; i <= sqlCheckList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlCheckList[i],
                    StringComparison.OrdinalIgnoreCase) >= 0))
                { isSQLInjection = true; }
            }
            return isSQLInjection;
        }
    }

}
