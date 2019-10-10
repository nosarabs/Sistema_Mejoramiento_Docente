CREATE PROCEDURE [dbo].[PromedioRespuestasPreguntaEscalaNumerica]
	(@codigoFormulario CHAR(8),
	@siglaCurso VARCHAR(50),
	@numeroGrupo TINYINT,
	@anno INT,
	@semestre TINYINT,
	@codigoPregunta CHAR(8),
	@promedio FLOAT OUTPUT)
AS
	BEGIN
		SELECT @promedio =	AVG(CAST (O.OpcionSeleccionada AS FLOAT))
		FROM Opciones_seleccionadas_respuesta_con_opciones AS O
		WHERE O.FCodigo = @codigoFormulario
			AND O.CSigla = @siglaCurso
			AND O.GNumero = @numeroGrupo
			AND O.GAnno = @anno
			AND O.GSemestre = @semestre
			AND O.PCodigo = @codigoPregunta
	END
RETURN 0
