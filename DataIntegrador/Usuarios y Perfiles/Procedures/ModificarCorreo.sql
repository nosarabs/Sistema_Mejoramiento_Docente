CREATE PROCEDURE [dbo].[ModificarCorreo]
	@anterior varchar(50),
	@nuevo varchar(50),
	@resultado bit output
AS
	-- Cambiar nombre de usuario si no existe ya ese nombre
	IF NOT EXISTS (SELECT TOP 1 Username FROM [dbo].[Usuario] WHERE Username=@nuevo)
	BEGIN
		UPDATE Usuario
		SET Username = @nuevo
		WHERE Username = @anterior

		SET @resultado = 1;
	END

	ELSE
		SET @resultado = 0;
RETURN 0
