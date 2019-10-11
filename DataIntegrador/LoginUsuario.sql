CREATE PROCEDURE LoginUsuario
	(@pLoginName VARCHAR(50),
	 @pPassword VARCHAR(50),
	  @result INT OUTPUT)
AS
BEGIN

	DECLARE @userID VARCHAR(50)

	IF EXISTS (SELECT TOP 1 Username FROM [dbo].[Usuario] WHERE Username=@pLoginName)
	BEGIN
		SET @userID=(SELECT Username FROM [dbo].[Usuario] 
		WHERE Username=@pLoginName AND Password=HASHBYTES('SHA2_256',
		@pPassword+CAST(Salt AS VARCHAR(36))))

		IF(@userID IS NULL)
			SET @result=2

		ELSE
			SET @result=0
	END

	ELSE
		SET @result=1
END;
