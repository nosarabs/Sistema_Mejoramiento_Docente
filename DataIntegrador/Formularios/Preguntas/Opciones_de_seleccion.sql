CREATE TABLE Opciones_de_seleccion
(
	Codigo CHAR(8) NOT NULL,
	Orden INT NOT NULL,
	Texto VARCHAR(50) NOT NULL,
	PRIMARY KEY(Codigo, Orden, Texto),
	FOREIGN KEY(Codigo) REFERENCES Pregunta_con_opciones_de_seleccion(Codigo),
)
