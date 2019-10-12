﻿-- Mosqueteros --

CREATE TABLE [dbo].[Origina]
(
	FCodigo CHAR(8) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	CSigla VARCHAR(10) NOT NULL,
	GNumero TINYINT NOT NULL,
	GAnno INT NOT NULL,
	GSemestre TINYINT NOT NULL,
	Fecha DATE NOT NULL,
	codPlan int not null,

	constraint PK_Origina PRIMARY KEY(codPlan, FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha),
	constraint FK_Origina_RespuestasAFormulario foreign key(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha)
		references Respuestas_a_formulario(FCodigo, Username, CSigla, GNumero, GAnno, GSemestre, Fecha),
	constraint FK_Origina_PlanDeMejora foreign key(codPlan) references PlanDeMejora(codigo)

)
