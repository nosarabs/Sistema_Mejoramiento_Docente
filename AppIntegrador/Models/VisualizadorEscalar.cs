using System.Collections.Generic;
using System.Web.Mvc;
namespace AppIntegrador.Models
{
    public class VisualizadorEscalar
    {
        // Estos 2 atributos son los que se usarán para visualizar
        public List<int> Opciones_Respuestas { get; set; } //labels
        public List<int> Cantidad_por_Respuesta { get; set; } //data

    }
}
