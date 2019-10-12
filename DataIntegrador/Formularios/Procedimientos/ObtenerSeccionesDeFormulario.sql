CREATE PROCEDURE ObtenerSeccionesDeFormulario @CodForm char(8)
AS
	SELECT s.Codigo, s.Nombre
	FROM (Seccion s JOIN Formulario_tiene_seccion t ON s.Codigo = t.SCodigo) JOIN Formulario f ON t.FCodigo = f.Codigo
	WHERE f.Codigo = @CodForm
	ORDER BY t.Orden ASC
GO;
