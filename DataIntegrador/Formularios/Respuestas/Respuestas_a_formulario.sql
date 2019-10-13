CREATE TABLE Respuestas_a_formulario
(
	FCodigo CHAR(8) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,

	PRIMARY KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha),
	CONSTRAINT fkRespuestasFormulario FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	CONSTRAINT fkRespuestasUsuario FOREIGN KEY(Correo) REFERENCES Persona(Correo),
	CONSTRAINT fkRespuestasGrupo FOREIGN KEY(CSigla, GNumero, GSemestre, GAnno) REFERENCES Grupo(SiglaCurso, NumGrupo, Semestre, Anno),
)
