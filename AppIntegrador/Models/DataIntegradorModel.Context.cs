﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppIntegrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataIntegradorEntities : DbContext
    {
        public DataIntegradorEntities()
            : base("name=DataIntegradorEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Enfasis> Enfasis { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilPermiso> PerfilPermiso { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }
        public virtual DbSet<UnidadAcademica> UnidadAcademica { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public virtual DbSet<Opciones_de_seleccion> Opciones_de_seleccion { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Pregunta_con_opciones> Pregunta_con_opciones { get; set; }
        public virtual DbSet<Pregunta_con_opciones_de_seleccion> Pregunta_con_opciones_de_seleccion { get; set; }
        public virtual DbSet<Activa_por> Activa_por { get; set; }
        public virtual DbSet<Escalar> Escalar { get; set; }
        public virtual DbSet<Formulario> Formulario { get; set; }
        public virtual DbSet<Formulario_tiene_seccion> Formulario_tiene_seccion { get; set; }
        public virtual DbSet<Opciones_seleccionadas_respuesta_con_opciones> Opciones_seleccionadas_respuesta_con_opciones { get; set; }
        public virtual DbSet<Periodo_activa_por> Periodo_activa_por { get; set; }
        public virtual DbSet<Pregunta_con_respuesta_libre> Pregunta_con_respuesta_libre { get; set; }
        public virtual DbSet<Responde_respuesta_con_opciones> Responde_respuesta_con_opciones { get; set; }
        public virtual DbSet<Responde_respuesta_libre> Responde_respuesta_libre { get; set; }
        public virtual DbSet<Respuestas_a_formulario> Respuestas_a_formulario { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }
        public virtual DbSet<Seccion_tiene_pregunta> Seccion_tiene_pregunta { get; set; }
        public virtual DbSet<Si_no_nr> Si_no_nr { get; set; }
    
        public virtual int AgregarUsuario(string pLogin, string pPassword, ObjectParameter estado)
        {
            var pLoginParameter = pLogin != null ?
                new ObjectParameter("pLogin", pLogin) :
                new ObjectParameter("pLogin", typeof(string));
    
            var pPasswordParameter = pPassword != null ?
                new ObjectParameter("pPassword", pPassword) :
                new ObjectParameter("pPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarUsuario", pLoginParameter, pPasswordParameter, estado);
        }
    }
}