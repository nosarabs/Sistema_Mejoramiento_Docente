CREATE TABLE [dbo].[EnlaceSeguro]
(
	[Hash] varchar(64) NOT NULL PRIMARY KEY,
	[UsuarioAsociado] VARCHAR(50) NULL,
	[UrlReal] varchar(1000) Not null,
	[Expira] datetime null DEFAULT dateadd(hour,48,getdate())
	CONSTRAINT fkEnlaceSeguroAPersonaCorreo Foreign key (UsuarioAsociado) references Persona(Correo)
)
