class TextoLibre {

    constructor() { }

    crearLista(caja, respuestas) {

        var list = document.createElement("ul");

        for (var i = 0; i < respuestas.length; ++i) {

            var item = document.createElement("li");


            item.appendChild(document.createTextNode(respuestas[i]));


            list.appendChild(item);

            if (i < (respuestas.length - 1)) {

                var br = document.createElement("br");
                list.appendChild(br);
            }
        }

        caja.appendChild(list);

    }

    recuperarJustificaciones(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var justificaciones = [];

        $.ajax({
            url: '/ResultadosFormulario/getJustificacionPregunta',
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                for (var i = 0; i < resultados.length; ++i) {
                    justificaciones.push(resultados[i].Value);
                }
            }
        });

        return justificaciones;

    }

    recuperarTextoAbierto(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var respuestas = [];

        $.ajax({
            url: "/ResultadosFormulario/ObtenerRespuestasTextoAbierto",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                for (var i = 0; i < resultados.length; ++i) {
                    respuestas.push(resultados[i].Value);
                }
            }
        });

        return respuestas;

    }

    generarJustificaciones(caja, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var respuestas = this.recuperarJustificaciones(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        this.crearLista(caja, respuestas);

    }

    generarTextoAbierto(caja, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var respuestas = this.recuperarTextoAbierto(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        this.crearLista(caja, respuestas);

    }


}