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

function addPreguntaToSeccion(element) {
    if (element.checked) {
        element.name = "preguntas[" + agregarPreguntas.current() + "].Codigo";
        element.id = element.name;
        agregarPreguntas.add();
    }
    else {
        removePregunta(element.value);
        agregarPreguntas.remove();
    }
}

function removePregunta(codigo) {
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
        pregunta.setAttribute("name", "preguntas[" + newIndex + "].Codigo");
        pregunta.id = pregunta.name;
        ++index;
    }
}