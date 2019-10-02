CREATE TABLE [dbo].[Accion_de_mejora]
(
	[Codigo] INT NOT NULL PRIMARY KEY,
	[Descripcion] NVARCHAR(500),
	[FechaInicio] DATE NOT NULL,
	[FechaFin] DATE NOT NULL
)
