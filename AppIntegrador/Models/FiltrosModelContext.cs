namespace AppIntegrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.Entity.SqlServer;

    public partial class FiltrosEntities : DbContext
    {

        public FiltrosEntities()
            : base("name=FiltrosEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        [DbFunction("Entities", "ObtenerProfesoresFiltros")]
        public virtual IQueryable<ProfesoresFiltros> ObtenerProfesoresFiltros(DataTable FiltroUnidadesAcademicas, DataTable FiltroCarrerasEnfasis, DataTable FiltroGrupos)
        {
            var FiltroUnidadesAcademicasParameter = FiltroUnidadesAcademicas != null ?
                new SqlParameter("@UnidadesAcademicas", SqlDbType.Structured) { Value = FiltroUnidadesAcademicas } :
                new SqlParameter("@UnidadesAcademicas", SqlDbType.Structured);
            FiltroUnidadesAcademicasParameter.TypeName = "dbo.FiltroUnidadesAcademicas";

            var FiltroCarrerasEnfasisParameter = FiltroCarrerasEnfasis != null ?
                new SqlParameter("@CarrerasEnfasis", SqlDbType.Structured) { Value = FiltroCarrerasEnfasis } :
                new SqlParameter("@CarrerasEnfasis", SqlDbType.Structured);
            FiltroCarrerasEnfasisParameter.TypeName = "dbo.FiltroCarrerasEnfasis";

            var FiltroGruposParameter = FiltroGrupos != null ?
                new SqlParameter("@Grupos", SqlDbType.Structured) { Value = FiltroGrupos } :
                new SqlParameter("@Grupos", SqlDbType.Structured);
            FiltroGruposParameter.TypeName = "dbo.FiltroGrupos";

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<ProfesoresFiltros>("SELECT Correo, Nombre1, Nombre2, Apellido1, Apellido2 FROM [ObtenerProfesoresFiltros](@UnidadesAcademicas, @CarrerasEnfasis, @Grupos)", FiltroUnidadesAcademicasParameter, FiltroCarrerasEnfasisParameter, FiltroGruposParameter).AsQueryable();
        }

        [DbFunction("Entities", "ObtenerFormulariosFiltros")]
        public virtual IQueryable<FormulariosFiltros> ObtenerFormulariosFiltros(DataTable FiltroUnidadesAcademicas, DataTable FiltroCarrerasEnfasis, DataTable FiltroGrupos, DataTable FiltroProfesores)
        {
            var FiltroUnidadesAcademicasParameter = FiltroUnidadesAcademicas != null ?
                new SqlParameter("@UnidadesAcademicas", SqlDbType.Structured) { Value = FiltroUnidadesAcademicas } :
                new SqlParameter("@UnidadesAcademicas", SqlDbType.Structured);
            FiltroUnidadesAcademicasParameter.TypeName = "dbo.FiltroUnidadesAcademicas";

            var FiltroCarrerasEnfasisParameter = FiltroCarrerasEnfasis != null ?
                new SqlParameter("@CarrerasEnfasis", SqlDbType.Structured) { Value = FiltroCarrerasEnfasis } :
                new SqlParameter("@CarrerasEnfasis", SqlDbType.Structured);
            FiltroCarrerasEnfasisParameter.TypeName = "dbo.FiltroCarrerasEnfasis";

            var FiltroGruposParameter = FiltroGrupos != null ?
                new SqlParameter("@Grupos", SqlDbType.Structured) { Value = FiltroGrupos } :
                new SqlParameter("@Grupos", SqlDbType.Structured);
            FiltroGruposParameter.TypeName = "dbo.FiltroGrupos";

            var FiltroProfesoresParameter = FiltroProfesores != null ?
                new SqlParameter("@CorreosProfesores", SqlDbType.Structured) { Value = FiltroProfesores } :
                new SqlParameter("@CorreosProfesores", SqlDbType.Structured);
            FiltroProfesoresParameter.TypeName = "dbo.FiltroProfesores";

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<FormulariosFiltros>("SELECT FCodigo, FNombre, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin FROM [ObtenerFormulariosFiltros](@UnidadesAcademicas, @CarrerasEnfasis, @Grupos, @CorreosProfesores)", FiltroUnidadesAcademicasParameter, FiltroCarrerasEnfasisParameter, FiltroGruposParameter, FiltroProfesoresParameter).AsQueryable();
        }
    }
}