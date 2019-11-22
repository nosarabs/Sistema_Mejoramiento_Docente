create function GetTeacherName(
	@Correo varchar(50)
)
returns varchar(50)
as
begin
	declare @NombreCompleto varchar(50);
	select @NombreCompleto = concat(p.Nombre1, ' ', p.Apellido1)
	from Persona p
	where @Correo = p.Correo;
	return @NombreCompleto;
end