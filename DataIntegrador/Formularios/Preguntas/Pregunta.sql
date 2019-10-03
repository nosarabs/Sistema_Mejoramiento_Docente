CREATE TABLE Pregunta
(
	Codigo CHAR(8) NOT NULL PRIMARY KEY,
	Enunciado VARCHAR(250) NOT NULL,
	-- Tipos: escala, seleccion_unica, seleccion_multiple, seleccion_cerrada, texto_abierto.
	Tipo VARCHAR(20) NOT NULL CHECK(Tipo = 'escala' OR Tipo = 'seleccion_unica' OR Tipo = 'seleccion_multiple' OR Tipo = 'seleccion_cerrada' OR Tipo = 'texto_abierto')
)
