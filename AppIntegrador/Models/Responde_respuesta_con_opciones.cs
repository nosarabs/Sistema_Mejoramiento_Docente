
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
    
public partial class Responde_respuesta_con_opciones
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Responde_respuesta_con_opciones()
    {

        this.Opciones_seleccionadas_respuesta_con_opciones = new HashSet<Opciones_seleccionadas_respuesta_con_opciones>();

    }


    public string FCodigo { get; set; }

    public string Correo { get; set; }

    public string CSigla { get; set; }

    public byte GNumero { get; set; }

    public int GAnno { get; set; }

    public byte GSemestre { get; set; }

    public System.DateTime Fecha { get; set; }

    public string PCodigo { get; set; }

    public string SCodigo { get; set; }

    public string Justificacion { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Opciones_seleccionadas_respuesta_con_opciones> Opciones_seleccionadas_respuesta_con_opciones { get; set; }

    public virtual Pregunta Pregunta { get; set; }

    public virtual Seccion Seccion { get; set; }

    public virtual Respuestas_a_formulario Respuestas_a_formulario { get; set; }

}

}
