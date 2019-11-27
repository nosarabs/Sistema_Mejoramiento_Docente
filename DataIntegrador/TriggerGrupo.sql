CREATE TRIGGER [TriggerGrupo]
	ON [dbo].[Grupo]
	INSTEAD OF INSERT
	AS
	DECLARE @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
	SELECT @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	BEGIN
		IF(NOT EXISTS (SELECT * FROM Grupo WHERE SiglaCurso = @sigla and NumGrupo = @num and Semestre =@semestre and Anno =@anno) and @sigla not like '' and @num not like '' and @semestre not like '' and @anno not like '')
		BEGIN
			INSERT INTO Grupo SELECT * FROM inserted
		END
	END