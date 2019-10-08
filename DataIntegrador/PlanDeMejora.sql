CREATE TABLE [dbo].[PlanDeMejora]
(
	[CorreoProf] VARCHAR(50) NOT NULL
		constraint FK_CorreoProf
			references Profesor(Correo), 
    [Codigo] INT NOT NULL,
	Nombre varchar(30),
	FechaInicio date,
	FechaFin date
		constraint DateOrderPM
		check(FechaFin > FechaInicio),
	CodigoF char(8),
	CorreoProfAsig varchar(50)
		constraint FK_CorreoProf_Asig
			references Profesor(Correo)
	constraint PK_PlanDeMejora
		primary key(CorreoProf, Codigo)
)
