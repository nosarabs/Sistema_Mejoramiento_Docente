CREATE TABLE [dbo].[PerfilPermiso]
(
	[Perfil] VARCHAR(50) NOT NULL,
	[PermisoId] INT NOT NULL,
	[CodCarrera] VARCHAR(10) NOT NULL,
	[CodEnfasis] VARCHAR(10) NOT NULL,
	PRIMARY KEY (Perfil, PermisoId, CodCarrera, CodEnfasis),
	FOREIGN KEY (Perfil) REFERENCES Perfil (Nombre),
	FOREIGN KEY (PermisoId) REFERENCES Permiso (Id),
	FOREIGN KEY (CodCarrera, CodEnfasis) REFERENCES Enfasis (CodCarrera, Codigo)
)
