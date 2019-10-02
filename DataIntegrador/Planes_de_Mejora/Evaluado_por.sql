CREATE TABLE [dbo].[Evaluado_por]
(
	[CodigoAM] INT NOT NULL, 
    [CodigoAC] INT NOT NULL,
	Primary key(CodigoAM, CodigoAC),
	Foreign key(CodigoAM) References Accion_de_mejora(Codigo),
	Foreign key(CodigoAC) references Accionable(Codigo)
)
