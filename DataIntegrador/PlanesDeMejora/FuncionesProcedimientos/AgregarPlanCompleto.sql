CREATE TYPE PlanDeMejoraParametro
AS TABLE(
	codigo			int			not null PRIMARY KEY,
	nombre			varchar(50) not null,
	fechaInicio		date,
	fechaFin		date,
	borrado			bit
)

/* ya se encuantra el tipo para los objetivos de un plan de mejora*/
/* ya se encuentra el tipo para las acciones de mejora de un plan de mejora*/

GO
CREATE TYPE AccionableParametro
AS TABLE(
	codPlan			int				not null,
	nombreObj		varchar(50)		not null,
	descripAcMej	varchar(50)		not null,
	descripcion		varchar(250)	not null, 
	fechaInicio		date,
	fechaFin		date,
	borrado			bit,
	PRIMARY KEY(codPlan, nombreObj, descripAcMej, descripcion)
)

/*Tipos de datos para los formularios*/
GO
CREATE TYPE FormularioVsPlanParametro
AS TABLE(
	codigoPlan	int			not null,
	codigoForm	varchar(8)	not null,
	PRIMARY KEY(codigoPlan, codigoForm)
)

GO
CREATE TYPE ObjVsSeccionParametro
AS TABLE(
	codigoPlanObj	int 		not null,
	nombreObj		varchar(50)	not null,
	codigoSeccion	varchar(8)
	PRIMARY KEY(codigoPlanObj, nombreObj, codigoSeccion)
)

GO
CREATE TYPE AccionVsPreguntaParametro
AS TABLE(
	codPlanADM	int				not null,
	nombreObj	varchar(50)		not null,
	descripcion	varchar(250)	not null,
	codPregunta	varchar(8)		not null,
	PRIMARY KEY(codPlanADM, nombreObj, descripcion, codPregunta)
)


/* REALIZACION DEL PROCEDIMIENTO PARA EL ALMACENAMIENTO DE UN PLAN SOLO O ASOCIADO CON UN FORMULARIO*/
GO
CREATE PROCEDURE [dbo].[AgregarPlanCompleto]
	@planDeMejora			PlanDeMejoraParametro		readonly,
	@objetivos				ObjetivoParametro			readonly,
	@accionesDeMejora		AccionDeMejoraParametro		readonly,
	@accionables			AccionableParametro			readonly,

	@formulariosVsPlan		FormularioVsPlanParametro	readonly,
	@seccionesVsObjetivos	ObjVsSeccionParametro		readonly,
	@preguntasVsAcciones	AccionVsPreguntaParametro	readonly
AS
BEGIN
	/* Esto se hace como una transacción atómica*/
	BEGIN TRY
		BEGIN TRANSACTION
			
			/* INCIANDO EL ALMACENAMIENTO DEL PLAN DE MEJORA */
			/* Agregando el plan de mejora*/
			INSERT INTO PlanDeMejora
			SELECT * FROM @planDeMejora
			/* Agregando los objetivos*/
			INSERT INTO Objetivo
			SELECT * FROM @objetivos
			/* Agregando las acciones de mejora*/
			INSERT INTO AccionDeMejora
			SELECT * FROM @accionesDeMejora
			/* Agregando los accionables*/
			INSERT INTO Accionable
			SELECT * FROM @accionables
			/* FIN DEL ALMACENAMIENTO DEL PLAN DE MEJORA  */


			/* INCIANDO DE LA ASOCIACION DEL PLAN DE MEJORA CON EL FORMULARIO */
			/* Agregando la asociacion de Plan de Mejora con Formulario*/
			INSERT INTO SeAsignaA
			SELECT * FROM @formulariosVsPlan
			/* Agregando la asociacion de Plan de Mejora con Formulario*/
			INSERT INTO ObjVsSeccion
			SELECT * FROM @seccionesVsObjetivos
			/* Agregando la asociacion de Plan de Mejora con Formulario*/
			INSERT INTO AccionVsPregunta
			SELECT * FROM @preguntasVsAcciones
			/* FIN DE LA ASOCIACION DEL PLAN DE MEJORA CON EL FORMULARIO */

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH;
END;
