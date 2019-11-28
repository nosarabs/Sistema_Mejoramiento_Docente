﻿CREATE PROCEDURE [dbo].[AgregarEnlaceSeguro]
	@usuarioAsociado  VARCHAR(50), 
	@urlReal varchar(1000),
	@expira datetime,
    @resultadohash varchar(64) OUTPUT,
	@estado nvarchar(255) OUTPUT
AS
BEGIN TRY
BEGIN TRANSACTION	
BEGIN
	--Se crea un hash basándonos en la dirección provista y un Salt que creamos.
	--diferentes IF que siguen a continuación manejan diferentes permutaciones de parámetros
    SET NOCOUNT ON	
	DECLARE @salt UNIQUEIDENTIFIER=NEWID()
	SET @resultadohash = CONVERT (VARCHAR(64), HASHBYTES('SHA2_256', @urlReal+CAST(@salt AS VARCHAR(36))), 2)
	--No se provee usuario ni fecha de expiración (se usa el default).
	IF @usuarioAsociado IS NULL AND @expira IS NULL
	
	BEGIN
		INSERT INTO EnlaceSeguro ([Hash], UrlReal)
		values (@resultadohash, @urlReal)
		SET @estado = 1
	END
	ELSE
		BEGIN
		--Solo se provee usuario.
		IF  @usuarioAsociado IS NOT NULL AND @expira IS NULL
			BEGIN
				INSERT INTO EnlaceSeguro ([Hash],UsuarioAsociado, UrlReal)
				values (@resultadohash, @usuarioAsociado, @urlReal)
				SET @estado = 1
			End
		ELSE
			BEGIN
			--Solo se provee fecha de expiración.
			IF @usuarioAsociado IS NULL AND @expira IS NOT NULL
				BEGIN
					INSERT INTO EnlaceSeguro ([Hash],UrlReal,Expira)
					values (@resultadohash, @urlReal, @expira)
					SET @estado = 1
				End
			ELSE
				BEGIN
				--Se proveen todos los parámetros opcionales.
				IF @usuarioAsociado IS NOT NULL AND @expira IS NOT NULL
					BEGIN
						INSERT INTO EnlaceSeguro ([Hash],UsuarioAsociado,UrlReal,Expira)
						values (@resultadohash,@usuarioAsociado, @urlReal, @expira)
						SET @estado = 1
					END
				END
			END
		END
END
COMMIT TRANSACTION
END TRY
BEGIN CATCH
    SET @estado=ERROR_MESSAGE()
	ROLLBACK TRANSACTION
END CATCH