CREATE TABLE [dbo].[PlanDeMejora]
(
	[CedProf] CHAR(10) NOT NULL 
		constraint FK_CedProf
			references Profesor(Cedula), 
    [Codigo] INT NOT NULL,
	Nombre varchar(30),
	FechaInicio date,
	FechaFin date
		constraint DateOrderPM
		check(FechaFin > FechaInicio),
	CodigoF char(8),
	CedProfAsig char(10)
		constraint FK_CedProf_Asig
			references Profesor(Cedula)
	constraint PK_PlanDeMejora
		primary key(CedProf, Codigo)
)
