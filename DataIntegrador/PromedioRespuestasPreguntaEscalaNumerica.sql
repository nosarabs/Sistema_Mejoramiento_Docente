CREATE FUNCTION dbo.PromedioRespuestasPreguntaEscalaNumerica
(
	@codigoFormulario CHAR(8),
	@siglaCurso VARCHAR(50),
	@numeroGrupo TINYINT,
	@anno INT,
	@semestre TINYINT,
	@codigoPregunta CHAR(8)
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @promedio FLOAT;
	SELECT @promedio =	AVG(CAST (O.OpcionSeleccionada AS FLOAT))
		FROM Opciones_seleccionadas_respuesta_con_opciones AS O
		WHERE O.FCodigo = @codigoFormulario
			AND O.CSigla = @siglaCurso
			AND O.GNumero = @numeroGrupo
			AND O.GAnno = @anno
			AND O.GSemestre = @semestre
			AND O.PCodigo = @codigoPregunta
	RETURN @promedio;
END