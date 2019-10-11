CREATE TABLE [dbo].[Pertenece_a]
(
	[CodCarrera] VARCHAR(10) NOT NULL,
	[CodEnfasis] VARCHAR(10) NOT NULL,
	[SiglaCurso] VARCHAR(10) NOT NULL,
	PRIMARY KEY (CodCarrera,CodEnfasis,SiglaCurso),
	FOREIGN KEY (CodCarrera, CodEnfasis) REFERENCES Enfasis (CodCarrera, Codigo),
	FOREIGN KEY (SiglaCurso) REFERENCES Curso (Sigla)
)
