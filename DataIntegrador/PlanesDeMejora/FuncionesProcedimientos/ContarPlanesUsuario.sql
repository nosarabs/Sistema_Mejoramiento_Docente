CREATE PROCEDURE [dbo].[ContarPlanesUsuario]
@username varchar(50),
@count int output
as
begin;
	declare @correo varchar(50);
	select @correo = p.Correo
		from Persona p
		where p.Usuario = @username;
	select @count = count(*)
		from AsignadoA aa
		where aa.corrProf = @correo
end;