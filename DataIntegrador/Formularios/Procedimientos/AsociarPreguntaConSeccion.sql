CREATE PROCEDURE [dbo].[AsociarPreguntaConSeccion]
	@CodigoSeccion varchar(8),
	@CodigoPregunta varchar(8),
	@Orden int
AS

	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES (@CodigoSeccion, @CodigoPregunta, @Orden);
RETURN 0
