//Berta Sánchez Jalet
//COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
//Tarea técnica: Mostrar las nota resultantes.
//Cumplimiento: 5/10
class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: {
                unidadesAcademicas: unidadesAcademicas,
                carrerasEnfasis: carrerasEnfasis,
                grupos: grupos,
                profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;
            }

        });
        return [promedio, cantidad]
    }

    recuperarPromedioCursos(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioCursos',
            data: {
                    unidadesAcademicas: unidadesAcademicas,
                    carrerasEnfasis: carrerasEnfasis,
                    grupos: grupos,
                    profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;
            }
        });
        return [promedio, cantidad]
    }

    generarGrafico(canvas) {

        var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(6);
        //chroma.js: https://gka.github.io/chroma.js/

        new Chart(canvas, {

            type: "bar",
            data: {
                labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
                datasets: [
                    {
                        label: "Cantidad de estudiantes",
                        backgroundColor: colors,
                        hoverBackgroundColor: colors,
                        borderColor: "black",
                        borderWidth: 0,
                        data: [12, 19, 3, 5, 2, 3]
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

}