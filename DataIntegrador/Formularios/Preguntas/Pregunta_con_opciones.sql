CREATE TABLE Pregunta_con_opciones
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,
	TituloCampoObservacion VARCHAR(50),
	FOREIGN KEY(Codigo) REFERENCES Pregunta(Codigo),
)
