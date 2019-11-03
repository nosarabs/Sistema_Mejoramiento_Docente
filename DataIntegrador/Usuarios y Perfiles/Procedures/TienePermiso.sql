CREATE PROCEDURE [dbo].[TienePermiso]
	@correoUsuario VARCHAR(50),
	@perfil VARCHAR(50),
	@codCarrera VARCHAR(10),
	@codEnfasis VARCHAR(10),
	@permiso INT,
	@resultado BIT OUTPUT
AS
	IF (@permiso in 
	(select PP.PermisoId from UsuarioPerfil as UP JOIN PerfilPermiso as PP ON 
		UP.Perfil = PP.Perfil AND 
		UP.CodCarrera = PP.CodCarrera AND 
		UP.CodEnfasis = PP.CodEnfasis
	 where UP.Usuario = @correoUsuario AND UP.CodCarrera = @codCarrera AND UP.CodEnfasis = @codEnfasis AND PP.Perfil = @perfil
	)
)
	SET @resultado = 1
ELSE
	SET @resultado = 0
