CREATE TRIGGER [TriggerGrupo]
	ON [dbo].[Grupo]
	INSTEAD OF INSERT
	AS
	DECLARE @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
	SELECT @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	BEGIN
		IF(((@sigla NOT IN (SELECT SiglaCurso FROM Grupo)) or (@num NOT IN (SELECT NumGrupo FROM Grupo)) or (@semestre NOT IN (SELECT Semestre FROM Grupo)) or (@anno NOT IN (SELECT Anno FROM Grupo))) and (@sigla not like '' and @num not like '' and @semestre not like '' and @anno not like ''))
		BEGIN
			INSERT INTO Grupo SELECT * FROM inserted
		END
	END