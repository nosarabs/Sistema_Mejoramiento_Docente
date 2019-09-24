CREATE TABLE [dbo].[Compuesto_Por]
(
	[Codigo_Plan] INT NOT NULL, 
    [Codigo_Obj] INT NOT NULL,
	FOREIGN KEY (Codigo_Obj) REFERENCES Objetivo(Codigo),
	FOREIGN KEY (Codigo_Plan) REFERENCES PlanMejora(Codigo)
)
