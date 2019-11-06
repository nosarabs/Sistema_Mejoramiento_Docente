--Berta Sánchez Jalet
--COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
--Tarea técnica: Realizar consultas a la BD por medio de procedimientos almacenados.
--Cumplimiento: 2/10

CREATE PROCEDURE [dbo].[PromedioProfesor]
	(@correo VARCHAR(50),
	 @promedio FLOAT OUTPUT)
AS

RETURN 0