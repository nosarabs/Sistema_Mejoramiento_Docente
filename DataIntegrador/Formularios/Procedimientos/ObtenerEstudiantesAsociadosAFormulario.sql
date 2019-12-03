CREATE PROCEDURE [dbo].[ObtenerEstudiantesAsociadosAFormulario]
	@codFormulario varchar(8) 
AS
BEGIN 
	/*SELECT * FROM Estudiante e join Matriculado_en me on e.Correo = me.CorreoEstudiante
								join Curso c on c.Sigla = me.SiglaCurso
								join Imparte im on im.SiglaCurso = me.SiglaCurso 
								join Profesor p on im.CorreoProfesor = p.Correo
								join Activa_por ap on ap.CSigla = me.SiglaCurso
	WHERE @codFormulario = ap.FCodigo;*/
	SELECT * FROM Estudiante e;
END
