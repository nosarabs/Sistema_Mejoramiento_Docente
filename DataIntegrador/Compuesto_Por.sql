CREATE TABLE [dbo].[Compuesto_Por]
(
	[UserID] INT NOT NULL,
	[Codigo_Plan] INT NOT NULL, 
    [Codigo_Obj] INT NOT NULL,
	FOREIGN KEY (Codigo_Obj) REFERENCES Objetivo(Codigo),
	FOREIGN KEY ([UserID], Codigo_Plan) REFERENCES PlanMejora([UserID], Codigo),
	Primary Key([UserID], Codigo_Plan, Codigo_Obj)
)
