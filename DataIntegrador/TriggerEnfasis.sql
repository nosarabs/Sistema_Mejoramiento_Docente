CREATE TRIGGER [TriggerEnfasis]
	ON [dbo].[Enfasis]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10), @codigoCarrera varchar(10)
	DECLARE cursor_enfasis CURSOR
	FOR SELECT Codigo, CodCarrera
	FROM inserted;
	OPEN cursor_enfasis
	FETCH NEXT FROM cursor_enfasis INTO @codigo, @codigoCarrera
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(NOT EXISTS (SELECT * FROM Enfasis WHERE Codigo = @codigo and CodCarrera = @codigoCarrera) and @codigo not like '' and @codigoCarrera not like '')
			BEGIN
				INSERT INTO Enfasis SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_enfasis INTO @codigo, @codigoCarrera
		END
	CLOSE cursor_enfasis
	DEALLOCATE cursor_enfasis