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

function addChart(cnt, chartData, className) {
	
	var cvs = document.createElement("canvas");
	cvs.setAttribute("width", "900" );
	cvs.setAttribute("height", "650" );
	
	switch(className) {
		
		case classes.ESCALA:
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