CREATE TRIGGER [TriggerTrabajaEn]
	ON [dbo].[Trabaja_en]
	INSTEAD OF INSERT
	AS
	declare @Correo varchar(50)
	declare @CodUnidad varchar(10)

	select @Correo = i.CorreoFuncionario, @CodUnidad = i.CodUnidadAcademica
	from inserted i
	BEGIN
		if(@CodUnidad not in (select CodUnidadAcademica from Trabaja_en) or @Correo not in (select CorreoFuncionario from Trabaja_en))
		begin
			insert into Trabaja_en select * from inserted
		end
	END