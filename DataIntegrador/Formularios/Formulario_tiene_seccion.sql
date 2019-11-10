﻿CREATE TABLE Formulario_tiene_seccion
(
	FCodigo VARCHAR(8) NOT NULL,
	SCodigo VARCHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(FCodigo, SCodigo),
	CONSTRAINT fkFormularioTieneSeccionCodigoFormulario FOREIGN KEY(FCodigo) REFERENCES Formulario(Codigo),
	CONSTRAINT fkFormularioTieneSeccionCodigoSeccion FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
)

GO

CREATE TRIGGER [dbo].[ActualizarOrdenSeccion]
	ON [dbo].[Formulario_tiene_seccion]
	AFTER DELETE
	AS
	BEGIN
		DECLARE @orden INT

		SELECT @orden = d.[Orden]
		FROM deleted d;

		UPDATE [Formulario_tiene_seccion]
		SET Orden = Orden - 1
		WHERE Orden > @orden;
	END