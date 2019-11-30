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

        public ActionResult Clase()
        {
            return View();
        }

        public ActionResult GuiaHorarios()
        {
            return View();
        }

        public ActionResult Funcionarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0) //Archivo no es nulo o vacío
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Estudiantes"), //Server mapPath contiene el path del proyecto + la carpeta ArchivoCSV que es donde va el archivo
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!cargarListaEstudiante(path))
                    {
                        ViewBag.Message = "ERROR en la carga"; //TO-DO: Debe cambiarse por llamados a validaciones
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

        [HttpPost]
        public ActionResult Clase(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            { //Archivo no es nulo o vacío
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Listas de Clase"), //Server mapPath contiene el path del proyecto + la carpeta ArchivoCSV que es donde va el archivo
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!cargarListaClase(path))
                    {
                        ViewBag.Message = "ERROR en la carga"; //TO-DO: Debe cambiarse por llamados a validaciones
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
            }
            else
            {
                ViewBag.Message = "Por favor especifique un archivo";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Funcionarios(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0) //Archivo no es nulo o vacío
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Funcionarios"), //Server mapPath contiene el path del proyecto + la carpeta ArchivoCSV que es donde va el archivo
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!cargarListaFuncionario(path))
                    {
                        ViewBag.Message = "ERROR en la carga"; //TO-DO: Debe cambiarse por llamados a validaciones
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

        [HttpPost]
        public ActionResult GuiaHorarios(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0) //Archivo no es nulo o vacío
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Guias Horario"), //Server mapPath contiene el path del proyecto + la carpeta ArchivoCSV que es donde va el archivo
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!cargarGuia(path))
                    {
                        ViewBag.Message = "ERROR en la carga"; //TO-DO: Debe cambiarse por llamados a validaciones
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

        private bool cargarListaClase(string path)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ListaClase> datos = cc.Read<ListaClase>(path, inputFileDescription);
            List<ListaClase> lista = datos.ToList();

            //Se valida cada fila de CSV
            foreach (ListaClase f in lista)
            {
                insertarListaClase(f);
            }
            return true;
        }

        private void insertarListaClase(ListaClase fila)
        {
            db.InsertarUnidadCSV(fila.CodigoUnidad, fila.NombreFacultad);
            db.InsertarCarreraCSV(fila.CodigoCarrera, fila.NombreCarrera);
            db.InsertarEnfasisCSV(fila.CodigoCarreraEnfasis, fila.CodigoEnfasis, fila.NombreEnfasis);
        }

        private bool cargarGuia(string path)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<GuiaHorario> datos = cc.Read<GuiaHorario>(path, inputFileDescription);
            List<GuiaHorario> lista = datos.ToList();

            //Se valida cada fila de CSV
            foreach (GuiaHorario f in lista)
            {
                insertarGuia(f);
            }
            return true;
        }

        private void insertarGuia(GuiaHorario fila)
        {
            db.InsertarCursoCSV(fila.SiglaCurso, fila.NombreCurso);
            db.InsertarGrupoCSV(fila.SiglaCursoGrupo, Convert.ToByte(fila.NumeroGrupo), Convert.ToByte(fila.Semestre), Convert.ToInt32(fila.Anno));
            db.InsertarImparte(fila.CorreoProfesorImparte, fila.SiglaCursoImparte, Convert.ToByte(fila.NumeroGrupoImparte), Convert.ToByte(fila.SemestreGrupoImparte), Convert.ToInt32(fila.AnnoGrupoImparte));
            db.InsertarMatriculado_en(fila.CorreoMatricula, fila.SiglaCursoMatricula, Convert.ToByte(fila.NumeroGrupoMatricula), Convert.ToByte(fila.SemestreMatricula), Convert.ToInt32(fila.AnnoMatricula));
        }

        private bool cargarListaEstudiante(string path)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ListaEstudiante> datos = cc.Read<ListaEstudiante>(path, inputFileDescription);
            List<ListaEstudiante> lista = datos.ToList();

 

            //Se valida cada fila de CSV
            foreach (ListaEstudiante f in lista)
            {
                insertarListaEstudiante(f);
            }
            return true;
        }

        private void insertarListaEstudiante(ListaEstudiante fila)
        {
            db.InsertarPersonaCSV(fila.CorreoPersona, fila.IdPersona, fila.NombrePersona, fila.ApellidoPersona, fila.TipoIdPersona, Convert.ToBoolean(fila.Borrado));
            db.InsertarEstudianteCSV(fila.CorreoEstudiante);
        }

        private bool cargarListaFuncionario(string path)
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',', //Indica qué es lo que separa cada valor en el archivo
                FirstLineHasColumnNames = true, //La primera fila corresponde a los títulos de los campos, no a un campo específico
                IgnoreUnknownColumns = true // Linea para evitar errores o algunos datos no desead0s
            };
            CsvContext cc = new CsvContext();
            //Este IEnumerable tiene cada modelo que fue llenado con los datos del CSV
            IEnumerable<ListaFuncionario> datos = cc.Read<ListaFuncionario>(path, inputFileDescription);
            List<ListaFuncionario> lista = datos.ToList();


            ValidadorListaDeEstudiantes val = new ValidadorListaDeEstudiantes();

            //Se valida cada fila de CSV
            foreach (ListaFuncionario f in lista)
            {
                System.Diagnostics.Debug.WriteLine(f.CorreoPersona +" = " + val.Validar(f));
                insertarListaFuncionario(f);
            }
            return true;
        }

        private void insertarListaFuncionario(ListaFuncionario fila)
        {
            db.InsertarPersonaCSV(fila.CorreoPersona, fila.IdPersona, fila.NombrePersona, fila.ApellidoPersona, fila.TipoIdPersona, Convert.ToBoolean(fila.Borrado));
            db.InsertarFuncionarioCSV(fila.CorreoProfesor); //TODO: Que pasa si no es profesor?
            db.InsertarProfesorCSV(fila.CorreoProfesor);
        }

    }
}
