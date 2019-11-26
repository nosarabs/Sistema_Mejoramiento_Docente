CREATE PROCEDURE [dbo].[getPlanFull]
	@codigoPlan int
AS
	BEGIN TRY
		BEGIN TRANSACTION 
			SELECT *
			FROM PlanDeMejora p JOIN Objetivo o			ON o.codPlan = p.codigo
								JOIN AccionDeMejora am	ON am.codPlan = o.codPlan AND
														   am.nombreObj = o.nombre
								JOIN Accionable a		ON a.codPlan = am.codPlan AND
														   a.nombreObj = am.nombreObj AND
														   a.descripAcMej = am.descripcion
			WHERE @codigoPlan = p.codigo
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT 'No se pudo obtener el plan de mejora completo.';
		THROW
	END CATCH
RETURN 0
