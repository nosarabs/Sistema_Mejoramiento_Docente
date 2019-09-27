CREATE TABLE [dbo].[Estudiante]
(
	[Cedula] CHAR(10) NOT NULL PRIMARY KEY, 
    [Carne] NCHAR(6) NOT NULL,
	FOREIGN KEY (Cedula) REFERENCES Persona (Cedula)
)