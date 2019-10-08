CREATE TABLE [dbo].[AsociadoA]
(
	[CodigoO] INT NOT NULL
		constraint FK_Asocia_Objetivo_A_Accion
			references Objetivo(Codigo), 
	[CodigoA] INT NOT NULL
		constraint FK_Asocia_Accion_A_Objetivo
			references AccionDeMejora(Codigo)
	constraint PK_AsociadoA
		primary key(CodigoO, CodigoA)
)

