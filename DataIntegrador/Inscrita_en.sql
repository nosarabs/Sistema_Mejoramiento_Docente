CREATE TABLE [dbo].[Inscrita_en]
(
	[CodUnidadAc] VARCHAR(10) NOT NULL,
	[CodCarrera] VARCHAR(10) NOT NULL,
	PRIMARY KEY (CodUnidadAc, CodCarrera),
	FOREIGN KEY (CodUnidadAc) REFERENCES UnidadAcademica (Codigo),
	FOREIGN KEY (CodCarrera) REFERENCES Carrera (Codigo)
)
