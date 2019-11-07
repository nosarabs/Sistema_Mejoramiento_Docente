using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Models
{
    /*Clase para almacenar los datos de un usuario loggeado en el sistema. Guarda el nombre de usuario (correo),
     el perfil, el código de carrera y el código de énfasis asignado al iniciar sesión basado en la configuración
     que le genere más valor. Puede editarse si después el usuario desea cambiar su configuración.*/

    public static class CurrentUser
    {
        private static string Username { get; set; }

        private static string Profile { get; set; }

        private static string MajorId { get; set; }

        private static string EmphasisId { get; set; }

        public static string getUsername()
        {
            updateCurrentUser();
            return Username;
        }

        public static string getUserProfile()
        {
            updateCurrentUser();
            return Profile;
        }

        public static string getUserMajorId()
        {
            updateCurrentUser();
            return MajorId;
        }

        public static string getUserEmphasisId()
        {
            updateCurrentUser();
            return EmphasisId;
        }

        public static void setUserProfile(string profile)
        {
            updateCurrentUser();
            Profile = profile;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(Username);
            user.Perfil = profile;
            db.SaveChanges();
        }

        public static void setUserMajor(string major)
        {
            updateCurrentUser();
            MajorId = major;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(Username);
            user.CodCarrera = major;
            db.SaveChanges();
        }

        public static void setUserEmphasis(string emphasis)
        {
            updateCurrentUser();
            EmphasisId = emphasis;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(Username);
            user.CodEnfasis = emphasis;
            db.SaveChanges();
        }

        //Método que guarda en la base de datos los datos del usuario loggeado. Busca primero si ya está en la tabla,
        //si ya está lo borra, y luego lo inserta de nuevo para habilitar su sesión.
        public static void setCurrentUser(string username, string profile, string majorId, string emphasisId)
        {
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual newUser = new UsuarioActual();
            newUser.CorreoUsuario = username;
            newUser.Perfil = profile;
            newUser.CodCarrera = majorId;
            newUser.CodEnfasis = emphasisId;

            if (db.UsuarioActual.Find(username) == null)
            {
                db.UsuarioActual.Add(newUser);
                db.SaveChanges();
            }
            else
            { 
                deleteCurrentUser(newUser.CorreoUsuario);
                db.UsuarioActual.Add(newUser);
                try
                {
                    db.SaveChanges();
                }//TO-DO: Por algún motivo genera excepciones aquí, arreglar esto.
                catch (Exception e) {
                    return;
                }
            }
        }

        public static void deleteCurrentUser(string username = null)
        {
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual newUser = new UsuarioActual();
            newUser.CorreoUsuario = username != null? username : Username;
            if (username == null)
            {
                newUser.Perfil = Profile;
                newUser.CodCarrera = MajorId;
                newUser.CodEnfasis = EmphasisId;
            }
            else {
                UsuarioActual otroUsuario = db.UsuarioActual.Find(username);
                if (otroUsuario != null)
                {
                    newUser = otroUsuario;
                }
            }
            if (!db.UsuarioActual.Local.Contains(newUser))
            {
                db.UsuarioActual.Attach(newUser);
            }
            db.UsuarioActual.Remove(newUser);
            db.SaveChanges();
        }

        //Método para actualizar desde base de datos los datos del usuario actual, en caso de que se borren
        //automáticamente. 
        private static void updateCurrentUser()
        {
            if (Username == null || System.Web.HttpContext.Current.User.Identity.Name != Username) {
                DataIntegradorEntities db = new DataIntegradorEntities();
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                UsuarioActual user = db.UsuarioActual.Find(name);
                if (user != null)
                {
                    Username = user.CorreoUsuario;
                    Profile = user.Perfil;
                    MajorId = user.CodCarrera;
                    EmphasisId = user.CodEnfasis;
                }
            }
        }
    }

}