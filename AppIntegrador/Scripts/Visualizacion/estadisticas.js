class Estadisticas {

    constructor() { }

    recuperarMedia(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta) {

        var media;
        $.ajax({
            url: "/ResultadosFormulario/getPromedio",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                fechaInicio: fechaInicio,
                fechaFin: fechaFin,
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                media = resultados.toFixed(2);
            }
        });

        return media;

    }

    recuperarMediana(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta) {

        var mediana;
        $.ajax({
            url: "/ResultadosFormulario/getMedianaRespuestaEscalar",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                fechaInicio: fechaInicio,
                fechaFin: fechaFin,
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                mediana = resultados.toFixed(2);
            }
        });

        return mediana;

    }

    recuperarDesviacion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta) {

        var desviacion;
        $.ajax({
            url: "/ResultadosFormulario/obtenerDesviacionEstandar",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                fechaInicio: fechaInicio,
                fechaFin: fechaFin,
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                desviacion = resultados.toFixed(2);
            }
        });

        return desviacion;

    }

    generarEstadisticas(row, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta) {

        var media = this.recuperarMedia(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta);
        var mediana = this.recuperarMediana(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta);
        var desviacion = this.recuperarDesviacion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoPregunta);

        var colMedia = row.firstElementChild;
        var colMediana = colMedia.nextElementSibling;
        var colDesviacion = colMediana.nextElementSibling;

        var mediaTitulo = document.createElement("h5");
        mediaTitulo.innerText = "Promedio";
        var medianaTitulo = document.createElement("h5");
        medianaTitulo.innerText = "Mediana";
        var desviacionTitulo = document.createElement("h5");
        desviacionTitulo.innerText = "Desviación Estándar";

        var mediaDiv = document.createElement("div");
        mediaDiv.className = "estadisticas";
        mediaDiv.innerText = ("" + media).replace(".", ",");
        var medianaDiv = document.createElement("div");
        medianaDiv.className = "estadisticas";
        medianaDiv.innerText = ("" + mediana).replace(".", ",");
        var desviacionDiv = document.createElement("div");
        desviacionDiv.className = "estadisticas";
        desviacionDiv.innerText = ("" + desviacion).replace(".", ",");


        colMedia.appendChild(mediaTitulo);
        colMedia.appendChild(mediaDiv);
        colMediana.appendChild(medianaTitulo);
        colMediana.appendChild(medianaDiv);
        colDesviacion.appendChild(desviacionTitulo);
        colDesviacion.appendChild(desviacionDiv);

    }

}