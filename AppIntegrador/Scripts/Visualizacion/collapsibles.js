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

function getTipo(id) {
    var tipo;

    $.get("/ResultadosFormulario/getTipoPregunta", { id: id },
        function (data) {
            tipo = data;
        }
    );

    /*
    OTRA FORMA DE HACERLO
    SI SE UTILIZA NO SE NECESITA [HTTPGET] EN LA FUNCION DEL .CS

    $.ajax({
        url: "/ResultadosFormulario/getTipoPregunta/?id=" + id,
        type: 'get',
        dataType: 'html',
        async: false,
        success:    function (data)
                    {
                        alert(data);
                    }
    });
    */

    return tipo;
}


function createCollapsible(id, question) {

    var btn = document.createElement("button");     //Create a <button> element
    btn.className = getTipo(id);   					// Set its class as a collapsible
	btn.innerHTML = question;						//Insert text
	var cnt = document.createElement("div");
	cnt.className = "content";

	btn.addEventListener("click", function() {		//Add an event listener to the button

		if (this.nextElementSibling.childElementCount == 0) {

			appendLineBreaks(cnt, 2);
			addChart(cnt, id);
			appendLineBreaks(cnt, 6);
			appendTitle(cnt, "Justificación de los resultados");
            addBox(cnt, id);
			appendLineBreaks(cnt, 2);
			
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
