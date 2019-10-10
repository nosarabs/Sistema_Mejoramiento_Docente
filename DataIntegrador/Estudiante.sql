CREATE TABLE [dbo].[Estudiante]
(
	[Correo] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Carne] NCHAR(6) NULL UNIQUE,
	FOREIGN KEY (Correo) REFERENCES Persona (Correo)
)