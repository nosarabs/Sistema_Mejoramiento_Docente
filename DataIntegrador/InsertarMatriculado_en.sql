CREATE PROCEDURE [dbo].[InsertarMatriculado_en]
	@Correo varchar(50),
	@Sigla varchar(10),
	@NumGrupo tinyint,
	@Semestre tinyint,
	@Anno int
AS

BEGIN

	INSERT INTO Matriculado_en(CorreoEstudiante, SiglaCurso, NumGrupo, Semestre, Anno)
	VALUES (@Correo, @Sigla, @NumGrupo, @Semestre, @Anno)

END