CREATE TABLE [dbo].[CompuestoPor]
(
	[CodigoP] INT NOT NULL
 		constraint FK_Asocia_Plan_A_Objetivo
			references PlanDeMejora(Codigo),
	[CodigoO] INT NOT NULL
		constraint FK_Asocia_Objetivo_A_Plan
			references Objetivo(Codigo),
	constraint PK_CompuestoPor
		primary key(CodigoP, CodigoO)
)
