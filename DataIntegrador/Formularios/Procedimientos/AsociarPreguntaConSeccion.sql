CREATE PROCEDURE [dbo].[AsociarPreguntaConSeccion]
	@CodigoSeccion char(8),
	@CodigoPregunta char(8),
	@Orden int
AS
	INSERT INTO Seccion_tiene_pregunta
	VALUES (@CodigoSeccion, @CodigoPregunta, @Orden)
RETURN 0
