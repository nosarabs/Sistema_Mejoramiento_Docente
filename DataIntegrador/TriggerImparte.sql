CREATE TRIGGER [TriggerImparte]
	ON [dbo].[Imparte]
	INSTEAD OF INSERT

	AS
	--Pair Programing Denisse y daniel
	set transaction isolation level serializable;
	set implicit_transactions off;

	Begin transaction transaccionImparte;

		DECLARE @correo varchar(50), @sigla varchar(10), @num tinyint, @semestre tinyint, @anno int
		SELECT @correo = i.CorreoProfesor, @sigla = i.SiglaCurso, @num = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
		FROM inserted i
		BEGIN
			IF(NOT exists (SELECT * FROM Matriculado_en where CorreoEstudiante= @correo and SiglaCurso = @sigla and Semestre =@semestre and Anno = @anno ) and @correo not like '' and @num not like '' and @semestre not like '' and @anno not like '' and @sigla not like '')
			BEGIN
				INSERT INTO Imparte SELECT * FROM inserted
			END
	Commit Transaction transaccionImparte;
	set transaction isolation level read committed;

END