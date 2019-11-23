using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AppIntegrador.Controllers
{
    public class Validador
    {

        private bool validarCorreo(String correo)
        {
    
            return false;
        }

        private bool validarTamano(String hilera, int tamano)
        {
            if (hilera.Length <= tamano)
            {
                return true;
            }
            return false;
        }

        private bool validarNumero(string valor)
        {
            if(int.TryParse(valor, out int integer))
            {
                return true;
            }
            return false;
        }
        private bool validarEmail(string email)
        {
            // source: http://thedailywtf.com/Articles/Validating_Email_Addresses.aspx
            Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }

    }
}