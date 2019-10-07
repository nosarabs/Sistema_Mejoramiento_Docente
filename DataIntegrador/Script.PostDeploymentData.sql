﻿/* Post-Deployment Script Template							 --------------------------------------------------------------------------------------  This file contains SQL statements that will be appended to the build script.		  Use SQLCMD syntax to include a file in the post-deployment script.			  Example:      :r .\myfile.sql								  Use SQLCMD syntax to reference a variable in the post-deployment script.		  Example:      :setvar TableName MyTable							                SELECT * FROM [$(TableName)]					 -------------------------------------------------------------------------------------- */    /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// CodeBeakers  ************************************************************************************************* Unidad académica, carrera, énfasis, curso y grupo */  INSERT INTO UnidadAcademica VALUES ('0000000001', 'ECCI', NULL);  INSERT INTO Carrera VALUES ('0000000001', 'Bachillerato en Computación', '0000000001');  INSERT INTO Enfasis VALUES ('0000000001', '0000000001', 'Ingeniería de Software');  INSERT INTO Curso VALUES ('CI0128', 'Proyecto Integrador de BD e IS', '0000000001', '0000000001');  INSERT INTO Grupo VALUES ('CI0128', 1, 2, 2019);  /* ************************************************************************************************* Usuarios */  EXEC dbo.AgregarUsuario 	@pLogin = 'ismael', 	@pPassword = 'ismael', 	@estado = NULL;  EXEC dbo.AgregarUsuario 	@pLogin = 'denisse', 	@pPassword = 'denisse', 	@estado = NULL;  EXEC dbo.AgregarUsuario 	@pLogin = 'daniel', 	@pPassword = 'daniel', 	@estado = NULL;  EXEC dbo.AgregarUsuario 	@pLogin = 'josue', 	@pPassword = 'josue', 	@estado = NULL;  EXEC dbo.AgregarUsuario 	@pLogin = 'berta', 	@pPassword = 'berta', 	@estado = NULL;  EXEC dbo.AgregarUsuario 	@pLogin = 'andres', 	@pPassword = 'andres', 	@estado = NULL;  /* ************************************************************************************************* Formulario y preguntas */  INSERT INTO Formulario VALUES ('00000001', 'Formulario de prueba');  INSERT INTO Seccion VALUES ('00000001', 'Información básica'), ('00000002', 'Conocimientos básicos'), ('00000003', 'Expectativas del curso');  INSERT INTO Formulario_tiene_seccion VAlUES ('00000001', '00000001', 1), ('00000001', '00000002', 2), ('00000001', '00000003', 3);  INSERT INTO Pregunta VALUES ('00000001', '¿Lleva este curso por primera vez?'), ('00000002', '¿Cuál es su experiencia con el uso de repositorios y control de versiones?'), ('00000003', '¿Cuál de los siguientes lenguajes/tecnologías ha usado previamente?'), ('00000004', '¿Qué calificación daría de manera general a sus conocimientos sobre programación de aplicaciones web?'), ('00000005', '¿Qué expectativas tiene de este curso?');  INSERT INTO Seccion_tiene_pregunta VALUES ('00000001', '00000001', 1), ('00000002', '00000002', 1), ('00000002', '00000003', 2), ('00000002', '00000004', 3), ('00000003', '00000005', 1);  INSERT INTO Pregunta_con_opciones VALUES ('00000001', 'Justificación'), ('00000002', 'Justificación'), ('00000003', 'Justificación'), ('00000004', 'Justificación');  INSERT INTO Pregunta_con_respuesta_libre VALUES ('00000005');  INSERT INTO Si_no_nr VALUES ('00000001');  INSERT INTO Pregunta_con_opciones_de_seleccion VALUES ('00000002', 'U'), ('00000003', 'M');  INSERT INTO Opciones_de_seleccion VALUES ('00000002', 1, 'Ninguna'), ('00000002', 2, 'Poca'), ('00000002', 3, 'Regular'), ('00000002', 4, 'Mucha'), ('00000003', 1, 'C#'), ('00000003', 2, 'ADO.NET'), ('00000003', 3, 'JavaScript'), ('00000003', 4, 'HTML'), ('00000003', 5, 'CSS'), ('00000003', 6, 'SQL');  INSERT INTO Escalar VALUES ('00000004', 1, 0, 9);  /* ************************************************************************************************* Respuestas a un formulario */  INSERT INTO Respuestas_a_formulario VALUES ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10');  INSERT INTO Responde_respuesta_con_opciones VALUES ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Ismael para la pregunta 1'), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Ismael para la pregunta 2'), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Ismael para la pregunta 3'), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Ismael para la pregunta 4'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Denisse para la pregunta 1'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Denisse para la pregunta 2'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Denisse para la pregunta 3'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Denisse para la pregunta 4'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Daniel para la pregunta 1'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Daniel para la pregunta 2'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Daniel para la pregunta 3'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Daniel para la pregunta 4'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Josué para la pregunta 1'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Josué para la pregunta 2'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Josué para la pregunta 3'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Josué para la pregunta 4'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Berta para la pregunta 1'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Berta para la pregunta 2'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Berta para la pregunta 3'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Berta para la pregunta 4'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Andrés para la pregunta 1'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Andrés para la pregunta 2'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Andrés para la pregunta 3'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Andrés para la pregunta 4');  INSERT INTO Opciones_seleccionadas_respuesta_con_opciones VALUES ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 2), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 2), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 3), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 2), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 1), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 4), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 3), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 2), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 3), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 2), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 6), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 2), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 6), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 1), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 3), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 6), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 6), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 9), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 4);  INSERT INTO Responde_respuesta_libre VALUES ('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Ismael para la pregunta 5'), ('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Denisse para la pregunta 5'), ('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Daniel para la pregunta 5'), ('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Josué para la pregunta 5'), ('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Berta para la pregunta 5'), ('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Andrés para la pregunta 5');     /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tamales  ************************************************************************************************* Login */  DELETE FROM Usuario WHERE Username = 'admin';  EXEC dbo.AgregarUsuario 	@pLogin = 'admin', 	@pPassword = 'admin', 	@estado = NULL;