CREATE PROCEDURE [dbo].[AsociarSeccionConFormulario]
	@codigoFormulario CHAR(8),
	@codigoSeccion CHAR(8),
	@orden INT
AS
	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES (@codigoFormulario, @codigoSeccion, @orden);
RETURN 0
