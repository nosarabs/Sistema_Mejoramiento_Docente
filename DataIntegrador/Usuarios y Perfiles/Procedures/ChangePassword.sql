--TAM-1.5 - Password Reset

CREATE PROCEDURE [dbo].[ChangePassword]
	@username VARCHAR(50),
	@newpassword VARCHAR(64)
AS
	DECLARE @salt VARCHAR(64)

	SELECT @salt=Salt
	FROM Usuario
	WHERE Username=@username

	UPDATE Usuario
	SET Password = HASHBYTES('SHA2_256', @newpassword+CAST(@salt AS VARCHAR(36)))
	Where username=@username

RETURN 0
