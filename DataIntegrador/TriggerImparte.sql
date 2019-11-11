CREATE TRIGGER [Trigger1]
	ON [dbo].[Imparte]
	INSTEAD OF INSERT
	AS
	declare @CorreoProfe varchar(50)
	declare @SiglaCurso varchar(10)
	declare @Numgrupo tinyint
	declare @semestre tinyint
	declare @anno int
	select @CorreoProfe = i.CorreoProfesor, @SiglaCurso = i.SiglaCurso, @Numgrupo = i.NumGrupo, @semestre = i.Semestre, @anno = i.Anno
	from inserted i
	BEGIN
		if(@CorreoProfe not in (select CorreoProfesor from Imparte) or @SiglaCurso not in (select SiglaCurso from Imparte) or @Numgrupo not in(select NumGrupo from Imparte) or @semestre not in (select Semestre from Imparte) or @anno not in (select anno from imparte))
		begin
			insert into Imparte select * from inserted
		end
	END
