
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
    
public partial class UnidadAcademica
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public UnidadAcademica()
    {

        this.UnidadAcademica1 = new HashSet<UnidadAcademica>();

        this.Carrera = new HashSet<Carrera>();

        this.Funcionario = new HashSet<Funcionario>();

    }


    public string Codigo { get; set; }

    public string Nombre { get; set; }

    public string Superior { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UnidadAcademica> UnidadAcademica1 { get; set; }

    public virtual UnidadAcademica UnidadAcademica2 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Carrera> Carrera { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Funcionario> Funcionario { get; set; }

}

}
