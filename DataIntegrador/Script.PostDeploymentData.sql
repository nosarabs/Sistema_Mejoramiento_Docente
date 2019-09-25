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
/*INSERT INTO Pregunta (Codigo, Enunciado)
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