/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios que pertenecen a un profesor.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosProfesor (@correoProfesor AS VARCHAR(50))
RETURNS @formulariosProfesor TABLE
(
	FCodigo CHAR(8),	/*Código del formulario.*/
	CSigla VARCHAR(10),	/*Sigla del curso.*/
	GNumero TINYINT,	/*Número de grupo.*/
	GSemestre TINYINT,	/*Número de semestre.*/
	GAnno INT,			/*Año.*/
	FechaInicio DATE,	/*Fecha de inicio del periodo de llenado el formulario.*/
	FechaFin DATE		/*Fecha de finalización del periodo de llenado del formulario.*/
)
AS
BEGIN
	
	/*Si el correo del profesor no es nulo, verifica si es válido y recupera la información de los formularios.*/
	IF (@correoProfesor IS NOT NULL)
	BEGIN

		/*Si el correo del profesor es válido, recupera la información de los formularios.*/
		IF (EXISTS (SELECT * FROM Profesor WHERE Correo = @correoProfesor))
		BEGIN

			INSERT INTO @formulariosProfesor
			SELECT	PAP.FCodigo, PAP.CSigla, PAP.GNumero, PAP.GSemestre, PAP.GAnno, PAP.FechaInicio, PAP.FechaFin
			FROM	Periodo_activa_por AS PAP
			WHERE	PAP.FechaFin < CONVERT (DATE, GETDATE()) /*Solo de formularios cuyo periodo de llenado haya finalizado.*/
					AND EXISTS	(
									SELECT *
									FROM Imparte AS I
									WHERE I.CorreoProfesor = @correoProfesor /*Solo de formularios activados para el profesor con el correo parámetro de cierto grupo.*/
									AND I.SiglaCurso = PAP.CSigla
									AND I.NumGrupo = PAP.GNumero
									AND I.Semestre = PAP.GSemestre
									AND I.Anno = PAP.GAnno
								);

		END;

	END;

	RETURN;

END;