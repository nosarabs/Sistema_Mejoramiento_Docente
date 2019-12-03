﻿/* Post-Deployment Script Template							 --------------------------------------------------------------------------------------  This file contains SQL statements that will be appended to the build script.		  Use SQLCMD syntax to include a file in the post-deployment script.			  Example:      :r .\myfile.sql								  Use SQLCMD syntax to reference a variable in the post-deployment script.		  Example:      :setvar TableName MyTable							                SELECT * FROM [$(TableName)]					 -------------------------------------------------------------------------------------- */  /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tamales  ************************************************************************************************* Agregar personas, usuarios, perfiles y permisos primero */ 	MERGE INTO Persona AS Target 	USING (VALUES 		('123456789', 'ismael@mail.com', 'Ismael', null, 'Gutiérrez', null,'Cédula'), 		('123456788', 'denisse@mail.com', 'Denisse', null, 'Alfaro', null,'Cédula'), 		('123456787', 'daniel@mail.com', 'Daniel', null, 'Escamilla', null,'Cédula'), 		('123456786', 'josue@mail.com', 'Josué', null, 'Zeledón', null,'Cédula'), 		('123456785', 'berta@mail.com', 'Berta', null, 'Sánchez', null,'Cédula'), 		('123456784', 'andres@mail.com', 'Andrés', null, 'Mesén', null, 'Cédula'), 		('123456783', 'admin@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('111111111', 'tamales@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('222222222', 'rip@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('333333333', 'bakers@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('444444444', 'mosqueteros@mail.com', 'Admin', null, 'Admin', null, 'Cédula'), 		('696969696', 'paco@mail.com', 'Paco', null, 'Brenes', null,'Cédula'), 		('424242424', 'lola@mail.com', 'Lola', null, 'Ramírez', null,'Cédula') 	) 	AS SOURCE ([Identificacion], [Correo], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [TipoIdentificacion]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion) 	VALUES (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion);  MERGE INTO Persona AS Target 	USING (VALUES 		('123456782', 'kirsten@mail.com', 'Kirsten', null, 'Dunst', null, 'Cédula'), 		('123456781', 'tina@mail.com', 'Tina', null, 'Fey', null, 'Cédula'), 		('123456780', 'ericrios24@gmail.com', 'Eric', null, 'Rios', 'Morales', 'Cédula'), 		('106670848', 'cristian.quesadalopez@ucr.ac.cr', 'Cristian', 'Ulises', 'Quesada', 'Lopez', 'Cédula'), 		('106670849', 'alexandra.martinez@ucr.ac.cr', 'Alexandra', null, 'Martínez', null, 'Cédula'), 		('106670850', 'marcelo.jenkins@ucr.ac.cr', 'Marcelo', null, 'Jenkins', null, 'Cédula'), 		('987654321', 'christian.asch4@gmail.com', 'Christian', 'Ariel', 'Asch', 'Burgos', 'Cédula') 	) 	AS SOURCE ([Identificacion], [Correo], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [TipoIdentificacion]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion) 	VALUES (Identificacion, Correo, Nombre1, Nombre2, Apellido1, Apellido2, TipoIdentificacion);  MERGE INTO Permiso AS Target 	USING (VALUES 		(101, 'Ver usuarios'), 		(102, 'Crear usuarios'), 		(103, 'Ver detalles de usuarios'), 		(104, 'Editar usuarios'), 		(105, 'Borrar usuarios'), 		(106, 'Ver administración de perfiles y permisos'), 		(107, 'Asignar permisos a perfiles'), 		(108, 'Asignar perfiles a usuarios'), 		(201, 'Crear formularios'), 		(202, 'Ver formularios'), 		(203, 'Crear secciones'), 		(204, 'Ver secciones'), 		(205, 'Crear preguntas'), 		(206, 'Ver preguntas'), 		(301, 'Ver planes de mejora'), 		(302, 'Crear planes de mejora'), 		(303, 'Editar planes de mejora'), 		(304, 'Borrar planes de mejora'), 		(305, 'Crear objetivos'), 		(306, 'Ver objetivos'), 		(307, 'Editar objetivos'), 		(308, 'Borrar objetivos'), 		(309, 'Crear acciones de mejora'), 		(310, 'Ver acciones de mejora'), 		(311, 'Editar acciones de mejora'), 		(312, 'Borrar acciones de mejora'), 		(401, 'Ver respuestas de mis formularios'), 		(402, 'Ver respuestas de formularios en el énfasis'), 		(501, 'Cargar datos desde CSV') 	) 	AS SOURCE([Id], Descripcion) 	ON Target.Id = SOURCE.Id 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Id, Descripcion) 	VALUES (Id, Descripcion);  MERGE INTO Perfil AS TARGET 	USING (VALUES 		('Superusuario'), 		('Administrador'), 		('Administrativo'), 		('Director'), 		('Profesor'), 		('Estudiante'), 		('Coordinador de núcleo'), 		('Coordinador de énfasis'), 		('Coordinador de comisión de docencia') 	) 	AS SOURCE([Nombre]) 	ON Target.Nombre = SOURCE.Nombre 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Nombre) 	VALUES (Nombre);  /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// CodeBeakers  ************************************************************************************************* Unidad académica, carrera, énfasis, curso y grupo */  MERGE INTO UnidadAcademica AS Target 	USING (VALUES  		('0000000001', 'ECCI', NULL), 		('0000000002', 'Escuela de Ingeniería Eléctrica', NULL) 	) 	AS SOURCE ([Codigo],[Nombre],[Superior]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre,Superior) 	VALUES (Codigo,Nombre,Superior);  MERGE INTO Carrera AS Target 	USING (VALUES 		('0000000001', 'Bachillerato en Computación'), 		('0000000002', 'Ingeniería Eléctrica') 	) 	AS SOURCE ([Codigo], [Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Inscrita_en AS Target 	USING (VALUES 		('0000000001', '0000000001'), 		('0000000002', '0000000002') 	) 	AS SOURCE ([CodUnidadAc],[CodCarrera]) 	ON Target.CodUnidadAc = Source.CodUnidadAc and Target.CodCarrera = Source.CodCarrera 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodUnidadAc,CodCarrera) 	VALUES (CodUnidadAc,CodCarrera);  MERGE INTO Enfasis AS Target 	USING (VALUES 		('0000000001', '0000000000', 'Tronco Común'), 		('0000000001', '0000000001', 'Ingeniería de Software'), 		('0000000001', '0000000002', 'Ciencias de la Computación'), 		('0000000001', '0000000003', 'Tecnologías de la Información'), 		('0000000002', '0000000000', 'Tronco Común'), 		('0000000002', '0000000001', 'Computadores y Redes'), 		('0000000002', '0000000002', 'Electrónica y Telecomunicaciones'), 		('0000000002', '0000000003', 'Sistemas de energía') 	) 	AS SOURCE ([CodCarrera],[Codigo],[Nombre]) 	ON Target.CodCarrera = Source.CodCarrera and Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,Codigo,Nombre) 	VALUES (CodCarrera,Codigo,Nombre);  MERGE INTO Curso AS Target 	USING (VALUES 		('CI0128', 'Proyecto Integrador de BD e IS'), 		('CI0127', 'Bases de Datos'), 		('CI0126', 'Ingeniería de Software'), 		('MA1001', 'Cálculo I') 	) 	AS SOURCE ([Sigla],[Nombre]) 	ON Target.Sigla = Source.Sigla 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Sigla,Nombre) 	VALUES (Sigla,Nombre);  MERGE INTO Pertenece_a AS Target 	USING (VALUES 		('0000000001', '0000000000', 'CI0128'), 		('0000000001', '0000000000', 'CI0127'), 		('0000000001', '0000000000', 'CI0126'), 		('0000000002', '0000000000', 'MA1001') 	) 	AS SOURCE ([CodCarrera],[CodEnfasis],[SiglaCurso]) 	ON Target.CodCarrera = Source.CodCarrera and Target.CodEnfasis = Source.CodEnfasis and Target.SiglaCurso = Source.SiglaCurso 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CodCarrera,CodEnfasis,SiglaCurso) 	VALUES (CodCarrera,CodEnfasis,SiglaCurso);  MERGE INTO Grupo AS Target 	USING (VALUES 		('CI0128', 1, 2, 2019), 		('CI0128', 1, 1, 2019), 		('CI0128', 1, 2, 2018), 		('CI0128', 1, 1, 2018), 		('CI0128', 1, 2, 2017), 		('CI0128', 1, 1, 2017), 		('CI0126', 1, 2, 2019), 		('CI0127', 1, 2, 2019), 		('MA1001', 1, 2, 2019), 		('MA1001', 2, 1, 2019) 	) 	AS SOURCE ([SiglaCurso],[NumGrupo],[Semestre],[Anno]) 	ON Target.SiglaCurso = Source.SiglaCurso and Target.NumGrupo = Source.NumGrupo and Target.Semestre = Source.Semestre and Target.Anno = Source.Anno 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SiglaCurso,NumGrupo,Semestre,Anno) 	VALUES (SiglaCurso,NumGrupo,Semestre,Anno);  /*Pruebas cursores*/ /*INSERT INTO Carrera VALUES ('101010', 'Informatica'), 	   ('202020', 'Ingenieria electrica'), 	   ('303030', 'Ingenieria mecanica'), 	   ('404040', 'Farmacia')  INSERT INTO Curso VALUES ('101010', 'HCI'), 	   ('202020', 'Desarrollo web'), 	   ('1001', 'Calculo 1'), 	   ('1002', 'Calculo 2')  INSERT INTO Enfasis VALUES ('101010', '101010', 'TI'), 	   ('101010', '202020', 'Ingenieria de software'), 	   ('202020', '101010', 'Potencia'), 	   ('303030', '101010', 'Redes')  INSERT INTO Estudiante VALUES ('ismael@mail.com', 'B6789'), 	   ('denisse@mail.com', 'B50220'), 	   ('daniel@mail.com', 'B55736'), 	   ('josue@mail.com', 'B76543')  INSERT INTO Funcionario VALUES ('berta@mail.com'), 	   ('andres@mail.com'), 	   ('admin@mail.com'), 	   ('tamales@mail.com')  INSERT INTO Profesor VALUES ('berta@mail.com'), 	   ('andres@mail.com'), 	   ('admin@mail.com'), 	   ('tamales@mail.com')  INSERT INTO UnidadAcademica VALUES ('101010', 'Ingenieria', NULL),        ('202020', 'Medicina', NULL), 	   ('303030', 'Letras', NULL), 	   ('404040', 'Matematicas', NULL)  INSERT INTO Grupo VALUES ('101010', 1, 1, 2019),        ('101010', 2, 1, 2019), 	   ('101010', 1, 2, 2019), 	   ('101010', 2, 3, 2019)  INSERT INTO Imparte VALUES ('berta@mail.com', '101010', 1, 1, 2019), 	   ('andres@mail.com', '101010', 2, 1, 2019), 	   ('admin@mail.com', '101010', 1, 2, 2019)  INSERT INTO Pertenece_a VALUES ('101010', '101010', '101010'), 	   ('202020', '101010', '1001'), 	   ('303030', '101010', '1002')  INSERT INTO Trabaja_en VALUES ('berta@mail.com', '101010'),        ('andres@mail.com', '202020'), 	   ('admin@mail.com', '101010')  INSERT INTO Inscrita_en VALUES ('101010', '202020'),        ('101010', '101010'), 	   ('202020', '404040')  INSERT INTO Matriculado_en VALUES ('ismael@mail.com', '101010', 1, 1, 2019),        ('denisse@mail.com', '101010', 1, 1, 2019), 	   ('daniel@mail.com', '101010', 1, 2, 2019)*/ /* //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Tamales ************************************************************************************************* Asignación permisos y perfiles */  MERGE INTO UsuarioPerfil AS Target 	USING (VALUES 		('andres@mail.com', 'Estudiante', '0000000001', '0000000001'), 		('andres@mail.com', 'Profesor', '0000000001', '0000000002'), 		('andres@mail.com', 'Profesor', '0000000002', '0000000002'), 		('alexandra.martinez@ucr.ac.cr', 'Profesor', '0000000001', '0000000000'), 		('ericrios24@gmail.com', 'Estudiante', '0000000001', '0000000002'), 		('ismael@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('denisse@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('daniel@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('josue@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('berta@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('kirsten@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('tina@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('paco@mail.com', 'Estudiante', '0000000001', '0000000000'), 		('lola@mail.com', 'Estudiante', '0000000002', '0000000000') 	) 	AS SOURCE ([Usuario], [Perfil], [CodCarrera], [CodEnfasis]) 	ON Target.Usuario = SOURCE.Usuario AND Target.Perfil = SOURCE.Perfil AND Target.CodCarrera = SOURCE.CodCarrera AND Target.CodEnfasis = SOURCE.CodEnfasis 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Usuario, Perfil, CodCarrera, CodEnfasis) 	VALUES (Usuario, Perfil, CodCarrera, CodEnfasis); 	 MERGE INTO PerfilPermiso AS Target 	USING (VALUES 		('Estudiante', 401, '0000000001', '0000000000'), 		('Estudiante', 401, '0000000001', '0000000001'), 		('Estudiante', 401, '0000000001', '0000000002'), 		('Profesor', 201, '0000000001', '0000000002'), 		('Profesor', 202, '0000000001', '0000000002'), 		('Profesor', 204, '0000000001', '0000000002'), 		('Profesor', 206, '0000000001', '0000000002'), 		('Profesor', 201, '0000000002', '0000000002'), 		('Profesor', 204, '0000000002', '0000000002'), 		('Profesor', 206, '0000000002', '0000000002') 	) 	AS SOURCE ([Perfil], [PermisoId], [CodCarrera], [CodEnfasis]) 	ON Target.Perfil = SOURCE.Perfil AND Target.PermisoId = SOURCE.PermisoID AND Target.CodCarrera = SOURCE.CodCarrera AND Target.CodEnfasis = SOURCE.CodEnfasis 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Perfil, PermisoId, CodCarrera, CodEnfasis) 	VALUES (Perfil, PermisoId, CodCarrera, CodEnfasis); 	 MERGE INTO Funcionario AS Target 	USING (VALUES 		('cristian.quesadalopez@ucr.ac.cr'), 		('alexandra.martinez@ucr.ac.cr'), 		('marcelo.jenkins@ucr.ac.cr'), 		('kirsten@mail.com'), 		('tina@mail.com'), 		('christian.asch4@gmail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);  MERGE INTO Profesor AS Target 	USING (VALUES 		('cristian.quesadalopez@ucr.ac.cr'), 		('alexandra.martinez@ucr.ac.cr'), 		('marcelo.jenkins@ucr.ac.cr'), 		('christian.asch4@gmail.com') 	) 	AS SOURCE ([Correo]) 	ON Target.Correo = Source.Correo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo) 	VALUES (Correo);  	MERGE INTO Imparte AS Target 	USING (VALUES 		('cristian.quesadalopez@ucr.ac.cr', 'CI0128', 1, 2, 2019), 		('cristian.quesadalopez@ucr.ac.cr', 'CI0126', 1, 2, 2019), 		('alexandra.martinez@ucr.ac.cr', 'CI0128', 1, 2, 2019), 		('alexandra.martinez@ucr.ac.cr', 'CI0127', 1, 2, 2019), 		('marcelo.jenkins@ucr.ac.cr', 'CI0128', 1, 2, 2019), 		('christian.asch4@gmail.com', 'MA1001', 1, 2, 2019) 	) 	AS SOURCE (CorreoProfesor, SiglaCurso, NumGrupo, Semestre, Anno) 	ON Target.CorreoProfesor = Source.CorreoProfesor AND Target.SiglaCurso = Source.SiglaCurso AND Target.NumGrupo = Source.NumGrupo AND Target.Semestre = Source.Semestre AND Target.Anno = Source.Anno 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CorreoProfesor, SiglaCurso, NumGrupo, Semestre, Anno) 	VALUES (CorreoProfesor, SiglaCurso, NumGrupo, Semestre, Anno); MERGE INTO Estudiante AS Target 	USING (VALUES 		('paco@mail.com', 'C73322'), 		('lola@mail.com', 'C74433') 	) 	AS SOURCE ([Correo], [Carne]) 	ON Target.Correo = Source.Correo AND Target.Carne = Source.Carne 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Correo, Carne) 	VALUES (Correo, Carne);  /* ************************************************************************************************* Formulario y preguntas */  MERGE INTO Formulario AS Target 	USING (VALUES 		('00000001', 'Formulario de PI de BD e IS') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Seccion AS Target 	USING (VALUES 		('00000001', 'Información básica'), 		('00000002', 'Conocimientos básicos'), 		('00000003', 'Expectativas del curso'), 		('00000004', 'Calificación del curso y profesor') 	) 	AS SOURCE ([Codigo],[Nombre]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Nombre) 	VALUES (Codigo,Nombre);  MERGE INTO Formulario_tiene_seccion AS Target 	USING (VALUES 		('00000001', '00000001', 0), 		('00000001', '00000002', 1), 		('00000001', '00000003', 2) 	) 	AS SOURCE ([FCodigo],[SCodigo],[Orden]) 	ON Target.FCodigo = Source.FCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo,SCodigo,Orden) 	VALUES (FCodigo,SCodigo,Orden);  MERGE INTO Pregunta AS Target 	USING (VALUES 		('00000001', '¿Lleva este curso por primera vez?', 'S'), 		('00000002', '¿Cuál es su experiencia con el uso de repositorios y control de versiones?', 'U'), 		('00000003', '¿Cuál de los siguientes lenguajes/tecnologías ha usado previamente?', 'M'), 		('00000004', '¿Qué calificación daría de manera general a sus conocimientos sobre programación de aplicaciones web?', 'E'), 		('00000005', '¿Qué expectativas tiene de este curso?', 'L'), 		('00000006', '¿Qué tan frecuentemente trabaja en equipo?', 'U'), 		('00000007', '¿Ha hecho uso alguna vez de metodologías ágiles para un proceso de desarrollo de software?', 'S'), 		('INFPROF', '¿Qué calificación le daría a los profesores de este curso?', 'E'), 		('INFCURSO', '¿Qué calificación le daría a este curso?', 'E') 	) 	AS SOURCE ([Codigo],[Enunciado], [Tipo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo,Enunciado, Tipo) 	VALUES (Codigo,Enunciado, Tipo);  MERGE INTO Seccion_tiene_pregunta AS Target 	USING (VALUES 		('00000001', '00000001', 0), 		('00000002', '00000006', 0), 		('00000002', '00000007', 1), 		('00000002', '00000002', 2), 		('00000002', '00000003', 3), 		('00000002', '00000004', 4), 		('00000003', '00000005', 0), 		('00000004', 'INFPROF', 0), 		('00000004', 'INFCURSO', 1) 	) 	AS SOURCE ([SCodigo],[PCodigo],[Orden]) 	ON Target.SCodigo = Source.SCodigo and Target.PCodigo = Source.PCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (SCodigo, PCodigo, Orden) 	VALUES (SCodigo, PCodigo, Orden);  MERGE INTO Pregunta_con_opciones AS Target 	USING (VALUES 		('00000001', 'Si lleva este curso de nuevo, justifique por qué'), 		('00000002', 'Justifique su respuesta'), 		('00000003', 'Si ha hecho uso de alguna, de ejemplos del cómo'), 		('00000004', 'Justifique su respuesta'), 		('00000006', 'Ejemplifique su experiencia de trabajo en equipo'), 		('00000007', 'Justifique su respuesta'), 		('INFPROF', 'Justifique su calificación a el o los profesores'), 		('INFCURSO', 'Justifique su calificación al curso') 	) 	AS SOURCE ([Codigo],[TituloCampoObservacion]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, TituloCampoObservacion) 	VALUES (Codigo, TituloCampoObservacion);  MERGE INTO Pregunta_con_respuesta_libre AS Target 	USING (VALUES 		('00000005') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Si_no_nr AS Target 	USING (VALUES 		('00000001'), 		('00000007') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Pregunta_con_opciones_de_seleccion AS Target 	USING (VALUES 		('00000002'), 		('00000003'), 		('00000006') 	) 	AS SOURCE ([Codigo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo) 	VALUES (Codigo);  MERGE INTO Opciones_de_seleccion AS Target 	USING (VALUES 		('00000002', 0, 'Ninguna'), 		('00000002', 1, 'Poca'), 		('00000002', 2, 'Regular'), 		('00000002', 3, 'Mucha'), 		('00000003', 0, 'C#'), 		('00000003', 1, 'ADO.NET'), 		('00000003', 2, 'JavaScript'), 		('00000003', 3, 'HTML'), 		('00000003', 4, 'CSS'), 		('00000003', 5, 'SQL'), 		('00000006', 0, 'Nunca'), 		('00000006', 1, 'A veces'), 		('00000006', 2, 'Regularmente'), 		('00000006', 3, 'Siempre') 	) 	AS SOURCE ([Codigo], [Orden], [Texto]) 	ON Target.Codigo = Source.Codigo and Target.Orden = Source.Orden and Target.Texto = Source.Texto 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Orden, Texto) 	VALUES (Codigo, Orden, Texto);  MERGE INTO Escalar AS Target 	USING (VALUES 		('00000004', 1, 0, 9), 		('INFPROF', 1, 0, 10), 		('INFCURSO', 1, 0, 10) 	) 	AS SOURCE ([Codigo], [Incremento], [Minimo], [Maximo]) 	ON Target.Codigo = Source.Codigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (Codigo, Incremento, Minimo, Maximo) 	VALUES (Codigo, Incremento, Minimo, Maximo);  /* ************************************************************************************************* Matrículas */   MERGE INTO Matriculado_en AS Target 	USING (VALUES 		('paco@mail.com', 'CI0128', 1, 2, 2019), 		('lola@mail.com', 'CI0128', 1, 1, 2019), 		('paco@mail.com', 'CI0128', 1, 2, 2018), 		('lola@mail.com', 'CI0128', 1, 1, 2018), 		('paco@mail.com', 'CI0128', 1, 2, 2017), 		('lola@mail.com', 'CI0128', 1, 1, 2017) 	) 	AS SOURCE ([CorreoEstudiante], [SiglaCurso], [NumGrupo], [Semestre], [Anno]) 	ON Target.CorreoEstudiante = Source.CorreoEstudiante AND Target.SiglaCurso = Source.SiglaCurso AND Target.NumGrupo = Source.NumGrupo AND 	Target.Semestre = Source.Semestre AND Target.Anno = Source.Anno 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (CorreoEstudiante, SiglaCurso, NumGrupo, Semestre, Anno) 	VALUES (CorreoEstudiante, SiglaCurso, NumGrupo, Semestre, Anno);  /* ************************************************************************************************* Respuestas a un formulario */  MERGE INTO Activa_por AS Target
	USING (VALUES
		('00000001', 'CI0128', 1, 2019, 2)
	)
	AS SOURCE (FCodigo, CSigla, GNumero, GAnno, GSemestre)
	ON Target.FCodigo = Source.FCodigo AND Target.CSigla = Source.CSigla AND Target.GNumero = Source.GNumero AND Target.GAnno = Source.GAnno AND Target.GSemestre = Source.GSemestre
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo, CSigla, GNumero, GAnno, GSemestre)
	VALUES (FCodigo, CSigla, GNumero, GAnno, GSemestre);

