CREATE PROCEDURE [dbo].[AgregarPreguntaConOpcion]
	@cod char(8),
	@type char,
	@texto varchar(50),
	@justificacion varchar(50) = NULL
AS
BEGIN
	INSERT INTO Pregunta([Codigo],[Enunciado])
	VALUES (@cod,@texto);

	INSERT INTO Pregunta_con_opciones(Codigo,TituloCampoObservacion)
	VALUES (@cod,@justificacion);

	EXEC [dbo].[AgregarPreguntaSeleccion]  @codigo = @cod, @tipo = @type;
END

