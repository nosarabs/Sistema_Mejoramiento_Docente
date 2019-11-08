CREATE PROCEDURE [dbo].[InsertarInscrita_En]
	@CodUnidadAc varchar(10),
	@CodCarrera varchar(10)
AS
begin
	Insert into Inscrita_en (CodUnidadAc, CodCarrera)
	values (@CodUnidadAc, @CodCarrera)
end