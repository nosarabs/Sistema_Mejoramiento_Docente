CREATE TYPE dbo.PlanTabla AS TABLE 
(
	codigo int,
	nombre varchar(50),
	fechaInicio date,
	fechaFin date,
	borrado bit
)
GO

CREATE TYPE dbo.AsocPlanFormulario AS TABLE 
(
	codigoPlan int,
	codigoForm varchar(8)
)
GO