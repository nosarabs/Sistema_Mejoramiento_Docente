function createBullets(parnt, answers) {

    var list = document.createElement("ul");

    for(var i = 0; i < answers.length; ++i) {
        // Create the list item:
        var item = document.createElement("li");

        // Set its contents:
        item.appendChild(document.createTextNode(answers[i]));

        // Add it to the list:
        list.appendChild(item);
		
		if (i < (answers.length - 1)) {
			//Add a line break between items.
            var br = document.createElement("br");
            list.appendChild(br);
		}
    }
	
	parnt.appendChild(list);

}

function getJustificacion(id) {
    var justificaciones = [];

    $.ajax({
        url: "/ResultadosFormulario/getJustificacionPregunta/?codigoPregunta=" + this.id,
        type: 'get',
        dataType: 'json',
        async: false,
        success: function (resultados) {
            justificaciones = resultados;
        }
    });

    return justificaciones;
}

function addBox(cnt, id) {
	
	var box = document.createElement("div");
	box.setAttribute("class", "myBox" );
    var list = document.createElement("div");
    var box_data = ["Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
        , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."];

    /*
    EVENTUALMENTE SE DEBE ASIGNAR ASI
    var box_data = getJustificacion(id);
    */

	createBullets(list, box_data);
	box.appendChild(list);
	cnt.appendChild(box);
	
}