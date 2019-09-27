CREATE TABLE Responde_respuesta_con_opciones
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	Username VARCHAR(20) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero INT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre INT NOT NULL,
	Fecha DATE NOT NULL,
	PCodigo CHAR(8) NOT NULL,

	Justificacion VARCHAR(250),

	PRIMARY KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo),
	FOREIGN KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha) REFERENCES Respuestas_a_formulario(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha),
	FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo),
)