
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
    
public partial class TipoObjetivo
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TipoObjetivo()
    {

        this.Objetivo = new HashSet<Objetivo>();

        this.PlantillaObjetivo = new HashSet<PlantillaObjetivo>();

    }


    public string nombre { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Objetivo> Objetivo { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<PlantillaObjetivo> PlantillaObjetivo { get; set; }

}

}
