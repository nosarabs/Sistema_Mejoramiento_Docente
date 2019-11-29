CREATE TRIGGER [TriggerInscritaEn] 
	ON [dbo].[Inscrita_En]
	INSTEAD OF INSERT
	AS
	declare @CodUnidad varchar(10)
	declare @CodCarrera varchar(10)
	DECLARE cursor_inscritaEn CURSOR
	FOR SELECT CodUnidadAc, CodCarrera
	FROM inserted;
	OPEN cursor_inscritaEn;
	FETCH NEXT FROM cursor_inscritaEn INTO @CodUnidad, @CodCarrera
	WHILE @@FETCH_STATUS = 0
		BEGIN
			if(not exists (select * from Inscrita_en where CodUnidadAc= @CodUnidad and CodCarrera =@CodCarrera) and @CodUnidad not like '' and @CodCarrera not like '')
			begin
				insert into Inscrita_en select * from inserted
			end
			FETCH NEXT FROM cursor_inscritaEn INTO @CodUnidad, @CodCarrera
		END
	CLOSE cursor_inscritaEn
	DEALLOCATE cursor_inscritaEn