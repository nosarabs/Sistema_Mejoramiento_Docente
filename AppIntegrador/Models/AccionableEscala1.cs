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
    
    public partial class AccionableEscala1
    {
        public int codPlan { get; set; }
        public string nombreObj { get; set; }
        public string descripAcMej { get; set; }
        public string descripcion { get; set; }
        public int valorMinimo { get; set; }
        public int valorMaximo { get; set; }
        public Nullable<int> avance { get; set; }
    
        public virtual Accionable Accionable { get; set; }
    }
}
