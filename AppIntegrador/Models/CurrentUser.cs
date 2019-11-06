using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    /*Clase para almacenar los datos de un usuario loggeado en el sistema. Guarda el nombre de usuario (correo),
     el perfil, el código de carrera y el código de énfasis asignado al iniciar sesión basado en la configuración
     que le genere más valor. Puede editarse si después el usuario desea cambiar su configuración.*/
    public static class CurrentUser
    {
        public static string Username { get; set; }

        public static string Profile { get; set; }

        public static string MajorId { get; set; }

        public static string EmphasisId { get; set; }

        public static string getUsername() {return Username; }

    }

}