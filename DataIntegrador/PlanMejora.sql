CREATE TABLE [dbo].[PlanMejora]
(
	[Username] NCHAR(10) NOT NULL,
	[Codigo] INT NOT NULL, 
    [Nombre] NCHAR(30) NULL, 
    [FechaInicio] DATE NULL, 
    [FechaFin] DATE NULL,
	[CodigoF] INT NULL, 
    [UsernameA] NCHAR(10) NULL, 
    Primary Key(Username, Codigo)
)