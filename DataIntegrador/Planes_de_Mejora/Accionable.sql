CREATE TABLE [dbo].[Accionable]
(
	[Codigo] INT NOT NULL PRIMARY KEY,
	[Descripcion] NVARCHAR(500),
	[Progreso] INT NOT NULL CHECK(Progreso >=0 AND Progreso <= 100),
	[FechaInicio] DATE NOT NULL,
	[FechaFin] DATE NOT NULL
)
