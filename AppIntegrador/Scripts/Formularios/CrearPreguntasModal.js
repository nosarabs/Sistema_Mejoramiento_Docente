
function GenerarModalPreguntas() {
    $('#ModalAgregarPregunta').modal();
    ImportarBancoPreguntas();
    GenerarCrearPreguntas();
    $('#ModalCrearPregunta').hide();
    $('#BancoDePreguntas').show();
}
function ImportarBancoPreguntas() {
    $.ajax({
        type: "post",
        url: "/Preguntas/ActualizarBancoPreguntas",
        dataType: "html",
        success: function (data) {
            $('#ModalPartialBancoPreguntas').html(data);
        }
    });
}

function MostrarCrearPregunta(){
    $('#ModalCrearPregunta').show("fast")
    $('#BancoDePreguntas').hide("fast")
}

function GenerarCrearPreguntas() {
    $.ajax({
        type: "get",
        url: "/Preguntas/CreateBase",
        dataType: "html",
        success: function (data) {
            $('#ModalCrearPregunta').html(data);
        }
    })
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
        url: "/Preguntas/ActualizarBancoPreguntas",
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
    var result = [];
    // (Workaround) Codigo trae el código del formulario, Enunciado el de la sección
    result.push({ 'Codigo': codigoFormulario, 'Enunciado': codigoSeccion, 'Tipo': null });

    var index = 0;
    var beforeElement = true;
    while (index < agregarPreguntas.current() && beforeElement) {
        var pregunta = document.getElementById("preguntas[" + index + "].Codigo");
        // Workaround para mandar las preguntas, como en la versión vieja de borrar opciones
        result.push({ 'Codigo': pregunta.value, 'Enunciado': null, 'Tipo': null });
        ++index;
    }
    console.log(result)

    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/AgregarPreguntas",
        data: JSON.stringify(result),
        dataType: "json",
        traditional: true,
        success: function(data) {
            console.log("todo bien")
            if (data.insertadoExitoso) {
                console.log("todo bien x2")
                ActualizarSecciones();
            }
        },
        error: function () {

        },
    })


}

