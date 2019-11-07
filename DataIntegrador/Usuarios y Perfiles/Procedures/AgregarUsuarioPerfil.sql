/*TAM 3.4 Task 1 Procedimiento para asignar y desasignar un perfil a un usuario*/
CREATE PROCEDURE [dbo].[AgregarUsuarioPerfil]
	@usuario VARCHAR(50),
	@perfil VARCHAR(50),
	@codCarrera	VARCHAR(10),
	@codEnfasis VARCHAR(10),
	@tienePerfil bit
AS
BEGIN
	-- Se verifica si se debe asignar o desasignar el perfil
	IF (@tienePerfil = 1)
	BEGIN
		-- Si no existe la asignacion del perfil al usuario entonces se asigna
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
		-- Se desasigna el perfil al usuario
		DELETE
		FROM UsuarioPerfil
		WHERE Usuario = @usuario AND Perfil = @perfil 
		      AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
	END
END
