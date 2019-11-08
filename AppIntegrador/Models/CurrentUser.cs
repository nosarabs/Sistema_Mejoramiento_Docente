﻿using System;
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
        public static string getUsername()
        {
            updateCurrentUser();
            return (string) HttpContext.Current.Session["Username"];
        }

        public static string getUserProfile()
        {
            updateCurrentUser();
            return (string) HttpContext.Current.Session["Profile"];
        }

        public static string getUserMajorId()
        {
            updateCurrentUser();
            return (string) HttpContext.Current.Session["MajorId"];
        }

        public static string getUserEmphasisId()
        {
            updateCurrentUser();
            return (string) HttpContext.Current.Session["EmphasisId"];
        }

        public static void setUserProfile(string profile)
        {
            updateCurrentUser();
            HttpContext.Current.Session["Profile"] = profile;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(getUsername());
            user.Perfil = profile;
            db.SaveChanges();
        }

        public static void setUserMajor(string major)
        {
            updateCurrentUser();
            HttpContext.Current.Session["MajorId"] = major;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(getUsername());
            user.CodCarrera = major;
            db.SaveChanges();
        }

        public static void setUserEmphasis(string emphasis)
        {
            updateCurrentUser();
            HttpContext.Current.Session["EmphasisId"] = emphasis;
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual user = new UsuarioActual();
            user = db.UsuarioActual.Find(getUsername());
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
                    Console.WriteLine(e.Message);
                }
            }
            HttpContext.Current.Session["Username"] = username;
            HttpContext.Current.Session["Profile"] = profile;
            HttpContext.Current.Session["MajorId"] = majorId;
            HttpContext.Current.Session["EmphasisId"] = emphasisId;
        }

        public static void deleteCurrentUser(string username)
        {
            DataIntegradorEntities db = new DataIntegradorEntities();
            UsuarioActual newUser = new UsuarioActual();
            newUser.CorreoUsuario = (username != null? username : (string)HttpContext.Current.Session["Username"]);
            if (username == null)
            {
                Console.WriteLine("Error al borrar el usuario.");
                return;
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
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Método para actualizar desde base de datos los datos del usuario actual, en caso de que se borren
        //automáticamente. 
        private static void updateCurrentUser()
        {
            if ((string)HttpContext.Current.Session["Username"] == null || System.Web.HttpContext.Current.User.Identity.Name != (string)HttpContext.Current.Session["Username"]) {
                DataIntegradorEntities db = new DataIntegradorEntities();
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                UsuarioActual user = db.UsuarioActual.Find(name);
                if (user != null)
                {
                    HttpContext.Current.Session["Username"] = user.CorreoUsuario;
                    HttpContext.Current.Session["Profile"] = user.Perfil;
                    HttpContext.Current.Session["MajorId"] = user.CodCarrera;
                    HttpContext.Current.Session["EmphasisId"] = user.CodEnfasis;
                }
            }
        }
    }

}