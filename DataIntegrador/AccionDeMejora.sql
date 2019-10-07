CREATE TABLE [dbo].[AccionDeMejora]
(
	[Codigo] INT NOT NULL 
		constraint PK_AccionDeMejora
			PRIMARY KEY, 
    [Descripcion] VARCHAR(500) NULL,
	FechaInicio date,
	FechaFin date
		constraint DateOrderAM
			check(FechaFin > FechaInicio),
	CodigoObj int NOT NULL
		constraint FK_PlanDeMejoraCod
			references Objetivo(Codigo)
)
