
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
    
public partial class Persona
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Persona()
    {

        this.Respuestas_a_formulario = new HashSet<Respuestas_a_formulario>();

    }


    public string Correo { get; set; }

    public string CorreoAlt { get; set; }

    public string Identificacion { get; set; }

    public string Nombre1 { get; set; }

    public string Nombre2 { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public string Usuario { get; set; }

    public string TipoIdentificacion { get; set; }



    public virtual Estudiante Estudiante { get; set; }

    public virtual Funcionario Funcionario { get; set; }

    public virtual Usuario Usuario1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Respuestas_a_formulario> Respuestas_a_formulario { get; set; }

}

}
