CREATE TABLE [dbo].[Objetivo]
(
	[Codigo] INT NOT NULL 
		constraint PK_Objetivo
			PRIMARY KEY, 
    [Nombre] VARCHAR(20) NULL, 
    [Descripcion] VARCHAR(500) NULL, 
    [TipoObjetivo] VARCHAR(20) NULL
		constraint FK_TipoO
			references TipoObjetivo(Nombre), 
    [CedulaProf] CHAR(10) NOT NULL, 
    [CodigoPlan] INT NOT NULL,
	constraint FK_PlanDeMejora
		foreign key(CedulaProf, CodigoPlan) references PlanDeMejora(CedProf, Codigo)
)
