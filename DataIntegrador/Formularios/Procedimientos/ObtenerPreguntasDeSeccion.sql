CREATE PROCEDURE [dbo].[ObtenerPreguntasDeSeccion]
	@sectionCode varchar(8)
AS
	SELECT p.Enunciado, st.Orden
	FROM Pregunta p JOIN Seccion_tiene_pregunta st ON p.Codigo = st.PCodigo 
					JOIN Seccion s ON st.SCodigo = s.Codigo
	WHERE s.Codigo = @sectionCode
	ORDER BY st.Orden ASC
GO;
