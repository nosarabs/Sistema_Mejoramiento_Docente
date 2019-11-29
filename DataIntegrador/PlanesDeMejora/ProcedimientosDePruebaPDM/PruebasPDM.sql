CREATE PROCEDURE [dbo].[PruebasPDM]
AS
BEGIN
	
	/*ingresando los planes de mejora para las pruebas*/
	MERGE INTO PlanDeMejora AS Target
	USING (VALUES
		(1, 'Plan prueba para la profesora Alexandra', '2019-12-5', '2019-12-25', 0),
		(2, 'Plan prueba para el profesor Cristian', '2019-12-01', '2019-12-28', 0)
	)
	AS SOURCE ([codigo], [nombre], [fechaInicio], [fechaFin], [borrado])
	ON	Target.codigo		= Source.codigo			and
		Target.nombre		= Source.nombre			and
		Target.fechaInicio	= Source.fechaInicio	and
		Target.fechaFin		= Source.fechaFin		and
		Target.borrado		= Source.borrado
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codigo, nombre, fechaInicio, fechaFin, borrado)
	VALUES (codigo, nombre, fechaInicio, fechaFin, borrado);

	/*Definiendo los creadores de los planes de prueba*/
	MERGE INTO CreadoPor AS Target
	USING (VALUES
		(1, 'christian.asch4@gmail.com'),
		(2, 'christian.asch4@gmail.com')
	)
	AS SOURCE ([codPlan], [corrFunc])
	ON Target.codPlan = Source.codPlan and Target.corrFunc = Source.corrFunc
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlan, corrFunc)
	VALUES (codPlan, corrFunc);

	/*Asignando el primer plan de mejora a un profesor*/
	MERGE INTO AsignadoA AS Target
	USING (VALUES
		(1, 'alexandra.martinez@ucr.ac.cr'),
		(2, 'cristian.quesadalopez@ucr.ac.cr')
	)
	AS SOURCE ([codPlan], [corrProf])
	ON Target.codPlan = Source.codPlan and Target.corrProf = Source.corrProf
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlan, corrProf)
	VALUES (codPlan, corrProf);

	/* Creacion de un objetivo para el plan de */
	MERGE INTO Objetivo AS Target
	USING (VALUES
		(1, 'Objetivo de plan de mejora - Alexandra', 'Descripcion objetivo Alexandra', '2019-12-5', '2019-12-20', 'Infraestructura', 0),
		(2, 'Objetivo de plan de mejora - Cristian', 'Descripcion objetivo Cristian', '2019-12-2', '2019-12-27', 'Curso', 0)
	)
	AS SOURCE ([codPlan], [nombre], [descripcion], [fechaInicio], [fechaFin], [nombTipoObj], [borrado])
	ON Target.codPlan = Source.codPlan 
		and Target.nombre = Source.nombre
		and Target.descripcion = Source.descripcion
		and Target.fechaInicio = Source.fechaInicio
		and Target.fechaFin = Source.fechaFin
		and Target.nombTipoObj = Source.nombTipoObj
		and Target.borrado = Source.borrado
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlan, nombre, descripcion, fechaInicio, fechaFin, nombTipoObj, borrado)
	VALUES (codPlan, nombre, descripcion, fechaInicio, fechaFin, nombTipoObj, borrado);

	/*Creando acciones de mejora para los objetivos*/
	MERGE INTO AccionDeMejora AS Target
	USING (VALUES
		(1, 'Objetivo de plan de mejora - Alexandra', 'Accion de mejora de objetivo - Alexandra', '2019-12-10', '2019-12-15', 0),
		(2, 'Objetivo de plan de mejora - Cristian', 'Accion de mejora de objetivo - Cristian', '2019-12-5', '2019-12-20', 0)
	)
	AS SOURCE ([codPlan], [nombreObj], [descripcion], [fechaInicio], [fechaFin], [borrado])
	ON	Target.codPlan		= Source.codPlan	and 
		Target.nombreObj	= Source.nombreObj	and 
		Target.descripcion	= Source.descripcion and
		Target.fechaInicio  = Source.fechaInicio and
		Target.fechaFin		= Source.fechaFin and
		Target.borrado		= Source.borrado
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlan, nombreObj, descripcion, fechaInicio, fechaFin, borrado)
	VALUES (codPlan, nombreObj, descripcion, fechaInicio, fechaFin, borrado);

	/* Creando los accionables resepctivos*/
	MERGE INTO Accionable AS Target
	USING (VALUES
		(1, 'Objetivo de plan de mejora - Alexandra', 'Accion de mejora de objetivo - Alexandra', 'Accionable de prof - Alexandra', '2019-12-12', '2019-12-13', 25),
		(2, 'Objetivo de plan de mejora - Cristian', 'Accion de mejora de objetivo - Cristian', 'Accionable de prof - Cristian', '2019-12-10', '2019-12-15', 50)
	)
	AS SOURCE ([codPlan], [nombreObj], [descripAcMej], [descripcion], [fechaInicio], [fechaFin], [progreso])
	ON	Target.codPlan			= Source.codPlan		and 
		Target.nombreObj		= Source.nombreObj		and 
		Target.descripAcMej		= Source.descripAcMej	and
		Target.descripcion		= Source.descripcion	and
		Target.fechaInicio		= Source.fechaInicio	and
		Target.fechaFin			= Source.fechaFin		and
		Target.progreso			= Source.progreso
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlan, nombreObj, descripAcMej, descripcion, fechaInicio, fechaFin, progreso)
	VALUES (codPlan, nombreObj, descripAcMej, descripcion, fechaInicio, fechaFin, progreso);


	/**********************************************************************************/

	/*Ahora creando formularios*/
	MERGE INTO Formulario AS Target
	USING (VALUES
		('00000420', 'Formulario de prueba - planes de mejora - Alexandra'),
		('00000421', 'Formulario de prueba - planes de mejora - Cristian')
	)
	AS SOURCE ([Codigo],[Nombre])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo,Nombre)
	VALUES (Codigo,Nombre);

	/*Creando secciones*/
	MERGE INTO Seccion AS Target
	USING (VALUES
		('00000004', 'Información básica - PDM'),
		('00000005', 'Conocimientos Básicos - PDM'),
		('00000006', 'Expectativas del Curso - PDM')
	)
	AS SOURCE ([Codigo],[Nombre])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo,Nombre)
	VALUES (Codigo,Nombre);

	/*Creando las preguntas*/
	MERGE INTO Pregunta AS Target
	USING (VALUES
		('00000006', '¿Lleva este curso por primera vez - prueba PDM?', 'S'),
		('00000007', '¿Lleva el curso en las mañanas - prueba PDM?', 'S'),
		('00000008', '¿Lleva el curso en las tardes - prueba PDM?', 'S')
	)
	AS SOURCE ([Codigo],[Enunciado], [Tipo])
	ON Target.Codigo = Source.Codigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (Codigo,Enunciado, Tipo)
	VALUES (Codigo,Enunciado, Tipo);

	/*Ahora haciendo las asociaciones entre los formularios y las secciones   */
	MERGE INTO Formulario_tiene_seccion AS Target
	USING (VALUES
		('00000420', '00000004', 0),
		('00000420', '00000005', 1),
		('00000420', '00000006', 2),
		('00000421', '00000004', 0),
		('00000421', '00000005', 1)
	)
	AS SOURCE ([FCodigo],[SCodigo],[Orden])
	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo,SCodigo,Orden)
	VALUES (FCodigo,SCodigo,Orden);

	/*Ahora haciendo las asociaciones entre las SECCIONES y las PREGUNTAS */
	MERGE INTO Seccion_tiene_pregunta AS Target
	USING (VALUES
		('00000004', '00000006', 0),
		('00000005', '00000006', 0),
		('00000005', '00000007', 1),
		('00000006', '00000006', 0),
		('00000006', '00000007', 1),
		('00000006', '00000008', 2)
	)
	AS SOURCE ([SCodigo],[PCodigo],[Orden])
	ON Target.SCodigo = Source.SCodigo and Target.PCodigo = Source.PCodigo
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (SCodigo, PCodigo, Orden)
	VALUES (SCodigo, PCodigo, Orden);

	/*Ahora haciendo las asociaciones entre el planDeMejora y Formulario*/
	MERGE INTO SeAsignaA AS Target
	USING (VALUES
		(1, '00000420'),
		(2, '00000421')
	)
	AS SOURCE ([codigoPlan],[codigoForm])
	ON Target.codigoPlan = Source.codigoPlan and Target.codigoForm = Source.codigoForm
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codigoPlan, codigoForm)
	VALUES (codigoPlan, codigoForm);

	/*Ahora haciendo las asociaciones entre el planDeMejora y Formulario*/
	MERGE INTO ObjVsSeccion AS Target
	USING (VALUES
		(1, 'Objetivo de plan de mejora - Alexandra', '00000004'),
		(2, 'Objetivo de plan de mejora - Cristian', '00000004')
	)
	AS SOURCE ([codPlanObj],[nombreObj],[codSeccion])
	ON  Target.codPlanObj	= Source.codPlanObj	and
		Target.nombreObj	= Source.nombreObj and
		Target.codSeccion	= Source.codSeccion
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlanObj, codSeccion, nombreObj)
	VALUES (codPlanObj, codSeccion, nombreObj);


	/*Ahora haciendo las asociaciones entre la accionDeMejora y las preguntas*/
	MERGE INTO AccionVsPregunta AS Target
	USING (VALUES
		(1, 'Objetivo de plan de mejora - Alexandra', 'Accion de mejora de objetivo - Alexandra', '00000006'),
		(2, 'Objetivo de plan de mejora - Cristian', 'Accion de mejora de objetivo - Cristian', '00000006')
	)
	AS SOURCE ([codPlanADM],[nombreObj],[descripcion],[codPregunta])
	ON  Target.codPlanADM	= Source.codPlanADM and
		Target.nombreObj	= Source.nombreObj and
		Target.descripcion	= Source.descripcion and
		Target.codPregunta	= Source.codPregunta
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (codPlanADM, nombreObj, descripcion, codPregunta)
	VALUES (codPlanADM, nombreObj, descripcion, codPregunta);
	
END;
