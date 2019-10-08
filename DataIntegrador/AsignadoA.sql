CREATE TABLE [dbo].[AsignadoA]
(
	[CedulaProf] CHAR(10) NOT NULL 
		constraint FK_Asignado_A_Prof
			references Profesor(Cedula), 
    [CodigoP] INT NOT NULL
		constraint FK_Asignado_A_Plan
			references PlanDeMejora(Codigo),
	constraint PK_AsignadoA
		primary key(CedulaProf, CodigoP)
)
