CREATE TABLE [dbo].[Persona]
(
	[Correo] VARCHAR(50) NOT NULL PRIMARY KEY, 
	[CorreoAlt] VARCHAR(50) NULL,
	[Identificacion] VARCHAR(30) NOT NULL, 
    [Nombre1] VARCHAR(15) NOT NULL, 
    [Nombre2] VARCHAR(15) NULL, 
    [Apellido1] VARCHAR(15) NOT NULL, 
    [Apellido2] VARCHAR(15) NULL, 
    [Usuario] VARCHAR(50) NULL,
	[TipoIdentificacion] VARCHAR(30) NOT NULL, 
    FOREIGN KEY (Usuario) REFERENCES Usuario (Username)
)