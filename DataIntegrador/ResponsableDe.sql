CREATE TABLE [dbo].[ResponsableDe]
(
	[CedulaFunc] CHAR(10) NOT NULL 
		constraint FK_ResponsableDeFunc
			references Funcionario(Cedula), 
    [CodigoA] INT NOT NULL,
	constraint PK_ResponsableDe
		primary key(CedulaFunc, CodigoA)

)
