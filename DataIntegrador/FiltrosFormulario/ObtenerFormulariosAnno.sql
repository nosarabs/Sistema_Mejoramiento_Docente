﻿/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios que pertenecen a un año de cierto número.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosAnno (@anno AS INT)
RETURNS @formulariosAnno TABLE
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
	
	/*Si el número de año no es nulo, verifica si es válido y recupera la información de los formularios.*/
	IF (@anno IS NOT NULL)
	BEGIN

		/*Si el número de año es válido, recupera la información de los formularios.*/
		IF (EXISTS (SELECT * FROM Grupo WHERE Anno = @anno))
		BEGIN

			INSERT INTO @formulariosAnno
			SELECT	PAP.FCodigo, PAP.CSigla, PAP.GNumero, PAP.GSemestre, PAP.GAnno, PAP.FechaInicio, PAP.FechaFin
			FROM	Periodo_activa_por AS PAP
			WHERE	PAP.FechaFin < CONVERT (DATE, GETDATE()) /*Solo de formularios cuyo periodo de llenado haya finalizado.*/
					AND PAP.GAnno = @anno;			 /*Solo de formularios que pertenecen al año cuyo número es el parámetro.*/

		END;

	END;

	RETURN;

END;