-- Procedimiento para sacar los planes que debe ejecutar un profesor.

-- Ejemplo de uso: 
-- EXEC dbo.ObtenerPlanesPorEjecutar @correoProf = 'cristian.asch@mail.com'

CREATE PROCEDURE [dbo].[ObtenerPlanesPorEjecutar]
	@correoProf VARCHAR(50)
AS
BEGIN
	IF(@correoProf IS NOT NULL)
	BEGIN
		-- Conseguir los planes basado en el correo del profesor
		SELECT a.codPlan
		FROM AsignadoA a
		JOIN PlanDeMejora p ON p.codigo = a.codPlan
		WHERE a.corrProf = @correoProf AND p.borrado = 0
	END
END