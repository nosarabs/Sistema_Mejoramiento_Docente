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

MERGE INTO PlanMejora AS Target
USING (VALUES
 (1, 'Plan Prueba', '2019-06-06', '2019-12-06'),
 (2, 'Plan Prueba', '2019-09-25', '2020-03-20'),
 (3, 'Plan Prueba', '2019-07-01', '2019-10-03')
)AS Source ([Codigo], Nombre, FechaInicio, FechaFin)
ON Target.Codigo = Source.Codigo 
WHEN NOT MATCHED BY TARGET THEN
INSERT (Codigo, Nombre, FechaInicio, FechaFin)
VALUES (Codigo, Nombre, FechaInicio, FechaFin);