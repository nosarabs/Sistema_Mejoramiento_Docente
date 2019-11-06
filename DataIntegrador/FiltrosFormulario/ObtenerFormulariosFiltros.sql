/*
Retorna una tabla con el código de formulario, sigla del curso, número de grupo, semestre, año, fecha de inicio, fecha de finalización de todos los formularios según los filtros de búsqueda.
Esta información se necesita para filtrar respuestas a formulario.
*/
CREATE FUNCTION ObtenerFormulariosFiltros (
@codigoUA AS VARCHAR(10),
@codigoCarrera AS VARCHAR(10),
@codigoEnfasis AS VARCHAR(10),
@siglaCurso AS VARCHAR(10),
@numeroGrupo AS TINYINT,
@semestre AS TINYINT,
@anno AS INT,
@correoProfesor AS VARCHAR(50)
)
RETURNS @formulariosFiltros TABLE
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

	/*Almacena resultados de forma temporal para poder hacer las intersecciones.*/
	DECLARE @formulariosTemp TABLE
	(
		FCodigo VARCHAR(8),	/*Código del formulario.*/
		CSigla VARCHAR(10),	/*Sigla del curso.*/
		GNumero TINYINT,	/*Número de grupo.*/
		GSemestre TINYINT,	/*Número de semestre.*/
		GAnno INT,			/*Año.*/
		FechaInicio DATE,	/*Fecha de inicio del periodo de llenado el formulario.*/
		FechaFin DATE		/*Fecha de finalización del periodo de llenado del formulario.*/
	);

	INSERT INTO @formulariosFiltros SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin FROM Periodo_activa_por;
	
	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@codigoUA IS NOT NULL)
	BEGIN

		/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
			INSERT INTO @formulariosTemp
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosUA(@codigoUA)
			INTERSECT
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM @formulariosFiltros;

			/*Limpia las variables y actualiza formulariosFiltros con el nuevo resultado.*/
			DELETE FROM @formulariosFiltros;

			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM @formulariosTemp;

			DELETE FROM @formulariosTemp;

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@codigoCarrera IS NOT NULL AND @codigoEnfasis IS NOT NULL)
	BEGIN

			/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
			INSERT INTO @formulariosTemp
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosCarreraEnfasis(@codigoCarrera, @codigoEnfasis)
			INTERSECT
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM @formulariosFiltros;

			/*Limpia las variables y actualiza formulariosFiltros con el nuevo resultado.*/
			DELETE FROM @formulariosFiltros;

			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM @formulariosTemp;

			DELETE FROM @formulariosTemp;

	END;

	/*Si los parámetros no son nulos, se toman en cuenta para el filtro.*/
	IF (@siglaCurso IS NOT NULL AND @numeroGrupo IS NOT NULL AND @semestre IS NOT NULL AND @anno IS NOT NULL)
	BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosGrupo(@siglaCurso, @numeroGrupo, @semestre, @anno)
				INTERSECT
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM @formulariosFiltros;

				/*Limpia las variables y actualiza formulariosFiltros con el nuevo resultado.*/
				DELETE FROM @formulariosFiltros;

				INSERT INTO @formulariosFiltros
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM @formulariosTemp;

				DELETE FROM @formulariosTemp;

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@correoProfesor IS NOT NULL)
	BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosProfesor(@correoProfesor)
				INTERSECT
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM @formulariosFiltros;

				/*Limpia las variables y actualiza formulariosFiltros con el nuevo resultado.*/
				DELETE FROM @formulariosFiltros;

				INSERT INTO @formulariosFiltros
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM @formulariosTemp;

				DELETE FROM @formulariosTemp;

	END;

	RETURN;

END;
