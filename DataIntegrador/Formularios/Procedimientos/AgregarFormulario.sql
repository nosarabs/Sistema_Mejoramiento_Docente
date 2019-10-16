CREATE PROCEDURE [dbo].[AgregarFormulario]
	@codigo CHAR(8),
	@nombre NVARCHAR(250)
AS
	INSERT INTO Formulario(Codigo, Nombre)
	VALUES (@codigo, @nombre);
RETURN 0
