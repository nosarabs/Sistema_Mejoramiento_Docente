CREATE TABLE [dbo].[Empadronado_en]
(
	[CedEstudiante] CHAR(10) NOT NULL,
	[CodCarrera] VARCHAR(10) NOT NULL,
	[CodEnfasis] VARCHAR(10) NOT NULL,
	PRIMARY KEY (CedEstudiante, CodCarrera, CodEnfasis),
	FOREIGN KEY (CedEstudiante) REFERENCES Estudiante (Cedula),
	FOREIGN KEY (CodCarrera, CodEnfasis) REFERENCES Enfasis (CodCarrera, Codigo)
)
