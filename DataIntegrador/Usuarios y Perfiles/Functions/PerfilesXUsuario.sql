CREATE FUNCTION [dbo].[PerfilesXUsuario]
(
	@correoUsuario VARCHAR(50)
)
RETURNS @returntable TABLE
(
	NombrePefil VARCHAR(50)
)
AS
BEGIN
	INSERT @returntable
	SELECT DISTINCT UP.Perfil from UsuarioPerfil as UP JOIN Usuario as U ON UP.Usuario = U.Username
	WHERE U.Username = @correoUsuario
	RETURN
END
