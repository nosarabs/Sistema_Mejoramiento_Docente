CREATE TABLE Formulario_activo
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo VARCHAR(8) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	CONSTRAINT FormActivoFechaInvalida CHECK(FechaInicio<=FechaFin),

	PRIMARY KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin),
	CONSTRAINT fkFormularioActivoFormulario FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	CONSTRAINT fkFormularioActivoGrupo FOREIGN KEY(CSigla, GNumero, GSemestre, GAnno) REFERENCES Grupo(SiglaCurso, NumGrupo, Semestre, Anno),
)