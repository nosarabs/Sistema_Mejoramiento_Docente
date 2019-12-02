CREATE PROCEDURE [dbo].[InsertarGrupoCSV]
	@SiglaCurso varchar(10),
	@NumGrupo tinyint,
	@Semestre tinyint,
	@Anno int
AS
BEGIN
	--El curso debe existir antes de insertar un grupo
	IF(EXISTS (SELECT * FROM Grupo WHERE SiglaCurso = @SiglaCurso and NumGrupo = @NumGrupo and Semestre = @Semestre and Anno = @Anno))
	BEGIN
		INSERT INTO Grupo(SiglaCurso, NumGrupo, Semestre, Anno)
		VALUES (@SiglaCurso, @NumGrupo, @Semestre, @Anno)
	END
END

