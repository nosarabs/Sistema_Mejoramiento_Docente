CREATE TABLE [dbo].[Persona]
(
	[Cedula] CHAR(10) NOT NULL PRIMARY KEY, 
    [Correo] VARCHAR(50) NOT NULL UNIQUE, 
    [Nombre1] VARCHAR(15) NOT NULL, 
    [Nombre2] VARCHAR(15) NULL, 
    [Apellido1] VARCHAR(15) NOT NULL, 
    [Apellido2] VARCHAR(15) NULL, 
    [Usuario] NVARCHAR(50) NULL,
	FOREIGN KEY (Usuario) REFERENCES Usuario (Username)
)