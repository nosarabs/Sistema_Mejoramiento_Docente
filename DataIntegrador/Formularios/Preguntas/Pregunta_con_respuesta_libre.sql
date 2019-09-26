CREATE TABLE Pregunta_con_respuesta_libre
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,
	FOREIGN KEY(Codigo) REFERENCES Pregunta(Codigo),
)
