CREATE TABLE [dbo].[Genera]
(
	[CodigoP] INT NOT NULL
		constraint FK_Genera_Plan_Por_Formulario
			references PlanDeMejora(Codigo), 
	--[CodigoF] INT NOT NULL
	--	constraint FK_Evalua_Formulario_A_Plan
	--		references Formulario(Codigo)
	constraint PK_Genera
		primary key(CodigoP)
)
