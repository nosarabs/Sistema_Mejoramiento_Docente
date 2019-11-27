CREATE TRIGGER [TriggerFuncionario]
	ON [dbo].[Funcionario]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)
	DECLARE cursor_funcionario CURSOR
	FOR SELECT Correo
	FROM inserted;
	OPEN cursor_funcionario;
	FETCH NEXT FROM cursor_funcionario INTO @correo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@correo NOT IN (SELECT Correo FROM Funcionario) and @correo not like '')
			BEGIN
				INSERT INTO Funcionario SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_funcionario INTO @correo
		END
	CLOSE cursor_funcionario
	DEALLOCATE cursor_funcionario