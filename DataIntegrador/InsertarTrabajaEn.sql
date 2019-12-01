CREATE PROCEDURE [dbo].[InsertarTrabajaEn]
	@CorreoFuncionario VARCHAR(50),
	@CodUnidadAcademica VARCHAR(10)
AS
begin
	IF(@CorreoFuncionario IN (SELECT Correo FROM Funcionario) AND @CodUnidadAcademica IN (SELECT Codigo FROM UnidadAcademica))
	BEGIN
		insert into Trabaja_en (CodUnidadAcademica, CorreoFuncionario)
		values (@CodUnidadAcademica, @CorreoFuncionario)
	END
end
