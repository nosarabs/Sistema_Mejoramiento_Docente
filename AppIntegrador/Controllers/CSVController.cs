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
using System.IO;
using AppIntegrador.Models;
using System.Text.RegularExpressions;


namespace AppIntegrador.Controllers
{
    public class CSVController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        public ActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0) //Archivo no es nulo o vacío
                try
                {
                    string path = Path.Combine(Server.MapPath("~/ArchivoCSV"), //Server mapPath contiene el path del proyecto + la carpeta ArchivoCSV que es donde va el archivo
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!carga(path))
                    {
                        ViewBag.Message = "ERROR en la carga";
                    }
                    else
                    {
                        ViewBag.Message = "Archivo subido exitosamente";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Por favor especifique un archivo";
            }

            return View();
        }
        private void insertarDatos(ArchivoCSV fila)
        {
            db.InsertarUnidadCSV(fila.CodigoUnidad, fila.NombreFacultad);
            db.InsertarCarreraCSV(fila.CodigoCarrera, fila.NombreCarrera);
            db.InsertarEnfasisCSV(fila.CodigoCarrera, fila.CodigoEnfasis, fila.NombreEnfasis);
            db.InsertarCursoCSV(fila.SiglaCurso, fila.NombreCurso);
            db.InsertarGrupoCSV(fila.SiglaCurso, Convert.ToByte(fila.NumeroGrupo), Convert.ToByte(fila.Semestre), Convert.ToInt32(fila.Anno));
          //  db.InsertarPersonaCSV(fila.CorreoProfesor, fila.IdProfesor, fila.NombreProfesor, fila.ApellidoProfesor, fila.IdProfesor);
            db.InsertarFuncionarioCSV(fila.CorreoProfesor);
            db.InsertarProfesorCSV(fila.CorreoProfesor);
           // db.InsertarPersonaCSV(fila.CorreoEstudiante, fila.IdEstudiante, fila.NombreEstudiante, fila.ApellidoEstudiante, fila.TipoIdEstudiante);
            db.InsertarEstudianteCSV(fila.CorreoEstudiante);
            db.InsertarImparte(fila.CorreoProfesor, fila.SiglaCurso, Convert.ToByte(fila.NumeroGrupo), Convert.ToByte(fila.Semestre), Convert.ToInt32(fila.Anno));
            db.InsertarInscrita_En(fila.CodigoUnidad, fila.CodigoCarrera);
            db.InsertarEmpadronadoEn(fila.CorreoEstudiante, fila.CodigoCarrera, fila.CodigoEnfasis);
            db.InsertarTrabajaEn(fila.CorreoProfesor, fila.CodigoUnidad);
            db.InsertarPertenece_a(fila.CodigoCarrera, fila.CodigoEnfasis, fila.SiglaCurso);
        }
       public bool carga(string path)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ArchivoCSV> datos = cc.Read<ArchivoCSV>(path, inputFileDescription); //TODO: De momento el path está fijo
            List<ArchivoCSV> lista = datos.ToList();

            var archivoValido = true;

            //Se valida cada fila de CSV
            foreach (ArchivoCSV f in lista)
            {
                System.Diagnostics.Debug.WriteLine(f.CodigoUnidad);
                /*       if (!validarEntradas(f))
                       {
                          archivoValido = false;
                          return archivoValido;
                       }*/
            }
         /*   if (archivoValido) // si todas las filas son correctas inserta
            {
                 foreach (ArchivoCSV f in lista)
                 {
 
                    insertarDatos(f); //inserta fila
                 }
            }*/
            return archivoValido;
        }

        private bool validarEntradas(ArchivoCSV archivo)
        {
            bool numerosValidos = validaNumeros(archivo); //Validar que los datos numéricos son correctos
            bool longitudesValidas = validaLongitudes(archivo); //Validar que las longitudes de los caracteres son correctas

            if (numerosValidos && longitudesValidas) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validaNumeros(ArchivoCSV archivo)
        {
            /*NumGrupo*/
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!int.TryParse(archivo.NumeroGrupo, out int numGrupo) && numGrupo > 0)  //NumGrupo
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NumGrupo");
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!int.TryParse(archivo.Anno, out int anno) && anno < 0)  //anno
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("anno");
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo menor o igual a 3*/
            if (!int.TryParse(archivo.Semestre, out int semestre) && semestre < 0 && semestre > 3)  //semestre
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("semestre");
                return false;
            }
            return true;
        }

        private bool validaLongitudes(ArchivoCSV archivo)
        {
            /*Unidad academica*/
            if (archivo.CodigoUnidad.Length > 10) //CodigoUnidad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoUnidad");
                return false;
            }
            if (archivo.NombreFacultad.Length > 50) //nombreFacultad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("nombreFacultad");
                return false;
            }
            /*Carrera*/
            if (archivo.CodigoCarrera.Length > 10) //CodigoCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoCarrera");
                return false;
            }
            if (archivo.NombreCarrera.Length > 50) //NombreCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCarrera");
                return false;
            }
            /*Enfasis*/
            if (archivo.CodigoEnfasis.Length > 10) //CodigoEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoEnfasis");
                return false;
            }
            if (archivo.NombreEnfasis.Length > 50) //NombreEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreEnfasis");
                return false;
            }
            /*Curso*/
            if (archivo.SiglaCurso.Length > 10) //Sigla Curso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Sigla Curso");
                return false;
            }
            if (archivo.NombreCurso.Length > 50) //NombreCurso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCurso");
                return false;
            }
            /*Profesor*/
            if (archivo.CorreoProfesor.Length > 50) //CorreoDocente
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoDocente");
                return false;
            }
            /*Estudiante*/
            if (archivo.CorreoEstudiante.Length > 50) //CorreoEstudiantes
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoEstudiantes");
                return false;
            }
            return true;
        }
    }
}