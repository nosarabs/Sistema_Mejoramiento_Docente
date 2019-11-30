-- Procedimiento para sacar los formularios de un estudiante
-- en un periodo definido por parámetros.

-- Ejemplo de uso: 
-- EXEC dbo.ObtenerFormulariosDeEstudiante @correoEstudiante = 'al@mail.com', @fechaInicio = NULL, @fechaFin = NULL

-- Colocar NULL en la fecha de inicio traería todos los formularios iniciados o no.
-- Colocar NULL en la fecha de fin traería todos los formularios finalizados o no.

CREATE PROCEDURE [dbo].[ObtenerFormulariosDeEstudiante]
	@correoEstudiante VARCHAR(50),
	@fechaInicio DATE,
	@fechaFin DATE
AS
BEGIN
	-- Null significaría que quiere todos los formularios desde el inicio de los tiempos
	IF(@fechaInicio IS NULL)
	BEGIN
		SET @fechaInicio = '12/31/9999';
	END;

	-- Null significaría que quiere todos los formularios desde una fecha de inicio
	-- hasta la actualidad
	IF(@fechaFin IS NULL)
	BEGIN
		SET @fechaFin = convert(datetime, 0);
	END;

	-- Conseguir los formularios basado los cursos matriculados, el grupo y los formularios activos
	-- Condicionado por la fecha definida y el correo del estudiante
	SELECT f.Codigo, m.SiglaCurso, m.NumGrupo, m.Semestre, m.Anno, p.FechaInicio, p.FechaFin
	FROM (Matriculado_en m 
	JOIN Activa_por a ON m.NumGrupo = a.GNumero AND m.SiglaCurso = a.CSigla AND m.Semestre = a.GSemestre AND m.Anno = a.GAnno
	JOIN Periodo_activa_por p ON p.GNumero = a.GNumero AND p.CSigla= a.CSigla AND p.GSemestre = a.GSemestre AND p.GAnno = a.GAnno)
	JOIN Formulario f ON f.Codigo = a.FCodigo
	WHERE m.CorreoEstudiante = @correoEstudiante AND p.FechaInicio <= @fechaInicio AND p.FechaFin >= @fechaFin
END