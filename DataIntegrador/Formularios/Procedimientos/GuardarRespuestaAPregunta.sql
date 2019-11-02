CREATE PROCEDURE [dbo].[GuardarRespuestaAPregunta]
	@tipo char,
	@codFormulario char(8),
	@correo varchar(50),
	@siglaCurso varchar(6),
	@numGrupo tinyint,
	@anno int,
	@semestre tinyint,
	@fecha date,
	@codPregunta char(8),
	@justificacion nvarchar(250) = NULL,
	@opcionSeleccionada tinyint = NULL,
	@observacion nvarchar(250) = NULL
AS
BEGIN
	IF (@tipo = 'L')
		INSERT INTO Responde_respuesta_libre
		VALUES (@codFormulario, @correo, @siglaCurso, @numGrupo, @anno, @semestre, @fecha, @codPregunta, @observacion);
	ELSE
		-- Aquí se agrega la tupla indicando que una pregunta fue contestada. De ser necesario, se guarda su justificación
		INSERT INTO Responde_respuesta_con_opciones 
		VALUES (@codFormulario, @correo, @siglaCurso, @numGrupo, @anno, @semestre, @fecha, @codPregunta, @justificacion);

		-- Aquí se guardan la opcion que se contestó para esa respuesta
		INSERT INTO Opciones_seleccionadas_respuesta_con_opciones 
		VALUES (@codFormulario, @correo, @siglaCurso, @numGrupo, @anno, @semestre, @fecha, @codPregunta, @opcionSeleccionada);
END;
