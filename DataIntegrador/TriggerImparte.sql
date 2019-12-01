CREATE TRIGGER [TriggerImparte]
	ON [dbo].[Imparte]
	INSTEAD OF INSERT

	AS
	--Pair Programing Denisse y daniel
	set transaction isolation level serializable;
	set implicit_transactions off;

	Begin transaction transaccionImparte;

		DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
		DECLARE cursor_imparte CURSOR
		FOR SELECT CorreoProfesor, SiglaCurso, NumGrupo, Semestre, Anno
		FROM inserted
		OPEN cursor_imparte;
		FETCH NEXT FROM cursor_imparte INTO @correo, @sigla, @num, @semestre, @anno
		WHILE @@FETCH_STATUS = 0
			BEGIN
				IF(NOT exists (SELECT * FROM Imparte where CorreoProfesor = @correo and SiglaCurso = @sigla and Semestre =@semestre and Anno = @anno ) and @correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')
				BEGIN
					INSERT INTO Imparte SELECT * FROM inserted
				END
				FETCH NEXT FROM cursor_imparte INTO @correo, @sigla, @num, @semestre, @anno
			END
		CLOSE cursor_imparte
		DEALLOCATE cursor_imparte
	
	Commit Transaction transaccionImparte;
	set transaction isolation level read committed;
