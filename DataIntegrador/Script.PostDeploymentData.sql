﻿-- Script /* Post-Deployment Script Template							 --------------------------------------------------------------------------------------  This file contains SQL statements that will be appended to the build script.		  Use SQLCMD syntax to include a file in the post-deployment script.			  Example:      :r .\myfile.sql								  Use SQLCMD syntax to reference a variable in the post-deployment script.		  Example:      :setvar TableName MyTable							                SELECT * FROM [$(TableName)]					 -------------------------------------------------------------------------------------- */  DELETE FROM Opciones_seleccionadas_respuesta_con_opciones;	 DELETE FROM Respuestas_a_formulario; DELETE FROM Responde_respuesta_con_opciones; DELETE FROM Responde_respuesta_libre; DELETE FROM Grupo; DELETE FROM Curso; DELETE FROM Enfasis; DELETE FROM Carrera; DELETE FROM UnidadAcademica; DELETE FROM Formulario_tiene_seccion; DELETE FROM Opciones_de_seleccion; DELETE FROM Pregunta_con_opciones; DELETE FROM Pregunta_con_opciones_de_seleccion; DELETE FROM Si_no_nr; DELETE FROM Escalar; DELETE FROM Pregunta_con_respuesta_libre; DELETE FROM Seccion_tiene_pregunta; DELETE FROM Pregunta; DELETE FROM Formulario; DELETE FROM Seccion; DELETE FROM Profesor; DELETE FROM Funcionario; DELETE FROM Persona; DELETE FROM Usuario;  /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// CodeBeakers  ************************************************************************************************* Unidad académica, carrera, énfasis, curso y grupo */  MERGE INTO UnidadAcademica AS Target 	USING (VALUES  		('0000000001', 'ECCI', NULL) 	) 	AS SOURCE ([Codigo],[Nombre],[Superior]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre,Superior) 	VALUES (Codigo,Nombre,Superior);  MERGE INTO Carrera AS Target 	USING (VALUES 		('0000000001', 'Bachillerato en Computación') 	) 	AS SOURCE ([Codigo], [Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Inscrita_en AS Target 	USING (VALUES 		('0000000001', '0000000001') 	) 	AS SOURCE ([CodUnidadAc],[CodCarrera]) 	ON Target.CodUnidadAc = Source.CodUnidadAc and Target.CodCarrera = Source.CodCarrera 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodUnidadAc,CodCarrera) 	VALUES (CodUnidadAc,CodCarrera);  MERGE INTO Enfasis AS Target 	USING (VALUES 		('0000000001', '0000000001', 'Ingeniería de Software') 	) 	AS SOURCE ([CodCarrera],[Codigo],[Nombre]) 	ON Target.CodCarrera = Source.CodCarrera and Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,Codigo,Nombre) 	VALUES (CodCarrera,Codigo,Nombre);  MERGE INTO Curso AS Target 	USING (VALUES 		('CI0128', 'Proyecto Integrador de BD e IS') 	) 	AS SOURCE ([Sigla],[Nombre]) 	ON Target.Sigla = Source.Sigla 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Sigla,Nombre) 	VALUES (Sigla,Nombre);  MERGE INTO Pertenece_a AS Target 	USING (VALUES 		('0000000001', '0000000001', 'CI0128') 	) 	AS SOURCE ([CodCarrera],[CodEnfasis],[SiglaCurso]) 	ON Target.CodCarrera = Source.CodCarrera and Target.CodEnfasis = Source.CodEnfasis and Target.SiglaCurso = Source.SiglaCurso 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,CodEnfasis,SiglaCurso) 	VALUES (CodCarrera,CodEnfasis,SiglaCurso);  MERGE INTO Grupo AS Target 	USING (VALUES 		('CI0128', 1, 2, 2019) 	) 	AS SOURCE ([SiglaCurso],[NumGrupo],[Semestre],[Anno]) 	ON Target.SiglaCurso = Source.SiglaCurso and Target.NumGrupo = Source.NumGrupo and Target.Semestre = Source.Semestre and Target.Anno = Source.Anno 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SiglaCurso,NumGrupo,Semestre,Anno) 	VALUES (SiglaCurso,NumGrupo,Semestre,Anno);  /* ************************************************************************************************* Usuarios */  EXEC dbo.AgregarUsuario 	@pLogin = 'ismael', 	@pPassword = 'ismael', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'denisse', 	@pPassword = 'denisse', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'daniel', 	@pPassword = 'daniel', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'josue', 	@pPassword = 'josue', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'berta', 	@pPassword = 'berta', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'andres', 	@pPassword = 'andres', 	@estado = NULL, 	@activo = 1;  /* ************************************************************************************************* Formulario y preguntas */  MERGE INTO Formulario AS Target 	USING (VALUES 		('00000001', 'Formulario de prueba') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Seccion AS Target 	USING (VALUES 		('00000001', 'Información básica'), 		('00000002', 'Conocimientos básicos'), 		('00000003', 'Expectativas del curso') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Formulario_tiene_seccion AS Target 	USING (VALUES 		('00000001', '00000001', 1), 		('00000001', '00000002', 2), 		('00000001', '00000003', 3) 	) 	AS SOURCE ([FCodigo],[SCodigo],[Orden]) 	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo,SCodigo,Orden) 	VALUES (FCodigo,SCodigo,Orden);  MERGE INTO Pregunta AS Target 	USING (VALUES 		('00000001', '¿Lleva este curso por primera vez?'), 		('00000002', '¿Cuál es su experiencia con el uso de repositorios y control de versiones?'), 		('00000003', '¿Cuál de los siguientes lenguajes/tecnologías ha usado previamente?'), 		('00000004', '¿Qué calificación daría de manera general a sus conocimientos sobre programación de aplicaciones web?'), 		('0000005', '¿Qué expectativas tiene de este curso?') 	) 	AS SOURCE ([Codigo],[Enunciado]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Enunciado) 	VALUES (Codigo,Enunciado);  MERGE INTO Seccion_tiene_pregunta AS Target 	USING (VALUES 		('00000001', '00000001', 1), 		('00000002', '00000002', 1), 		('00000002', '00000003', 2), 		('00000002', '00000004', 3), 		('00000003', '00000005', 1) 	) 	AS SOURCE ([SCodigo],[PCodigo],[Orden]) 	ON Target.SCodigo = Source.SCodigo and Target.PCodigo = Source.PCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SCodigo, PCodigo, Orden) 	VALUES (SCodigo, PCodigo, Orden);  MERGE INTO Pregunta_con_opciones AS Target 	USING (VALUES 		('00000001', 'Justificación'), 		('00000002', 'Justificación'), 		('00000003', 'Justificación'), 		('00000004', 'Justificación') 	) 	AS SOURCE ([Codigo],[TituloCampoObservacion]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, TituloCampoObservacion) 	VALUES (Codigo, TituloCampoObservacion);  MERGE INTO Pregunta_con_respuesta_libre AS Target 	USING (VALUES 		('00000005') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Si_no_nr AS Target 	USING (VALUES 		('00000001') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Pregunta_con_opciones_de_seleccion AS Target 	USING (VALUES 		('00000002', 'U'), 		('00000003', 'M') 	) 	AS SOURCE ([Codigo], [Tipo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Tipo) 	VALUES (Codigo, Tipo);  MERGE INTO Opciones_de_seleccion AS Target 	USING (VALUES 		('00000002', 1, 'Ninguna'), 		('00000002', 2, 'Poca'), 		('00000002', 3, 'Regular'), 		('00000002', 4, 'Mucha'), 		('00000003', 1, 'C#'), 		('00000003', 2, 'ADO.NET'), 		('00000003', 3, 'JavaScript'), 		('00000003', 4, 'HTML'), 		('00000003', 5, 'CSS'), 		('00000003', 6, 'SQL') 	) 	AS SOURCE ([Codigo], [Orden], [Texto]) 	ON Target.Codigo = Source.Codigo and Target.Orden = Source.Orden and Target.Texto = Source.Texto 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Orden, Texto) 	VALUES (Codigo, Orden, Texto);  MERGE INTO Escalar AS Target 	USING (VALUES 		('00000004', 1, 0, 9) 	) 	AS SOURCE ([Codigo], [Incremento], [Minimo], [Maximo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Incremento, Minimo, Maximo) 	VALUES (Codigo, Incremento, Minimo, Maximo);  /* ************************************************************************************************* Respuestas a un formulario */  MERGE INTO Respuestas_a_formulario AS Target 	USING (VALUES 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10') 	) 	AS SOURCE ([FCodigo], [Username],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha]) 	ON Target.FCodigo = Source.FCodigo and Target.Username = Source.Username and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha) 	VALUES (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha);  MERGE INTO Responde_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Ismael para la pregunta 1'), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Ismael para la pregunta 2'), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Ismael para la pregunta 3'), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Ismael para la pregunta 4'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Denisse para la pregunta 1'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Denisse para la pregunta 2'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Denisse para la pregunta 3'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Denisse para la pregunta 4'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Daniel para la pregunta 1'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Daniel para la pregunta 2'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Daniel para la pregunta 3'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Daniel para la pregunta 4'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Josué para la pregunta 1'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Josué para la pregunta 2'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Josué para la pregunta 3'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Josué para la pregunta 4'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Berta para la pregunta 1'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Berta para la pregunta 2'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Berta para la pregunta 3'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Berta para la pregunta 4'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 'Justificación de Andrés para la pregunta 1'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 'Justificación de Andrés para la pregunta 2'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 'Justificación de Andrés para la pregunta 3'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 'Justificación de Andrés para la pregunta 4') 	) 	AS SOURCE ([FCodigo], [Username],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[Justificacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Username = Source.Username and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, Justificacion) 	VALUES (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, Justificacion);  MERGE INTO Opciones_seleccionadas_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 2), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 1), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 2), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', 3), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 2), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 1), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 4), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 3), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 2), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', 3), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 2), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 6), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 1), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 2), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 6), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 3), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 4), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', 5), 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 1), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 3), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 6), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 6), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 9), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', 4) 	) 	AS SOURCE ([FCodigo], [Username],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[OpcionSeleccionada]) 	ON Target.FCodigo = Source.FCodigo and Target.Username = Source.Username and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and 		Target.OpcionSeleccionada = Source.OpcionSeleccionada 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, OpcionSeleccionada) 	VALUES (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, OpcionSeleccionada);  MERGE INTO Responde_respuesta_libre AS Target 	USING (VALUES 		('00000001', 'ismael', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Ismael para la pregunta 5'), 		('00000001', 'denisse', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Denisse para la pregunta 5'), 		('00000001', 'daniel', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Daniel para la pregunta 5'), 		('00000001', 'josue', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Josué para la pregunta 5'), 		('00000001', 'berta', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Berta para la pregunta 5'), 		('00000001', 'andres', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', 'Respuesta de Andrés para la pregunta 5') 	) 	AS SOURCE ([FCodigo], [Username],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[Observacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Username = Source.Username and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, Observacion) 	VALUES (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, Observacion);     /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tamales  ************************************************************************************************* Login */  EXEC dbo.AgregarUsuario 	@pLogin = 'admin', 	@pPassword = 'admin', 	@estado = NULL, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'kdunst', 	@pPassword = 'kdunst', 	@estado = null, 	@activo = 1;  EXEC dbo.AgregarUsuario 	@pLogin = 'tfey', 	@pPassword = 'tfey', 	@estado = null, 	@activo = 1;    MERGE INTO Persona(Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, Usuario, TipoIdentificacion) AS Target 	USING (VALUES 		('1234567891', 'kirsten@mail.com', 'Kirsten', null, 'Dunst', null, 'kdunst', 'Cedula'), 		('2345678912', 'tina@mail.com', 'Tina', null, 'Fey', null, 'tfey', 'Cedula') 	) 	AS SOURCE ([Identificacion], [Correo], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [Usuario], [TipoIdentificacion]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, Usuario, TipoIdentificacion) 	VALUES (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, Usuario, TipoIdentificacion);  MERGE INTO Funcionario AS Target 	USING (VALUES 		('kirsten@mail.com'), 		('tina@mail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);  MERGE INTO Profesor AS Target 	USING (VALUES 		('kirsten@mail.com'), 		('tina@mail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);  MERGE INTO Formulario AS Target 	USING (VALUES 		('23456789', 'Prueba') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  EXEC dbo.PopularSeccionesConPreguntas;  