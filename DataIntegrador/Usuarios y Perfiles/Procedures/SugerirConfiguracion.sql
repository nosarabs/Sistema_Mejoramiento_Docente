CREATE PROCEDURE [dbo].[SugerirConfiguracion]
	@CorreoUsuario VARCHAR(50),
	@PerfilPoderoso VARCHAR(50) OUTPUT,
	@CarreraPoderosa VARCHAR(10) OUTPUT,
	@EnfasisPoderoso VARCHAR(10) OUTPUT
AS
	DECLARE @PerfilI VARCHAR(50)
	DECLARE @CantidadPermisos INT = 0
	DECLARE @CantidadPermisosAnterior INT = 0
	DECLARE iterador CURSOR
	FOR SELECT * FROM PerfilesXUsuario(@CorreoUsuario);
	OPEN iterador;
	FETCH NEXT FROM iterador INTO @PerfilI

	WHILE @@FETCH_STATUS = 0
	BEGIN 
		SELECT @CantidadPermisos = COUNT(*) FROM PerfilPermiso
		WHERE Perfil = @PerfilI

		IF(@CantidadPermisos >= @CantidadPermisos) 
			SET @PerfilPoderoso = @PerfilI
	
		SET @CantidadPermisosAnterior = @CantidadPermisos

		FETCH NEXT FROM iterador INTO @PerfilI
	END
	CLOSE iterador;
	DEALLOCATE iterador;
	SELECT TOP 1 @CarreraPoderosa = codCarrera FROM CarrerasXPerfilXUsuario(@CorreoUsuario, @PerfilPoderoso)
	SELECT TOP 1 @EnfasisPoderoso = codEnfasis FROM EnfasisXCarreraXPerfil(@CorreoUsuario, @CarreraPoderosa, @PerfilPoderoso)

