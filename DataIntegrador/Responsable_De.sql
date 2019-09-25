CREATE TABLE [dbo].[Responsable_De]
(
	[Username] VARCHAR(20) NOT NULL,
	[Codigo_A] INT NOT NULL,

	PRIMARY KEY (Username, Codigo_A),
	FOREIGN KEY(Codigo_A) REFERENCES Accionable(Codigo)
)
