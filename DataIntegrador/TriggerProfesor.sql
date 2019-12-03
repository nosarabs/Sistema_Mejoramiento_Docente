CREATE TRIGGER [TriggerProfesor]
	ON [dbo].[Profesor]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)

	--Nivel de aislamiento maximo porque no podemos permitir modificaciones o nuevas inserciones mientras revisamos las condiciones
	--de insercion
	set transaction isolation level serializable;
	set implicit_transactions off;
	Begin transaction transaccionProfesor;

	DECLARE cursor_Profesor CURSOR
	FOR SELECT Correo
	FROM inserted
	OPEN cursor_Profesor;
	FETCH NEXT FROM cursor_Profesor INTO @correo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@correo NOT IN (SELECT Correo FROM Profesor) and @correo not like '')
			BEGIN
				INSERT INTO Profesor SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_Profesor INTO @correo
		END
	CLOSE cursor_Profesor
	DEALLOCATE cursor_Profesor

	Commit Transaction transaccionProfesor;
	--Volver al nivel de aislamiento por default
	set transaction isolation level read committed;