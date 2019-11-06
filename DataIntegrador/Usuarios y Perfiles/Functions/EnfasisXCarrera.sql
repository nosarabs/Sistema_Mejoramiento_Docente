/*TAM 3.1: Función almacenada que devuelve una lista de códigos de énfasis que tiene una carrera dada.*/
CREATE FUNCTION [dbo].[EnfasisXCarrera]
(
	@codCarrera VARCHAR(10)
)
RETURNS @returntable TABLE
(
	codEnfasis VARCHAR(10)
)
AS
BEGIN
	INSERT @returntable
	SELECT E.Codigo
	FROM Enfasis AS E JOIN Carrera AS C ON E.CodCarrera = C.Codigo
	WHERE C.Codigo = @codCarrera
	RETURN
END
