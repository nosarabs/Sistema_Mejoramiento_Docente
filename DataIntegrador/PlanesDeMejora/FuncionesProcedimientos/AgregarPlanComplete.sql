CREATE PROCEDURE [dbo].[AgregarPlanComplete]
(
	@tablaPlan					as dbo.PlanTabla			readonly,
	@tablaAsocPlanForm			as dbo.AsocPlanFormulario   readonly,
	@tablaAsocObjSecciones		as dbo.AsocObjetivoSeccion  readonly,
	@tablaAsocAccionesPreguntas as dbo.AsocAccionPregunta   readonly
)
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
				
		insert into PlanDeMejora
		select * from @tablaPlan

		insert into Evalua
		select * from @tablaAsocPlanForm

		insert into ObjVsSeccion
		select * from @tablaAsocObjSecciones

		insert into AccionVsPregunta
		select * from @tablaAsocAccionesPreguntas

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH
	
END;
