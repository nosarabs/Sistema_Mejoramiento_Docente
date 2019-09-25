CREATE TABLE Pregunta_con_opciones_de_seleccion
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,
	Tipo CHAR NOT NULL,
	-- 'U' para preguntas de selección única y 'M' para preguntas de selección múltiple.
	CONSTRAINT TipoSeleccion CHECK(Tipo = 'U' OR Tipo = 'M'),
	FOREIGN KEY(Codigo) REFERENCES Pregunta_con_opciones(Codigo),
)
