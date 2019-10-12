CREATE TABLE [dbo].[Responsable_De]
(
	[CorreoFunc] VARCHAR(50) NOT NULL 
		constraint FK_Responsable_De_Func
			references Funcionario(Correo), 
    [CodigoA] INT NOT NULL,
	constraint PK_Responsable_De
		primary key(CorreoFunc, CodigoA)

)
