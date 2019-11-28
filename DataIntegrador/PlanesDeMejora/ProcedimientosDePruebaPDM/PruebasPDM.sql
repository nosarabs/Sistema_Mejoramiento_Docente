CREATE PROCEDURE [dbo].[PruebasPDM]
AS
BEGIN
	
	-- ingresando los planes de mejora para las pruebas
	MERGE INTO PlanDeMejora AS Target
	USING (VALUES
		('Plan prueba para la profesora Alexandra', '2019-12-24', '2019-12-30', 0),
		('Plan prueba para el profesor Cristian', '2019-12-01', '2019-12-10', 0)
	)
	AS SOURCE ([nombre], [fechaInicio], [fechaFin], [borrado])
	ON	Target.nombre		= Source.nombre			and
		Target.fechaInicio	= Source.fechaInicio	and
		Target.fechaFin		= Source.fechaFin		and
		Target.borrado		= Source.borrado
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (nombre, fechaInicio, fechaFin, borrado)
	VALUES (nombre, fechaInicio, fechaFin, borrado);

	-- Definiendo los creadores de los planes de prueba
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

	-- Asignando el primer plan de mejora a un profesor
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

	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------

	--Ahora creando formularios
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

	--Creando secciones
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

	--Creando las preguntas
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

	---------------Ahora haciendo las asociaciones entre los formularios y las secciones---------------
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

	---------------Ahora haciendo las asociaciones entre las SECCIONES y las PREGUNTAS---------------
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


	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------

	--Ahora haciendo las asociaciones entre el planDeMejora y Formulario


END;
