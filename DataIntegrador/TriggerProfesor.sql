CREATE TRIGGER [TriggerProfesor]
	ON [dbo].[Profesor]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)
	SELECT @correo = i.Correo
	FROM inserted i
	BEGIN
		IF(@correo NOT IN (SELECT Correo FROM Profesor))
		BEGIN
			INSERT INTO Profesor SELECT * FROM inserted
		END
	END