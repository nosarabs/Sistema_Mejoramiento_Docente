﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppIntegrador.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<Accionable> Accionable { get; set; }
        public virtual DbSet<AccionDeMejora> AccionDeMejora { get; set; }
        public virtual DbSet<Activa_por> Activa_por { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Enfasis> Enfasis { get; set; }
        public virtual DbSet<Escalar> Escalar { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Formulario> Formulario { get; set; }
        public virtual DbSet<Formulario_tiene_seccion> Formulario_tiene_seccion { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Objetivo> Objetivo { get; set; }
        public virtual DbSet<Opciones_de_seleccion> Opciones_de_seleccion { get; set; }
        public virtual DbSet<Opciones_seleccionadas_respuesta_con_opciones> Opciones_seleccionadas_respuesta_con_opciones { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilPermiso> PerfilPermiso { get; set; }
        public virtual DbSet<Periodo_activa_por> Periodo_activa_por { get; set; }
        public virtual DbSet<Permiso> Permiso { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PlanDeMejora> PlanDeMejora { get; set; }
        public virtual DbSet<PlantillaAccionDeMejora> PlantillaAccionDeMejora { get; set; }
        public virtual DbSet<PlantillaObjetivo> PlantillaObjetivo { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Pregunta_con_opciones> Pregunta_con_opciones { get; set; }
        public virtual DbSet<Pregunta_con_opciones_de_seleccion> Pregunta_con_opciones_de_seleccion { get; set; }
        public virtual DbSet<Pregunta_con_respuesta_libre> Pregunta_con_respuesta_libre { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }
        public virtual DbSet<Responde_respuesta_con_opciones> Responde_respuesta_con_opciones { get; set; }
        public virtual DbSet<Responde_respuesta_libre> Responde_respuesta_libre { get; set; }
        public virtual DbSet<Respuestas_a_formulario> Respuestas_a_formulario { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }
        public virtual DbSet<Seccion_tiene_pregunta> Seccion_tiene_pregunta { get; set; }
        public virtual DbSet<Si_no_nr> Si_no_nr { get; set; }
        public virtual DbSet<TipoObjetivo> TipoObjetivo { get; set; }
        public virtual DbSet<UnidadAcademica> UnidadAcademica { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
    
        [DbFunction("Entities", "EnfasisXCarreraXPerfil")]
        public virtual IQueryable<EnfasisXCarreraXPerfil_Result> EnfasisXCarreraXPerfil(string correoUsuario, string codCarrera, string nombrePerfil)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            var codCarreraParameter = codCarrera != null ?
                new ObjectParameter("codCarrera", codCarrera) :
                new ObjectParameter("codCarrera", typeof(string));
    
            var nombrePerfilParameter = nombrePerfil != null ?
                new ObjectParameter("nombrePerfil", nombrePerfil) :
                new ObjectParameter("nombrePerfil", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<EnfasisXCarreraXPerfil_Result>("[Entities].[EnfasisXCarreraXPerfil](@correoUsuario, @codCarrera, @nombrePerfil)", correoUsuarioParameter, codCarreraParameter, nombrePerfilParameter);
        }
    
        [DbFunction("Entities", "PerfilesXUsuario")]
        public virtual IQueryable<PerfilesXUsuario_Result> PerfilesXUsuario(string correoUsuario)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<PerfilesXUsuario_Result>("[Entities].[PerfilesXUsuario](@correoUsuario)", correoUsuarioParameter);
        }
    
        public virtual int AgregarFormulario(string codigo, string nombre)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarFormulario", codigoParameter, nombreParameter);
        }
    
        public virtual int AgregarOpcion(string cod, Nullable<byte> orden, string texto)
        {
            var codParameter = cod != null ?
                new ObjectParameter("cod", cod) :
                new ObjectParameter("cod", typeof(string));
    
            var ordenParameter = orden.HasValue ?
                new ObjectParameter("orden", orden) :
                new ObjectParameter("orden", typeof(byte));
    
            var textoParameter = texto != null ?
                new ObjectParameter("texto", texto) :
                new ObjectParameter("texto", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarOpcion", codParameter, ordenParameter, textoParameter);
        }
    
        public virtual int AgregarPlan(Nullable<int> codigo, string nombre, Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin)
        {
            var codigoParameter = codigo.HasValue ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarPlan", codigoParameter, nombreParameter, fechaInicioParameter, fechaFinParameter);
        }
    
        public virtual int AgregarPreguntaConOpcion(string cod, string type, string enunciado, string justificacion)
        {
            var codParameter = cod != null ?
                new ObjectParameter("cod", cod) :
                new ObjectParameter("cod", typeof(string));
    
            var typeParameter = type != null ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(string));
    
            var enunciadoParameter = enunciado != null ?
                new ObjectParameter("enunciado", enunciado) :
                new ObjectParameter("enunciado", typeof(string));
    
            var justificacionParameter = justificacion != null ?
                new ObjectParameter("justificacion", justificacion) :
                new ObjectParameter("justificacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarPreguntaConOpcion", codParameter, typeParameter, enunciadoParameter, justificacionParameter);
        }
    
        public virtual int AgregarPreguntas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarPreguntas");
        }
    
        public virtual int AgregarPreguntaSeleccion(string codigo)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarPreguntaSeleccion", codigoParameter);
        }
    
        public virtual int AgregarSeccion(string codigo, string nombre)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarSeccion", codigoParameter, nombreParameter);
        }
    
        public virtual int AgregarUsuario(string pLogin, string pPassword, Nullable<bool> activo, ObjectParameter estado)
        {
            var pLoginParameter = pLogin != null ?
                new ObjectParameter("pLogin", pLogin) :
                new ObjectParameter("pLogin", typeof(string));
    
            var pPasswordParameter = pPassword != null ?
                new ObjectParameter("pPassword", pPassword) :
                new ObjectParameter("pPassword", typeof(string));
    
            var activoParameter = activo.HasValue ?
                new ObjectParameter("activo", activo) :
                new ObjectParameter("activo", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AgregarUsuario", pLoginParameter, pPasswordParameter, activoParameter, estado);
        }
    
        public virtual int AsociarPreguntaConSeccion(string codigoSeccion, string codigoPregunta, Nullable<int> orden)
        {
            var codigoSeccionParameter = codigoSeccion != null ?
                new ObjectParameter("CodigoSeccion", codigoSeccion) :
                new ObjectParameter("CodigoSeccion", typeof(string));
    
            var codigoPreguntaParameter = codigoPregunta != null ?
                new ObjectParameter("CodigoPregunta", codigoPregunta) :
                new ObjectParameter("CodigoPregunta", typeof(string));
    
            var ordenParameter = orden.HasValue ?
                new ObjectParameter("Orden", orden) :
                new ObjectParameter("Orden", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AsociarPreguntaConSeccion", codigoSeccionParameter, codigoPreguntaParameter, ordenParameter);
        }
    
        public virtual int AsociarSeccionConFormulario(string codigoFormulario, string codigoSeccion, Nullable<int> orden)
        {
            var codigoFormularioParameter = codigoFormulario != null ?
                new ObjectParameter("codigoFormulario", codigoFormulario) :
                new ObjectParameter("codigoFormulario", typeof(string));
    
            var codigoSeccionParameter = codigoSeccion != null ?
                new ObjectParameter("codigoSeccion", codigoSeccion) :
                new ObjectParameter("codigoSeccion", typeof(string));
    
            var ordenParameter = orden.HasValue ?
                new ObjectParameter("orden", orden) :
                new ObjectParameter("orden", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AsociarSeccionConFormulario", codigoFormularioParameter, codigoSeccionParameter, ordenParameter);
        }
    
        public virtual int ChangePassword(string username, string newpassword)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var newpasswordParameter = newpassword != null ?
                new ObjectParameter("newpassword", newpassword) :
                new ObjectParameter("newpassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ChangePassword", usernameParameter, newpasswordParameter);
        }
    
        public virtual int CheckID(string identificacion, ObjectParameter result)
        {
            var identificacionParameter = identificacion != null ?
                new ObjectParameter("identificacion", identificacion) :
                new ObjectParameter("identificacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CheckID", identificacionParameter, result);
        }
    
        public virtual int DesviacionEstandarEscalar(string fCod, string cSigla, Nullable<byte> grupo, Nullable<int> gAnno, Nullable<byte> gSem, string pCod, ObjectParameter desviacion)
        {
            var fCodParameter = fCod != null ?
                new ObjectParameter("FCod", fCod) :
                new ObjectParameter("FCod", typeof(string));
    
            var cSiglaParameter = cSigla != null ?
                new ObjectParameter("CSigla", cSigla) :
                new ObjectParameter("CSigla", typeof(string));
    
            var grupoParameter = grupo.HasValue ?
                new ObjectParameter("Grupo", grupo) :
                new ObjectParameter("Grupo", typeof(byte));
    
            var gAnnoParameter = gAnno.HasValue ?
                new ObjectParameter("GAnno", gAnno) :
                new ObjectParameter("GAnno", typeof(int));
    
            var gSemParameter = gSem.HasValue ?
                new ObjectParameter("GSem", gSem) :
                new ObjectParameter("GSem", typeof(byte));
    
            var pCodParameter = pCod != null ?
                new ObjectParameter("PCod", pCod) :
                new ObjectParameter("PCod", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DesviacionEstandarEscalar", fCodParameter, cSiglaParameter, grupoParameter, gAnnoParameter, gSemParameter, pCodParameter, desviacion);
        }
    
        public virtual int GuardarRespuestaAFormulario(string codFormulario, string correo, string siglaCurso, Nullable<byte> numGrupo, Nullable<int> anno, Nullable<byte> semestre, Nullable<System.DateTime> fecha)
        {
            var codFormularioParameter = codFormulario != null ?
                new ObjectParameter("codFormulario", codFormulario) :
                new ObjectParameter("codFormulario", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var siglaCursoParameter = siglaCurso != null ?
                new ObjectParameter("siglaCurso", siglaCurso) :
                new ObjectParameter("siglaCurso", typeof(string));
    
            var numGrupoParameter = numGrupo.HasValue ?
                new ObjectParameter("numGrupo", numGrupo) :
                new ObjectParameter("numGrupo", typeof(byte));
    
            var annoParameter = anno.HasValue ?
                new ObjectParameter("anno", anno) :
                new ObjectParameter("anno", typeof(int));
    
            var semestreParameter = semestre.HasValue ?
                new ObjectParameter("semestre", semestre) :
                new ObjectParameter("semestre", typeof(byte));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarRespuestaAFormulario", codFormularioParameter, correoParameter, siglaCursoParameter, numGrupoParameter, annoParameter, semestreParameter, fechaParameter);
        }
    
        public virtual int GuardarRespuestaAPregunta(string tipo, string codFormulario, string correo, string siglaCurso, Nullable<byte> numGrupo, Nullable<int> anno, Nullable<byte> semestre, Nullable<System.DateTime> fecha, string codPregunta, string justificacion, Nullable<byte> opcionSeleccionada, string observacion)
        {
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            var codFormularioParameter = codFormulario != null ?
                new ObjectParameter("codFormulario", codFormulario) :
                new ObjectParameter("codFormulario", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var siglaCursoParameter = siglaCurso != null ?
                new ObjectParameter("siglaCurso", siglaCurso) :
                new ObjectParameter("siglaCurso", typeof(string));
    
            var numGrupoParameter = numGrupo.HasValue ?
                new ObjectParameter("numGrupo", numGrupo) :
                new ObjectParameter("numGrupo", typeof(byte));
    
            var annoParameter = anno.HasValue ?
                new ObjectParameter("anno", anno) :
                new ObjectParameter("anno", typeof(int));
    
            var semestreParameter = semestre.HasValue ?
                new ObjectParameter("semestre", semestre) :
                new ObjectParameter("semestre", typeof(byte));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(System.DateTime));
    
            var codPreguntaParameter = codPregunta != null ?
                new ObjectParameter("codPregunta", codPregunta) :
                new ObjectParameter("codPregunta", typeof(string));
    
            var justificacionParameter = justificacion != null ?
                new ObjectParameter("justificacion", justificacion) :
                new ObjectParameter("justificacion", typeof(string));
    
            var opcionSeleccionadaParameter = opcionSeleccionada.HasValue ?
                new ObjectParameter("opcionSeleccionada", opcionSeleccionada) :
                new ObjectParameter("opcionSeleccionada", typeof(byte));
    
            var observacionParameter = observacion != null ?
                new ObjectParameter("observacion", observacion) :
                new ObjectParameter("observacion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GuardarRespuestaAPregunta", tipoParameter, codFormularioParameter, correoParameter, siglaCursoParameter, numGrupoParameter, annoParameter, semestreParameter, fechaParameter, codPreguntaParameter, justificacionParameter, opcionSeleccionadaParameter, observacionParameter);
        }
    
        public virtual int LoginUsuario(string pLoginName, string pPassword, ObjectParameter result)
        {
            var pLoginNameParameter = pLoginName != null ?
                new ObjectParameter("pLoginName", pLoginName) :
                new ObjectParameter("pLoginName", typeof(string));
    
            var pPasswordParameter = pPassword != null ?
                new ObjectParameter("pPassword", pPassword) :
                new ObjectParameter("pPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LoginUsuario", pLoginNameParameter, pPasswordParameter, result);
        }
    
        public virtual int Mediana(string codigoFormulario, string siglaCurso, Nullable<byte> numeroGrupo, Nullable<int> anio, Nullable<byte> semestre, string codigoPregunta, ObjectParameter mediana)
        {
            var codigoFormularioParameter = codigoFormulario != null ?
                new ObjectParameter("codigoFormulario", codigoFormulario) :
                new ObjectParameter("codigoFormulario", typeof(string));
    
            var siglaCursoParameter = siglaCurso != null ?
                new ObjectParameter("siglaCurso", siglaCurso) :
                new ObjectParameter("siglaCurso", typeof(string));
    
            var numeroGrupoParameter = numeroGrupo.HasValue ?
                new ObjectParameter("numeroGrupo", numeroGrupo) :
                new ObjectParameter("numeroGrupo", typeof(byte));
    
            var anioParameter = anio.HasValue ?
                new ObjectParameter("anio", anio) :
                new ObjectParameter("anio", typeof(int));
    
            var semestreParameter = semestre.HasValue ?
                new ObjectParameter("semestre", semestre) :
                new ObjectParameter("semestre", typeof(byte));
    
            var codigoPreguntaParameter = codigoPregunta != null ?
                new ObjectParameter("codigoPregunta", codigoPregunta) :
                new ObjectParameter("codigoPregunta", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Mediana", codigoFormularioParameter, siglaCursoParameter, numeroGrupoParameter, anioParameter, semestreParameter, codigoPreguntaParameter, mediana);
        }
    
        public virtual int ModificarCorreo(string anterior, string nuevo, ObjectParameter resultado)
        {
            var anteriorParameter = anterior != null ?
                new ObjectParameter("anterior", anterior) :
                new ObjectParameter("anterior", typeof(string));
    
            var nuevoParameter = nuevo != null ?
                new ObjectParameter("nuevo", nuevo) :
                new ObjectParameter("nuevo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ModificarCorreo", anteriorParameter, nuevoParameter, resultado);
        }
    
        public virtual ObjectResult<ObtenerOpcionesDePregunta_Result> ObtenerOpcionesDePregunta(string questionCode)
        {
            var questionCodeParameter = questionCode != null ?
                new ObjectParameter("questionCode", questionCode) :
                new ObjectParameter("questionCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerOpcionesDePregunta_Result>("ObtenerOpcionesDePregunta", questionCodeParameter);
        }
    
        public virtual ObjectResult<string> ObtenerPreguntasDeSeccion(string sectionCode)
        {
            var sectionCodeParameter = sectionCode != null ?
                new ObjectParameter("sectionCode", sectionCode) :
                new ObjectParameter("sectionCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ObtenerPreguntasDeSeccion", sectionCodeParameter);
        }
    
        public virtual ObjectResult<ObtenerSeccionesDeFormulario_Result> ObtenerSeccionesDeFormulario(string codForm)
        {
            var codFormParameter = codForm != null ?
                new ObjectParameter("CodForm", codForm) :
                new ObjectParameter("CodForm", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerSeccionesDeFormulario_Result>("ObtenerSeccionesDeFormulario", codFormParameter);
        }
    
        public virtual int PopularFormulariosConSecciones()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PopularFormulariosConSecciones");
        }
    
        public virtual int PopularSeccionesConPreguntas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PopularSeccionesConPreguntas");
        }
    
        public virtual int PromedioRespuestasPreguntaEscalaNumerica(string codigoFormulario, string siglaCurso, Nullable<byte> numeroGrupo, Nullable<int> anno, Nullable<byte> semestre, string codigoPregunta, ObjectParameter promedio)
        {
            var codigoFormularioParameter = codigoFormulario != null ?
                new ObjectParameter("codigoFormulario", codigoFormulario) :
                new ObjectParameter("codigoFormulario", typeof(string));
    
            var siglaCursoParameter = siglaCurso != null ?
                new ObjectParameter("siglaCurso", siglaCurso) :
                new ObjectParameter("siglaCurso", typeof(string));
    
            var numeroGrupoParameter = numeroGrupo.HasValue ?
                new ObjectParameter("numeroGrupo", numeroGrupo) :
                new ObjectParameter("numeroGrupo", typeof(byte));
    
            var annoParameter = anno.HasValue ?
                new ObjectParameter("anno", anno) :
                new ObjectParameter("anno", typeof(int));
    
            var semestreParameter = semestre.HasValue ?
                new ObjectParameter("semestre", semestre) :
                new ObjectParameter("semestre", typeof(byte));
    
            var codigoPreguntaParameter = codigoPregunta != null ?
                new ObjectParameter("codigoPregunta", codigoPregunta) :
                new ObjectParameter("codigoPregunta", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PromedioRespuestasPreguntaEscalaNumerica", codigoFormularioParameter, siglaCursoParameter, numeroGrupoParameter, annoParameter, semestreParameter, codigoPreguntaParameter, promedio);
        }
    
        [DbFunction("Entities", "CarrerasXPerfilXUsuario")]
        public virtual IQueryable<string> CarrerasXPerfilXUsuario(string correoUsuario, string param2)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            var param2Parameter = param2 != null ?
                new ObjectParameter("param2", param2) :
                new ObjectParameter("param2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[Entities].[CarrerasXPerfilXUsuario](@correoUsuario, @param2)", correoUsuarioParameter, param2Parameter);
        }
    
        public virtual int TienePermiso(string correoUsuario, string perfil, string codCarrera, string codEnfasis, Nullable<int> permiso, ObjectParameter resultado)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            var perfilParameter = perfil != null ?
                new ObjectParameter("perfil", perfil) :
                new ObjectParameter("perfil", typeof(string));
    
            var codCarreraParameter = codCarrera != null ?
                new ObjectParameter("codCarrera", codCarrera) :
                new ObjectParameter("codCarrera", typeof(string));
    
            var codEnfasisParameter = codEnfasis != null ?
                new ObjectParameter("codEnfasis", codEnfasis) :
                new ObjectParameter("codEnfasis", typeof(string));
    
            var permisoParameter = permiso.HasValue ?
                new ObjectParameter("permiso", permiso) :
                new ObjectParameter("permiso", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TienePermiso", correoUsuarioParameter, perfilParameter, codCarreraParameter, codEnfasisParameter, permisoParameter, resultado);
        }
    
        public virtual int TienePermisoSinEnfasis(string correoUsuario, string perfil, string codCarrera, Nullable<int> permiso, ObjectParameter resultado)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            var perfilParameter = perfil != null ?
                new ObjectParameter("perfil", perfil) :
                new ObjectParameter("perfil", typeof(string));
    
            var codCarreraParameter = codCarrera != null ?
                new ObjectParameter("codCarrera", codCarrera) :
                new ObjectParameter("codCarrera", typeof(string));
    
            var permisoParameter = permiso.HasValue ?
                new ObjectParameter("permiso", permiso) :
                new ObjectParameter("permiso", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TienePermisoSinEnfasis", correoUsuarioParameter, perfilParameter, codCarreraParameter, permisoParameter, resultado);
        }
    
        public virtual int TienePermisoSinEnfasisNiCarrera(string correoUsuario, string perfil, Nullable<int> permiso, ObjectParameter resultado)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("correoUsuario", correoUsuario) :
                new ObjectParameter("correoUsuario", typeof(string));
    
            var perfilParameter = perfil != null ?
                new ObjectParameter("perfil", perfil) :
                new ObjectParameter("perfil", typeof(string));
    
            var permisoParameter = permiso.HasValue ?
                new ObjectParameter("permiso", permiso) :
                new ObjectParameter("permiso", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TienePermisoSinEnfasisNiCarrera", correoUsuarioParameter, perfilParameter, permisoParameter, resultado);
        }
    
        [DbFunction("Entities", "EnfasisXCarrera")]
        public virtual IQueryable<string> EnfasisXCarrera(string codCarrera)
        {
            var codCarreraParameter = codCarrera != null ?
                new ObjectParameter("codCarrera", codCarrera) :
                new ObjectParameter("codCarrera", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<string>("[Entities].[EnfasisXCarrera](@codCarrera)", codCarreraParameter);
        }
    
        public virtual int SugerirConfiguracion(string correoUsuario, ObjectParameter perfilPoderoso, ObjectParameter carreraPoderosa, ObjectParameter enfasisPoderoso)
        {
            var correoUsuarioParameter = correoUsuario != null ?
                new ObjectParameter("CorreoUsuario", correoUsuario) :
                new ObjectParameter("CorreoUsuario", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SugerirConfiguracion", correoUsuarioParameter, perfilPoderoso, carreraPoderosa, enfasisPoderoso);
        }
    }
}
