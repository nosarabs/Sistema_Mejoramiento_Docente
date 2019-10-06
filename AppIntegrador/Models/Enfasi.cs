//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Enfasi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enfasi()
        {
            this.Cursoes = new HashSet<Curso>();
            this.PerfilPermisoes = new HashSet<PerfilPermiso>();
            this.UsuarioPerfils = new HashSet<UsuarioPerfil>();
            this.Estudiantes = new HashSet<Estudiante>();
        }
    
        public string CodCarrera { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    
        public virtual Carrera Carrera { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Cursoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerfilPermiso> PerfilPermisoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estudiante> Estudiantes { get; set; }
    }
}