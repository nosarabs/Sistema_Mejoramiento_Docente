CREATE TRIGGER [TriggerImparte]
	ON [dbo].[Imparte]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int, @codEnfasis varchar(10), @codCarrera varchar(10)
	SELECT @correo = i.CorreoProfesor, @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	FROM inserted i
	-- Obtener los énfasis a los que pertenece el grupo
	DECLARE cursorEnfasis CURSOR FOR
		SELECT CodCarrera, CodEnfasis
		FROM Pertenece_a
		WHERE SiglaCurso = @sigla

	BEGIN
		IF((@correo NOT IN (SELECT CorreoProfesor FROM Imparte) or @sigla NOT IN (SELECT SiglaCurso FROM Imparte) or @num NOT IN (SELECT NumGrupo FROM Imparte) or @semestre NOT IN (SELECT Semestre FROM Imparte) or @anno NOT IN (SELECT Anno FROM Imparte)) and @correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')
		BEGIN
			INSERT INTO Imparte SELECT * FROM inserted
		END

		-- Dar perfil de profesor en los énfasis, si no los tiene
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
						WHERE Usuario = @correo AND Perfil = 'Profesor' AND CodCarrera = @codCarrera AND CodEnfasis = @codEnfasis
					)
				BEGIN
					INSERT INTO UsuarioPerfil (Usuario, Perfil, CodCarrera, CodEnfasis)
					VALUES (@correo, 'Profesor', @codCarrera, @codEnfasis)
				END
			END
			FETCH NEXT FROM cursorEnfasis into @codCarrera, @codEnfasis;
		END
		CLOSE cursorEnfasis
		DEALLOCATE cursorEnfasis
END