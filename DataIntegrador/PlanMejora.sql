CREATE TABLE [dbo].[PlanMejora]
(
	[UserID] INT NOT NULL,
	[Codigo] INT NOT NULL, 
    [Nombre] NCHAR(30) NULL, 
    [FechaInicio] DATE NULL, 
    [FechaFin] DATE NULL,
	[CodigoF] CHAR(8) NULL, 
    [UserIDA] INT NULL, 
    Primary Key([UserID], Codigo),
	Foreign key([UserID]) references UserProfile(UserId),
	Foreign key(CodigoF) references Formulario(Codigo),
	Foreign key(UserIDA) references UserProfile(UserId)
)