CREATE PROCEDURE [dbo].[AsociarPreguntaConSeccion]
	@CodigoSeccion varchar(8),
	@CodigoPregunta varchar(8)
AS
	BEGIN TRY
		BEGIN TRANSACTION AsociarPreguntaConSeccion
			DECLARE @orden INT
			SELECT @orden = count(*)
			FROM Seccion_tiene_pregunta sp
			WHERE sp.SCodigo = @CodigoSeccion;

			INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
			VALUES (@CodigoSeccion, @CodigoPregunta, @Orden);
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
RETURN 0
