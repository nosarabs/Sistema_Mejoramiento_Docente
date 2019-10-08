﻿CREATE TABLE Respuestas_a_formulario
(
	FCodigo CHAR(8) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,

	PRIMARY KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha),
	CONSTRAINT fkRespuestasFormulario FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	CONSTRAINT fkRespuestasUsuario FOREIGN KEY(Username) REFERENCES Usuario(Username),
	CONSTRAINT fkRespuestasGrupo FOREIGN KEY(CSigla, GNumero, GSemestre, GAnno) REFERENCES Grupo(SiglaCurso, NumGrupo, Semestre, Anno),
)
