CREATE PROCEDURE [dbo].[AgregarPreguntaConOpcion]
	@cod char(8),
	@type char,
	@texto varchar(50),
	@justificacion varchar(50) = NULL
AS
BEGIN
	MERGE INTO Pregunta AS Target
	USING (VALUES
			(@cod, @texto) 
	)
	AS Source ([Codigo],[Enunciado])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo, Enunciado)
	VALUES (@cod,@texto);


	MERGE INTO Pregunta_con_opciones AS Target
	USING (VALUES 
		(@cod, @justificacion)
	)
	As Source ([Codigo],[TituloCampoObservacion])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN 
	INSERT (Codigo, TituloCampoObservacion)
	VALUES (@cod,@justificacion);


	EXEC [dbo].[AgregarPreguntaSeleccion]  @codigo = @cod, @tipo = @type;
END

