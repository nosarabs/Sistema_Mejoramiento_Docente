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

MERGE INTO Usuario AS Target
USING (VALUES
  ('admin', 'admin')  )  AS Source (Username, Password)  ON Target.Username = Source.Username  WHEN NOT MATCHED BY TARGET THEN
INSERT (Password)
VALUES (Password);