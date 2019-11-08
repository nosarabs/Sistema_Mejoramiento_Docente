CREATE PROCEDURE [dbo].[InsertarPertenece_a]
	@CodCarrera VARCHAR(10),
	@CodEnfasis VARCHAR(10),
	@SiglaCurso VARCHAR(10)
AS
	begin
	insert into Pertenece_a (CodCarrera, CodEnfasis, SiglaCurso)
	values (@CodCarrera, @CodEnfasis, @SiglaCurso)
end
