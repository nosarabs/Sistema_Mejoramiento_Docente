CREATE TABLE Periodo_activa_por
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo VARCHAR(8) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	CONSTRAINT ActivaPorFechaInvalida CHECK(FechaInicio<=FechaFin),

	PRIMARY KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin),
	CONSTRAINT fkPeriodoActivaPor FOREIGN KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre) REFERENCES Activa_por(FCodigo, CSigla, GNumero, GAnno, GSemestre),
)
GO

CREATE INDEX [IXPeriodoActivaPorGrupo] ON [dbo].[Periodo_activa_por] (CSigla, GNumero, GAnno, GSemestre) INCLUDE (FechaInicio, FechaFin, FCodigo)
