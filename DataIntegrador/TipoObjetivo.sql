CREATE TABLE [dbo].[TipoObjetivo]
(
	[Nombre] VARCHAR(20) NOT NULL 
		constraint PK_TipoObj
			PRIMARY KEY, 
    [Descripcion] VARCHAR(500) NULL
)
