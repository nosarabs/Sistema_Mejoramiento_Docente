CREATE TABLE Seccion_tiene_pregunta
(
	SCodigo CHAR(8) NOT NULL,
	PCodigo CHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(SCodigo, PCodigo),
	FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
	FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo),
)
