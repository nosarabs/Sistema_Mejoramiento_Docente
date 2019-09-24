CREATE TABLE [dbo].[Compuesto_Por]
(
	[Codigo_Plan] INT NOT NULL, 
    [Codigo_Obj] INT NULL,
	FOREIGN KEY (Codigo_Obj) REFERENCES Objetivo(Codigo)
)
