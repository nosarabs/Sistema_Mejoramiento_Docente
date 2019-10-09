CREATE FUNCTION [dbo].[Mediana]
(
	@codigoFormulario char(8),
	@siglaCurso varchar(10),
	@numeroGrupo tinyint,
	@anio int,
	@semestre tinyint,
	@codigoPregunta char(8)
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @mediana FLOAT
	SELECT @mediana =
		CAST (PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY o.OpcionSeleccionada)
			OVER (PARTITION BY o.FCodigo) AS FLOAT)
	FROM Opciones_seleccionadas_respuesta_con_opciones o
	WHERE o.FCodigo = @codigoFormulario and
		  o.CSigla = @siglaCurso and
		  o.GNumero = @numeroGrupo and
		  o.GAnno = @anio and
		  o.GSemestre = @semestre and
		  o.PCodigo = @codigoPregunta
	ORDER BY o.FCodigo

	RETURN @mediana
END
