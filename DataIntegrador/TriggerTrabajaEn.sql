CREATE TRIGGER [TriggerTrabajaEn]
	ON [dbo].[Trabaja_en]
	INSTEAD OF INSERT
	AS
	declare @Correo varchar(50)
	declare @CodUnidad varchar(10)

	select @Correo = i.CorreoFuncionario, @CodUnidad = i.CodUnidadAcademica
	from inserted i
	BEGIN
		if(not exists (select * from Trabaja_en where CodUnidadAcademica=@CodUnidad and CorreoFuncionario = @Correo) and (@CodUnidad not like '' and @Correo not like ''))
		begin
			insert into Trabaja_en select * from inserted
		end
	END