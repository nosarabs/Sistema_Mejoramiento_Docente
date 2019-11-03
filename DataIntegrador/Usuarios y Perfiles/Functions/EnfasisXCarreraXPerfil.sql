CREATE FUNCTION [dbo].[EnfasisXCarreraXPerfil]
(
	@correoUsuario VARCHAR(50),
	@codCarrera VARCHAR(10),
	@nombrePerfil VARCHAR(50)
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
	WHERE UP.Usuario = @correoUsuario AND UP.CodCarrera = @codCarrera AND UP.Perfil = @nombrePerfil
	RETURN
END
