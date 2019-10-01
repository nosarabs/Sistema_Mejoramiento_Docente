﻿CREATE TABLE Periodo_activa_por
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	CHECK(FechaInicio<=FechaFin),

	PRIMARY KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin),
	FOREIGN KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre) REFERENCES Activa_por(FCodigo, CSigla, GNumero, GAnno, GSemestre),
)