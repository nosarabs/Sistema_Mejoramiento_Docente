CREATE TABLE [dbo].[AccionDeMejora]
(
	[Codigo] INT NOT NULL 
		constraint PK_AccionDeMejora
			PRIMARY KEY, 
    [Descripcion] VARCHAR(500) NULL,
	FechaInicio date,
	FechaFin date
		constraint DateOrderAM
			check(FechaFin > FechaInicio)
)
