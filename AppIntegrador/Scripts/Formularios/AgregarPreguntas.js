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

function addPreguntaToSeccion() {
    var index = 0;

    while (index < agregarPreguntas.current()) {
        var seccion = document.getElementById("preguntas[" + index + "].Codigo");
        seccion.setAttribute("name", seccion.id);
        var tupla = document.getElementById("preg(" + seccion.value + ")");
        tupla.setAttribute("hidden", "hidden");
        ++index;
    }
}



