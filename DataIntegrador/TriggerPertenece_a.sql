CREATE TRIGGER [TriggerPertenece_a]
	ON [dbo].[Pertenece_a]
	INSTEAD OF INSERT
	AS
	declare @CodCarrera varchar(50)
	declare @SiglaCurso varchar(10)
	declare @codEnf tinyint
	select @CodCarrera = i.CodCarrera, @SiglaCurso = i.SiglaCurso, @codEnf = i.CodEnfasis
	from inserted i
	BEGIN
		if(@CodCarrera not in (select CodCarrera from Pertenece_a) or @SiglaCurso not in (select SiglaCurso from Pertenece_a) or @codEnf not in(select CodEnfasis from Pertenece_a))
		begin
			insert into Pertenece_a select * from inserted
		end
	END