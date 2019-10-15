CREATE PROCEDURE [dbo].[AgregarSeccion]
	@codigo char(8),
	@nombre nvarchar(250)
AS
BEGIN
	MERGE INTO Seccion AS Target
	USING (VALUES
			(@codigo, @nombre)
	)
	AS Source ([Codigo],[Nombre])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo, Nombre)
	VALUES (@codigo,@nombre);
END

