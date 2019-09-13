CREATE TABLE [dbo].[Estudiante]
(
	[EstudianteID]		INT				IDENTITY (1, 1) NOT NULL,
	[Apellido]			NVARCHAR (50)	NULL,
	[Nombre]			NVARCHAR (50)	NULL,
	[FechaMatricula]	DATETIME		NULL,
	PRIMARY KEY CLUSTERED ([EstudianteID] ASC)
) 