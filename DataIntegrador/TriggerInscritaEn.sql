CREATE TRIGGER [TriggerInscritaEn]
	ON [dbo].[Inscrita_En]
	INSTEAD OF INSERT
	AS
	declare @CodUnidad varchar(10)
	declare @CodCarrera varchar(10)

	select @CodUnidad = i.CodUnidadAc, @CodCarrera = i.CodCarrera
	from inserted i
	BEGIN
		if(@CodUnidad not in (select CodUnidadAc from Inscrita_en) or @CodCarrera not in (select @CodCarrera from Inscrita_en))
		begin
			insert into Inscrita_en select * from inserted
		end
	END