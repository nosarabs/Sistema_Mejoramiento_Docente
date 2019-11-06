CREATE TABLE Seccion_tiene_pregunta
(
	SCodigo VARCHAR(8) NOT NULL,
	PCodigo VARCHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(SCodigo, PCodigo),
	CONSTRAINT fkSeccionTienePreguntaCodigoSeccion FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
	CONSTRAINT fkSeccionTienePreguntaCodigoPregunta FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo),
)
