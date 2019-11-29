CREATE TRIGGER [TriggerEnfasis]
	ON [dbo].[Enfasis]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10), @codigoCarrera varchar(10)
	SELECT @codigo = i.Codigo, @codigoCarrera = i.CodCarrera
	FROM inserted i
	BEGIN
		IF(NOT EXISTS (SELECT * FROM Enfasis WHERE Codigo = @codigo and CodCarrera = @codigoCarrera) and @codigo not like '' and @codigoCarrera not like '')
		BEGIN
			INSERT INTO Enfasis SELECT * FROM inserted
		END
	END