CREATE TRIGGER [TriggerCurso]
	ON [dbo].[Curso]
	INSTEAD OF INSERT
	AS
	DECLARE @sigla varchar(10)
	DECLARE cursor_curso CURSOR 
	FOR SELECT Sigla
	FROM inserted;
	OPEN cursor_curso;
	FETCH NEXT FROM cursor_curso INTO @sigla
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@sigla NOT IN (SELECT Sigla FROM Curso) and @sigla not like '')
			BEGIN
				INSERT INTO Curso SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_curso INTO @sigla
		END
	CLOSE cursor_curso;
	DEALLOCATE cursor_curso;