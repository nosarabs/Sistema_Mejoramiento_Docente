/*TAM-3.2: Listar usuarios con sus respectivos permisos según su énfasis y perfil elegido en la página de administración de 
permisos y perfiles. Este procedimiento devuelve TRUE si el usuario con el correo ingresado tiene el permiso indicado, FALSE 
en caso contrario.*/

CREATE FUNCTION [dbo].[TienePermisoSinEnfasisNiCarrera]
(
	@correoUsuario NVARCHAR(50),
	@perfil NVARCHAR(50),
	@permiso INT
)
RETURNS BIT
AS
BEGIN
	DECLARE @Resultado BIT = 1;
	IF (@permiso in 
	(select PP.PermisoId from UsuarioPerfil as UP JOIN PerfilPermiso as PP ON 
		UP.Perfil = PP.Perfil AND 
		UP.CodCarrera = PP.CodCarrera AND 
		UP.CodEnfasis = PP.CodEnfasis
	 where UP.Usuario = @correoUsuario AND PP.Perfil = @perfil
	)
)
	SET @Resultado = 1
ELSE
	SET @Resultado = 0

	RETURN @Resultado
END
