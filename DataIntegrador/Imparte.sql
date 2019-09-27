CREATE TABLE [dbo].[Imparte]
(
	[CedProfesor] CHAR(10) NOT NULL,
	[SiglaCurso] VARCHAR(10) NOT NULL,
	[NumGrupo] TINYINT NOT NULL,
	[Semestre] TINYINT NOT NULL,
	[Anno] INT NOT NULL,
	PRIMARY KEY (CedProfesor, SiglaCurso, NumGrupo, Semestre, Anno),
	FOREIGN KEY (CedProfesor) REFERENCES Profesor (Cedula),
	FOREIGN KEY (SiglaCurso, NumGrupo, Semestre, Anno) REFERENCES Grupo (SiglaCurso, NumGrupo, Semestre, Anno)
)
