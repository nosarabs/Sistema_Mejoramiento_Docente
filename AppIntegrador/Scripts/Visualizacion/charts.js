function drawChart(cvs, chartData, type) {
	
	var dataLength = chartData.DATA.length;
	var colors = chroma.scale(["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"]).colors(dataLength);
	//chroma.js: https://gka.github.io/chroma.js/
	
	new Chart(cvs, {
		
		type: type,
		data: {
		labels: chartData.LABELS,
		datasets: [
			{
				label: "Cantidad de estudiantes",
				backgroundColor: colors,
				data: chartData.DATA
			}
		]
		},
		options: {
			legend: { display: true },
			title: {
				display: false,
			},
			responsive: false,
			maintainAspectRatio: false,
			plugins: {
				datalabels: {
					color: "white",
					anchor: "end",
					align: "start",
					clamp: true,
					offset: 16,
					textAlign: "center",
					font: {
						size: "14",
						weight: "bold"
					},
					formatter: function(value, ctx) {
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
			drawChart(cvs, chartData, types.ESCALA);
			break;
			
		case classes.SELECCION_UNICA:
			drawChart(cvs, chartData, types.SELECCION_UNICA);
			break;
			
		case classes.SELECCION_MULTIPLE:
			drawChart(cvs, chartData, types.SELECCION_MULTIPLE);
			break;
			
		case classes.SELECCION_CERRADA:
			drawChart(cvs, chartData, types.SELECCION_CERRADA);
			break;
			
		case classes.TEXTO_ABIERTO:
			
			break;
			
	}
	
	cnt.appendChild(cvs);
}