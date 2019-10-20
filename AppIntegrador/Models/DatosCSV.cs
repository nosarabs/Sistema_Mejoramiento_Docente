using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace AppIntegrador.Models
{
    public class DatosCSV
    {
        public string Nombre { get; set; }
        public string SiglaCurso { get; set; }
        public int Anno { get; set; }
        public int Semestre { get; set; }
        public int Grupo { get; set; }
        public DatosCSV ParseRow(string row)
        {
            var columnas = row.Split(',');
            return new DatosCSV()
            {
                Nombre = columnas[0],
                SiglaCurso = columnas[1],
                Anno = int.Parse(columnas[2]),
                Semestre = int.Parse(columnas[3]),
                Grupo = int.Parse(columnas[4])
            };
        }
    }
}