CREATE TABLE Responde_respuesta_con_opciones
(
	FCodigo CHAR(8) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,
	PCodigo CHAR(8) NOT NULL,

	Justificacion NVARCHAR(250),

	PRIMARY KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo),
	CONSTRAINT fkRespondeOpcionesRespuestasAFormulario FOREIGN KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha) REFERENCES Respuestas_a_formulario(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha),
	CONSTRAINT fkRespondeOpcionesCodigo FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo),
)