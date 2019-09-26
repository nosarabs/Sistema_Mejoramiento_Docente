/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
DROP TABLE Compuesto_Por
DROP TABLE Asociado_A
DROP TABLE Evaluado_Por
DROP TABLE Responsable_De
DROP TABLE Accionable
DROP TABLE Objetivo
DROP TABLE Tipo_Objetivo
DROP TABLE PlanMejora
DROP TABLE Accion_De_Mejora
*/