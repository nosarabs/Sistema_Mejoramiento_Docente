-- Procedimiento para sacar los accionables que debe evaluar un funcionario.

-- Ejemplo de uso: 
-- EXEC dbo.ObtenerAccionablesPorEvaluar @correoFunc = 'cristian.asch@mail.com'

CREATE PROCEDURE [dbo].[ObtenerAccionablesPorEvaluar]
	@correoFunc VARCHAR(50)
AS
BEGIN
	IF(@correoFunc IS NOT NULL)
	BEGIN
		-- Conseguir los accionables basado en el correo del funcionario
		SELECT r.codPlan, r.nombreObj, r.descripAcMej, r.descripAcci, a.fechaInicio, a.fechaFin, a.peso, a.pesoPorcentaje, a.tipo
		FROM Responsable_De r
		JOIN Accionable a ON a.codPlan = r.codPlan AND
							 a.nombreObj = r.nombreObj AND
							 a.descripAcMej = r.descripAcMej AND
							 a.descripcion = r.descripAcci
		JOIN PlanDeMejora p ON r.codPlan = p.codigo
		WHERE r.corrFunc = @correoFunc AND p.borrado = 0
	END
END
