CREATE TABLE [dbo].[PlanDeMejora]
(
    [Codigo] INT NOT NULL PRIMARY KEY,
	Nombre varchar(30),
	FechaInicio date,
	FechaFin date
		constraint DateOrderPM
		check(FechaFin > FechaInicio)
)
