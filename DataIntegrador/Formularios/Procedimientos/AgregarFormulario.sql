CREATE PROCEDURE [dbo].[AgregarFormulario]
	@codigo VARCHAR(8),
	@nombre NVARCHAR(250)
AS
	MERGE INTO Formulario AS TARGET
	USING ( VALUES 
			(@codigo, @nombre)
	)
	AS Source ([Codigo],[Nombre])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Codigo],[Nombre])
	VALUES (@codigo, @nombre);
RETURN 0
