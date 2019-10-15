namespace AppIntegrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AppIntegrador.Models;
    using System.Collections.Generic;

    public partial class CrearFormularioModel : DbContext
    {
        public Formulario Formulario { get; set; }
        public IEnumerable<Seccion> seccion { get; set; }
    }


}