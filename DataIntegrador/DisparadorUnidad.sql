CREATE TRIGGER [DisparadorUnidad]
	ON [dbo].[UnidadAcademica]
	FOR INSERT
	AS
	DECLARE @cod varchar(10)
	SELECT @cod = i.Codigo
	FROM inserted i
	BEGIN
		IF(@cod NOT IN(SELECT Codigo FROM UnidadAcademica))
		BEGIN
			SELECT * FROM inserted i;
			INSERT INTO UnidadAcademica SELECT * FROM inserted;
		END
	END
