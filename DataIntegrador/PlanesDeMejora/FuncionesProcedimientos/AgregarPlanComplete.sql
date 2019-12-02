CREATE PROCEDURE [dbo].[AgregarPlanComplete]
(
	@tablaPlan					 as dbo.PlanTabla			 readonly,
	@tablaObjetivos				 as dbo.ObjetivosTabla		 readonly,
	@tablaAcciones				 as dbo.AccionDeMejoraTabla	 readonly,
	@tablaAccionables			 as dbo.AccionableTabla 	 readonly,
	@tablaAsocPlanFormularios	 as dbo.AsocPlanFormulario   readonly,
	@tablaAsocObjetivosSecciones as dbo.AsocObjetivoSeccion  readonly,
	@tablaAsocAccionesPreguntas  as dbo.AsocAccionPregunta   readonly
)
AS
BEGIN

BEGIN TRY
	BEGIN TRANSACTION
				
		insert into PlanDeMejora
		select * from @tablaPlan

		insert into Objetivo
		select * from @tablaObjetivos

		insert into AccionDeMejora
		select * from @tablaAcciones

		insert into Accionable
		select * from @tablaAccionables

		insert into Evalua
		select * from @tablaAsocPlanFormularios

		insert into ObjVsSeccion
		select * from @tablaAsocObjetivosSecciones

		insert into AccionVsPregunta
		select * from @tablaAsocAccionesPreguntas

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH
	
END;
