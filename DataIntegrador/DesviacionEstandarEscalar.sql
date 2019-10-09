CREATE PROCEDURE [dbo].[DesviacionEstandarEscalar]
(
	@FCod CHAR(8),
	@CSigla VARCHAR(50),
	@Grupo TINYINT,
	@GAnno INT,
	@GSem TINYINT,
	@PCod CHAR(8),
	@Desviacion Float output
)
AS
BEGIN
	SELECT @Desviacion = STDEV(CAST(OpcionSeleccionada AS FLOAT))
	FROM Opciones_seleccionadas_respuesta_con_opciones
			 WHERE FCodigo	= @FCod 
			 AND CSigla		= @CSigla
			 AND GNumero	= @GRUPO
			 AND GAnno		= @GAnno
			 AND GSemestre	= @GSem
			 AND PCodigo	= @PCod
END
RETURN 0
