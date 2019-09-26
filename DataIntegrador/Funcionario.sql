CREATE TABLE [dbo].[Funcionario]
(
	[Cedula] CHAR(10) NOT NULL PRIMARY KEY
	FOREIGN KEY (Cedula) REFERENCES Persona (Cedula)
)
