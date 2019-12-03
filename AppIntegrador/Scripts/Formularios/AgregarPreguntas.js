﻿agregarPreguntas = almacenador();

function selectPregunta(element) {
    if (element.checked) {
        agregarPreguntas.add(element.value);
    }
    else {
        agregarPreguntas.remove(element.value);
    }
}

function addPreguntaToSeccion() {
    var codPreguntas = agregarPreguntas.getArray();
    var codSeccion = SeccionModalActual;
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
                $('#ModalAgregarPregunta').modal('hide');$('body').removeClass('modal-open');$('.modal-backdrop').remove();
                ActualizarSecciones();
                agregarPreguntas.removeAll();
            }
        }
    })
}



