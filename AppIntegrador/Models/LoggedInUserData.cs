using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    /*Clase para almacenar los datos de un usuario loggeado en el sistema. Guarda el nombre de usuario (correo),
     el perfil, el código de carrera y el código de énfasis asignado al iniciar sesión basado en la configuración
     que le genere más valor. Puede editarse si después el usuario desea cambiar su configuración.*/
    public class LoggedInUserData
    {
        public string Username { get; set; }

        public string Profile { get; set; }

        public string MajorId { get; set; }

        public string EmphasisId { get; set; }

        public LoggedInUserData(string username, string profile, string majorId, string empahsisId)
        {
            this.Username = username;
            this.Profile = profile;
            this.MajorId = majorId;
            this.EmphasisId = empahsisId;
        }
    }
}