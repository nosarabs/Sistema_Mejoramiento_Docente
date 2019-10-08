CREATE TABLE [dbo].[Profesor]
(
	[Correo] VARCHAR(50) NOT NULL PRIMARY KEY
	FOREIGN KEY (Correo) REFERENCES Funcionario (Correo)
)
