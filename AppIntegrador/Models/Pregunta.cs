//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppIntegrador.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pregunta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pregunta()
        {
            this.Responde_respuesta_libre = new HashSet<Responde_respuesta_libre>();
            this.Responde_respuesta_con_opciones = new HashSet<Responde_respuesta_con_opciones>();
            this.Seccion_tiene_pregunta = new HashSet<Seccion_tiene_pregunta>();
        }
    
        public string Codigo { get; set; }
        public string Enunciado { get; set; }
        public string Tipo { get; set; }
    
        public virtual Pregunta_con_opciones Pregunta_con_opciones { get; set; }
        public virtual Pregunta_con_respuesta_libre Pregunta_con_respuesta_libre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Responde_respuesta_libre> Responde_respuesta_libre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Responde_respuesta_con_opciones> Responde_respuesta_con_opciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seccion_tiene_pregunta> Seccion_tiene_pregunta { get; set; }
    }
}
