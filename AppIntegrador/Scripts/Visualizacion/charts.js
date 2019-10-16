//Classes and types

const classes = {

    ESCALA: "escala",
    SELECCION_UNICA: "seleccion_unica",
    SELECCION_MULTIPLE: "seleccion_multiple",
    SELECCION_CERRADA: "seleccion_cerrada",
    TEXTO_ABIERTO: "texto_abierto"

}

const types = {

    ESCALA: "bar",
    SELECCION_UNICA: "pie",
    SELECCION_MULTIPLE: "bar",
    SELECCION_CERRADA: "pie",
    TEXTO_ABIERTO: "texto_abierto"

}

function drawBarChart(cvs, chartData) {

	var dataLength = chartData.DATA.length;
	var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(dataLength);
	//chroma.js: https://gka.github.io/chroma.js/
	
	new Chart(cvs, {
		
		type: "bar",
		data: {
		labels: chartData.LABELS,
		datasets: [
			{
				label: "Cantidad de estudiantes",
                backgroundColor: colors,
                hoverBackgroundColor: colors,
                borderColor: "black",
                borderWidth: 0,
				data: chartData.DATA
			}
		]
		},
        options: {
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
			responsive: false,
            maintainAspectRatio: false,
            devicePixelRatio: 2,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        stepSize: 1,
                        fontColor: "black",
                        fontSize: 16,
                    }
                }],
                xAxes: [{
                    ticks: {
                        fontColor: "black",
                        fontSize: 16,
                    }
                }]
            },
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
					offset: 8,
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

function drawPieChart(cvs, chartData) {

    var dataLength = chartData.DATA.length;
    var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(dataLength);
    //chroma.js: https://gka.github.io/chroma.js/

    new Chart(cvs, {

        type: "pie",
        data: {
            labels: chartData.LABELS,
            datasets: [
                {
                    label: "Cantidad de estudiantes",
                    backgroundColor: colors,
                    hoverBackgroundColor: colors,
                    borderColor: "white",
                    borderWidth: 1,
                    data: chartData.DATA
                }
            ]
        },
        options: {
            legend: {
                display: true,
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
            responsive: false,
            maintainAspectRatio: false,
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

function addChart(leftCol, rightCol, id, tipo) {

    var cvs = document.createElement("canvas");
    var divDesviacion = document.createElement("div");
    divDesviacion.className = "desviacion";
	cvs.setAttribute("width", "800" );
    cvs.setAttribute("height", "550");
    var chartData;
	
	switch(tipo) {
		
        case classes.ESCALA:

            var labels;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerEtiquetasEscala",
                data: {
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    labels = resultados;
                }
            });

            var data;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerRespuestasEscala",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    data = resultados;
                }
            });

            var desviacion;
            $.ajax({
                url: "/ResultadosFormulario/obtenerDesviacionEstandar",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    desviacion = resultados.toFixed(2);
                }
            });

            
            var desviacionTitulo = document.createElement("h3");
            desviacionTitulo.innerText = "Desviación Estándar";
            divDesviacion.appendChild(desviacionTitulo);
            var desviacionValor = document.createElement("div");
            desviacionValor.innerText = ("" + desviacion).replace(".",",");


            divDesviacion.appendChild(desviacionValor);

            var medianita;
            $.ajax({
                url: "/ResultadosFormulario/getMedianaRespuestaEscalar",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    medianita = resultados;
                }
            });

            chartData = { DATA: data, LABELS: labels };

            var divMediana = document.createElement("div");
            divMediana.style.width = "100px";
            divMediana.style.height = "100px";
            divMediana.innerHTML = "Mediana: " + medianita;
            divMediana.style.textAlign = "center";

            drawBarChart(cvs, chartData);

			break;
			
        case classes.SELECCION_UNICA:
            var labels;
            $.ajax({
                url: "/ResultadosFormulario/ObtenterOpcionesPreguntasSeleccion",
                data: {
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    labels = resultados;
                }
            });

            var data;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerOpcionesSeleccionadasPreguntasSeleccion",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id,
                    numOpciones: labels.length
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    data = resultados;
                }
            });
            chartData = { DATA: data, LABELS: labels };
			drawPieChart(cvs, chartData);
			break;
			
        case classes.SELECCION_MULTIPLE:
            var labels;
            $.ajax({
                url: "/ResultadosFormulario/ObtenterOpcionesPreguntasSeleccion",
                data: {
                    codigoPregunta: id
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    labels = resultados;
                }
            });
            var data;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerOpcionesSeleccionadasPreguntasSeleccion",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id,
                    numOpciones: labels.length
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    data = resultados;
                }
            });
            chartData = { DATA: data, LABELS: labels };
            drawBarChart(cvs, chartData);
			break;
			
        case classes.SELECCION_CERRADA:
            var labels = ["No", "Sí", "No responde"];

            var data;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerOpcionesSeleccionadasPreguntasSeleccion",
                data: {
                    codigoFormulario: codigoFormulario,
                    siglaCurso: siglaCurso,
                    numeroGrupo: numeroGrupo,
                    semestre: semestre,
                    ano: ano,
                    codigoPregunta: id,
                    numOpciones: 3
                },
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    data = resultados;
                }
            });
            chartData = { DATA: data, LABELS: labels };
			drawPieChart(cvs, chartData);
			break;
			
		case classes.TEXTO_ABIERTO:
			
			break;
			
	}

    leftCol.appendChild(cvs);
    rightCol.appendChild(divDesviacion);
}