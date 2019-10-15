CREATE PROCEDURE [dbo].[ModificarCorreo]
	@anterior varchar(50),
	@nuevo varchar(50)
AS
	UPDATE Persona 
	SET Correo = @nuevo
	WHERE Correo = @anterior
RETURN 0
