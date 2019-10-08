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

function addBox(cnt, id) {
	
	var box = document.createElement("div");
	box.setAttribute("class", "myBox" );
    var list = document.createElement("div");
    var box_data = ["Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."
        , "Hola esto es una prueba de que se puede scrollear sin ning�n problema y que se muestran los bullets de la caja de texto. Tambi�n quiero ver c�mo se ven los p�rrafos de m�s de una l�nea."];

	createBullets(list, box_data);
	box.appendChild(list);
	cnt.appendChild(box);
	
}