CREATE TRIGGER [TriggerUnidad]
	ON [dbo].[UnidadAcademica]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)
	DECLARE cursor_Unidad CURSOR
	FOR SELECT Codigo
	FROM inserted
	OPEN cursor_Unidad;
	FETCH NEXT FROM cursor_Unidad INTO @codigo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@codigo NOT IN (SELECT Codigo FROM UnidadAcademica) and @codigo not like '')
			BEGIN
				INSERT INTO UnidadAcademica SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_Unidad INTO @codigo
		END
	CLOSE cursor_Unidad
	DEALLOCATE cursor_Unidad