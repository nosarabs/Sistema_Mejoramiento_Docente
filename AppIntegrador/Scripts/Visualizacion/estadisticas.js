class Estadisticas {

    constructor() { }

    recuperarMedia(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var media;
        $.ajax({
            url: "/ResultadosFormulario/getPromedio",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta
            },
            type: "get",
            dataType: "json",
            async: false,
            success: function (resultados) {
                media = resultados.toFixed(2);
            }
        });

        return media;

    }

    recuperarMediana(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var mediana;
        $.ajax({
            url: "/ResultadosFormulario/getMedianaRespuestaEscalar",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta
            },
            type: "get",
            dataType: "json",
            async: false,
            success: function (resultados) {
                mediana = resultados.toFixed(2);
            }
        });

        return mediana;

    }

    recuperarDesviacion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var desviacion;
        $.ajax({
            url: "/ResultadosFormulario/obtenerDesviacionEstandar",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta
            },
            type: "get",
            dataType: "json",
            async: false,
            success: function (resultados) {
                desviacion = resultados.toFixed(2);
            }
        });

        return desviacion;

    }

    generarEstadisticas(row, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var media = this.recuperarMedia(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var mediana = this.recuperarMediana(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var desviacion = this.recuperarDesviacion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

        var colMedia = row.firstChildElement();
        var colMediana = 

    }

}