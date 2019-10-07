﻿CREATE TABLE [dbo].[Accionable]
(
	[Codigo] INT NOT NULL 
		constraint PK_Accionable
			PRIMARY KEY, 
    [Descripcion] VARCHAR(500) NULL,
	Progreso real
		constraint ProgresoValue
			check(Progreso <= 100 and Progreso >= 100),
	FechaInicio date,
	FechaFin date
		constraint DateOrderAC
			check(FechaFin > FechaInicio),
	CodigoObj int NOT NULL
		constraint FK_AccionMejCod
			references Objetivo(Codigo)
)
