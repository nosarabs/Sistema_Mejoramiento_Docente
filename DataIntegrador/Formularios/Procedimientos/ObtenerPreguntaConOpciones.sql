CREATE PROCEDURE [dbo].[ObtenerPreguntaConOpciones]
	@cod
AS
BEGIN
	SELECT op.Texto, op.Orden
	FROM Opciones_de_seleccion op
	WHERE 
END
