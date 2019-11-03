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
	SELECT DISTINCT E.Codigo
	FROM UsuarioPerfil AS UP JOIN Enfasis AS E ON UP.CodEnfasis = E.Codigo
	WHERE UP.CodCarrera = @codCarrera
	RETURN
END
