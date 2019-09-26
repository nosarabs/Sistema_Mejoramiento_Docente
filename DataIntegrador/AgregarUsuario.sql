CREATE PROCEDURE [dbo].[AgregarUsuario]
	@pLogin VARCHAR(50), 
    @pPassword VARCHAR(50),
    @estado bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[Usuario] (Username, Password, Salt)
        VALUES(@pLogin, HASHBYTES('SHA2_256', @pPassword+CAST(@salt AS VARCHAR(36))), @salt)

       SET @estado = 1

    END TRY
    BEGIN CATCH
        SET @estado=ERROR_MESSAGE() 
    END CATCH

END
