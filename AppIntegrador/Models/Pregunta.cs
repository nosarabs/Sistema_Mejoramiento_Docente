
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
