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
    
    public partial class Respuestas_a_formulario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Respuestas_a_formulario()
        {
            this.Responde_respuesta_con_opciones = new HashSet<Responde_respuesta_con_opciones>();
            this.Responde_respuesta_libre = new HashSet<Responde_respuesta_libre>();
        }
    
        public string FCodigo { get; set; }
        public string Username { get; set; }
        public string CSigla { get; set; }
        public byte GNumero { get; set; }
        public int GAnno { get; set; }
        public byte GSemestre { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual Formulario Formulario { get; set; }
        public virtual Grupo Grupo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Responde_respuesta_con_opciones> Responde_respuesta_con_opciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Responde_respuesta_libre> Responde_respuesta_libre { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
