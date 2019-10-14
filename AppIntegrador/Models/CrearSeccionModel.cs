namespace AppIntegrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using AppIntegrador.Models;
    using System.Collections.Generic;

    public partial class CrearSeccionModel : DbContext
    {
        public Seccion Seccion { get; set; }
        public IEnumerable <Pregunta_con_opciones_de_seleccion> pregunta_Con_Opciones_De_Seleccion { get; set; }
    }


}