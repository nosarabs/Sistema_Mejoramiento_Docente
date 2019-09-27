/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*INSERT INTO Formulario (Codigo, Nombre)
VALUES
('11111', 'Formulario 1');

INSERT INTO Pregunta (Codigo, Enunciado)
VALUES
('55555555', 'Prgunta 1'), 
('44444444', 'Pregunta 2'), 
('88888888', 'Pregunta 3');

INSERT INTO Pregunta_con_opciones(Codigo, TituloCampoObservacion)
VALUES
('55555555', 'Titulo1'), 
('44444444', 'Titulo2'), 
('88888888', 'Titulo3');

INSERT INTO Escalar (Codigo, Incremento, Minimo, Maximo)
VALUES
('55555555', 1, 1, 10), 
('44444444', 2, 1, 10), 
('88888888', 4, 0, 9);*/

/*INSERT INTO Respuestas_a_formulario (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha)
VALUES 
('55555555', 'AMesen', 'CI1312', 1, 2017, 2, '2019-09-11'),
('44444444', 'DEscamilla', 'CI1312', 2, 2019, 2, '2018-09-11'),
('88888888', 'DAlfaro', 'CI1212', 5, 2011, 2, '2008-09-11');

INSERT INTO Formulario (Codigo, Nombre)
VALUES
('111111', 'Formulario4');

INSERT INTO Respuestas_a_formulario (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha)
VALUES
('111111', 'DAlfaro', 'CI1312', 1, 2019, 2, '2019-09-11');

INSERT INTO Responde_respuesta_con_opciones (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, Justificacion)
VALUES 
('111111', 'DAlfaro', 'CI1312', 1, 2019, 2, '2019-09-11', '55555555', 'Esta es una justificacion');

INSERT INTO Opciones_seleccionadas_respuesta_con_opciones (FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, OpcionSeleccionada)
VALUES
('111111', 'DAlfaro', 'CI1312', 1, 2019, 2, '2019-09-11', '55555555', 2);*/