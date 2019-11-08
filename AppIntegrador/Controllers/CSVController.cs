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
        ArchivoCSV fila;

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
                    ViewBag.Message = "Archivo subido exitosamente";
                    carga(path);
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
            db.InsertarPersonaCSV(fila.CorreoProfesor, fila.IdProfesor, fila.NombreProfesor, fila.ApellidoProfesor, fila.IdProfesor);
            db.InsertarFuncionarioCSV(fila.CorreoProfesor);
            db.InsertarProfesorCSV(fila.CorreoProfesor);
            db.InsertarPersonaCSV(fila.CorreoEstudiante, fila.IdEstudiante, fila.NombreEstudiante, fila.ApellidoEstudiante, fila.TipoIdEstudiante);
            db.InsertarEstudianteCSV(fila.CorreoEstudiante);
        }
        private void carga(string path)
        {
            bool datosValidos = true;

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

            fila = lista[0];
            if (validarEntradas(fila)) // si la primera linea esta con blancos no se pueden ingresar
            {
                //Se valida cada fila de CSV
                foreach (ArchivoCSV f in lista)
                {
                    //cargaFila(f);
                    if (!validarEntradas(fila))
                    {
                        archivoValido = false;
                    }
                }
                if (archivoValido) // si todas las filas son correctas inserta
                {
                    foreach (ArchivoCSV f in lista)
                    {
                        cargaFila(f);
                        insertarDatos(fila); //inserta fila
                    }
                }
            }
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
            if (!String.IsNullOrEmpty(archivo.NumeroGrupo) && !int.TryParse(archivo.NumeroGrupo, out int numGrupo) && numGrupo > 0)  //NumGrupo
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NumGrupo");
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!String.IsNullOrEmpty(archivo.Anno) && !int.TryParse(archivo.Anno, out int anno) && anno < 0)  //anno
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("anno");
                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo menor o igual a 3*/
            if (!String.IsNullOrEmpty(archivo.Semestre) && !int.TryParse(archivo.Semestre, out int semestre) && semestre < 0 && semestre > 3)  //semestre
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
            if (!String.IsNullOrEmpty(archivo.CodigoUnidad) && archivo.CodigoUnidad.Length > 10) //CodigoUnidad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoUnidad");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreFacultad) && archivo.NombreFacultad.Length > 50) //nombreFacultad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("nombreFacultad");
                return false;
            }
            /*Carrera*/
            if (!String.IsNullOrEmpty(archivo.CodigoCarrera) && archivo.CodigoCarrera.Length > 10) //CodigoCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoCarrera");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreCarrera) && archivo.NombreCarrera.Length > 50) //NombreCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCarrera");
                return false;
            }
            /*Enfasis*/
            if (!String.IsNullOrEmpty(archivo.CodigoEnfasis) && archivo.CodigoEnfasis.Length > 10) //CodigoEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoEnfasis");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreEnfasis) && archivo.NombreEnfasis.Length > 50) //NombreEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreEnfasis");
                return false;
            }
            /*Curso*/
            if (!String.IsNullOrEmpty(archivo.SiglaCurso) && archivo.SiglaCurso.Length > 10) //Sigla Curso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Sigla Curso");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreCurso) && archivo.NombreCurso.Length > 50) //NombreCurso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCurso");
                return false;
            }
            /*Profesor*/
            if (!String.IsNullOrEmpty(archivo.CorreoProfesor) && archivo.CorreoProfesor.Length > 50) //CorreoDocente
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoDocente");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.IdProfesor) && archivo.IdProfesor.Length > 30) //Id Profesor
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Id Profesor");

                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreProfesor) && archivo.NombreProfesor.Length > 15) //Nombre Profe
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Nombre Profe");

                return false;
            }
            if (!String.IsNullOrEmpty(archivo.ApellidoProfesor) && archivo.ApellidoProfesor.Length > 15) //apellidoProfa
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("apellido Profe");

                return false;
            }
            if (!String.IsNullOrEmpty(archivo.TipoIdProfesor) && archivo.TipoIdProfesor.Length > 15) //TipoIdProfesor
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("TipoIdProfesor");

                return false;
            }
            /*Estudiante*/
            if (!String.IsNullOrEmpty(archivo.CorreoEstudiante) && archivo.CorreoEstudiante.Length > 50) //CorreoEstudiantes
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoEstudiantes");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.IdEstudiante) && archivo.IdEstudiante.Length > 30) //Id Estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("IdEstudiante");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.NombreEstudiante) && archivo.NombreEstudiante.Length > 15) //Nombre Estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Nombre Estudiante");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.ApellidoEstudiante) && archivo.ApellidoEstudiante.Length > 15) //apellido estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("apellido Estudiante");
                return false;
            }
            if (!String.IsNullOrEmpty(archivo.TipoIdEstudiante) && archivo.TipoIdEstudiante.Length > 30) //TipoIdEstudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("TipoIdEstudiante");
                return false;
            }
            return true;
        }

        //COD-70: Yo como administrador quiero almacenar los datos de un archivo CSV en el sistema
        //Tarea técnica: Cargar datos en blanco con el dato anteriomente registrado
        private void cargaFila(ArchivoCSV filaActual)
        {
            /*Unidad academica*/
            if (!string.IsNullOrEmpty(filaActual.CodigoUnidad)) //CodigoUnidad
            {

                fila.CodigoUnidad = filaActual.CodigoUnidad;

            }
            if (!string.IsNullOrEmpty(filaActual.NombreFacultad)) //nombreFacultad
            {
                fila.NombreFacultad = filaActual.NombreFacultad;
            }

            /*Carrera*/
            if (!string.IsNullOrEmpty(filaActual.CodigoCarrera)) //CodigoCarrera
            {
                fila.CodigoCarrera = filaActual.CodigoCarrera;
            }
            if (!string.IsNullOrEmpty(filaActual.NombreCarrera)) //NombreCarrera
            {
                fila.NombreCarrera = filaActual.NombreCarrera;
            }

            /*Enfasis*/
            if (!string.IsNullOrEmpty(filaActual.CodigoEnfasis)) //CodigoEnfasis
            {
                fila.CodigoEnfasis = filaActual.CodigoEnfasis;
            }
            if (!string.IsNullOrEmpty(filaActual.NombreEnfasis)) //NombreEnfasis
            {
                fila.NombreEnfasis = filaActual.NombreEnfasis;
            }

            /*Curso*/
            if (!string.IsNullOrEmpty(filaActual.SiglaCurso)) //Sigla Curso
            {
                fila.SiglaCurso = filaActual.SiglaCurso;
            }
            if (!string.IsNullOrEmpty(filaActual.NombreCurso)) //NombreCurso
            {
                fila.NombreCurso = filaActual.NombreCurso;
            }

            /*NumGrupo*/
            if (!string.IsNullOrEmpty(filaActual.NumeroGrupo))  //NumGrupo
            {
                fila.NumeroGrupo = filaActual.NumeroGrupo;
            }
            if (!string.IsNullOrEmpty(filaActual.Anno))  //anno
            {
                fila.Anno = filaActual.Anno;
            }
            if (!string.IsNullOrEmpty(filaActual.Semestre))  //semestre
            {
                fila.Semestre = filaActual.Semestre;
            }

            /*Profesor*/
            if (!string.IsNullOrEmpty(filaActual.CorreoProfesor)) //CorreoDocente
            {
                fila.CorreoProfesor = filaActual.CorreoProfesor;
            }
            if (!string.IsNullOrEmpty(filaActual.IdProfesor)) //Id Profesor
            {
                fila.IdProfesor = filaActual.IdProfesor;
            }
            if (!string.IsNullOrEmpty(filaActual.NombreProfesor)) //Nombre Profe
            {
                fila.NombreProfesor = filaActual.NombreProfesor;
            }
            if (!string.IsNullOrEmpty(filaActual.ApellidoProfesor)) //apellidoProfa
            {
                fila.ApellidoProfesor = filaActual.ApellidoProfesor;
            }
            if (!string.IsNullOrEmpty(filaActual.TipoIdProfesor)) //TipoIdProfesor
            {
                fila.TipoIdProfesor = filaActual.TipoIdProfesor;
            }

            /*Estudiante*/
            if (!string.IsNullOrEmpty(filaActual.CorreoEstudiante)) //CorreoEstudiantes
            {
                fila.CorreoEstudiante = filaActual.CorreoEstudiante;
            }
            if (!string.IsNullOrEmpty(filaActual.IdEstudiante)) //Id Estudiante
            {
                fila.IdEstudiante = filaActual.IdEstudiante;
            }
            if (!string.IsNullOrEmpty(filaActual.NombreEstudiante)) //Nombre Estudiante
            {
                fila.NombreEstudiante = filaActual.NombreEstudiante;
            }
            if (!string.IsNullOrEmpty(filaActual.ApellidoEstudiante)) //apellido estudiante
            {
                fila.ApellidoEstudiante = filaActual.ApellidoEstudiante;
            }
            if (!string.IsNullOrEmpty(filaActual.TipoIdEstudiante)) //TipoIdEstudiante
            {
                fila.TipoIdEstudiante = filaActual.TipoIdEstudiante;
            }
        }

    }
}