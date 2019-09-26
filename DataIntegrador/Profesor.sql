CREATE TABLE [dbo].[Profesor]
(
	[Cedula] CHAR(10) NOT NULL PRIMARY KEY
	FOREIGN KEY (Cedula) REFERENCES Funcionario (Cedula)
)
