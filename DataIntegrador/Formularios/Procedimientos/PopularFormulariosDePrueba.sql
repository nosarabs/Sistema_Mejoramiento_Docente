CREATE PROCEDURE [dbo].[PopularFormulariosDePrueba]
@NumeroError int = NULL OUTPUT
AS
BEGIN

	-- Este nivel de isolación, es para evitar que otra transacción lea 
	-- hasta que este no haya agregado los datos correspondientes al formulario
	SET implicit_transactions off;
	SET transaction isolation level REPEATABLE READ;

	BEGIN TRY
		-- Se hacer una transacción para no popular un formulario si falla la agregacion de Preguntas o Formularios
		BEGIN TRANSACTION popularFormulariosPrueba;
			-- Procedimiento que crea las preguntas de prueba
			EXEC dbo.AgregarPreguntas;

			-- Procedimiento que crea las secciones y las llena con preguntas
			EXEC dbo.PopularSeccionesConPreguntas;

			-- Se crea un formulario que será el principal para pruebas: CI0128G1. Este va a contener
			-- preguntas de todos los tipos, relacionadas al curso CI0128, Grupo 1.
			MERGE INTO Formulario AS Target
				USING (VALUES
					('FORMSU01', 'Formulario selección única'),
					('CI0128G1', 'Cuestionario 1, CI-0128, Grupo 1.')
				)
				AS SOURCE ([Codigo],[Nombre])
				ON Target.Codigo = Source.Codigo
				WHEN NOT MATCHED BY TARGET THEN
				INSERT (Codigo,Nombre)
				VALUES (Codigo,Nombre);


			-- Asigna las secciones a este formulario de prueba
			MERGE INTO Formulario_tiene_seccion AS Target
			USING (VALUES
				('FORMSU01', 'INFOPROF', 0),
				('FORMSU01', 'SUPRUEBA', 1),
				('CI0128G1', 'CI0128S1', 0),
				('CI0128G1', 'CI0128S2', 1),
				('CI0128G1', 'CI0128S3', 2),
				('CI0128G1', 'CI0128S4', 3)
			)
			AS SOURCE ([FCodigo],[SCodigo],[Orden])
			ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo
			WHEN NOT MATCHED BY TARGET THEN
			INSERT (FCodigo,SCodigo,Orden)
			VALUES (FCodigo,SCodigo,Orden);
		
		COMMIT TRANSACTION popularFormulariosPrueba
	END TRY
	-- En caso de fallo
	BEGIN CATCH
		PRINT 'No se logró popular el formulario';
			SET @NumeroError = ERROR_NUMBER();
			THROW;
			ROLLBACK TRANSACTION
	END CATCH
END;