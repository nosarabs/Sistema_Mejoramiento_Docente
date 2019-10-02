CREATE TABLE [dbo].[Asociado_a]
(
	[CodigoO] INT NOT NULL, 
    [CodigoA] INT NOT NULL,
	Primary key(CodigoO, CodigoA),
	Foreign key(CodigoO) references Objetivo(Codigo),
	Foreign Key(CodigoA) references Accion_de_mejora(Codigo)
)
