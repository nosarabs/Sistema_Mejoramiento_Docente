CREATE TRIGGER [TriggerGrupo]
	ON [dbo].[Grupo]
	INSTEAD OF INSERT
	AS
	DECLARE @num tinyint
	DECLARE @semestre tinyint
	DECLARE @anno int
	SELECT @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	BEGIN
		IF(@num NOT IN (SELECT NumGrupo FROM Grupo) and @semestre NOT IN (SELECT Semestre FROM Grupo) and @anno NOT IN (SELECT Anno FROM Grupo))
		BEGIN
			INSERT INTO Grupo SELECT * FROM inserted
		END
	END