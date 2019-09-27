CREATE TABLE [dbo].[Curso]
(
	[Sigla] VARCHAR(10) NOT NULL PRIMARY KEY,
	[Nombre] VARCHAR(50) NOT NULL,
	[CodCarrera] VARCHAR(10),
	[CodEnfasis] VARCHAR(10),
	FOREIGN KEY (CodCarrera, CodEnfasis) REFERENCES Enfasis (CodCarrera, Codigo)
)
