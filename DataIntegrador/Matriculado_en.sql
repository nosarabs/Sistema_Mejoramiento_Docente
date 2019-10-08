CREATE TABLE [dbo].[Matriculado_en]
(
	[CorreoEstudiante] VARCHAR(50) NOT NULL,
	[SiglaCurso] VARCHAR(10) NOT NULL,
	[NumGrupo] TINYINT NOT NULL,
	[Semestre] TINYINT NOT NULL,
	[Anno] INT NOT NULL,
	PRIMARY KEY (CorreoEstudiante, SiglaCurso, NumGrupo, Semestre, Anno),
	FOREIGN KEY (CorreoEstudiante) REFERENCES Estudiante (Correo),
	FOREIGN KEY (SiglaCurso, NumGrupo, Semestre, Anno) REFERENCES Grupo (SiglaCurso, NumGrupo, Semestre, Anno)
)
