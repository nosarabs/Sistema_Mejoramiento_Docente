
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
    
public partial class Usuario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Usuario()
    {

        this.UsuarioPerfil = new HashSet<UsuarioPerfil>();

        this.Persona2 = new HashSet<Persona>();

    }


    public string Username { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public bool Activo { get; set; }



    public virtual Persona Persona { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }

    public virtual Persona Persona1 { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Persona> Persona2 { get; set; }

}

}
