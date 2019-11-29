CREATE TRIGGER [TriggerCurso]
	ON [dbo].[Curso]
	INSTEAD OF INSERT
	AS
	DECLARE @sigla varchar(10)
	SELECT @sigla = i.Sigla
	FROM inserted i
	BEGIN
		IF(@sigla NOT IN (SELECT Sigla FROM Curso) and @sigla not like '')
		BEGIN
			INSERT INTO Curso SELECT * FROM inserted
		END
	END