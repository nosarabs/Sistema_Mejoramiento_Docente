CREATE PROCEDURE [dbo].[InsertarImparte]
	@CorreoProfesor VARCHAR(50),
	@SiglaCurso VARCHAR(10),
	@NumGrupo TINYINT,
	@Semestre TINYINT,
	@Anno INT
AS
BEGIN
	insert into Imparte (CorreoProfesor, SiglaCurso, NumGrupo, Semestre,Anno)
	VALUES ( @CorreoProfesor, @SiglaCurso, @NumGrupo, @Semestre, @Anno)
END