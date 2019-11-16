CREATE TABLE Seccion_tiene_pregunta
(
	SCodigo VARCHAR(8) NOT NULL,
	PCodigo VARCHAR(8) NOT NULL,
	Orden INT NOT NULL,
	PRIMARY KEY(SCodigo, PCodigo),
	CONSTRAINT fkSeccionTienePreguntaCodigoSeccion FOREIGN KEY(SCodigo) REFERENCES Seccion(Codigo),
	CONSTRAINT fkSeccionTienePreguntaCodigoPregunta FOREIGN KEY(PCodigo) REFERENCES Pregunta(Codigo),
)

GO

CREATE TRIGGER [dbo].[ActualizarOrdenPregunta]
	ON [dbo].[Seccion_tiene_pregunta]
	AFTER DELETE
	AS
	BEGIN
		DECLARE @orden INT

		SELECT @orden = d.[Orden]
		FROM deleted d;

		UPDATE [Seccion_tiene_pregunta]
		SET Orden = Orden - 1
		WHERE Orden > @orden;
	END