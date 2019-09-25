CREATE TABLE Si_no_nr
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,
	FOREIGN KEY(Codigo) REFERENCES Pregunta_con_opciones(Codigo),
)
