--Berta Sánchez Jalet
--COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
--Tarea técnica: Realizar consultas a la BD por medio de procedimientos almacenados.
--Cumplimiento: 10/10

CREATE PROCEDURE [dbo].[PromedioProfesor]
	(@correo VARCHAR(50),
	 @promedio FLOAT = -1 OUTPUT,
	 @cantidad INT = 1 OUTPUT)
	 
AS

IF EXISTS (SELECT * FROM Imparte WHERE CorreoProfesor = @correo) 
BEGIN

DECLARE @min AS INT 
DECLARE @max AS INT
DECLARE @inc AS INT
DECLARE @opcion AS TINYINT
DECLARE @valor AS FLOAT = 0

	SELECT @min = E.Minimo, @max = E.Maximo, @inc = E.Incremento
	FROM Escalar AS E 
	WHERE E.Codigo = 'INFPROF'
	

	DECLARE @Resultados TABLE (Opcion_seleccionada TINYINT);
		
	INSERT INTO @Resultados
		SELECT  O.OpcionSeleccionada 
		FROM	Opciones_seleccionadas_respuesta_con_opciones AS O
				JOIN Grupo AS G ON O.CSigla = G.SiglaCurso
					AND O.GNumero = G.NumGrupo
					AND O.GSemestre = G.Semestre
					AND O.GAnno = G.Anno
				JOIN Imparte AS I ON G.SiglaCurso = I.SiglaCurso 
					AND G.NumGrupo = I.NumGrupo
					AND G.Semestre = I.Semestre
					AND G.Anno = I.Anno
		WHERE I.CorreoProfesor = @correo 
			AND O.PCodigo = 'INFPROF'
			AND O.FCodigo IN (
								SELECT ObtenerFormulariosFiltros(
										
								)
							)

	DECLARE C CURSOR FOR SELECT Opcion_seleccionada FROM @Resultados
	OPEN C
	FETCH NEXT FROM C INTO @opcion
	WHILE @@FETCH_STATUS = 0 
	BEGIN
		SET @valor = @valor + ((@opcion * @inc)+@min); 
		FETCH NEXT FROM C INTO @opcion
	END
	CLOSE C 
	DEALLOCATE C
	
	SELECT @cantidad = COUNT(*)
	FROM @Resultados

	SET @promedio = @valor / @cantidad;

END
RETURN 0