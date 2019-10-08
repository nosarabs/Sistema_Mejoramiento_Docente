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

function addBox(cnt, id, tipo) {
	
	var box = document.createElement("div");
	box.setAttribute("class", "myBox" );
    var list = document.createElement("div");
    var box_data;

    if (tipo == "texto_abierto") {

        $.ajax({
            url: "/ResultadosFormulario/ObtenerRespuestasTextoAbierto",
            data: {
                codigoFormulario: codigoFormulario,
                siglaCurso: siglaCurso,
                numeroGrupo: numeroGrupo,
                semestre: semestre,
                año: año,
                codigoPregunta: id,
            },
            type: "get",
            dataType: "json",
            async: false,
            success: function (resultados) {
                box_data = resultados;
            }
        });
        
    } else {

        box_data = ["Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."
            , "Hola esto es una prueba de que se puede scrollear sin ningún problema y que se muestran los bullets de la caja de texto. También quiero ver cómo se ven los párrafos de más de una línea."];

    }

	createBullets(list, box_data);
	box.appendChild(list);
	cnt.appendChild(box);
	
}