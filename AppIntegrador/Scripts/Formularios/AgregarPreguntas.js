agregarPreguntas = almacenador();

function selectPregunta(element) {
    if (element.checked) {
        agregarPreguntas.add(element.value);
    }
    else {
        agregarPreguntas.remove(element.value);
    }
}

function addPreguntaToSeccion(codSeccion) {
    console.log(agregarPreguntas);
    var codPreguntas = agregarPreguntas.getArray();
    var result = {codSeccion, codPreguntas};

    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Seccion/AgregarPreguntas",
        data: JSON.stringify(result),
        dataType: "json",
        traditional: true,
        success: function (data) {
            console.log("todo bien")
            if (data.insertadoExitoso) {
                console.log("todo bien x2")
                ActualizarSecciones();
            }
        }
    })
}



