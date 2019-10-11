CREATE PROCEDURE [dbo].[ObtenerPreguntasDeSeccion]
	@sectionCode varchar(8)
AS
BEGIN
	SELECT sp.PCodigo
	FROM Seccion_tiene_pregunta sp
	WHERE sp.SCodigo = @sectionCode;
END;
