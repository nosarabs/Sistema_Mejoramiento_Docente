CREATE TRIGGER [TriggerFuncionario]
	ON [dbo].[Funcionario]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)
	SELECT @correo = i.Correo
	FROM inserted i
	BEGIN
		IF(@correo NOT IN (SELECT Correo FROM Funcionario) and @correo not like '')
		BEGIN
			INSERT INTO Funcionario SELECT * FROM inserted
		END
	END