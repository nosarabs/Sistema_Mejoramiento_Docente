CREATE TABLE [dbo].[Funcionario]
(
	[Correo] VARCHAR(50) NOT NULL PRIMARY KEY
	FOREIGN KEY (Correo) REFERENCES Persona (Correo)
)
