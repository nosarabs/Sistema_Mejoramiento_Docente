CREATE TABLE [dbo].[Curso]
(
	[CursoID]	INT				IDENTITY (1,1) NOT NULL,
	[Titulo]	NVARCHAR(50)	NULL,
	[Creditos]	INT				NULL,
	PRIMARY KEY CLUSTERED ([CursoID] ASC)
)