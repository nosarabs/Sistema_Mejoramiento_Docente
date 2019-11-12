/*using System;
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

                SqlCommand cmd11 = new SqlCommand("InsertarEmpadronadoEn", con);
                cmd11.CommandType = CommandType.StoredProcedure;
                cmd11.Parameters.AddWithValue("@CorreoEstudiante", archivos.CorreoEstudiante);
                cmd11.Parameters.AddWithValue("@CodCarrera", archivos.CodigoCarrera);
                cmd11.Parameters.AddWithValue("@CodEnfasis", archivos.CodigoEnfasis);

                SqlCommand cmd12 = new SqlCommand("InsertarImparte", con);
                cmd12.CommandType = CommandType.StoredProcedure;
                cmd12.Parameters.AddWithValue("@CorreoProfesor", archivos.CorreoProfesor);
                cmd12.Parameters.AddWithValue("@SiglaCurso", archivos.SiglaCurso);
                cmd12.Parameters.AddWithValue("@NumGrupo", Int32.Parse(archivos.NumeroGrupo));
                cmd12.Parameters.AddWithValue("@Semestre", Int32.Parse(archivos.Semestre));
                cmd12.Parameters.AddWithValue("@Anno", Int32.Parse(archivos.Anno));

                SqlCommand cmd13 = new SqlCommand("InsertarInscrita_En", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@CodUnidadAc", archivos.CodigoUnidad);
                cmd13.Parameters.AddWithValue("@CodCarrera", archivos.CodigoCarrera);
            

                SqlCommand cmd14 = new SqlCommand("InsertarPertenece_a", con);
                cmd14.CommandType = CommandType.StoredProcedure;
                cmd14.Parameters.AddWithValue("@CodCarrera", archivos.CodigoCarrera);
                cmd14.Parameters.AddWithValue("@CodEnfasis", archivos.CodigoEnfasis);
                cmd14.Parameters.AddWithValue("@SiglaCurso", archivos.SiglaCurso);

                SqlCommand cmd15 = new SqlCommand("InsertarTrabajaEn", con);
                cmd15.CommandType = CommandType.StoredProcedure;
                cmd15.Parameters.AddWithValue("@CodUnidadAcademica", archivos.CodigoUnidad);
                cmd15.Parameters.AddWithValue("@CorreoFuncionario", archivos.CorreoProfesor);

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
                cmd11.ExecuteNonQuery();
                cmd12.ExecuteNonQuery();
                cmd13.ExecuteNonQuery();
                cmd14.ExecuteNonQuery();
                cmd15.ExecuteNonQuery();




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
}*/