CREATE TABLE [dbo].[Accion_De_Mejora]
(
	[Codigo] INT NOT NULL PRIMARY KEY,
	[Descripcion] VARCHAR(500),
	[FechaInicio] DATE NOT NULL,
	[FechaFin] DATE NOT NULL
)
