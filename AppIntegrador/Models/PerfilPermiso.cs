
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
    
public partial class PerfilPermiso
{

    public string Perfil { get; set; }

    public int PermisoId { get; set; }

    public string CodCarrera { get; set; }

    public string CodEnfasis { get; set; }



    public virtual Enfasis Enfasis { get; set; }

    public virtual Perfil Perfil1 { get; set; }

    public virtual Permiso Permiso { get; set; }

}

}
