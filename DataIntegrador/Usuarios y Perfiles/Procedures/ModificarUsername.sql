CREATE PROCEDURE [dbo].[ModificarUsername]
	@anterior varchar(50),
	@nuevo varchar(50)
AS
	UPDATE Usuario
	SET Username = @nuevo
	WHERE Username = @anterior

	UPDATE Persona
	SET Usuario = @nuevo
	WHERE Usuario = @anterior
RETURN 0