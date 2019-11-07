using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AppIntegrador.Models;

namespace AppIntegrador.Models
{
    public class LlenarCSV
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DataIntegrador;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
        public void insertarDatos(ArchivoCSV archivos)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertarUnidadCSV", con); //Cada uno de estos comandos llama a un procedimiento almacenado de inserción 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodUnidad", archivos.CodigoUnidad);
                cmd.Parameters.AddWithValue("@Nfacultad", archivos.NombreFacultad);

                SqlCommand cmd2 = new SqlCommand("InsertarCarreraCSV", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@Cod", archivos.CodigoCarrera);
                cmd2.Parameters.AddWithValue("@Nombre", archivos.NombreCarrera);

                SqlCommand cmd3 = new SqlCommand("InsertarEnfasisCSV", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@CodCarrera", archivos.CodigoCarrera);
                cmd3.Parameters.AddWithValue("@Codigo", archivos.CodigoEnfasis);
                cmd3.Parameters.AddWithValue("@Nombre", archivos.NombreEnfasis);

                SqlCommand cmd4 = new SqlCommand("InsertarCursoCSV", con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@Sigla", archivos.SiglaCurso);
                cmd4.Parameters.AddWithValue("@Nombre", archivos.NombreCurso);

                SqlCommand cmd5 = new SqlCommand("InsertarGrupoCSV", con);
                cmd5.CommandType = CommandType.StoredProcedure;
                cmd5.Parameters.AddWithValue("@SiglaCurso", archivos.SiglaCurso);
                cmd5.Parameters.AddWithValue("@NumGrupo", Int32.Parse(archivos.NumeroGrupo));
                cmd5.Parameters.AddWithValue("@Semestre", Int32.Parse(archivos.Semestre));
                cmd5.Parameters.AddWithValue("@Anno", Int32.Parse(archivos.Anno));

                SqlCommand cmd6 = new SqlCommand("InsertarPersonaCSV", con);
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.AddWithValue("@Correo", archivos.CorreoProfesor);
                cmd6.Parameters.AddWithValue("@Id", archivos.IdProfesor);
                cmd6.Parameters.AddWithValue("@Nombre", archivos.NombreProfesor);
                cmd6.Parameters.AddWithValue("@Apellido", archivos.ApellidoProfesor);
                cmd6.Parameters.AddWithValue("@TipoId", archivos.TipoIdProfesor);

                SqlCommand cmd7 = new SqlCommand("InsertarFuncionarioCSV", con);
                cmd7.CommandType = CommandType.StoredProcedure;
                cmd7.Parameters.AddWithValue("@Correo", archivos.CorreoProfesor);

                SqlCommand cmd8 = new SqlCommand("InsertarProfesorCSV", con);
                cmd8.CommandType = CommandType.StoredProcedure;
                cmd8.Parameters.AddWithValue("@Correo", archivos.CorreoProfesor);

                SqlCommand cmd9 = new SqlCommand("InsertarPersonaCSV", con);
                cmd9.CommandType = CommandType.StoredProcedure;
                cmd9.Parameters.AddWithValue("@Correo", archivos.CorreoEstudiante);
                cmd9.Parameters.AddWithValue("@Id", archivos.IdEstudiante);
                cmd9.Parameters.AddWithValue("@Nombre", archivos.NombreEstudiante);
                cmd9.Parameters.AddWithValue("@Apellido", archivos.ApellidoEstudiante);
                cmd9.Parameters.AddWithValue("@TipoId", archivos.TipoIdEstudiante);

                SqlCommand cmd10 = new SqlCommand("InsertarEstudianteCSV", con);
                cmd10.CommandType = CommandType.StoredProcedure;
                cmd10.Parameters.AddWithValue("@Correo", archivos.CorreoEstudiante);

                con.Open(); //Abre conexion con la db

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                cmd6.ExecuteNonQuery();
                cmd7.ExecuteNonQuery();
                cmd8.ExecuteNonQuery();
                cmd9.ExecuteNonQuery();
                cmd10.ExecuteNonQuery();

                con.Close(); //Cierra la conexion
            }
            catch (Exception ex) //Si ocurre algun error
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
    }
}