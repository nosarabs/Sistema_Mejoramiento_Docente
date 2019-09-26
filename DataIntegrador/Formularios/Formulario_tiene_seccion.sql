CREATE TABLE Formulario_tiene_seccion
(
	FCodigo CHAR(8) NOT NULL,
	SCodigo CHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(FCodigo, SCodigo),
	FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
)
