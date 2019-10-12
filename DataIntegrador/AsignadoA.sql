﻿CREATE TABLE [dbo].[AsignadoA]
(
	codPlan int not null,
	corrProf varchar(50) not null,
	constraint FK_AsignadoA_PlanDeMejora foreign key(codPlan) references PlanDeMejora(codigo),
	constraint FK_AsignadoA_Profesor foreign key(corrProf) references Profesor(Correo)
)
