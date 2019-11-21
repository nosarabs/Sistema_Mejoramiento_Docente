CREATE TRIGGER [TriggerImparte]
	ON [dbo].[Imparte]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
	SELECT @correo = i.CorreoProfesor, @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	BEGIN
		IF((@correo NOT IN (SELECT CorreoProfesor FROM Imparte) or @sigla NOT IN (SELECT SiglaCurso FROM Imparte) or @num NOT IN (SELECT NumGrupo FROM Imparte) or @semestre NOT IN (SELECT Semestre FROM Imparte) or @anno NOT IN (SELECT Anno FROM Imparte)) and @correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')
		BEGIN
			INSERT INTO Imparte SELECT * FROM inserted
		END
END