CREATE PROCEDURE [dbo].[AgregarPreguntaSeleccion]
	@codigo char(8),
	@tipo char
AS
BEGIN 
	MERGE INTO Pregunta_con_opciones_de_seleccion AS Target
	USING (VALUES
			(@codigo, @tipo) 
	)
	AS Source ([Codigo],[Tipo])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo, Tipo)
	VALUES (@codigo,@tipo);
END