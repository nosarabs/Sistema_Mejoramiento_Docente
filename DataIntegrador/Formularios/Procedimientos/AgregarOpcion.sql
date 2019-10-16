CREATE PROCEDURE [dbo].[AgregarOpcion]
	@cod char(8),
	@orden tinyint,
	@texto varchar(50)
AS
BEGIN 
	MERGE INTO Opciones_de_seleccion AS Target
	USING (VALUES
			(@cod, @orden, @texto) 
	)
	AS Source ([Codigo],[Orden],[Texto])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Codigo],[Orden],[Texto])
	VALUES (@cod, @orden, @texto);
END