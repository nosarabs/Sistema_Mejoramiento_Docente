CREATE TABLE [dbo].[Matricula]
(
	[MatriculaID]	INT IDENTITY (1,1) NOT NULL, 
    [Nota]			DECIMAL(3, 2) NULL, 
    [CursoID]		INT NOT NULL, 
    [EstudianteID]	INT NOT NULL,
	PRIMARY KEY CLUSTERED ([MatriculaID] ASC),
	CONSTRAINT [FK_dbo.Matricula_dbo.Curso_CursoID] FOREIGN KEY ([CursoID])
		REFERENCES [DBO].[Curso] ([CursoID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Matricula_dbo.Estudiante_EstudianteID] FOREIGN KEY ([EstudianteID])
		REFERENCES [DBO].[Estudiante] ([EstudianteID]) ON DELETE CASCADE
)
