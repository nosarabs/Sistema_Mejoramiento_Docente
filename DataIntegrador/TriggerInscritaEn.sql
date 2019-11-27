CREATE TRIGGER [TriggerInscritaEn] 
	ON [dbo].[Inscrita_En]
	INSTEAD OF INSERT
	AS
	declare @CodUnidad varchar(10)
	declare @CodCarrera varchar(10)

	select @CodUnidad = i.CodUnidadAc, @CodCarrera = i.CodCarrera
	from inserted i
	BEGIN
		if(not exists (select * from Inscrita_en where CodUnidadAc= @CodUnidad and CodCarrera =@CodCarrera) and @CodUnidad not like '' and @CodCarrera not like '')
		begin
			insert into Inscrita_en select * from inserted
		end
	END