CREATE TABLE Opciones_seleccionadas_respuesta_con_opciones
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	Username VARCHAR(20) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero INT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre INT NOT NULL,
	Fecha DATE NOT NULL,
	PCodigo CHAR(8) NOT NULL,

	OpcionSeleccionada INT NOT NULL,

	PRIMARY KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, OpcionSeleccionada),
	FOREIGN KEY(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo) 
		REFERENCES Responde_respuesta_con_opciones(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo),
)