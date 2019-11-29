CREATE TABLE Respuestas_a_formulario
(
	FCodigo VARCHAR(8) NOT NULL,
	Correo VARCHAR(50) NOT NULL DEFAULT 'admin@mail.com',
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	Fecha DATE NOT NULL,
	Finalizado BIT NOT NULL DEFAULT 0

	CONSTRAINT RespuestasAFormFechaInvalida CHECK(FechaInicio<=FechaFin),
	PRIMARY KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin),
	CONSTRAINT fkRespuestasUsuario FOREIGN KEY(Correo) REFERENCES Persona(Correo) on delete cascade,
	CONSTRAINT fkRespuestasFormFormularioActivo FOREIGN KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin) REFERENCES Formulario_activo(FCodigo, CSigla, GNumero, GAnno, GSemestre, FechaInicio, FechaFin) on delete cascade,
)
