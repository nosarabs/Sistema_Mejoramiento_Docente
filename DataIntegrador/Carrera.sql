CREATE TABLE [dbo].[Carrera]
(
	[Codigo] VARCHAR(10) NOT NULL PRIMARY KEY, 
    [Nombre] VARCHAR(50) NOT NULL, 
    [CodUnidadAcademica] VARCHAR(10) NULL
	FOREIGN KEY (CodUnidadAcademica) REFERENCES UnidadAcademica (Codigo)
)
