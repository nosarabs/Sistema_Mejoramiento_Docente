-- Procedimiento para sacar los formularios de un estudiante
-- en un periodo definido por parámetros.

-- Ejemplo de uso: 
-- EXEC dbo.ObtenerFormulariosDeEstudiante @correoEstudiante = 'al@mail.com', @fechaInicio = NULL, @fechaFin = NULL

CREATE PROCEDURE [dbo].[ObtenerFormulariosDeEstudiante]
	@correoEstudiante VARCHAR(50),
	@fechaInicio DATE,
	@fechaFin DATE
AS
BEGIN
	-- Null significaría que quiere todos los formularios desde el inicio de los tiempos
	IF(@fechaInicio IS NULL)
	BEGIN
		SET @fechaInicio = GETDATE();
	END;

	-- Null significaría que quiere todos los formularios desde una fecha de inicio
	-- hasta la actualidad
	IF(@fechaFin IS NULL)
	BEGIN
		SET @fechaFin = convert(datetime, 0);
	END;

	-- Conseguir los formularios basado los cursos matriculados, el grupo y los formularios activos
	-- Condicionado por la fecha definida y el correo del estudiante
	SELECT p.FCodigo, p.CSigla, p.GNumero, p.GSemestre, p.GAnno, p.FechaInicio, p.FechaFin
	FROM Estudiante e 
	JOIN Matriculado_en m ON m.CorreoEstudiante = e.Correo
	JOIN Grupo g ON g.SiglaCurso = m.SiglaCurso
	JOIN Activa_por a ON a.CSigla = g.SiglaCurso AND a.GSemestre = g.Semestre AND a.GNumero = g.NumGrupo AND a.GAnno = g.Anno
	JOIN Periodo_activa_por p ON p.CSigla = g.SiglaCurso AND p.GSemestre = g.Semestre AND p.GNumero = g.NumGrupo AND p.GAnno = g.Anno
	JOIN Formulario f ON a.FCodigo = f.Codigo
	WHERE p.FechaInicio <= @fechaInicio AND  @fechaFin <= p.FechaFin AND e.Correo = @correoEstudiante
END