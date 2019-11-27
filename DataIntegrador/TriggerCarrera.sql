CREATE TRIGGER [TriggerCarrera]
	ON [dbo].[Carrera]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)
	DECLARE cursor_carrera CURSOR
	FOR SELECT Codigo
	FROM inserted;
	OPEN cursor_carrera;
	FETCH NEXT FROM cursor_carrera INTO @codigo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@codigo NOT IN (SELECT Codigo FROM Carrera) and @codigo not like '')
			BEGIN
				INSERT INTO Carrera SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_carrera INTO @codigo
		END
	CLOSE cursor_carrera;
	DEALLOCATE cursor_carrera;