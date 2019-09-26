CREATE TABLE [dbo].[Compuesto_Por]
(
	[Username] NCHAR(10) NOT NULL,
	[Codigo_Plan] INT NOT NULL, 
    [Codigo_Obj] INT NOT NULL,
	FOREIGN KEY (Codigo_Obj) REFERENCES Objetivo(Codigo),
	FOREIGN KEY (Username, Codigo_Plan) REFERENCES PlanMejora(Username, Codigo),
	Primary Key(Username, Codigo_Plan, Codigo_Obj)
)
