-- Procedimiento para sacar los accionables que debe evaluar un funcionario.

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
		JOIN PlanDeMejora p ON a.codPlan = p.codigo
		WHERE a.corrProf = @correoProf AND p.borrado = 0
	END
END
