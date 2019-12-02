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

CREATE TYPE dbo.AsocObjetivoSeccion AS TABLE 
(
	codigoPlan int,
	nombreObjetivo varchar(50),
	codigoSeccion varchar(8)
)
GO

CREATE TYPE dbo.AsocAccionPregunta AS TABLE 
(
	codigoPlan int,
	nombreObjetivo varchar(50),
	descripcionObjetivo varchar(250),
	codigoPregunta varchar(8)
)
GO