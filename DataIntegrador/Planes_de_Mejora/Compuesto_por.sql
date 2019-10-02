CREATE TABLE [dbo].[Compuesto_por]
(
	[UserID] INT NOT NULL,
	[Codigo_Plan] INT NOT NULL, 
    [Codigo_Obj] INT NOT NULL,
	FOREIGN KEY (Codigo_Obj) REFERENCES Objetivo(Codigo),
	FOREIGN KEY ([UserID], Codigo_Plan) REFERENCES Plan_de_mejora([UserID], Codigo),
	Primary Key([UserID], Codigo_Plan, Codigo_Obj)
)
