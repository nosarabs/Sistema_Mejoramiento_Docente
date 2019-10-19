﻿CREATE TABLE [dbo].[Estudiante]
(
	[Correo] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Carne] NCHAR(6) NULL,
	CONSTRAINT fkEstudentAPersonaCorreo FOREIGN KEY (Correo) REFERENCES Persona (Correo) ON UPDATE CASCADE ON DELETE CASCADE
)