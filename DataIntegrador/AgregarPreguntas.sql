CREATE PROCEDURE [dbo].[AgregarPreguntas]
AS
BEGIN
	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PERSNAL1',
		@type = 'U',
		@texto = '¿Cuántas horas le dedica a este curso a la semana?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL1', @orden = 0, @texto = 'Menos de 2';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL1', @orden = 1, @texto = 'Entre 2 y 4';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL1', @orden = 2, @texto = 'Entre 4 y 6';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL1', @orden = 3, @texto = 'Más de 6';

	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PERSNAL2',
		@type = 'U',
		@texto = '¿Se prepara debidamente para las clases antes de la lección?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL2', @orden = 0, @texto = 'Nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL2', @orden = 1, @texto = 'Casi nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL2', @orden = 2, @texto = 'A veces';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL2', @orden = 3, @texto = 'Casi siempre';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL2', @orden = 4, @texto = 'Siempre';

	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PERSNAL3',
		@type = 'U',
		@texto = '¿Asiste regularmente a lecciones?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL3', @orden = 0, @texto = 'Nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL3', @orden = 1, @texto = 'Casi nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL3', @orden = 2, @texto = 'A veces';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL3', @orden = 3, @texto = 'Casi siempre';
	EXEC [dbo].[AgregarOpcion] @cod = 'PERSNAL3', @orden = 4, @texto = 'Siempre';

	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PROFES01',
		@type = 'U',
		@texto = '¿El profesor da las lecciones en el horario establecido?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES01', @orden = 0, @texto = 'Nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES01', @orden = 1, @texto = 'Casi nunca';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES01', @orden = 2, @texto = 'A veces';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES01', @orden = 3, @texto = 'Casi siempre';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES01', @orden = 4, @texto = 'Siempre';

	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PROFES02',
		@type = 'U',
		@texto = '¿El profesor trata temas relacionados con la realidad nacional cuando la materia lo permite?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES02', @orden = 0, @texto = 'No sabe';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES02', @orden = 1, @texto = 'A veces';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES02', @orden = 2, @texto = 'Sí';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES02', @orden = 3, @texto = 'No aplica';
	

	EXEC [dbo].[AgregarPreguntaConOpcion]
		@cod = 'PROFES03',
		@type = 'U',
		@texto = '¿El profesor ha sido respetuoso hacia su persona?';

	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES03', @orden = 0, @texto = 'No mucho';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES03', @orden = 1, @texto = 'Un poco';
	EXEC [dbo].[AgregarOpcion] @cod = 'PROFES03', @orden = 2, @texto = 'Mucho';
END
