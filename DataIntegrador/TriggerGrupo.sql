CREATE TRIGGER [TriggerGrupo]
	ON [dbo].[Grupo]
	INSTEAD OF INSERT
	AS
	DECLARE @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
	DECLARE cursor_grupo CURSOR
	FOR SELECT SiglaCurso, NumGrupo, Semestre, Anno
	FROM inserted;
	OPEN cursor_grupo;
	FETCH NEXT FROM cursor_grupo INTO @sigla, @num, @semestre, @anno
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(NOT EXISTS (SELECT * FROM Grupo WHERE SiglaCurso = @sigla and NumGrupo = @num and Semestre = @semestre and Anno = @anno) and @sigla not like '' and @num not like '' and @semestre not like '' and @anno not like '')
			BEGIN
				INSERT INTO Grupo SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_grupo INTO @sigla, @num, @semestre, @anno
		END
	CLOSE cursor_grupo
	DEALLOCATE cursor_grupo