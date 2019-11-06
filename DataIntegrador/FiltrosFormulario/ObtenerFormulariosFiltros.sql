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

	/*Declara que hay un primer filtro.*/
	DECLARE @primero BIT = 1;

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
	
	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@codigoUA IS NOT NULL)
	BEGIN

		SET @primero = 0;

		INSERT INTO @formulariosFiltros
		SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
		FROM ObtenerFormulariosUA(@codigoUA);

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@codigoCarrera IS NOT NULL AND @codigoEnfasis IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Le indica a los otros que ya hubo un primer filtro.*/
			SET @primero = 0;

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosCarreraEnfasis(@codigoCarrera, @codigoEnfasis);

		END
		ELSE BEGIN

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

	END
	ELSE
	BEGIN

		/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
		IF (@codigoCarrera IS NOT NULL)
		BEGIN

			/*Si es el primer filtro.*/
			IF (@primero = 1)
			BEGIN

				/*Le indica a los otros que ya hubo un primer filtro.*/
				SET @primero = 0;

				/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
				INSERT INTO @formulariosFiltros
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosCarreraEnfasis(@codigoCarrera, null);

			END
			ELSE BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosCarreraEnfasis(@codigoCarrera, null)
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

		END;

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@siglaCurso IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Le indica a los otros que ya hubo un primer filtro.*/
			SET @primero = 0;

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosCurso(@siglaCurso);

		END
		ELSE BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosCurso(@siglaCurso)
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

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@numeroGrupo IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Le indica a los otros que ya hubo un primer filtro.*/
			SET @primero = 0;

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosGrupo(@numeroGrupo);

		END
		ELSE BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosGrupo(@numeroGrupo)
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

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@semestre IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Le indica a los otros que ya hubo un primer filtro.*/
			SET @primero = 0;

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosSemestre(@semestre);

		END
		ELSE BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosSemestre(@semestre)
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

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@anno IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Le indica a los otros que ya hubo un primer filtro.*/
			SET @primero = 0;

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosAnno(@anno);

		END
		ELSE BEGIN

				/*Llena formulariosTemp con la intersección de los resultados del nuevo filtro y los resultados de formulariosFiltros.*/
				INSERT INTO @formulariosTemp
				SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
				FROM ObtenerFormulariosAnno(@anno)
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

	END;

	/*Si el parámetro no es nulo, se toma en cuenta para el filtro.*/
	IF (@correoProfesor IS NOT NULL)
	BEGIN

		/*Si es el primer filtro.*/
		IF (@primero = 1)
		BEGIN

			/*Hace el llenado inicial de formulariosFiltros según el parámetro que corresponde.*/
			INSERT INTO @formulariosFiltros
			SELECT FCodigo, CSigla, GNumero, GSemestre, GAnno, FechaInicio, FechaFin
			FROM ObtenerFormulariosProfesor(@correoProfesor);

		END
		ELSE BEGIN

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

	END;

	RETURN;

END;
