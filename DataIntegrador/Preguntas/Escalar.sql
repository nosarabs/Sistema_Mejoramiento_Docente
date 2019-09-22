﻿CREATE TABLE Escalar
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,

	-- Hay que validar los defaults
	Incremento INT NOT NULL DEFAULT 1,
	Minimo INT NOT NULL DEFAULT 1,
	Maximo INT NOT NULL DEFAULT 5,
	FOREIGN KEY(Codigo) REFERENCES Pregunta_con_opciones(Codigo),
)
