CREATE TRIGGER [TriggerPertenece_a]
	ON [dbo].[Pertenece_a]
	INSTEAD OF INSERT
	AS
	declare @CodCarrera varchar(10)
	declare @SiglaCurso varchar(10)
	declare @codEnf varchar(10)
	select @CodCarrera = i.CodCarrera, @SiglaCurso = i.SiglaCurso, @codEnf = i.CodEnfasis
	from inserted i
	BEGIN
		if((@CodCarrera not in (select CodCarrera from Pertenece_a) or @SiglaCurso not in (select SiglaCurso from Pertenece_a) or @codEnf not in(select CodEnfasis from Pertenece_a)) and (@codEnf not like '' and @CodCarrera not like '' and @SiglaCurso not like ''))
		begin
			insert into Pertenece_a select * from inserted
		end
	END