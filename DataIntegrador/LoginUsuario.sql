CREATE FUNCTION loginUsuario
	(@pLoginName VARCHAR(50),@pPassword VARCHAR(50))
RETURNS BIT
BEGIN

	DECLARE @userID INT, @result BIT

	IF EXISTS (SELECT TOP 1 Username FROM [dbo].[Usuario] WHERE Username=@pLoginName)
	BEGIN
		SET @userID=(SELECT Username FROM [dbo].[Usuario] 
		WHERE Username=@pLoginName AND Password=HASHBYTES('SHA2_256',
		@pPassword+CAST(Salt AS VARCHAR(36))))

		IF(@userID IS NULL)
			SET @result=0

		ELSE
			SET @result=1
	END

	ELSE
		SET @result=0
	RETURN @result
END;
