CREATE TRIGGER [TriggerCarrera]
	ON [dbo].[Carrera]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)
	SELECT @codigo = i.Codigo
	FROM inserted i
	BEGIN
		IF(@codigo NOT IN (SELECT Codigo FROM Carrera))
		BEGIN
			INSERT INTO Carrera SELECT * FROM inserted
		END
	END