CREATE PROCEDURE [dbo].[AgregarPlan]
	@codigo int,
	@nombre varchar(50),
	@fechaInicio date,
	@fechaFin date
AS
	INSERT INTO PlanDeMejora(codigo, nombre, fechaInicio, fechaFin)
	VALUES(@codigo, @nombre, @fechaInicio, @fechaFin)
