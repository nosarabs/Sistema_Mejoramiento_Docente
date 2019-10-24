CREATE PROCEDURE [dbo].[PopularFormulariosConSecciones]
AS
	-- Se asignan las secciones a los formularios
	
	MERGE INTO Formulario_tiene_seccion AS Target
	USING (VALUES
		('CI0128G1', 'CI0128S1', 0),
		('CI0128G1', 'CI0128S2', 1),
		('CI0128G1', 'CI0128S3', 2)
	)
	AS SOURCE ([FCodigo],[SCodigo],[Orden])
	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo,SCodigo,Orden)
	VALUES (FCodigo,SCodigo,Orden);


RETURN 0
