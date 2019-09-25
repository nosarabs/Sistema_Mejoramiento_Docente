CREATE TABLE [dbo].[Accionable]
(
	[Codigo] INT NOT NULL PRIMARY KEY,
	[Descripcion] VARCHAR(500),
	[Progreso] INT NOT NULL,
	[FechaInicio] DATE NOT NULL,
	[FechaFin] DATE NOT NULL
)
