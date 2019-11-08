CREATE TRIGGER [EmpadronadoEnInsertar]
	ON [dbo].[Empadronado_En]
	INSTEAD OF INSERT
	AS
	declare @correoEst varchar(50)
	declare @codCarrera varchar(10)
	declare @codEnfas varchar(10)
	select @correoEst = i.CorreoEstudiante, @codCarrera = i.CodCarrera, @codEnfas = i.CodEnfasis
	from inserted i
	BEGIN
		if(@correoEst not in (select correo from Estudiante) or @codCarrera not in (select Codigo from Carrera) or @codEnfas not in (select Codigo from Enfasis))
		begin
		insert into Empadronado_en select * from inserted
		end
	END
