CREATE TABLE [dbo].[Responsable_De]
(
	[CedulaFunc] CHAR(10) NOT NULL 
		constraint FK_Responsable_De_Func
			references Funcionario(Cedula), 
    [CodigoA] INT NOT NULL,
	constraint PK_Responsable_De
		primary key(CedulaFunc, CodigoA)

)
