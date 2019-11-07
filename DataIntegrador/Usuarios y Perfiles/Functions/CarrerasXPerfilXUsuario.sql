﻿/*TAM 3.1: Función almacenada que devuelve una lista de códigos de carreras en las que el usuario dado con 
la carrera seleccionada tiene perfiles asignados.*/
CREATE FUNCTION [dbo].[CarrerasXPerfilXUsuario]
(
	@correoUsuario VARCHAR(50),
	@param2 char(5)
)
RETURNS @returntable TABLE
(
	codCarrera VARCHAR(10)
)
AS
BEGIN
	INSERT @returntable
	SELECT DISTINCT C.Codigo FROM UsuarioPerfil AS UP JOIN Enfasis AS E on UP.CodEnfasis = E.Codigo 
	JOIN Carrera AS C ON E.CodCarrera = C.Codigo
	WHERE UP.Usuario = @correoUsuario
	RETURN
END