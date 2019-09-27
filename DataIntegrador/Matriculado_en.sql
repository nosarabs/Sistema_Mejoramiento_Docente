CREATE TABLE [dbo].[Matriculado_en]
(
	[CedEstudiante] CHAR(10) NOT NULL,
	[SiglaCurso] VARCHAR(10) NOT NULL,
	[NumGrupo] TINYINT NOT NULL,
	[Semestre] TINYINT NOT NULL,
	[Anno] INT NOT NULL,
	PRIMARY KEY (CedEstudiante, SiglaCurso, NumGrupo, Semestre, Anno),
	FOREIGN KEY (CedEstudiante) REFERENCES Estudiante (Cedula),
	FOREIGN KEY (SiglaCurso, NumGrupo, Semestre, Anno) REFERENCES Grupo (SiglaCurso, NumGrupo, Semestre, Anno)
)
