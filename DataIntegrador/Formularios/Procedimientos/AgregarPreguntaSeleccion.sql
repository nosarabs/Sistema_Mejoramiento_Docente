CREATE PROCEDURE [dbo].[AgregarPreguntaSeleccion]
	@codigo char(8)
AS
BEGIN 
	MERGE INTO Pregunta_con_opciones_de_seleccion AS Target
	USING (VALUES
			(@codigo) 
	)
	AS Source ([Codigo])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo)
	VALUES (@codigo);
END