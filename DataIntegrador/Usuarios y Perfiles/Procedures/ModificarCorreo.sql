CREATE PROCEDURE [dbo].[ModificarCorreo]
	@anterior varchar(50),
	@nuevo varchar(50),
	@resultado bit output
AS
	-- Cambiar correo si no existe ya en la base
	IF NOT EXISTS (SELECT TOP 1 Correo FROM [dbo].[Persona] WHERE Correo=@nuevo)
	BEGIN
		UPDATE Persona
		SET Correo = @nuevo
		WHERE Correo = @anterior

		SET @resultado = 1;
	END

	ELSE
		SET @resultado = 0;
RETURN 0
