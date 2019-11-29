﻿CREATE TABLE Responde_respuesta_libre
(
	FCodigo VARCHAR(8) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,
	PCodigo VARCHAR(8) NOT NULL,
	SCodigo VARCHAR(8) NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	Observacion NVARCHAR(250),

	CONSTRAINT RespondeRespuestaLibreFechaInvalida CHECK(FechaInicio<=FechaFin),
	PRIMARY KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin, PCodigo, SCodigo),
	CONSTRAINT fkRespondeLibreRespuestasAFormulario FOREIGN KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin) REFERENCES Respuestas_a_formulario(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin) on delete cascade,
	CONSTRAINT fkRespondeLibreCodigoPregunta FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo) on delete cascade,
	CONSTRAINT fkRespondeLibreCodigoSeccion FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo) on delete cascade,
)