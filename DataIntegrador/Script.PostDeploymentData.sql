﻿/* Post-Deployment Script Template							 --------------------------------------------------------------------------------------  This file contains SQL statements that will be appended to the build script.		  Use SQLCMD syntax to include a file in the post-deployment script.			  Example:      :r .\myfile.sql								  Use SQLCMD syntax to reference a variable in the post-deployment script.		  Example:      :setvar TableName MyTable							                SELECT * FROM [$(TableName)]					 -------------------------------------------------------------------------------------- */ /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// CodeBeakers  ************************************************************************************************* Unidad académica, carrera, énfasis, curso y grupo */  MERGE INTO UnidadAcademica AS Target 	USING (VALUES  		('0000000001', 'ECCI', NULL) 	) 	AS SOURCE ([Codigo],[Nombre],[Superior]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre,Superior) 	VALUES (Codigo,Nombre,Superior);  MERGE INTO Carrera AS Target 	USING (VALUES 		('0000000001', 'Bachillerato en Computación'), 		('0000000002', 'Ingeniería Eléctrica') 	) 	AS SOURCE ([Codigo], [Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Inscrita_en AS Target 	USING (VALUES 		('0000000001', '0000000001') 	) 	AS SOURCE ([CodUnidadAc],[CodCarrera]) 	ON Target.CodUnidadAc = Source.CodUnidadAc and Target.CodCarrera = Source.CodCarrera 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodUnidadAc,CodCarrera) 	VALUES (CodUnidadAc,CodCarrera);  MERGE INTO Enfasis AS Target 	USING (VALUES 		('0000000001', '0000000000', 'Tronco Común'), 		('0000000001', '0000000001', 'Ingeniería de Software'), 		('0000000001', '0000000002', 'Ciencias de la Computación'), 		('0000000001', '0000000003', 'Tecnologías de la Información'), 		('0000000002', '0000000000', 'Tronco Común'), 		('0000000002', '0000000001', 'Computadores y Redes'), 		('0000000002', '0000000002', 'Electrónica y Telecomunicaciones'), 		('0000000002', '0000000003', 'Sistemas de energía') 	) 	AS SOURCE ([CodCarrera],[Codigo],[Nombre]) 	ON Target.CodCarrera = Source.CodCarrera and Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,Codigo,Nombre) 	VALUES (CodCarrera,Codigo,Nombre);  MERGE INTO Curso AS Target 	USING (VALUES 		('CI0128', 'Proyecto Integrador de BD e IS') 	) 	AS SOURCE ([Sigla],[Nombre]) 	ON Target.Sigla = Source.Sigla 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Sigla,Nombre) 	VALUES (Sigla,Nombre);  MERGE INTO Pertenece_a AS Target 	USING (VALUES 		('0000000001', '0000000001', 'CI0128') 	) 	AS SOURCE ([CodCarrera],[CodEnfasis],[SiglaCurso]) 	ON Target.CodCarrera = Source.CodCarrera and Target.CodEnfasis = Source.CodEnfasis and Target.SiglaCurso = Source.SiglaCurso 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,CodEnfasis,SiglaCurso) 	VALUES (CodCarrera,CodEnfasis,SiglaCurso);  MERGE INTO Grupo AS Target 	USING (VALUES 		('CI0128', 1, 2, 2019) 	) 	AS SOURCE ([SiglaCurso],[NumGrupo],[Semestre],[Anno]) 	ON Target.SiglaCurso = Source.SiglaCurso and Target.NumGrupo = Source.NumGrupo and Target.Semestre = Source.Semestre and Target.Anno = Source.Anno 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SiglaCurso,NumGrupo,Semestre,Anno) 	VALUES (SiglaCurso,NumGrupo,Semestre,Anno);  /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tamales ************************************************************************************************* Usuarios, Personas & Login */  	MERGE INTO Persona AS Target 	USING (VALUES 		('123456789', 'ismael@mail.com', 'Ismael', null, 'Gutiérrez', null,'Cédula'), 		('123456788', 'denisse@mail.com', 'Denisse', null, 'Alfaro', null,'Cédula'), 		('123456787', 'daniel@mail.com', 'Daniel', null, 'Escamilla', null,'Cédula'), 		('123456786', 'josue@mail.com', 'Josué', null, 'Zeledón', null,'Cédula'), 		('123456785', 'berta@mail.com', 'Berta', null, 'Sánchez', null,'Cédula'), 		('123456784', 'andres@mail.com', 'Andrés', null, 'Mesén', null, 'Cédula'), 		('123456783', 'admin@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('111111111', 'tamales@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('222222222', 'rip@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('333333333', 'bakers@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('444444444', 'mosqueteros@mail.com', 'Admin', null, 'Admin', null, 'Cédula')  	) 	AS SOURCE ([Identificacion], [Correo], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [TipoIdentificacion]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion) 	VALUES (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion);   MERGE INTO Persona AS Target 	USING (VALUES 		('123456782', 'kirsten@mail.com', 'Kirsten', null, 'Dunst', null, 'Cédula'), 		('123456781', 'tina@mail.com', 'Tina', null, 'Fey', null, 'Cédula'), 		('123456780', 'ericrios24@gmail.com', 'Eric', null, 'Rios', 'Morales', 'Cédula'), 		('106670848', 'cristian.quesadalopez@ucr.ac.cr', 'Cristian', 'Ulises', 'Quesada', 'Lopez', 'Cédula') 	) 	AS SOURCE ([Identificacion], [Correo], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [TipoIdentificacion]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion) 	VALUES (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion);  MERGE INTO Permiso AS Target 	USING (VALUES 		(101, 'Ver usuarios'), 		(102, 'Crear usuarios'), 		(103, 'Ver detalles de usuarios'), 		(104, 'Editar usuarios'), 		(105, 'Borrar usuarios'), 		(201, 'Crear formularios'), 		(202, 'Ver formularios'), 		(203, 'Crear secciones'), 		(204, 'Ver secciones'), 		(205, 'Crear preguntas'), 		(206, 'Ver preguntas'), 		(301, 'Ver planes de mejora'), 		(302, 'Crear planes de mejora'), 		(303, 'Editar planes de mejora'), 		(304, 'Borrar planes de mejora'), 		(305, 'Crear objetivos'), 		(306, 'Ver objetivos'), 		(307, 'Editar objetivos'), 		(308, 'Borrar objetivos'), 		(309, 'Crear acciones de mejora'), 		(310, 'Ver acciones de mejora'), 		(311, 'Editar acciones de mejora'), 		(312, 'Borrar acciones de mejora'), 		(401, 'Ver respuestas de formularios') 	) 	AS SOURCE([Id], Descripcion) 	ON Target.Id = SOURCE.Id 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Id, Descripcion) 	VALUES (Id, Descripcion);  MERGE INTO Perfil AS TARGET 	USING (VALUES 		('Superusuario'), 		('Administrador'), 		('Administrativo'), 		('Director'), 		('Profesor'), 		('Estudiante'), 		('Coordinador de núcleo'), 		('Coordinador de énfasis'), 		('Coordinador de comisión de docencia') 	) 	AS SOURCE([Nombre]) 	ON Target.Nombre = SOURCE.Nombre 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Nombre) 	VALUES (Nombre);  MERGE INTO UsuarioPerfil AS Target 	USING (VALUES 		('admin@mail.com', 'Superusuario', '0000000001', '0000000000'), 		('admin@mail.com', 'Superusuario', '0000000001', '0000000001'), 		('admin@mail.com', 'Superusuario', '0000000001', '0000000002'), 		('admin@mail.com', 'Superusuario', '0000000001', '0000000003'), 		('andres@mail.com', 'Estudiante', '0000000001', '0000000001'), 		('andres@mail.com', 'Profesor', '0000000001', '0000000002'), 		('andres@mail.com', 'Profesor', '0000000002', '0000000002'), 		('ericrios24@gmail.com', 'Estudiante', '0000000001', '0000000002'), 		('ismael@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('denisse@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('daniel@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('josue@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('berta@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('kirsten@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('tina@mail.com', 'Estudiante', '0000000001', '0000000000') 	) 	AS SOURCE ([Usuario], [Perfil], [CodCarrera], [CodEnfasis]) 	ON Target.Usuario = SOURCE.Usuario AND Target.Perfil = SOURCE.Perfil AND Target.CodCarrera = SOURCE.CodCarrera AND Target.CodEnfasis = SOURCE.CodEnfasis 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Usuario, Perfil, CodCarrera, CodEnfasis) 	VALUES (Usuario, Perfil, CodCarrera, CodEnfasis); 	 MERGE INTO PerfilPermiso AS Target 	USING (VALUES 		('Superusuario', 101, '0000000001', '0000000000'), 		('Superusuario', 102, '0000000001', '0000000000'), 		('Superusuario', 103, '0000000001', '0000000000'), 		('Superusuario', 104, '0000000001', '0000000000'), 		('Superusuario', 105, '0000000001', '0000000000'), 		('Superusuario', 201, '0000000001', '0000000000'), 		('Superusuario', 202, '0000000001', '0000000000'), 		('Superusuario', 203, '0000000001', '0000000000'), 		('Superusuario', 204, '0000000001', '0000000000'), 		('Superusuario', 205, '0000000001', '0000000000'), 		('Superusuario', 206, '0000000001', '0000000000'), 		('Superusuario', 301, '0000000001', '0000000000'), 		('Superusuario', 302, '0000000001', '0000000000'), 		('Superusuario', 303, '0000000001', '0000000000'), 		('Superusuario', 304, '0000000001', '0000000000'), 		('Superusuario', 305, '0000000001', '0000000000'), 		('Superusuario', 306, '0000000001', '0000000000'), 		('Superusuario', 307, '0000000001', '0000000000'), 		('Superusuario', 308, '0000000001', '0000000000'), 		('Superusuario', 309, '0000000001', '0000000000'), 		('Superusuario', 310, '0000000001', '0000000000'), 		('Superusuario', 311, '0000000001', '0000000000'), 		('Superusuario', 312, '0000000001', '0000000000'), 		('Superusuario', 401, '0000000001', '0000000000'), 		('Superusuario', 101, '0000000001', '0000000001'), 		('Superusuario', 102, '0000000001', '0000000001'), 		('Superusuario', 103, '0000000001', '0000000001'), 		('Superusuario', 104, '0000000001', '0000000001'), 		('Superusuario', 105, '0000000001', '0000000001'), 		('Superusuario', 201, '0000000001', '0000000001'), 		('Superusuario', 202, '0000000001', '0000000001'), 		('Superusuario', 203, '0000000001', '0000000001'), 		('Superusuario', 204, '0000000001', '0000000001'), 		('Superusuario', 205, '0000000001', '0000000001'), 		('Superusuario', 206, '0000000001', '0000000001'), 		('Superusuario', 301, '0000000001', '0000000001'), 		('Superusuario', 302, '0000000001', '0000000001'), 		('Superusuario', 303, '0000000001', '0000000001'), 		('Superusuario', 304, '0000000001', '0000000001'), 		('Superusuario', 305, '0000000001', '0000000001'), 		('Superusuario', 306, '0000000001', '0000000001'), 		('Superusuario', 307, '0000000001', '0000000001'), 		('Superusuario', 308, '0000000001', '0000000001'), 		('Superusuario', 309, '0000000001', '0000000001'), 		('Superusuario', 310, '0000000001', '0000000001'), 		('Superusuario', 311, '0000000001', '0000000001'), 		('Superusuario', 312, '0000000001', '0000000001'), 		('Superusuario', 401, '0000000001', '0000000001'), 		('Superusuario', 101, '0000000001', '0000000002'), 		('Superusuario', 102, '0000000001', '0000000002'), 		('Superusuario', 103, '0000000001', '0000000002'), 		('Superusuario', 104, '0000000001', '0000000002'), 		('Superusuario', 105, '0000000001', '0000000002'), 		('Superusuario', 201, '0000000001', '0000000002'), 		('Superusuario', 202, '0000000001', '0000000002'), 		('Superusuario', 203, '0000000001', '0000000002'), 		('Superusuario', 204, '0000000001', '0000000002'), 		('Superusuario', 205, '0000000001', '0000000002'), 		('Superusuario', 206, '0000000001', '0000000002'), 		('Superusuario', 301, '0000000001', '0000000002'), 		('Superusuario', 302, '0000000001', '0000000002'), 		('Superusuario', 303, '0000000001', '0000000002'), 		('Superusuario', 304, '0000000001', '0000000002'), 		('Superusuario', 305, '0000000001', '0000000002'), 		('Superusuario', 306, '0000000001', '0000000002'), 		('Superusuario', 307, '0000000001', '0000000002'), 		('Superusuario', 308, '0000000001', '0000000002'), 		('Superusuario', 309, '0000000001', '0000000002'), 		('Superusuario', 310, '0000000001', '0000000002'), 		('Superusuario', 311, '0000000001', '0000000002'), 		('Superusuario', 312, '0000000001', '0000000002'), 		('Superusuario', 401, '0000000001', '0000000002'), 		('Superusuario', 101, '0000000001', '0000000003'), 		('Superusuario', 102, '0000000001', '0000000003'), 		('Superusuario', 103, '0000000001', '0000000003'), 		('Superusuario', 104, '0000000001', '0000000003'), 		('Superusuario', 105, '0000000001', '0000000003'), 		('Superusuario', 201, '0000000001', '0000000003'), 		('Superusuario', 202, '0000000001', '0000000003'), 		('Superusuario', 203, '0000000001', '0000000003'), 		('Superusuario', 204, '0000000001', '0000000003'), 		('Superusuario', 205, '0000000001', '0000000003'), 		('Superusuario', 206, '0000000001', '0000000003'), 		('Superusuario', 301, '0000000001', '0000000003'), 		('Superusuario', 302, '0000000001', '0000000003'), 		('Superusuario', 303, '0000000001', '0000000003'), 		('Superusuario', 304, '0000000001', '0000000003'), 		('Superusuario', 305, '0000000001', '0000000003'), 		('Superusuario', 306, '0000000001', '0000000003'), 		('Superusuario', 307, '0000000001', '0000000003'), 		('Superusuario', 308, '0000000001', '0000000003'), 		('Superusuario', 309, '0000000001', '0000000003'), 		('Superusuario', 310, '0000000001', '0000000003'), 		('Superusuario', 311, '0000000001', '0000000003'), 		('Superusuario', 312, '0000000001', '0000000003'), 		('Superusuario', 401, '0000000001', '0000000003'), 		('Estudiante', 401, '0000000001', '0000000000'), 		('Estudiante', 401, '0000000001', '0000000001'), 		('Estudiante', 401, '0000000001', '0000000002'), 		('Profesor', 201, '0000000001', '0000000002'), 		('Profesor', 202, '0000000001', '0000000002'), 		('Profesor', 204, '0000000001', '0000000002'), 		('Profesor', 206, '0000000001', '0000000002'), 		('Profesor', 201, '0000000002', '0000000002'), 		('Profesor', 204, '0000000002', '0000000002'), 		('Profesor', 206, '0000000002', '0000000002') 	) 	AS SOURCE ([Perfil], [PermisoId], [CodCarrera], [CodEnfasis]) 	ON Target.Perfil = SOURCE.Perfil AND Target.PermisoId = SOURCE.PermisoID AND Target.CodCarrera = SOURCE.CodCarrera AND Target.CodEnfasis = SOURCE.CodEnfasis 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Perfil, PermisoId, CodCarrera, CodEnfasis) 	VALUES (Perfil, PermisoId, CodCarrera, CodEnfasis); 	 MERGE INTO Funcionario AS Target 	USING (VALUES 		('kirsten@mail.com'), 		('tina@mail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);  MERGE INTO Profesor AS Target 	USING (VALUES 		('kirsten@mail.com'), 		('tina@mail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);   /* ************************************************************************************************* Formulario y preguntas */  MERGE INTO Formulario AS Target 	USING (VALUES 		('00000001', 'Formulario de prueba') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Seccion AS Target 	USING (VALUES 		('00000001', 'Información básica'), 		('00000002', 'Conocimientos básicos'), 		('00000003', 'Expectativas del curso') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Formulario_tiene_seccion AS Target 	USING (VALUES 		('00000001', '00000001', 0), 		('00000001', '00000002', 1), 		('00000001', '00000003', 2) 	) 	AS SOURCE ([FCodigo],[SCodigo],[Orden]) 	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo,SCodigo,Orden) 	VALUES (FCodigo,SCodigo,Orden);  MERGE INTO Pregunta AS Target 	USING (VALUES 		('00000001', '¿Lleva este curso por primera vez?', 'S'), 		('00000002', '¿Cuál es su experiencia con el uso de repositorios y control de versiones?', 'U'), 		('00000003', '¿Cuál de los siguientes lenguajes/tecnologías ha usado previamente?', 'M'), 		('00000004', '¿Qué calificación daría de manera general a sus conocimientos sobre programación de aplicaciones web?', 'E'), 		('00000005', '¿Qué expectativas tiene de este curso?', 'L') 	) 	AS SOURCE ([Codigo],[Enunciado], [Tipo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Enunciado, Tipo) 	VALUES (Codigo,Enunciado, Tipo);  MERGE INTO Seccion_tiene_pregunta AS Target 	USING (VALUES 		('00000001', '00000001', 0), 		('00000002', '00000002', 0), 		('00000002', '00000003', 1), 		('00000002', '00000004', 2), 		('00000003', '00000005', 0) 	) 	AS SOURCE ([SCodigo],[PCodigo],[Orden]) 	ON Target.SCodigo = Source.SCodigo and Target.PCodigo = Source.PCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SCodigo, PCodigo, Orden) 	VALUES (SCodigo, PCodigo, Orden);  MERGE INTO Pregunta_con_opciones AS Target 	USING (VALUES 		('00000001', 'Justificación'), 		('00000002', 'Justificación'), 		('00000003', 'Justificación'), 		('00000004', 'Justificación') 	) 	AS SOURCE ([Codigo],[TituloCampoObservacion]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, TituloCampoObservacion) 	VALUES (Codigo, TituloCampoObservacion);  MERGE INTO Pregunta_con_respuesta_libre AS Target 	USING (VALUES 		('00000005') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Si_no_nr AS Target 	USING (VALUES 		('00000001') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Pregunta_con_opciones_de_seleccion AS Target 	USING (VALUES 		('00000002'), 		('00000003') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Opciones_de_seleccion AS Target 	USING (VALUES 		('00000002', 0, 'Ninguna'), 		('00000002', 1, 'Poca'), 		('00000002', 2, 'Regular'), 		('00000002', 3, 'Mucha'), 		('00000003', 0, 'C#'), 		('00000003', 1, 'ADO.NET'), 		('00000003', 2, 'JavaScript'), 		('00000003', 3, 'HTML'), 		('00000003', 4, 'CSS'), 		('00000003', 5, 'SQL') 	) 	AS SOURCE ([Codigo], [Orden], [Texto]) 	ON Target.Codigo = Source.Codigo and Target.Orden = Source.Orden and Target.Texto = Source.Texto 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Orden, Texto) 	VALUES (Codigo, Orden, Texto);  MERGE INTO Escalar AS Target 	USING (VALUES 		('00000004', 1, 0, 9) 	) 	AS SOURCE ([Codigo], [Incremento], [Minimo], [Maximo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Incremento, Minimo, Maximo) 	VALUES (Codigo, Incremento, Minimo, Maximo);  /* ************************************************************************************************* Respuestas a un formulario */  MERGE INTO Respuestas_a_formulario AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10') 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha);   MERGE INTO Responde_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Ismael para la pregunta 1'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Ismael para la pregunta 2'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003','00000002', 'Justificación de Ismael para la pregunta 3'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Ismael para la pregunta 4'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Denisse para la pregunta 1'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Denisse para la pregunta 2'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Justificación de Denisse para la pregunta 3'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Denisse para la pregunta 4'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Daniel para la pregunta 1'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Daniel para la pregunta 2'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Justificación de Daniel para la pregunta 3'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Daniel para la pregunta 4'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Josué para la pregunta 1'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Josué para la pregunta 2'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Justificación de Josué para la pregunta 3'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Josué para la pregunta 4'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Berta para la pregunta 1'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Berta para la pregunta 2'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Justificación de Berta para la pregunta 3'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Berta para la pregunta 4'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Justificación de Andrés para la pregunta 1'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Justificación de Andrés para la pregunta 2'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Justificación de Andrés para la pregunta 3'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Justificación de Andrés para la pregunta 4') 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[Justificacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Justificacion) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Justificacion);  MERGE INTO Opciones_seleccionadas_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 1), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 1), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 2), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 1), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 3), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 2), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 1), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 2), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 1), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 5), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 1), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 5), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 2), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 5), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 5), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 8), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 3) 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[OpcionSeleccionada]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and 		Target.SCodigo = Source.SCodigo and Target.OpcionSeleccionada = Source.OpcionSeleccionada 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, OpcionSeleccionada) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, OpcionSeleccionada);  MERGE INTO Responde_respuesta_libre AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Ismael para la pregunta 5'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Denisse para la pregunta 5'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Daniel para la pregunta 5'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Josué para la pregunta 5'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Berta para la pregunta 5'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Respuesta de Andrés para la pregunta 5') 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[Observacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Observacion) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Observacion);  /* Se crean los formularios de prueba para el sprint con todos los tipos de pregunta */ EXEC dbo.PopularFormulariosDePrueba;  /* ************************************************************************************************* Planes de Mejora */  MERGE INTO TipoObjetivo AS Target 	USING (VALUES 		('Profesor'), 		('Curso'), 		('Infraestructura') 	) 	AS SOURCE ([nombre]) 	ON Target.nombre = Source.nombre 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (nombre) 	VALUES (nombre); 