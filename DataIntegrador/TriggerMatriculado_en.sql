CREATE TRIGGER [TriggerMatriculado_en]
	ON [dbo].[Matriculado_en]
	INSTEAD OF INSERT
	AS
	--Pair Programing Denisse y daniel
	--Nivel de aislamiento maximo porque no podemos permitir modificaciones o nuevas inserciones mientras revisamos las condiciones
	--de insercion
	set transaction isolation level serializable;
	set implicit_transactions off;
	Begin transaction transaccionMatriculadoEn;

		DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
		DECLARE cursor_Matriculado_en CURSOR
		FOR SELECT CorreoEstudiante, SiglaCurso, NumGrupo, Semestre, Anno
		FROM inserted;
		OPEN cursor_Matriculado_en;
		FETCH NEXT FROM cursor_Matriculado_en INTO @correo, @sigla, @num, @semestre, @anno
		WHILE @@FETCH_STATUS = 0
			BEGIN
				IF(NOT exists (SELECT * FROM Matriculado_en where CorreoEstudiante= @correo and SiglaCurso = @sigla and Semestre =@semestre and Anno = @anno ) and @correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')
				BEGIN
					INSERT INTO Matriculado_en SELECT * FROM inserted
				END
				FETCH NEXT FROM cursor_Matriculado_en INTO @correo, @sigla, @num, @semestre, @anno
		CLOSE cursor_Matriculado_en
		DEALLOCATE cursor_Matriculado_en

	Commit Transaction transaccionMatriculadoEn;
	--Volver al nivel de aislamiento por default
	set transaction isolation level read committed;
END