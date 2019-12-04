/**
Función almacenada que recibe unas serie de parámetros,
para filtrar por unidad academica, carrera-enfasis, profesor,
grupo; y sus respectivas combinaciones. Devuelve una tabla
que contiene todos grupos
*/
CREATE PROCEDURE [dbo].[ObtenerEstudiantesAsociados]
(
	@CodigoUnidadAcademica varchar(10),
	@CodigoCarrera	varchar(10),
	@CodigoEnfasis	varchar(10),
	@SiglaCurso		varchar(10),
	@NumGrupo		tinyint,
	@Semestre		tinyint,
	@Anno			int,
	@CorreoProfesor	varchar(50)
)
AS
SELECT * FROM Estudiante;
/*
	--Unidad academica
	IF (@CodigoUnidadAcademica IS NOT NULL AND @CodigoCarrera IS NULL AND @CodigoEnfasis IS NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND	@Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NULL)
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
									JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
									JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
									JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
									JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
									JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
			WHERE UnidadAcademica.Codigo = @CodigoUnidadAcademica

	-- Carrera-Enfasis
	IF (@CodigoUnidadAcademica IS NULL AND @CodigoCarrera IS NOT NULL AND @CodigoEnfasis IS NOT NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND	@Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NULL )
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON  UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
								 JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
								 JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
								 JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
								 JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
								 JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
			WHERE Carrera.Codigo = @CodigoCarrera AND Enfasis.Codigo = @CodigoEnfasis

	--Profesor 
	IF (@CodigoUnidadAcademica IS NULL AND @CodigoCarrera IS  NULL AND @CodigoEnfasis IS  NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND @Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NOT NULL)
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM Profesor JOIN Imparte ON Profesor.Correo = Imparte.CorreoProfesor
			JOIN Grupo ON Grupo.SiglaCurso = Imparte.SiglaCurso AND Grupo.NumGrupo = Imparte.NumGrupo AND Grupo.Semestre = Imparte.Semestre AND Grupo.Anno = Imparte.Anno
			WHERE Profesor.Correo = @CorreoProfesor

	--UA-Enfasis
	IF (@CodigoUnidadAcademica IS NOT NULL AND @CodigoCarrera IS NOT NULL AND @CodigoEnfasis IS NOT NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND	@Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NULL )
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON  UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
								 JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
								 JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
								 JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
								 JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
								 JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
			WHERE UnidadAcademica.Codigo = @CodigoUnidadAcademica AND Carrera.Codigo = @CodigoCarrera AND Enfasis.Codigo = @CodigoEnfasis

	-- UA-Profesor
	IF (@CodigoUnidadAcademica IS NOT NULL AND @CodigoCarrera IS  NULL AND @CodigoEnfasis IS  NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND	@Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NOT NULL)
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON  UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
								 JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
								 JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
								 JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
								 JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
								 JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
								 JOIN Imparte ON  Imparte.NumGrupo = Grupo.NumGrupo AND Imparte.Semestre = Grupo.Semestre AND Grupo.Anno = Imparte.Anno AND Imparte.SiglaCurso = Grupo.SiglaCurso
								 JOIN Profesor ON Profesor.Correo = Imparte.CorreoProfesor
			WHERE Profesor.Correo = @CorreoProfesor AND UnidadAcademica.Codigo =  @CodigoUnidadAcademica

	-- Carrera-Enfasis-Profesor
	IF  (@CodigoUnidadAcademica IS NULL AND @CodigoCarrera IS NOT NULL AND @CodigoEnfasis IS NOT NULL AND @SiglaCurso IS NULL AND @NumGrupo IS NULL AND	@Semestre IS NULL AND @Anno IS NULL AND @CorreoProfesor IS NOT NULL)
		
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON  UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
								 JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
								 JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
								 JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
								 JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
								 JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
								 JOIN Imparte ON  Imparte.NumGrupo = Grupo.NumGrupo AND Imparte.Semestre = Grupo.Semestre AND Grupo.Anno = Imparte.Anno AND Imparte.SiglaCurso = Grupo.SiglaCurso
								 JOIN Profesor ON Profesor.Correo = Imparte.CorreoProfesor
			WHERE Carrera.Codigo = @CodigoCarrera	AND Enfasis.Codigo = @CodigoEnfasis AND Profesor.Correo = @CorreoProfesor

	-- Todos los que vengan con un grupo
	IF ( @SiglaCurso IS NOT NULL AND @NumGrupo IS NOT NULL AND	@Semestre IS NOT NULL AND @Anno IS NOT NULL )
			SELECT Grupo.SiglaCurso, Grupo.NumGrupo, Grupo.Semestre, Grupo.Anno
			FROM UnidadAcademica JOIN Inscrita_en ON  UnidadAcademica.Codigo = Inscrita_en.CodUnidadAc 
								 JOIN Carrera ON Carrera.Codigo = Inscrita_en.CodCarrera 
								 JOIN Enfasis ON Enfasis.CodCarrera = Carrera.Codigo
								 JOIN Pertenece_a ON Pertenece_a.CodCarrera = Carrera.Codigo AND Pertenece_a.CodEnfasis = Enfasis.Codigo
								 JOIN Curso ON Curso.Sigla = Pertenece_a.SiglaCurso
								 JOIN Grupo ON Grupo.SiglaCurso = Curso.Sigla
			WHERE Grupo.Anno = @Anno AND Grupo.SiglaCurso = @SiglaCurso AND Grupo.Semestre = @Semestre
RETURN 0*/