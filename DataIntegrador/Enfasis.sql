﻿CREATE TABLE [dbo].[Enfasis]
(
	[CodCarrera] VARCHAR(10) NOT NULL,
	[Codigo] VARCHAR(10) NOT NULL,
	[Nombre] VARCHAR(50) NOT NULL,
	PRIMARY KEY (CodCarrera, Codigo),
	FOREIGN KEY (CodCarrera) REFERENCES Carrera (Codigo)
)
