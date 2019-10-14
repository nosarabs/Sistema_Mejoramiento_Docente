CREATE PROCEDURE [dbo].[ObtenerEmailUsuario]
	@pUsername varchar(50),
	@email varchar(50) output
AS
BEGIN
	SELECT @email = p.Correo
	FROM Usuario u join Persona p on u.Username = p.Usuario
	WHERE u.Username = @pUsername
END