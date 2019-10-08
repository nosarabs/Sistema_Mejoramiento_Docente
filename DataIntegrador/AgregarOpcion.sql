CREATE PROCEDURE [dbo].[AgregarOpcion]
	@cod char(8),
	@orden tinyint,
	@texto varchar(50)
AS
BEGIN 
	INSERT INTO Opciones_de_seleccion([Codigo],[Orden],[Texto])
	VALUES (@cod, @orden, @texto);
END