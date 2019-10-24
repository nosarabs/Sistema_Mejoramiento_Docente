CREATE PROCEDURE [dbo].[PopularFormulariosDePrueba]
AS
BEGIN

	-- Se crea un formulario que será el principal para pruebas: CI0128G1. Este va a contener
	-- preguntas de todos los tipos, relacionadas al curso CI0128, Grupo 1.
	MERGE INTO Formulario AS Target
		USING (VALUES
			('CI0128G1', 'Cuestionario 1, CI-0128, Grupo 1.')
		)
		AS SOURCE ([Codigo],[Nombre])
		ON Target.Codigo = Source.Codigo
		WHEN NOT MATCHED BY TARGET THEN
		INSERT (Codigo,Nombre)
		VALUES (Codigo,Nombre);

	-- Procedimiento que crea las secciones y las llena con preguntas
	EXEC dbo.PopularSeccionesConPreguntas;

	-- Procedimiento que asigna las secciones a este formulario de prueba
	EXEC dbo.PopularFormulariosConSecciones;
END;