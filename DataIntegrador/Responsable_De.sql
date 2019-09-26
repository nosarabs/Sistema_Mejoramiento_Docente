CREATE TABLE [dbo].[Responsable_De]
(
	UserId int NOT NULL,
	[Codigo_A] INT NOT NULL,

	PRIMARY KEY (UserId, Codigo_A),
	FOREIGN KEY(Codigo_A) REFERENCES Accionable(Codigo)
)
