CREATE PROCEDURE [dbo].[InsertarTrabajaEn]
	@CorreoFuncionario VARCHAR(50),
	@CodUnidadAcademica VARCHAR(10)
AS
begin
	insert into Trabaja_en (CodUnidadAcademica, CorreoFuncionario)
	values (@CodUnidadAcademica, @CorreoFuncionario)
end