MERGE INTO Periodo_activa_por AS Target
	USING (VALUES
		('00000001', 'CI0128', 1, 2019, 2, '2019-06-09', '2019-06-12')
	)
	AS SOURCE (FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin)
	ON Target.FCodigo = Source.FCodigo AND Target.CSigla = Source.CSigla AND Target.GNumero = Source.GNumero AND Target.GAnno = Source.GAnno AND Target.GSemestre = Source.GSemestre AND Target.FechaInicio = Source.FechaInicio AND Target.FechaFin = Source.FechaFin
	WHEN NOT MATCHED BY TARGET THEN
	INSERT (FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin)
	VALUES (FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin);  MERGE INTO Respuestas_a_formulario AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', 1) 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[Finalizado]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, Finalizado) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, Finalizado);   MERGE INTO Responde_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Porque es la primera vez que llevo el curso'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Porque no tengo mucha experiencia con el uso de repositorios. Se supone que deberíamos haberlos usado en el curso de programación pero no fue así, al final un compañero me enseñó a utilizarlos el semestre pasado en el curso de ensambla'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003','00000002', 'Porque utilicé Javascript para programar en vacaciones y es bastante fácil de usar'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque nunca he hecho programación web y siento que no sé casi nada'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque nunca he tenido que trabajar en equipo, en mis cursos todos los proyectos han sido individuales'), 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'Porque ni siquiera sé qué son metodologías ágiles'),  		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Porque estoy llevando este curso por primera vez'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'No estoy muy segura de para qué sirven los repositorios. He escuchado a compañeros mencionar qué son, pero no los he utilizado nunca'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Porque en el curso de desarrollo web utilizamos C# y ADO.NET y me me siento bastante cómoda con la tecnología'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque llevé el curso de programación web, siento que aprendimos algo pero no mucho'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque hay cursos en los que he trabajado en equipo, pero no han sido muchos'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'No sé qué es eso'),  		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Porque lo llevé el año pasado en el segundo semestre, pero tuve una emergencia familiar y tuve que abandonar el curso'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Porque he utilizado repositorios desde Progra I y los he utilizado en todos los cursos de la carrera que involucran programción'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Porque cuando lleve el curso el semestre pasado, yo me encargué de toda la parte de la base de datos y de la conexión con esta desde los controladores'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque considero que sé bastante de programación web'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque me gusta el trabajo en equipo y en todos mis cursos propongo que trabajemos en equipo'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'Porque en los cursos anteriores hemos utilizado principios de metodologías ágiles'),  		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Porque di, nunca lo he llevado antes'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Porque los utilicé en mis cursos de programación'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Porque usamos Html y Css para el curso de programación web'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque siento que sé bastante de programación web, pero tampoco soy un experto'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque para los cursos de progra tuve que trabajar en grupo pero aparte de eso no he tenido que volver a trabajar en grupo'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'He escuchado de ellas, pero nunca las hemos utilizado en ningún curso'),  		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'Porque lo lleve el semestre pasado pero me tocó un grupo de compañeros que no trabajaban mucho e injustamente me calificaron mal por culpa de ellos'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Porque los usamos un poco en Progra II, pero no los he usado desde entonces'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Porque aprendí a utilizar todas las tecnologías por mi cuenta y de hecho estoy trabajando en varios proyectos extracurriculares de desarrollo web en dónde las utilizo frecuentemente'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque soy una crack en programación web'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque como dije anteriormente, estoy acostumbrada al trabajo en equipo y tengo varios proyectos extracurriculares de desarrollo de software en los cuales tengo que trabajar en equipo todo el tiempo'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'Porque en mis proyectos de extracurriculares de desarrollo web todo se realiza bajo metodologías ágiles'),  		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 'No me siento cómodo respondiendo esa pregunta ni tampoco justificando mi respuesta'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 'Porque los he usado en mis cursos de progra, pero no en otros cursos'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 'Porque las utilizamos para el proyecto del curso de desarrollo web'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 'Porque siento que el profe de programación web era muy malo y no aprendí mucho que digamos'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 'Porque he trabajado en equipo para algunos cursos pero no en todos'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 'Porque nunca había escuchado de ellas')  	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[Justificacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Justificacion) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Justificacion);  MERGE INTO Opciones_seleccionadas_respuesta_con_opciones AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 1), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 0), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 1), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000001', '00000001', 2),  		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 1), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 3), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 2), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 1), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000002', '00000002', 2),  		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 1), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 5), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 0), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 1), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 5), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 2), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 3), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000003', '00000002', 4),  		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 2), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 5), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 5), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 8), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000004', '00000002', 3),  		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 0), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 1), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 3), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 2), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 3), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000006', '00000002', 2),  		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 1), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 1), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 0), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 1), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 0), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000007', '00000002', 1) 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[OpcionSeleccionada]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and 		Target.SCodigo = Source.SCodigo and Target.OpcionSeleccionada = Source.OpcionSeleccionada 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, OpcionSeleccionada) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, OpcionSeleccionada);  MERGE INTO Responde_respuesta_libre AS Target 	USING (VALUES 		('00000001', 'ismael@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Ojalá que para este proyecto integrador los profesores coordinen bien y que no sucedan cosas negativas que han sucesido en otros proyectos integradores en semestres anteriores'), 		('00000001', 'denisse@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Que aprendamos bastante de progamación web y que aprendamos a trabajar en proyectos más complejos y grandes de desarrollo de software'), 		('00000001', 'daniel@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Que el curso no consuma tanto tiempo como los proyectos integradores anteriores'), 		('00000001', 'josue@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Que los profesores se lleven bien para que el curso esté bien organizado y sea agradable'), 		('00000001', 'berta@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Aprender sobre nuevas tecnologías de desarrollo web, mejorar mi experiencia con metodologías ágiles'), 		('00000001', 'andres@mail.com', 'CI0128', 1, 2019, 2, '2019-06-10', '00000005', '00000003', 'Que el curso tenga una carga de trabajo razonable dado que tenemos otros cursos de énfasis en este nivel del plan de estudios') 	) 	AS SOURCE ([FCodigo], [Correo],[CSigla],[GNumero],[GAnno],[GSemestre],[Fecha],[PCodigo],[SCodigo],[Observacion]) 	ON Target.FCodigo = Source.FCodigo and Target.Correo = Source.Correo and Target.CSigla = Source.CSigla and Target.GNumero = Source.GNumero and 		Target.GAnno = Source.GAnno and Target.GSemestre = Source.GSemestre and Target.Fecha = Source.Fecha and Target.PCodigo = Source.PCodigo and Target.SCodigo = Source.SCodigo 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Observacion) 	VALUES (FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, SCodigo, Observacion);  /* Se crean los formularios de prueba para el sprint con todos los tipos de pregunta */ EXEC dbo.PopularFormulariosDePrueba;  /* ************************************************************************************************* Planes de Mejora */  MERGE INTO TipoObjetivo AS Target 	USING (VALUES 		('Profesor'), 		('Curso'), 		('Infraestructura') 	) 	AS SOURCE ([nombre]) 	ON Target.nombre = Source.nombre 	WHEN NOT MATCHED BY TARGET THEN 	INSERT (nombre) 	VALUES (nombre);   EXEC dbo.PruebasPDM 