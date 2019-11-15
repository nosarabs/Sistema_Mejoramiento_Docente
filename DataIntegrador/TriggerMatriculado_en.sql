CREATE TRIGGER [TriggerMatriculado_en]
	ON [dbo].[Matriculado_en]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
	SELECT @correo = i.CorreoEstudiante, @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	BEGIN
		IF((@correo NOT IN (SELECT CorreoEstudiante FROM Matriculado_en) or @sigla NOT IN (SELECT SiglaCurso FROM Matriculado_en) or @num NOT IN (SELECT NumGrupo FROM Matriculado_en) or @semestre NOT IN (SELECT Semestre FROM Matriculado_en) or @anno NOT IN (SELECT Anno FROM Matriculado_en)) and ((@correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')))
		BEGIN
			INSERT INTO Matriculado_en SELECT * FROM inserted
		END
END