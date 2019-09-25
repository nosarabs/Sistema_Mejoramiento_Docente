﻿/*
Deployment script for EvalDocente

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "EvalDocente"
:setvar DefaultFilePrefix "EvalDocente"
:setvar DefaultDataPath "C:\Users\marci\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\marci\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
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

MERGE INTO Curso AS Target 
USING (VALUES 
(1, 'Ingenieria de Software', 5), 
(2, 'Bases de Datos', 5), 
(3, 'Proyecto', 3) 
) 
AS Source ([CursoID], Titulo, Creditos) 
ON Target.CursoID = Source.CursoID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Titulo, Creditos) 
VALUES (Titulo, Creditos); 

MERGE INTO Estudiante AS Target 
USING (VALUES 
(1, 'Salas', 'Andrea', '2015-09-01'), 
(2, 'Guzman', 'Luis', '2016-01-13'), 
(3, 'Ramirez', 'Erick', '2017-09-03') 
) 
AS Source (EstudianteID, Apellido, Nombre, FechaMatricula) 
ON Target.EstudianteID = Source.EstudianteID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Apellido, Nombre, FechaMatricula) 
VALUES (Apellido, Nombre, FechaMatricula);

MERGE INTO Matricula AS Target 
USING (VALUES 
(1, 2.00, 1, 1), 
(2, 3.50, 1, 2), 
(3, 4.00, 2, 3),
(4, 1.80, 2, 1), 
(5, 3.20, 3, 1), 
(6, 4.00, 3, 2) 
) 
AS Source (MatriculaID, Nota, CursoID, EstudianteID) 
ON Target.MatriculaID = Source.MatriculaID 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nota, CursoID, EstudianteID) 
VALUES (Nota, CursoID, EstudianteID);

MERGE INTO UserProfile AS Target
USING (VALUES
  (1, 'admin', 'admin')  )  AS Source ([UserID], UserName, Password)  ON Target.UserID = Source.UserID  WHEN NOT MATCHED BY TARGET THEN
INSERT (UserName, Password)
VALUES (UserName, Password);
GO

GO
PRINT N'Update complete.';


GO
