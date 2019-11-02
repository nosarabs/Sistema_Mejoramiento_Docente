/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios que pertenecen a una Unidad Académica.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosUA (@codigoUA AS VARCHAR(10))
RETURNS @formulariosUA TABLE
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
	
	/*Si el código de la unidad académica no es nulo, verifica si es válido y recupera la información de los formularios.*/
	IF (@codigoUA IS NOT NULL)
	BEGIN

		/*Si el código de la unidad académica es válido, recupera la información de los formularios.*/
		IF (EXISTS (SELECT * FROM UnidadAcademica WHERE Codigo = @codigoUA))
		BEGIN

			INSERT INTO @formulariosUA
			SELECT	PAP.FCodigo, PAP.CSigla, PAP.GNumero, PAP.GSemestre, PAP.GAnno, PAP.FechaInicio, PAP.FechaFin
			FROM	UnidadAcademica AS U
					JOIN Inscrita_en AS I ON U.Codigo = I.CodUnidadAc
					JOIN Pertenece_a AS PE ON I.CodCarrera = PE.CodCarrera
					JOIN Periodo_activa_por AS PAP ON PE.SiglaCurso = PAP.CSigla
			WHERE PAP.FechaFin < CONVERT (DATE, GETDATE()); /*Solo de formularios cuyo periodo de llenado haya finalizado.*/

		END;

	END;

	RETURN;

END;
