/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios que pertenecen a un curso.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosCurso (@siglaCurso AS VARCHAR(10))
RETURNS @formulariosCurso TABLE
(
	FCodigo VARCHAR(8),	/*Código del formulario.*/
	CSigla VARCHAR(10),	/*Sigla del curso.*/
	GNumero TINYINT,	/*Número de grupo.*/
	GSemestre TINYINT,	/*Número de semestre.*/
	GAnno INT,			/*Año.*/
	FechaInicio DATE,	/*Fecha de inicio del periodo de llenado el formulario.*/
	FechaFin DATE		/*Fecha de finalización del periodo de llenado del formulario.*/
)
AS
BEGIN
	
	/*Si la sigla del curso no es nula, verifica si es válida y recupera la información de los formularios.*/
	IF (@siglaCurso IS NOT NULL)
	BEGIN

		/*Si la sigla del curso es válida, recupera la información de los formularios.*/
		IF (EXISTS (SELECT * FROM Curso WHERE Sigla = @siglaCurso))
		BEGIN

			INSERT INTO @formulariosCurso
			SELECT	PAP.FCodigo, PAP.CSigla, PAP.GNumero, PAP.GSemestre, PAP.GAnno, PAP.FechaInicio, PAP.FechaFin
			FROM	Periodo_activa_por AS PAP
			WHERE	PAP.FechaFin < CONVERT (DATE, GETDATE()) /*Solo de formularios cuyo periodo de llenado haya finalizado.*/
					AND PAP.CSigla = @siglaCurso;			 /*Solo de formularios que pertenecen al curso.*/

		END;

	END;

	RETURN;

END;
