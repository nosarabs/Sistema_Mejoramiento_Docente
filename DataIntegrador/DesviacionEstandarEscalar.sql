CREATE PROCEDURE [dbo].[DesviacionEstandarEscalar]
(
	@FCod VARCHAR(8),
	@CSigla VARCHAR(50),
	@Grupo TINYINT,
	@GAnno INT,
	@GSem TINYINT,
	@FechaInicio DATE,
	@FechaFin DATE,
	@SCod VARCHAR(8),
	@PCod VARCHAR(8),
	@Desviacion Float output
)
AS
BEGIN
	SELECT @Desviacion = STDEV(CAST(OpcionSeleccionada AS FLOAT))
	FROM Opciones_seleccionadas_respuesta_con_opciones
			 WHERE FCodigo	= @FCod 
			 AND CSigla		= @CSigla
			 AND GNumero	= @Grupo
			 AND GAnno		= @GAnno
			 AND GSemestre	= @GSem
			 AND SCodigo	= @SCod
			 AND PCodigo	= @PCod
			 AND Fecha >= @FechaInicio
			 AND Fecha <= @FechaFin
END
RETURN 0
