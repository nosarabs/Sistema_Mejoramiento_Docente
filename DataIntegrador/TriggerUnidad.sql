CREATE TRIGGER [TriggerUnidad]
	ON [dbo].[UnidadAcademica]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)
	SELECT @codigo = i.Codigo
	FROM inserted i
	BEGIN
		IF(@codigo NOT IN (SELECT Codigo FROM UnidadAcademica) and @codigo not like '')
		BEGIN
			INSERT INTO UnidadAcademica SELECT * FROM inserted
		END
	END