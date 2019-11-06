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
using System.Text.RegularExpressions;


namespace AppIntegrador.Controllers
{
    public class CSVController : Controller
    {
        ArchivoCSV fila;
        LlenarCSV ll = new LlenarCSV();
        public ActionResult Index()
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ArchivoCSV> datos = cc.Read<ArchivoCSV>("c:\\Users\\Daniel\\DatosCSV.csv", inputFileDescription); //TODO: De momento el path está fijo
            List<ArchivoCSV> lista = datos.ToList();
            ll.insertarDatos(lista[0]);

            return View(lista[0]); //TODO: Cambiar esto. Fue usado solo para prueba
        }

        private bool validarEntradas (ArchivoCSV archivo)
        {

            /*Unidad academica*/
            if (archivo.CodigoUnidad.Length > 10 && !string.IsNullOrEmpty(archivo.CodigoUnidad)) //CodigoUnidad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoUnidad");

                return false;
            }
            if (archivo.NombreFacultad.Length > 50 && !string.IsNullOrEmpty(archivo.NombreFacultad)) //nombreFacultad
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("nombreFacultad");
                return false;
            }

            /*Carrera*/
            if (archivo.CodigoCarrera.Length > 10 && !string.IsNullOrEmpty(archivo.CodigoCarrera)) //CodigoCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoCarrera");
                return false;
            }
            if (archivo.NombreCarrera.Length > 50 && !string.IsNullOrEmpty(archivo.NombreCarrera)) //NombreCarrera
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCarrera");

                return false;
            }

            /*Enfasis*/
            if (archivo.CodigoEnfasis.Length > 10 && !string.IsNullOrEmpty(archivo.CodigoEnfasis)) //CodigoEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CodigoEnfasis");

                return false;
            }
            if (archivo.NombreEnfasis.Length > 50 && !string.IsNullOrEmpty(archivo.NombreEnfasis)) //NombreEnfasis
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreEnfasis");

                return false;
            }

            /*Curso*/
            if (archivo.SiglaCurso.Length > 10 && !string.IsNullOrEmpty(archivo.SiglaCurso)) //Sigla Curso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Sigla Curso");
                return false;
            }
            if (archivo.NombreCurso.Length > 50 && !string.IsNullOrEmpty(archivo.NombreCurso)) //NombreCurso
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NombreCurso");

                return false;
            }

            /*NumGrupo*/
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!int.TryParse(archivo.NumeroGrupo, out int numGrupo) && numGrupo < 0 && !string.IsNullOrEmpty(archivo.NumeroGrupo))  //NumGrupo
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("NumGrupo");

                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo*/
            if (!int.TryParse(archivo.Anno, out int anno) && anno < 0 && !string.IsNullOrEmpty(archivo.Anno))  //anno
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("anno");

                return false;
            }
            /*Compara que el valor ingresado sea un numero positivo menor o igual a 3*/
            if (!int.TryParse(archivo.Semestre, out int semestre) && semestre < 0 && semestre > 3 && !string.IsNullOrEmpty(archivo.Semestre))  //semestre
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("semestre");
                return false;
            }

            /*Profesor*/
            if (archivo.CorreoProfesor.Length > 50 && !string.IsNullOrEmpty(archivo.CorreoProfesor)) //CorreoDocente
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoDocente");
                return false;
            }
            if (archivo.IdProfesor.Length > 30 && !string.IsNullOrEmpty(archivo.IdProfesor)) //Id Profesor
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Id Profesor");

                return false;
            }
            if (archivo.NombreProfesor.Length > 15 && !string.IsNullOrEmpty(archivo.NombreProfesor)) //Nombre Profe
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Nombre Profe");

                return false;
            }
            if (archivo.ApellidoProfesor.Length > 15 && !string.IsNullOrEmpty(archivo.ApellidoProfesor)) //apellidoProfa
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("apellido Profe");

                return false;
            }
            if (archivo.TipoIdProfesor.Length > 15 && !string.IsNullOrEmpty(archivo.TipoIdProfesor)) //TipoIdProfesor
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("TipoIdProfesor");

                return false;
            }

            /*Estudiante*/
            if (archivo.CorreoEstudiante.Length > 50 && !string.IsNullOrEmpty(archivo.CorreoEstudiante)) //CorreoEstudiantes
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("CorreoEstudiantes");

                return false;
            }
            if (archivo.IdEstudiante.Length > 30 && !string.IsNullOrEmpty(archivo.IdEstudiante)) //Id Estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("IdEstudiante");

                return false;
            }
            if (archivo.NombreEstudiante.Length > 15 && !string.IsNullOrEmpty(archivo.NombreEstudiante)) //Nombre Estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("Nombre Estudiante");

                return false;
            }
            if (archivo.ApellidoEstudiante.Length > 15 && !string.IsNullOrEmpty(archivo.ApellidoEstudiante)) //apellido estudiante
            {
                /*Imprimir donde fallo*/
                System.Diagnostics.Debug.WriteLine("apellido Estudiante");

                return false;
            }
            if (archivo.TipoIdEstudiante.Length > 30 && !string.IsNullOrEmpty(archivo.TipoIdEstudiante)) //TipoIdEstudiante
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