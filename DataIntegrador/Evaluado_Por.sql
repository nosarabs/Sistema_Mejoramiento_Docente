CREATE TABLE [dbo].[Evaluado_Por]
(
	[CodigoAM] INT NOT NULL, 
    [CodigoAC] INT NOT NULL,
	Primary key(CodigoAM, CodigoAC),
	Foreign key(CodigoAM) References Accion_De_Mejora(Codigo),
	Foreign key(CodigoAC) references Accionable(Codigo)
)
