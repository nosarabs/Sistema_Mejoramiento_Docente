CREATE TRIGGER [TriggerEnfasis]
	ON [dbo].[Enfasis]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10), @codigoCarrera varchar(10)
	SELECT @codigo = i.Codigo, @codigoCarrera = i.CodCarrera
	FROM inserted i
	BEGIN
		IF((@codigo NOT IN (SELECT Codigo FROM Enfasis) OR @codigoCarrera NOT IN (SELECT CodCarrera FROM Enfasis)) and @codigo not like '' and @codigoCarrera not like '')
		BEGIN
			INSERT INTO Enfasis SELECT * FROM inserted
		END
	END