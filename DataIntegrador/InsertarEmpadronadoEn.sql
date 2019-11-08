CREATE PROCEDURE [dbo].[InsertarEmpadronadoEn]
	@CorreoEstudiante VARCHAR(50),
	@CodCarrera VARCHAR(10),
	@CodEnfasis VARCHAR(10)
AS
begin
	insert into Empadronado_en (CorreoEstudiante, CodEnfasis, CodCarrera)
	values (@CorreoEstudiante,@CodEnfasis,@CodCarrera)
end
