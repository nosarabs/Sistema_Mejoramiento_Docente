var data = [
				[2478,5267,734,784,433],
				[1100,784,2200,5267,4333]
			];

//Create bar char.
for (var i = 0; i < questions.length; ++i) {
	chart = new Chart(document.getElementById("chart_" + i), {
		type: 'bar',
		data: {
		labels: ["1", "2", "3", "4", "5"],
		datasets: [
			{
			label: "Cantidad de estudiantes",
			backgroundColor: ["#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"],
			data: data[i]
			}
		]
		},
		options: {
			legend: { display: false },
			title: {
				display: true,
				text: 'Resultados'
			},
			responsive: false,
			maintainAspectRatio: false
		}
	});
}

//Create bullet list.
var answers = [
					['Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1',
					'Respuesta pregunta 1'],
					
					['Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2',
					'Respuesta pregunta 2']
					];

for (var i = 0; i < questions.length; ++i) {
    // Create the list element:
    var list = document.createElement("ul");

    for(var j = 0; j < answers[i].length; ++j) {
        // Create the list item:
        var item = document.createElement("li");

        // Set its contents:
        item.appendChild(document.createTextNode(answers[i][j]));

        // Add it to the list:
        list.appendChild(item);
		
		if (j < (answers[i].length - 1)) {
			//Add a line break between items.
			var br = document.createElement("br");
		
			//Add it to the list.
			list.appendChild(br);
		}
    }
	
	document.getElementById("list_" + i).appendChild(list);
}