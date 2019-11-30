CREATE TRIGGER [TriggerMatriculado_en]
	ON [dbo].[Matriculado_en]
	INSTEAD OF INSERT
	AS
	--Pair Programing Denisse y daniel
	--Nivel de aislamiento maximo porque no podemos permitir modificaciones o nuevas inserciones mientras revisamos las condiciones
	--de insercion
	DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int, @codEnfasis varchar(10), @codCarrera varchar(10)
	set transaction isolation level serializable;
	set implicit_transactions off;
	Begin transaction transaccionMatriculadoEn;

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
			END
		CLOSE cursor_Matriculado_en
		DEALLOCATE cursor_Matriculado_en

	Commit Transaction transaccionMatriculadoEn;
	--Volver al nivel de aislamiento por default
	set transaction isolation level read committed;

	SELECT @correo = i.CorreoEstudiante, @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	-- Obtener los énfasis a los que pertenece el grupo
	DECLARE cursorEnfasis CURSOR FOR
		SELECT CodCarrera, CodEnfasis
		FROM Pertenece_a
		WHERE SiglaCurso = @sigla
	BEGIN

		-- Dar perfil de estudiante en los énfasis, si no los tiene
		OPEN cursorEnfasis
		FETCH NEXT FROM cursorEnfasis into @codCarrera, @codEnfasis;
		WHILE @@FETCH_STATUS=0
		BEGIN
			-- Asegurarse de que tienen un énfasis asociado
			IF (@codCarrera IS NOT NULL AND @codEnfasis IS NOT NULL)
			BEGIN
				-- No insertar duplicados
				IF NOT EXISTS (
						SELECT *
						FROM UsuarioPerfil
						WHERE Usuario = @correo AND Perfil = 'Estudiante' AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
					)
				BEGIN
					INSERT INTO UsuarioPerfil (Usuario, Perfil, CodCarrera, CodEnfasis)
					VALUES (@correo, 'Estudiante', @codCarrera, @codEnfasis)
				END
			END
			FETCH NEXT FROM cursorEnfasis into @codCarrera, @codEnfasis;
		END
		CLOSE cursorEnfasis
		DEALLOCATE cursorEnfasis
END