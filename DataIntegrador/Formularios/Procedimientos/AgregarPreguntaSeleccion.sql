CREATE PROCEDURE [dbo].[AgregarPreguntaSeleccion]
	@codigo char(8),
	@tipo char
AS
BEGIN 
	INSERT INTO Pregunta_con_opciones_de_seleccion([Codigo],[Tipo])
	VALUES (@codigo, @tipo);
END