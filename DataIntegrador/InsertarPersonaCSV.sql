CREATE PROCEDURE [dbo].[InsertarPersonaCSV]
	@Correo varchar(50),
	@Id varchar(30),
	@Nombre varchar(15),
	@Apellido varchar(15),
	@TipoId varchar(30)
AS
BEGIN
	IF(@Correo NOT IN (SELECT Correo FROM Persona))
	BEGIN
		INSERT INTO Persona(Correo, Identificacion, Nombre1, Apellido1, TipoIdentificacion)
		VALUES (@Correo, @Id, @Nombre, @Apellido, @TipoId)
	END
END
