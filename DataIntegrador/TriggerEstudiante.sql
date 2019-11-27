CREATE TRIGGER [TriggerEstudiante]
	ON [dbo].[Estudiante]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)
	DECLARE cursor_estudiante CURSOR
	FOR SELECT Correo
	FROM inserted;
	OPEN cursor_estudiante;
	FETCH NEXT FROM cursor_estudiante INTO @correo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@correo NOT IN (SELECT Correo FROM Estudiante) and @correo not like '')
			BEGIN
				INSERT INTO Estudiante SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_estudiante INTO @correo
		END
	CLOSE cursor_estudiante
	DEALLOCATE cursor_estudiante