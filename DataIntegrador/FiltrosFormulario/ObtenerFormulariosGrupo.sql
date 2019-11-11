/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios que pertenecen a un grupo.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosGrupo (@siglaCurso AS VARCHAR(10), @numeroGrupo AS TINYINT, @semestre AS TINYINT, @anno AS INT)
RETURNS @formulariosGrupo TABLE
(
	FCodigo VARCHAR(8),		/*Código del formulario.*/
	FNombre NVARCHAR(250),	/*Nombre del formulario*/
	CSigla VARCHAR(10),		/*Sigla del curso.*/
	GNumero TINYINT,		/*Número de grupo.*/
	GSemestre TINYINT,		/*Número de semestre.*/
	GAnno INT,				/*Año.*/
	FechaInicio DATE,		/*Fecha de inicio del periodo de llenado el formulario.*/
	FechaFin DATE			/*Fecha de finalización del periodo de llenado del formulario.*/
)
AS
BEGIN
	
	/*Si los parámetros del grupo no son nulos, verifica si son válidos y recupera la información de los formularios.*/
	IF (@siglaCurso IS NOT NULL AND @numeroGrupo IS NOT NULL AND @semestre IS NOT NULL AND @anno IS NOT NULL)
	BEGIN

		/*Si los parámetros de grupo son válidos, recupera la información de los formularios.*/
		IF (EXISTS (SELECT * FROM Grupo WHERE SiglaCurso = @siglaCurso AND NumGrupo = @numeroGrupo AND Semestre = @semestre AND Anno = @anno))
		BEGIN

			INSERT INTO @formulariosGrupo
			SELECT	PAP.FCodigo, F.Nombre, PAP.CSigla, PAP.GNumero, PAP.GSemestre, PAP.GAnno, PAP.FechaInicio, PAP.FechaFin
			FROM	Periodo_activa_por AS PAP JOIN Formulario AS F ON PAP.FCodigo = F.Codigo
			WHERE	PAP.FechaFin < CONVERT (DATE, GETDATE()) /*Solo de formularios cuyo periodo de llenado haya finalizado.*/
					AND PAP.CSigla = @siglaCurso			/*Solo de formularios que pertenecen al grupo parámetro.*/
					AND PAP.GNumero = @numeroGrupo
					AND PAP.GSemestre = @semestre
					AND PAP.GAnno = @anno;
					

		END;

	END;

	RETURN;

END;