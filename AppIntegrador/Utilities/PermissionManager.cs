using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace AppIntegrador.Utilities
{
    public class PermissionManager
    {
        private Entities db = new Entities();
        /*Permission enum that lists all the possible permissions to be used in the system.*/
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

        /* Permissions interface to be used on all the system wherever permission authorization is required.*/

        /// <summary>Este método revisa si el usuario tiene el permiso asignado en el énfasis de la carrera.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="majorCode">Código de la carrera.</param>
        /// <param name="emphasisCode">Código del énfasis de la carrera.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, string majorCode, string emphasisCode, int permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermiso(userMail, profileName, majorCode, emphasisCode, permissionId, resultado);
            /*For now, just an empty implementation.*/
            return (bool) resultado.Value;
        }

        /// <summary>Este método revisa si el usuario tiene el permiso asignado en la carrera.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="majorCode">Código de la carrera.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, string majorCode, int permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermisoSinEnfasis(userMail, profileName, majorCode, permissionId, resultado);
            /*For now, just an empty implementation.*/
            return (bool)resultado.Value;
        }

        /// <summary>Este método revisa si el usuario tiene el permiso asignado.</summary>
        /// <param name="userMail">Nombre del usuario.</param>
        /// <param name="profileName">Nombre del perfil con que está trabajando.</param>
        /// <param name="permissionId">Código identificador del permiso.</param>
        /// <returns>true si tiene el permiso, false en otro caso</returns>
        public bool IsAllowed(string userMail, string profileName, int permissionId)
        {
            ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
            db.TienePermisoSinEnfasisNiCarrera(userMail, profileName, permissionId, resultado);
            /*For now, just an empty implementation.*/
            return (bool)resultado.Value;
        }
    }

}