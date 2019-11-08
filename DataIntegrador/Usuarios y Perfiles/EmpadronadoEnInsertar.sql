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
		if(@correoEst not in (select CorreoEstudiante from Empadronado_en) or @codCarrera not in (select CodCarrera from Empadronado_en) or @codEnfas not in (select CodEnfasis from Empadronado_en))
		begin
		insert into Empadronado_en select * from inserted
		end
	END
