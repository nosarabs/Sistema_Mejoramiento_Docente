CREATE PROCEDURE [dbo].[PopularSeccionesConPreguntas]
AS
BEGIN
	EXEC dbo.AgregarPreguntas;

	EXEC dbo.AgregarSeccion 
		@cod = 'INFOPROF',
		@nombre = 'Sobre el profesor';

	EXEC dbo.AgregarSeccion 
		@cod = 'PERSONAL',
		@nombre = 'Sobre usted como estudiante del curso';

	MERGE INTO Seccion_tiene_pregunta AS Target
		USING (VALUES
				('INFOPROF','PROFES01',0),
				('INFOPROF','PROFES02',1),
				('INFOPROF','PROFES03',2),
				('PERSONAL','PERSNAL1',0),
				('PERSONAL','PERSNAL2',1),
				('PERSONAL','PERSNAL3',2)
		)
		AS Source ([SCodigo],[PCodigo],Orden)
		ON Target.SCodigo = Source.SCodigo AND Target.PCodigo = Source.PCodigo
		WHEN NOT MATCHED BY TARGET THEN
		INSERT (SCodigo, PCodigo, Orden)
		VALUES (SCodigo, PCodigo, Orden);
END
