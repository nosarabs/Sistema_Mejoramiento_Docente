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
    
    public partial class proyEntities : DbContext
    {
        public proyEntities()
            : base("name=proyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Accion_De_Mejora> Accion_De_Mejora { get; set; }
        public virtual DbSet<Accionable> Accionables { get; set; }
        public virtual DbSet<Escalar> Escalars { get; set; }
        public virtual DbSet<Formulario> Formularios { get; set; }
        public virtual DbSet<Formulario_tiene_seccion> Formulario_tiene_seccion { get; set; }
        public virtual DbSet<Objetivo> Objetivoes { get; set; }
        public virtual DbSet<Opciones_de_seleccion> Opciones_de_seleccion { get; set; }
        public virtual DbSet<PlanMejora> PlanMejoras { get; set; }
        public virtual DbSet<Pregunta> Preguntas { get; set; }
        public virtual DbSet<Pregunta_con_opciones> Pregunta_con_opciones { get; set; }
        public virtual DbSet<Pregunta_con_opciones_de_seleccion> Pregunta_con_opciones_de_seleccion { get; set; }
        public virtual DbSet<Pregunta_con_respuesta_libre> Pregunta_con_respuesta_libre { get; set; }
        public virtual DbSet<Responsable_De> Responsable_De { get; set; }
        public virtual DbSet<Seccion> Seccions { get; set; }
        public virtual DbSet<Seccion_tiene_pregunta> Seccion_tiene_pregunta { get; set; }
        public virtual DbSet<Si_no_nr> Si_no_nr { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tipo_Objetivo> Tipo_Objetivo { get; set; }
    }
}
