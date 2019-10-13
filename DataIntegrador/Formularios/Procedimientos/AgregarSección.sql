CREATE PROCEDURE [dbo].[AgregarSección]
	@Codigo char(8),
	@Nombre nvarchar(250) 
AS
	INSERT INTO Seccion
	VALUES(@Codigo, @Nombre)
RETURN 0
