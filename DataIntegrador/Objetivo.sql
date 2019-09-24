CREATE TABLE [dbo].[Objetivo]
(
	[Codigo] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(20) NULL, 
    [Descripcion] NVARCHAR(50) NULL, 
    [Tipo_O] NVARCHAR(20) NULL
	FOREIGN KEY (Tipo_O) REFERENCES Tipo_Objetivo(Nombre)
)
