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
    
    public partial class Asociado_A
    {
        public int CodigoO { get; set; }
        public int CodigoA { get; set; }
    
        public virtual Accion_De_Mejora Accion_De_Mejora { get; set; }
    }
}