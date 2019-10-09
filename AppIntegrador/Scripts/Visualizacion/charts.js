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
            scales: {
                yAxes: [{
                    ticks: {
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
                        size: "14",
                        weight: "normal"
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
                        size: "14",
                        weight: "normal"
                    },
                    formatter: function (value, ctx) {
                        var sum = 0;
                        var data = ctx.chart.data.datasets[0].data;
                        for (var i = 0; i < data.length; ++i) {
                            sum += data[i];
                        }
                        var percentage = (value * 100 / sum).toFixed(2) + "%";
                        return percentage + "\n\n" + value;
                    }
                }
            }
        }

    });

}

function getTipoPregunta(id) {
    //alert(id);
    var tipo;
    //return "escala";
   /* $.get("/ResultadosFormulario/GetTipoPregunta",
        { codigoPregunta: id },
        function (resultado) {
            tipo = resultado;
        }
    );*/
    //return tipo;
    $.ajax(
        {
            url: '/ResultadosFormulario/GetTipoPregunta',
            data: { codigoPregunta: id },
            type: 'get',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                tipo = resultados;
                //alert(tipo);
            },
            error: function ()
            {
                alert("No sirve :'(");
            }
        });
    return tipo;
}


function addChart(cnt, id) {
	
	var cvs = document.createElement("canvas");
	cvs.setAttribute("width", "900" );
    cvs.setAttribute("height", "650");
    var tipoPregunta = getTipoPregunta(id)[0];
    //alert("Class name: " + className);
    var chartData;

    switch (tipoPregunta) {
		
        case classes.ESCALA:

            var labels;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerEtiquetasEscala/?codigoPregunta=" + id,
                type: "get",
                dataType: "json",
                async: false,
                success: function (resultados) {
                    labels = resultados;
                }
            });

            var data;
            $.ajax({
                url: "/ResultadosFormulario/ObtenerRespuestasEscala/?codigoPregunta=" + id,
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
			
		case classes.SELECCION_UNICA:
			drawPieChart(cvs, chartData);
			break;
			
		case classes.SELECCION_MULTIPLE:
			drawBarChart(cvs, chartData);
			break;
			
		case classes.SELECCION_CERRADA:
			drawPieChart(cvs, chartData);
			break;
			
		case classes.TEXTO_ABIERTO:
			
			break;
			
	}
	
	cnt.appendChild(cvs);
}