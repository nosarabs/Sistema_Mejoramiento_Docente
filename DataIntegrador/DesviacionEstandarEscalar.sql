CREATE FUNCTION [dbo].[DesviacionEstandarEscalar]
(
	@FCod CHAR(8),
	@CSigla VARCHAR(50),
	@Grupo TINYINT,
	@GAnno INT,
	@GSem TINYINT,
	@PCod CHAR(8)
)
RETURNS FLOAT
AS
BEGIN
	DECLARE @DEV AS FLOAT
	SELECT @DEV = STDEV(CAST(OpcionSeleccionada AS FLOAT)) FROM Opciones_seleccionadas_respuesta_con_opciones
			 WHERE FCodigo	= @FCod 
			 AND CSigla		= @CSigla
			 AND GNumero	= @GRUPO
			 AND GAnno		= @GAnno
			 AND GSemestre	= @GSem
			 AND PCodigo	= @PCod
	RETURN @DEV
END
