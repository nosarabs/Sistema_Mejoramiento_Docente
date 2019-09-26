CREATE TABLE [dbo].[Trabaja_en]
(
	[CedFuncionario] CHAR(10) NOT NULL,
	[CodUnidadAcademica] VARCHAR(10) NOT NULL,
	PRIMARY KEY (CedFuncionario, CodUnidadAcademica),
	FOREIGN KEY (CedFuncionario) REFERENCES Funcionario (Cedula),
	FOREIGN KEY (CodUnidadAcademica) REFERENCES UnidadAcademica (Codigo)
)
