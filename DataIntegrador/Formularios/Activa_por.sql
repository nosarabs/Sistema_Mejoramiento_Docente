CREATE TABLE Activa_por
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero INT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre INT NOT NULL,

	PRIMARY KEY(FCodigo, CSigla, GNumero, GAnno, GSemestre),
	FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	--FOREIGN KEY(CSigla, GNumero, GAnno, GSemestre) REFERENCES Grupo(Sigla, Numero, Anno, Semestre),
)