CREATE PROCEDURE [dbo].[SugerirConfiguracion]
	@CorreoUsuario VARCHAR(50),
	@PerfilPoderoso VARCHAR(50) OUTPUT,
	@CarreraPoderosa VARCHAR(10) OUTPUT,
	@EnfasisPoderoso VARCHAR(10) OUTPUT
AS
	DECLARE @PerfilI VARCHAR(50)
	DECLARE @CarreraI VARCHAR(10)
	DECLARE @EnfasisI VARCHAR(10)
	DECLARE @CantidadPermisos INT = 0
	DECLARE @CantidadPermisosAnterior INT = 0

	DECLARE i CURSOR
	FOR SELECT * FROM PerfilesXUsuario(@CorreoUsuario);
	
	OPEN i;
	FETCH NEXT FROM i INTO @PerfilI

	WHILE @@FETCH_STATUS = 0
	BEGIN 
		DECLARE j CURSOR
		FOR SELECT codCarrera FROM CarrerasXPerfilXUsuario(@CorreoUsuario, @PerfilI)
		OPEN j
		FETCH NEXT FROM j INTO @CarreraI
		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE k CURSOR
			FOR SELECT codEnfasis FROM EnfasisXCarreraXPerfil(@CorreoUsuario, @CarreraI, @PerfilI)

			OPEN k;
			FETCH NEXT FROM k INTO @EnfasisI

			WHILE @@FETCH_STATUS = 0
			BEGIN 

				SELECT @CantidadPermisos = COUNT(*) FROM PerfilPermiso
				WHERE Perfil = @PerfilI AND  CodCarrera = @CarreraI AND CodEnfasis = @EnfasisI

				IF(@CantidadPermisos >= @CantidadPermisosAnterior)
				BEGIN
					SET @PerfilPoderoso = @PerfilI
					SET @CarreraPoderosa = @CarreraI
					SET @EnfasisPoderoso = @EnfasisI

				END

				SET @CantidadPermisosAnterior = @CantidadPermisos
				
				FETCH NEXT FROM k INTO @EnfasisI

				END
				CLOSE k;
				DEALLOCATE k;

			FETCH NEXT FROM j INTO @CarreraI
			
		END

		CLOSE j;
		DEALLOCATE j;

		FETCH NEXT FROM i INTO @PerfilI
	END

	CLOSE i;
	DEALLOCATE i;
