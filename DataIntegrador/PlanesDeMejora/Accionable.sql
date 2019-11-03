﻿CREATE TABLE [dbo].[Accionable]
(
	codPlan int not null,
	nombreObj varchar(50) not null,
	descripAcMej varchar(250) not null,
	descripcion varchar(250) not null,
	fechaInicio date,
	fechaFin date,
	progreso int,

	constraint DateOrderAcci check(fechaFin >= fechaInicio),
	constraint PK_Accionable primary key(codPlan, nombreObj, descripAcMej, descripcion),
	constraint FK_Accionable_AccionDeMejora foreign key(codPlan, nombreObj, descripAcMej)
		references AccionDeMejora(codPlan, nombreObj, descripcion) on delete cascade
)