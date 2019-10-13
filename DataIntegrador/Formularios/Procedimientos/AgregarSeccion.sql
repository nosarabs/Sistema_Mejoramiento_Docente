CREATE PROCEDURE [dbo].[AgregarSeccion]
	@codigo char(8),
	@nombre nvarchar(250)
AS
BEGIN
	INSERT INTO Seccion(Codigo,Nombre)
	VALUES (@codigo, @nombre);
END

