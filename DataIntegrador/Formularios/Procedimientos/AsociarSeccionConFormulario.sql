CREATE PROCEDURE [dbo].[AsociarSeccionConFormulario]
	@codigoFormulario VARCHAR(8),
	@codigoSeccion VARCHAR(8),
	@orden INT
AS
	MERGE INTO Formulario_tiene_seccion AS Target
	USING (VALUES
			(@codigoFormulario, @codigoSeccion, @orden)
	)
	AS Source (FCodigo, SCodigo, Orden)
	ON Target.FCodigo = Source.FCodigo AND Target.SCodigo = Source.SCodigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo, SCodigo, Orden)
	VALUES (@codigoFormulario, @codigoSeccion, @orden);
RETURN 0
