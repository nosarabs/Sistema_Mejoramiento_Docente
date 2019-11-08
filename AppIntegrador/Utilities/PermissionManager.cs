using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace AppIntegrador.Utilities
{
    /*Interfaz de permisos para ser utilizada por las demás funcionalidades del sistema cuando se requiera
     saber si un usuario con un perfil en una carrera y un énfasis seleccionado tiene un determinado permiso.*/
    public class PermissionManager
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        /*Listado constantes de permisos.*/
        public enum Permission : int {
            VER_USUARIOS = 101,
            CREAR_USUARIOS,
            VER_DETALLES_USUARIOS,
            EDITAR_USUARIOS,
            BORRAR_USUARIOS,

            CREAR_FORMULARIO = 201,
            VER_FORMULARIO,
            CREAR_SECCION,
            VER_SECCION,
            CREAR_PREGUNTA,
            VER_PREGUNTA,

            VER_PLANES_MEJORA = 301,
            CREAR_PLANES_MEJORA,
            EDITAR_PLANES_MEJORA,
            BORRAR_PLANES_MEJORA,
            CREAR_OBJETIVOS,
            EDITAR_OBJETIVOS,
            BORRAR_OBJETIVOS,
            CREAR_ACCIONES_MEJORA,
            EDITAR_ACCIONES_MEJORA,
            BORRAR_ACCIONES_MEJORA,

            VER_RESPUESTAS_FORMULARIO = 401

        };

        /// <summary>Este método revisa si el usuario tiene el permiso asignado en el énfasis de la carrera.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="majorCode">Código de la carrera.</param>
        /// <param name="emphasisCode">Código del énfasis de la carrera.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, string majorCode, string emphasisCode, PermissionManager.Permission permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermiso(userMail, profileName, majorCode, emphasisCode, (int) permissionId, resultado);
            return (bool) resultado.Value;
        }

        /// <summary>Este método revisa si el usuario tiene el permiso asignado en el énfasis de la carrera.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="majorCode">Código de la carrera.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, string majorCode, PermissionManager.Permission permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermisoSinEnfasis(userMail, profileName, majorCode, (int) permissionId, resultado);
            return (bool)resultado.Value;
        }

        /// <summary>Este método revisa si el usuario tiene el permiso asignado en el énfasis de la carrera.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, PermissionManager.Permission permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermisoSinEnfasisNiCarrera(userMail, profileName, (int) permissionId, resultado);
            return (bool)resultado.Value;
        }

        public bool IsUserAuthorized(PermissionManager.Permission permissionId)
        {
            /*Both major and emphasis are null*/
            if(CurrentUser.getUserEmphasisId() == null && CurrentUser.getUserMajorId() == null)
                return IsAllowed(
                CurrentUser.getUsername(),
                CurrentUser.getUserProfile(),
                permissionId
            );
            /*Only emphasis is null*/
            else if (CurrentUser.getUserEmphasisId() == null)
                return IsAllowed(
                CurrentUser.getUsername(),
                CurrentUser.getUserProfile(),
                CurrentUser.getUserMajorId(),
                permissionId
            );
            /*All parameters supplied*/
            else
            return this.IsAllowed(
                CurrentUser.getUsername(),
                CurrentUser.getUserProfile(),
                CurrentUser.getUserMajorId(),
                CurrentUser.getUserEmphasisId(),
                permissionId
            );
        }

        public static bool IsAuthorized(Permission permission)
        {
            return new PermissionManager().IsUserAuthorized(permission);
        }
    }

}