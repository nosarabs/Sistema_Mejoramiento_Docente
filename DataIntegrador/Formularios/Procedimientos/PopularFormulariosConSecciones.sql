CREATE PROCEDURE [dbo].[PopularFormulariosConSecciones]
AS
	-- Se crean los formularios
	MERGE INTO Formulario AS Target
	USING (VALUES
		('FORMSU01', 'Formulario selección única') -- Formulario que solo tendrá preguntas de selección única
	)
	AS SOURCE ([Codigo],[Nombre])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo,Nombre)
	VALUES (Codigo,Nombre);

	-- Se asignan las secciones a los formularios
	
	MERGE INTO Formulario_tiene_seccion AS Target
	USING (VALUES
		('FORMSU01', 'PERSONAL', 0),
		('FORMSU01', 'INFOPROF', 1),
		('FORMSU01', 'SUPRUEBA', 2)
	)
	AS SOURCE ([FCodigo],[SCodigo],[Orden])
	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo,SCodigo,Orden)
	VALUES (FCodigo,SCodigo,Orden);


RETURN 0
