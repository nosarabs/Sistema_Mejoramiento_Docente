--Id historia de usuario: 7
--Tareas técnicas: 1. Tomar información de respuestas de la base de datos
				 --2. escoger como hacer los cálculos (nivel app o almacenado)
                 --3. escoger forma de calcular la mediana (ya que no existe una funcion de sql server para hacerlo)
CREATE FUNCTION [dbo].[Mediana]
(
	@codigoFormulario char(8),
	@siglaCurso varchar(10),
	@numeroGrupo tinyint,
	@anio int,
	@semestre tinyint,
	@codigoPregunta char(8),
	@seleccion tinyint
)
RETURNS INT
AS
BEGIN
	DECLARE @mediana INT
	SELECT o.FCodigo,
		PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY o.OpcionSeleccionada)
			OVER (PARTITION BY o.FCodigo) AS Mediana
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
