CREATE PROCEDURE [dbo].[PruebasPDM2]
AS
	BEGIN

	-- ingresando los planes de mejora para las pruebas
	insert into PlanDeMejora(nombre, fechaInicio, fechaFin, borrado)
	values('Plan prueba para la profesora Alexandra', '2019-12-24', '2019-12-30', 0);
	insert into PlanDeMejora(nombre, fechaInicio, fechaFin, borrado)
	values('Plan prueba para el profesor Cristian', '2019-12-01', '2019-12-10', 0);

	-- Definiendo los creadores de los planes de prueba
	insert into CreadoPor(codPlan, corrFunc)
	values(1, 'christian.asch4@gmail.com');
	insert into CreadoPor(codPlan, corrFunc)
	values(2, 'christian.asch4@gmail.com');

	-- Asignando planes a profesores
	insert into AsignadoA(codPlan, corrProf)
	values(1, 'alexandra.martinez@ucr.ac.cr');
	insert into AsignadoA(codPlan, corrProf)
	values(2, 'cristian.quesadalopez@ucr.ac.cr');

	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------

	--Ahora creando formularios
	insert into Formulario(Codigo, Nombre)
	values('00000420', 'Formulario de prueba - planes de mejora - Alexandra');
	insert into Formulario(Codigo, Nombre)
	values('00000421', 'Formulario de prueba - planes de mejora - Cristian');

	--Creando secciones
	insert into Seccion(Codigo, Nombre)
	values('00000004', 'Información básica - PDM');
	insert into Seccion(Codigo, Nombre)
	values('00000005', 'Conocimientos Básicos - PDM');
	insert into Seccion(Codigo, Nombre)
	values('00000006', 'Expectativas del Curso - PDM');
	
	--Creando las preguntas
	INSERT INTO Pregunta(Codigo, Enunciado, Tipo)
	values('00000006', '¿Lleva este curso por primera vez - prueba PDM?', 'S');
	INSERT INTO Pregunta(Codigo, Enunciado, Tipo)
	values('00000007', '¿Lleva el curso en las mañanas - prueba PDM?', 'S');
	INSERT INTO Pregunta(Codigo, Enunciado, Tipo)
	values('00000008', '¿Lleva el curso en las tardes - prueba PDM?', 'S');


	---------------Ahora haciendo las asociaciones entre los formularios y las secciones---------------
	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES('00000420', '00000004', 0);
	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES('00000420', '00000005', 1);
	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES('00000420', '00000006', 2);

	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES('00000421', '00000004', 0);
	INSERT INTO Formulario_tiene_seccion(FCodigo, SCodigo, Orden)
	VALUES('00000421', '00000005', 1);

	---------------Ahora haciendo las asociaciones entre las SECCIONES y las PREGUNTAS---------------
	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000004', '00000006', 0);
	
	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000005', '00000006', 0);
	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000005', '00000007', 1);

	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000006', '00000006', 0);
	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000006', '00000007', 1);
	INSERT INTO Seccion_tiene_pregunta(SCodigo, PCodigo, Orden)
	VALUES('00000006', '00000008', 2);

	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------
	---------------------------------------------------------------------------------------------

	--Ahora haciendo las asociaciones entre el planDeMejora y Formulario
	INSERT INTO SeAsignaA(codigoPlan, codigoForm)
	VALUES(1, '00000420');
	INSERT INTO SeAsignaA(codigoPlan, codigoForm)
	VALUES(2, '00000421');


END;
