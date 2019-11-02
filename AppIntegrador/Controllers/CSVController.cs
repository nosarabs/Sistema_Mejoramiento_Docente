using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LINQtoCSV;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.Entity;
using System.Net;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class CSVController : Controller
    {
        public ActionResult Index()
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true //La primera fila corresponde a los títulos de los campos, no a un campo específico
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ArchivoCSV> datos = cc.Read<ArchivoCSV>("c:\\Users\\Denisse Alfaro\\Documents\\DatosCSV.csv", inputFileDescription); //TODO: De momento el path está fijo
            List<ArchivoCSV> lista = datos.ToList(); 
            
            return View(lista[0]); //TODO: Cambiar esto. Fue usado solo para prueba
        }
    }
}