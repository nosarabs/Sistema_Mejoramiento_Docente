CREATE TABLE [dbo].[Objetivo]
(
	[Codigo] INT NOT NULL 
		constraint PK_Objetivo
			PRIMARY KEY, 
    [Nombre] VARCHAR(20) NULL, 
    [Descripcion] VARCHAR(500) NULL, 
    [TipoObjetivo] VARCHAR(20) NULL
		constraint FK_TipoO
			references TipoObjetivo(Nombre)
)
