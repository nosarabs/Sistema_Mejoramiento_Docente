CREATE PROCEDURE [dbo].[AgregarUsuarioPerfil]
	@usuario VARCHAR(50),
	@perfil VARCHAR(50),
	@codCarrera	VARCHAR(10),
	@codEnfasis VARCHAR(10),
	@tienePerfil bit
AS
BEGIN
	IF (@tienePerfil = 1)
	BEGIN
		IF NOT EXISTS (SELECT * 
					   FROM UsuarioPerfil 
					   WHERE Usuario = @usuario AND Perfil = @perfil 
					         AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
					   )
			BEGIN
				INSERT INTO UsuarioPerfil (Usuario, Perfil, CodCarrera, CodEnfasis)
				VALUES (@usuario, @perfil, @codCarrera, @codEnfasis)
			END
	END
	ELSE
	BEGIN
		DELETE
		FROM UsuarioPerfil
		WHERE Usuario = @usuario AND Perfil = @perfil 
		      AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
	END
END
