CREATE TRIGGER [TriggerEnfasis]
	ON [dbo].[Enfasis]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)
	SELECT @codigo = i.Codigo
	FROM inserted i
	BEGIN
		IF(@codigo NOT IN (SELECT Codigo FROM Enfasis) and @codigo not like '')
		BEGIN
			INSERT INTO Enfasis SELECT * FROM inserted
		END
	END