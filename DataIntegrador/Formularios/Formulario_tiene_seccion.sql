CREATE TABLE Formulario_tiene_seccion
(
	FCodigo VARCHAR(8) NOT NULL,
	SCodigo VARCHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(FCodigo, SCodigo),
	CONSTRAINT fkFormularioTieneSeccionCodigoFormulario FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	CONSTRAINT fkFormularioTieneSeccionCodigoSeccion FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
)
