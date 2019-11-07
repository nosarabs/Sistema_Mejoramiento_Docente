CREATE TRIGGER [TriggerEstudiante]
	ON [dbo].[Estudiante]
	INSTEAD OF INSERT
	AS
	DECLARE @correo varchar(50)
	SELECT @correo = i.Correo
	FROM inserted i
	BEGIN
		IF(@correo NOT IN (SELECT Correo FROM Estudiante))
		BEGIN
			INSERT INTO Estudiante SELECT * FROM inserted
		END
	END