CREATE VIEW [dbo].[Visualizacion_Respuestas_Escalar]
	AS
	SELECT * FROM Escalar E jOIN Opciones_seleccionadas_respuesta_con_opciones O on E.Codigo = O.PCodigo
