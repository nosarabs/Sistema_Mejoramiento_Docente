using System.Collections.Generic;
using System.Web.Mvc;
namespace ECCI_IS_Lab01_WebApp.Models
{
    public class VisualizadorEscalar
    {
        public IEnumerable<SelectListItem> Cursos { get; set; }
        public double PromedioClase { get; set; }

        // Estos 2 atributos son los que se usarán para visualizar
        public List<int> Opciones_Respuestas { get; set; } //labels
        public List<int?> Cantidad_por_Respuesta { get; set; } //data

    }
}
