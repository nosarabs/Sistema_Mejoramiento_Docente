class Graficos {

    constructor() { }

    generarGraficoBarras(canvas, datos) {

        var dataLength = datos.DATA.length;
        var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(dataLength);
        //chroma.js: https://gka.github.io/chroma.js/

        new Chart(canvas, {

            type: "bar",
            data: {
                labels: datos.LABELS,
                datasets: [
                    {
                        label: "Cantidad de estudiantes",
                        backgroundColor: colors,
                        hoverBackgroundColor: colors,
                        borderColor: "black",
                        borderWidth: 0,
                        data: datos.DATA
                    }
                ]
            },
            options: {
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 30,
                        bottom: 0
                    }
                },
                legend: {
                    display: false,
                    labels: {
                        fontColor: "black",
                        fontSize: 16,
                    }
                },
                title: {
                    display: false,
                },
                tooltips: {
                    enabled: false
                },
                responsive: true,
                maintainAspectRatio: true,
                devicePixelRatio: 2,
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            stepSize: 1,
                            fontColor: "#747474",
                            fontSize: 16,
                        }
                    }],
                    xAxes: [{
                        ticks: {
                            fontColor: "#747474",
                            fontSize: 16,
                        }
                    }]
                },
                plugins: {
                    datalabels: {
                        display: function (context) {
                            return context.dataset.data[context.dataIndex] !== 0;
                        },
                        color: "#747474",
                        textStrokeColor: "black",
                        textStrokeWidth: 0,
                        anchor: "end",
                        align: "start",
                        clamp: true,
                        offset: -30,
                        textAlign: "center",
                        font: {
                            size: "16",
                            weight: "normal"
                        },
                        formatter: function (value, ctx) {
                            var sum = 0;
                            var data = ctx.chart.data.datasets[0].data;
                            for (var i = 0; i < data.length; ++i) {
                                sum += data[i];
                            }
                            var percentage = ((value * 100 / sum).toFixed(2) + "%").replace(".", ",");
                            return percentage;
                        }
                    }
                }
            }

        });

    }

    generarGraficoPie(canvas, datos) {

        var dataLength = datos.DATA.length;
        var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(dataLength);
        //chroma.js: https://gka.github.io/chroma.js/

        new Chart(canvas, {

            type: "pie",
            data: {
                labels: datos.LABELS,
                datasets: [
                    {
                        label: "Cantidad de estudiantes",
                        backgroundColor: colors,
                        hoverBackgroundColor: colors,
                        borderColor: "white",
                        borderWidth: 1,
                        data: datos.DATA
                    }
                ]
            },
            options: {
                legend: {
                    display: true,
                    labels: {
                        fontColor: "#747474",
                        fontSize: 16,
                    }
                },
                title: {
                    display: false,
                },
                tooltips: {
                    enabled: false
                },
                responsive: true,
                maintainAspectRatio: true,
                devicePixelRatio: 2,
                plugins: {
                    datalabels: {
                        display: function (context) {
                            return context.dataset.data[context.dataIndex] !== 0; // or >= 1 or ...
                        },
                        color: "white",
                        textStrokeColor: "black",
                        textStrokeWidth: 0,
                        anchor: "end",
                        align: "start",
                        clamp: true,
                        offset: 16,
                        textAlign: "center",
                        font: {
                            size: "16",
                            weight: "bold"
                        },
                        formatter: function (value, ctx) {
                            var sum = 0;
                            var data = ctx.chart.data.datasets[0].data;
                            for (var i = 0; i < data.length; ++i) {
                                sum += data[i];
                            }
                            var percentage = ((value * 100 / sum).toFixed(2) + "%").replace(".", ",");
                            return percentage + "\n\n" + value;
                        }
                    }
                }
            }

        });

    }

    recuperarEtiquetasEscala(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas;

        $.ajax({
            url: "/ResultadosFormulario/ObtenerEtiquetasEscala",
            data: {
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                etiquetas = resultados;
            }
        });

        return etiquetas;

    }

    recuperarValoresEscala(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var valores;

        $.ajax({
            url: "/ResultadosFormulario/ObtenerRespuestasEscala",
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
                valores = resultados;
            }
        });

        return valores;

    }

    recuperarEtiquetasSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas;

        $.ajax({
            url: "/ResultadosFormulario/ObtenterOpcionesPreguntasSeleccion",
            data: {
                codigoPregunta: codigoPregunta
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                etiquetas = resultados;
            }
        });

        return etiquetas;

    }

    recuperarValoresSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta, numeroOpciones) {

        var valores;

        $.ajax({
            url: "/ResultadosFormulario/ObtenerOpcionesSeleccionadasPreguntasSeleccion",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                ano: ano,
                codigoPregunta: codigoPregunta,
                numOpciones: numeroOpciones
            },
            type: "post",
            dataType: "json",
            async: false,
            success: function (resultados) {
                valores = resultados;
            }
        });

        return valores;

    }

    generarGraficoEscala(canvas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas = this.recuperarEtiquetasEscala(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var valores = this.recuperarValoresEscala(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var datos = { DATA: valores, LABELS: etiquetas };
        this.generarGraficoBarras(canvas, datos);

    }

    generarGraficoSeleccionUnica(canvas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas = this.recuperarEtiquetasSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var valores = this.recuperarValoresSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta, etiquetas.length);
        var datos = { DATA: valores, LABELS: etiquetas };
        this.generarGraficoPie(canvas, datos);

    }

    generarGraficoSeleccionMultiple(canvas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas = this.recuperarEtiquetasSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
        var valores = this.recuperarValoresSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta, etiquetas.length);
        var datos = { DATA: valores, LABELS: etiquetas };
        this.generarGraficoBarras(canvas, datos);

    }

    generarGraficoSeleccionCerrada(canvas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var etiquetas = ["No", "Sí", "No responde"];
        var valores = this.recuperarValoresSeleccion(codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta, etiquetas.length);
        var datos = { DATA: valores, LABELS: etiquetas };
        this.generarGraficoPie(canvas, datos);

    }

}