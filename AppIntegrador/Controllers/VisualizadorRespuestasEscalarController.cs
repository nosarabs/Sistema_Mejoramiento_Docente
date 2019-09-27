using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class VisualizadorRespuestasEscalarController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: VisualizadorRespuestasEscalar
        public ActionResult Index()
        {
            
            List<int> temp = ObtenerRespuestas();

            var modelo = new VisualizadorEscalar
            {
                Opciones_Respuestas = temp,
                Cantidad_por_Respuesta = ObtenerResultados(temp),
            };

            return View(modelo);
        }


        [HttpGet]
        public List<int> ObtenerRespuestas()
        {

            List<int> respuestas = new List<int>();
            // Obtiene el minimo de la vista
            int minimo = db.Visualizacion_Respuestas_Escalar.Select(x=> x.Minimo).First();
            int maximo = db.Visualizacion_Respuestas_Escalar.Select(x => x.Maximo).First();
            int incremento = db.Visualizacion_Respuestas_Escalar.Select(x => x.Incremento).First();

            

            for (int index = minimo; index <= maximo; index+=incremento)
            {
                respuestas.Add(index);
            }
            return respuestas;
        }

        [HttpGet]
        public List<int> ObtenerResultados(List<int> respuestilla)
        {
            List<int> resultados = new List<int>();
            for (int index = 0; index < respuestilla.Count(); ++index)
            {
                Console.WriteLine(respuestilla[index]);

                int y = respuestilla[index];
                
                resultados.Add(db.Visualizacion_Respuestas_Escalar
                    .Where(x => x.OpcionSeleccionada == y)
                    .Count());

            }
            return resultados;
        }

    }
}