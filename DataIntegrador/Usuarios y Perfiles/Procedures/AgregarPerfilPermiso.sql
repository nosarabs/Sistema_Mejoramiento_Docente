CREATE PROCEDURE [dbo].[AgregarPerfilPermiso]
	@perfil VARCHAR(50),
	@idPermiso INT,
	@codCarrera VARCHAR(10),
	@codEnfasis VARCHAR(10),
	@tienePermiso bit
AS
BEGIN
	IF (@tienePermiso = 1)
	BEGIN
		IF NOT EXISTS (SELECT * 
					   FROM PerfilPermiso 
					   WHERE Perfil = @perfil AND PermisoId = @idPermiso 
					         AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
					   )
			BEGIN
				INSERT INTO PerfilPermiso (Perfil, PermisoId, CodCarrera, CodEnfasis)
				VALUES (@perfil, @idPermiso, @codCarrera, @codEnfasis)
			END
	END
	ELSE
	BEGIN
		DELETE
		FROM PerfilPermiso
		WHERE Perfil = @perfil AND PermisoId = @idPermiso
		      AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
	END
END
RETURN 0
