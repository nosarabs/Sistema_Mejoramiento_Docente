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
        public string insertarDatos(ArchivoCSV archivos)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertarUnidadCSV", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CodUnidad", archivos.CodigoUnidad);
                cmd.Parameters.AddWithValue("@Nfacultad", archivos.NombreFacultad);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                System.Diagnostics.Debug.WriteLine("Sirve");
                return ("Los datos han sido insertados");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                System.Diagnostics.Debug.WriteLine("NO SIRVE");
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return (ex.Message.ToString());
            }
        }
    }
}