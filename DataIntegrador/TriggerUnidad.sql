CREATE TRIGGER [TriggerUnidad]
	ON [dbo].[UnidadAcademica]
	INSTEAD OF INSERT
	AS
	DECLARE @codigo varchar(10)

	--Nivel de aislamiento maximo porque no podemos permitir modificaciones o nuevas inserciones mientras revisamos las condiciones
	--de insercion
	set transaction isolation level serializable;
	set implicit_transactions off;
	Begin transaction transaccionCodigoUnidad;

	DECLARE cursor_Unidad CURSOR
	FOR SELECT Codigo
	FROM inserted
	OPEN cursor_Unidad;
	FETCH NEXT FROM cursor_Unidad INTO @codigo
	WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@codigo NOT IN (SELECT Codigo FROM UnidadAcademica) and @codigo not like '')
			BEGIN
				INSERT INTO UnidadAcademica SELECT * FROM inserted
			END
			FETCH NEXT FROM cursor_Unidad INTO @codigo
		END
	CLOSE cursor_Unidad
	DEALLOCATE cursor_Unidad

	Commit Transaction transaccionCodigoUnidad;
	--Volver al nivel de aislamiento por default
	set transaction isolation level read committed;