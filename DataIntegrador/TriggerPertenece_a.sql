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
		if(not exists (select * from Pertenece_a where CodCarrera= @CodCarrera and @SiglaCurso = SiglaCurso and  CodEnfasis = @codEnf) and (@codEnf not like '' and @CodCarrera not like '' and @SiglaCurso not like ''))
		begin
			insert into Pertenece_a select * from inserted
		end
	END