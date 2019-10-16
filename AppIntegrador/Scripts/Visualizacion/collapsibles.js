//Auxiliar functions

function appendLineBreaks(elmnt, number) {
	
	for (var i = 0; i < number; ++i) {
		elmnt.appendChild(document.createElement("br"));
	}
	
}

function appendTitle(elmnt, txt) {
	var title = document.createElement("h3");
	title.innerHTML = txt;
	elmnt.appendChild(title);
}

function getTipoPregunta(id) {
    var tipo;
    $.ajax({
        url: '/ResultadosFormulario/GetTipoPregunta',
        data: { codigoPregunta: id },
        type: 'get',
        dataType: 'json',
        async: false,
        success: function (resultados) {
            tipo = resultados;
        }
    });
    return tipo;
}

function createCollapsible(id, question) {
    var btn = document.createElement("button");   	//Create a <button> element
    btn.className = "collapsible";                  //Set the button as collapsible
	btn.innerHTML = question;						//Insert text
	var cnt = document.createElement("div");
    cnt.className = "content";
	
	btn.addEventListener("click", function() {		//Add an event listener to the button
		
        if (this.nextElementSibling.childElementCount == 0) {

            this.id = getTipoPregunta(id)[0];

            var cnt = this.nextElementSibling;

            if (this.id == "texto_abierto") {

                addBox(cnt, id, this.id);

            } else {
                var contenedor = document.createElement("div");
                contenedor.className = "container";
                var row = document.createElement("div");
                row.className = "row d-flex justify-content-center";
                var leftCol = document.createElement("div");
                leftCol.className = "col";
                var rightCol = document.createElement("div");
                rightCol.className = "col";

                //leftCol.appendChild(wrapper);
                row.appendChild(leftCol);
                row.appendChild(rightCol);
                contenedor.appendChild(row);
                cnt.appendChild(contenedor);
                //cnt.appendChild(row);


                addChart(leftCol, rightCol, id, this.id);
                appendTitle(rightCol, "Justificación de los resultados");
                addBox(rightCol, id, this.id);

            }
			
		}
		
		this.classList.toggle("activeCollapsible");
		var content = this.nextElementSibling;
		
		if (content.style.maxHeight){
			
			content.style.maxHeight = null;
			
		} else {
			
			content.style.maxHeight = content.scrollHeight + "px";
			
		} 
		
	});

	document.body.appendChild(btn);
	document.body.appendChild(cnt);
	
}

//Main
for (var i = 0; i < questions.length; ++i) {
    var id = questions[i].Value;
	var question = String(i + 1) + ". " + questions[i].Text;
	createCollapsible(id, question);
}