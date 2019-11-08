
function GenerarModalPreguntas() {
    $('#ModalAgregarPregunta').modal();
    ImportarBancoPreguntas();
    $('ModalPartialBancoPreguntas').show();
}
function ImportarBancoPreguntas() {
    var resultado = ["", "", "", ""];
    console.log(JSON.stringify(resultado));
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/ActualizarBancoPreguntas",
        data: JSON.stringify(resultado),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            $('#ModalPartialBancoPreguntas').html(data);
        },
        error: function () {

        }
    });
}


function FiltrarBancoPreguntas() {

    var input0 = document.getElementById("other-option").value;
    var input1 = document.getElementById("codigo-option").value;
    var input2 = document.getElementById("enunciado-option").value;
    var tmp = document.getElementById("type-option");
    var input3 = tmp.options[tmp.selectedIndex].value;


    var resultado = { input0, input1, input2, input3 };
    console.log(JSON.stringify(resultado));
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/ActualizarBancoPreguntas",
        data: JSON.stringify(resultado),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            $('#ModalPartialBancoPreguntas').html(data);
        },
        error: function () {

        }
    });
}


agregarPreguntas = function () {
    var current = null;
    function init() {
        current = 0;
    }
    function add() {
        current++;
    }
    function remove() {
        current--;
    }

    return {
        init: init,
        add: add,
        remove: remove,
        current: function () { return current; }
    }
}();

function selectPregunta(element) {
    if (element.checked) {
        element.id = "preguntas[" + agregarPreguntas.current() + "].Codigo";
        agregarPreguntas.add();
    }
    else {
        deselectPregunta(element.value);
        agregarPreguntas.remove();
    }
}

function deselectPregunta(codigo) {
    var index = 0;
    var beforeElement = true;
    while (index < agregarPreguntas.current() && beforeElement) {
        var pregunta = document.getElementById("preguntas[" + index + "].Codigo");
        if (pregunta.value == codigo) {
            pregunta.setAttribute("name", "");
            pregunta.id = null;
            beforeElement = false;
        }
        ++index;
    }

    while (index < agregarPreguntas.current()) {
        var newIndex = index - 1;
        var pregunta = document.getElementById("preguntas[" + index + "].Codigo");
        pregunta.id = "preguntas[" + newIndex + "].Codigo";
        ++index;
    }
}

function addPreguntaToSeccion(codigoSeccion) {

    var codigoFormulario = document.getElementById("textCode").value;

    var index = 0;
    var preguntasAsociadas = [];
    var beforeElement = true;
    while (index < agregarPreguntas.current() && beforeElement) {
        var pregunta = document.getElementById("preguntas[" + index + "].Codigo");
        preguntasAsociadas.push(pregunta.value);
        ++index;
    }

    console.log(JSON.stringify(resultado));
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/AgregarPreguntasASeccion",
        data: JSON.stringify({ codigoFormulario:codigoFormulario, codigoSeccion:codigoSeccion, preguntas:preguntasAsociadas}),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            $('#seccionesActuales').html(data);
        },
    })


}





