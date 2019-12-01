CREATE TRIGGER [TriggerTrabajaEn]
	ON [dbo].[Trabaja_en]
	INSTEAD OF INSERT
	AS
	declare @Correo varchar(50)
	declare @CodUnidad varchar(10)
	DECLARE cursor_TrabajaEn CURSOR
	FOR SELECT CorreoFuncionario, CodUnidadAcademica
	FROM inserted;
	OPEN cursor_TrabajaEn;
	FETCH NEXT FROM cursor_TrabajaEn INTO @Correo, @CodUnidad
	WHILE @@FETCH_STATUS = 0
		BEGIN
			if(not exists (select * from Trabaja_en where CodUnidadAcademica=@CodUnidad and CorreoFuncionario = @Correo) and (@CodUnidad not like '' and @Correo not like ''))
			begin
				insert into Trabaja_en select * from inserted
			end
			FETCH NEXT FROM cursor_TrabajaEn INTO @Correo, @CodUnidad
		END
	CLOSE cursor_TrabajaEn
	DEALLOCATE cursor_TrabajaEn