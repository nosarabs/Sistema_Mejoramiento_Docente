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
            try
            {
                db.InsertarUnidadCSV(fila.CodigoUnidad, fila.NombreFacultad);
                db.InsertarCarreraCSV(fila.CodigoCarrera, fila.NombreCarrera);
                db.InsertarInscrita_En(fila.CodigoUnidadCarrera, fila.CodigoCarrera);
                db.InsertarEnfasisCSV(fila.CodigoCarreraEnfasis, fila.CodigoEnfasis, fila.NombreEnfasis);
                db.InsertarCursoCSV(fila.SiglaCurso, fila.NombreCurso);
                db.InsertarPertenece_a(fila.CodigoCarreraCurso, fila.CodigoEnfasisCurso, fila.SiglaCurso);
                db.InsertarGrupoCSV(fila.SiglaCursoGrupo, Convert.ToByte(fila.NumeroGrupo), Convert.ToByte(fila.Semestre), Convert.ToInt32(fila.Anno));
                db.InsertarPersonaCSV(fila.CorreoPersona, fila.IdPersona, fila.NombrePersona, fila.ApellidoPersona, fila.TipoIdPersona);
                db.InsertarFuncionarioCSV(fila.CorreoProfesor);
                db.InsertarProfesorCSV(fila.CorreoProfesor);
                db.InsertarEstudianteCSV(fila.CorreoEstudiante);
                db.InsertarMatriculado_en(fila.CorreoMatricula, fila.SiglaCursoMatricula, Convert.ToByte(fila.NumeroGrupoMatricula), Convert.ToByte(fila.SemestreMatricula), Convert.ToInt32(fila.AnnoMatricula));
                db.InsertarImparte(fila.CorreoProfesorImparte, fila.SiglaCursoImparte, Convert.ToByte(fila.NumeroGrupoImparte), Convert.ToByte(fila.SemestreGrupoImparte), Convert.ToInt32(fila.AnnoGrupoImparte));
                /*   db.InsertarEmpadronadoEn(fila.CorreoEstudiante, fila.CodigoCarrera, fila.CodigoEnfasis);
                   db.InsertarTrabajaEn(fila.CorreoProfesor, fila.CodigoUnidad);*/
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }


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
                if (!validarEntradas(f))
                {
                    archivoValido = false;
                    return archivoValido;
                }
            }
            if (archivoValido) // si todas las filas son correctas inserta
            {
                foreach (ArchivoCSV f in lista)
                {
                    insertarDatos(f); //inserta fila
                }
            }
            return archivoValido;
        }

        public bool validarEntradas(ArchivoCSV archivo)
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
            if (!String.IsNullOrEmpty(archivo.NumeroGrupo) && (!int.TryParse(archivo.NumeroGrupo, out int numGrupo) || numGrupo < 0))  //NumGrupo
            {
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!String.IsNullOrEmpty(archivo.Anno) && (!int.TryParse(archivo.Anno, out int anno) || anno < 0))  //anno
            {
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo menor o igual a 3*/
            if (!String.IsNullOrEmpty(archivo.Semestre) && (!int.TryParse(archivo.Semestre, out int semestre) || semestre < 0 || semestre > 3))  //semestre
            {
                return false;
            }
            return true;
        }

        private bool validaLongitudes(ArchivoCSV archivo)
        {
            /*Unidad academica*/
            if (!String.IsNullOrEmpty(archivo.CodigoUnidad) && archivo.CodigoUnidad.Length > 10) //CodigoUnidad
            {
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreFacultad) && archivo.NombreFacultad.Length > 50) //nombreFacultad
            {
                return false;
            }
            /*Carrera*/
            if (!String.IsNullOrEmpty(archivo.CodigoCarrera) && archivo.CodigoCarrera.Length > 10) //CodigoCarrera
            { 
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreCarrera) && archivo.NombreCarrera.Length > 50) //NombreCarrera
            {
                return false;
            }
            /*Enfasis*/
            if (!String.IsNullOrEmpty(archivo.CodigoEnfasis) && archivo.CodigoEnfasis.Length > 10) //CodigoEnfasis
            {
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreEnfasis) && archivo.NombreEnfasis.Length > 50) //NombreEnfasis
            { 
                return false;
            }
            /*Curso*/
            if (!String.IsNullOrEmpty(archivo.SiglaCurso) && archivo.SiglaCurso.Length > 10) //Sigla Curso
            {
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreCurso) && archivo.NombreCurso.Length > 50) //NombreCurso
            {
                return false;
            }
            /*Profesor*/
            if (!String.IsNullOrEmpty(archivo.CorreoProfesor) && archivo.CorreoProfesor.Length > 50) //CorreoDocente
            {
                return false;
            }
            /*Estudiante*/
            if (!String.IsNullOrEmpty(archivo.CorreoEstudiante) && archivo.CorreoEstudiante.Length > 50) //CorreoEstudiantes
            {
                return false;
            }
            return true;
        }
    }
}