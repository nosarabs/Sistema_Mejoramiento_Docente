--Berta Sánchez Jalet
--COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
--Tarea técnica: Realizar consultas a la BD por medio de procedimientos almacenados.
--Cumplimiento: 8/10

CREATE PROCEDURE [dbo].[PromedioCursos]
	(	@UnidadesAcademicas FiltroUnidadesAcademicas READONLY,
		@CarrerasEnfasis FiltroCarrerasEnfasis READONLY,
		@Grupos FiltroGrupos READONLY,
		@CorreosProfesores FiltroProfesores READONLY,
		@promedio FLOAT OUTPUT,
		@cantidad INT OUTPUT
	)
	 
AS

BEGIN

DECLARE @min AS INT 
DECLARE @max AS INT
DECLARE @inc AS INT
DECLARE @opcion AS TINYINT
DECLARE @valor AS FLOAT = 0

	SET @promedio = -1;

	SELECT @min = E.Minimo, @max = E.Maximo, @inc = E.Incremento
	FROM Escalar AS E 
	WHERE E.Codigo = 'INFCURSO'

	DECLARE @Resultados TABLE (Opcion_seleccionada TINYINT);
		
	INSERT INTO @Resultados
		SELECT  O.OpcionSeleccionada 
		FROM	Opciones_seleccionadas_respuesta_con_opciones AS O
		WHERE O.PCodigo = 'INFCURSO' AND EXISTS (SELECT *
												FROM ObtenerFormulariosFiltros(@UnidadesAcademicas, @CarrerasEnfasis, @Grupos, @CorreosProfesores)
												WHERE FCodigo = O.FCodigo AND CSigla = O.CSigla AND GNumero = O.GNumero AND GSemestre = O.GSemestre AND GAnno = O.GAnno);

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

	IF (@cantidad != 0)
	BEGIN
		SET @promedio = @valor / @cantidad;
	END

END