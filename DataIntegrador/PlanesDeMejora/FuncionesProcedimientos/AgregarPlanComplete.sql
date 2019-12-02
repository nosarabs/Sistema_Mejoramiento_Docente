CREATE PROCEDURE [dbo].[AgregarPlanComplete]
(
	@tablaPlan			as dbo.PlanTabla			readonly,
	@tablaAsocPlanForm  as dbo.AsocPlanFormulario   readonly
)
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
				
		insert into PlanDeMejora
		select * from @tablaPlan

		insert into SeAsignaA
		select * from @tablaAsocPlanForm

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH
	
END;
