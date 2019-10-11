CREATE PROCEDURE [dbo].[AgregarSeccion]
	@cod varchar(8),
	@nombre varchar(250)
AS
BEGIN
	INSERT INTO Seccion([Codigo],[Nombre])
	VALUES (@cod, @nombre);
END

