CREATE TABLE Opciones_seleccionadas_respuesta_con_opciones
(
	-- Hay que cambiar varios valores cuando estén las otras tablas.
	FCodigo CHAR(8) NOT NULL,
	Correo VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,
	PCodigo CHAR(8) NOT NULL,

	OpcionSeleccionada TINYINT NOT NULL,

	PRIMARY KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo, OpcionSeleccionada),
	CONSTRAINT fkOpcionesSeleccionadas FOREIGN KEY(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo) 
		REFERENCES Responde_respuesta_con_opciones(FCodigo, Correo, CSigla, GNumero, GAnno, GSemestre, Fecha, PCodigo),
)