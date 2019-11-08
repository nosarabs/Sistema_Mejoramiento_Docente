CREATE PROCEDURE [dbo].[InsertarGrupoCSV]
	@SiglaCurso varchar(10),
	@NumGrupo tinyint,
	@Semestre tinyint,
	@Anno int
AS
BEGIN
	INSERT INTO Grupo(SiglaCurso, NumGrupo, Semestre, Anno)
	VALUES (@SiglaCurso, @NumGrupo, @Semestre, @Anno)
END

