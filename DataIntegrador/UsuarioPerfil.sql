CREATE TABLE [dbo].[UsuarioPerfil]
(
	[Usuario] varchar(50) NOT NULL,
	[Perfil] VARCHAR(50) NOT NULL,
	[CodCarrera] VARCHAR(10) NOT NULL,
	[CodEnfasis] VARCHAR(10) NOT NULL,
	PRIMARY KEY (Usuario, Perfil, CodCarrera, CodEnfasis),
	FOREIGN KEY (Usuario) REFERENCES Usuario (Username),
	FOREIGN KEY (Perfil) REFERENCES Perfil (Nombre),
	FOREIGN KEY (CodCarrera, CodEnfasis) REFERENCES Enfasis (CodCarrera, Codigo)
)
