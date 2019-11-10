CREATE PROCEDURE [dbo].[EliminarPreguntaDeSeccion]
	@SCodigo VARCHAR(8),
	@PCodigo VARCHAR(8)
AS
BEGIN
	DELETE FROM Seccion_tiene_pregunta
	FROM Seccion_tiene_pregunta s
	WHERE s.SCodigo = @SCodigo AND s.PCodigo = @PCodigo
END
