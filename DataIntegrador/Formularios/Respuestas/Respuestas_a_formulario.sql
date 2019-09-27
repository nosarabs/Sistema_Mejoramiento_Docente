CREATE TABLE Respuestas_a_formulario
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	Username VARCHAR(20) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero INT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre INT NOT NULL,
	Fecha DATE NOT NULL,

	PRIMARY KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha),
	FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	--FOREIGN KEY(Username) REFERENCES Usuario(Username),
	--FOREIGN KEY(CSigla, GNumero, GAnno, GSemestre) REFERENCES Grupo(Sigla, Numero, Anno, Semestre),
)
