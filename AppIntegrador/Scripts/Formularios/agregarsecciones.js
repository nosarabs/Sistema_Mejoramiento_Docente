agregarsecciones = function() {
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

function selectSeccion(element) {
    if (element.checked) {
        element.id = "secciones[" + agregarsecciones.current() + "].Codigo";
        agregarsecciones.add();
    }
    else {
        deselectSeccion(element.value);
        agregarsecciones.remove();
    }
}

function deselectSeccion(codigo) {
    var index = 0;
    var beforeElement = true;
    while (index < agregarsecciones.current() && beforeElement) {
        var seccion = document.getElementById("secciones[" + index + "].Codigo");
        if (seccion.value == codigo) {
            seccion.setAttribute("name", "");
            seccion.id = null;
            beforeElement = false;
        }
        ++index;
    }

    while (index < agregarsecciones.current()) {
        var newIndex = index - 1;
        var seccion = document.getElementById("secciones[" + index + "].Codigo");
        seccion.id = "secciones[" + newIndex + "].Codigo";
        ++index;
    }
}

function addSeccionToFormulario() {
    var index = 0;

    while (index < agregarsecciones.current()) {
        var seccion = document.getElementById("secciones[" + index + "].Codigo");
        seccion.setAttribute("name", seccion.id);
        var tupla = document.getElementById("sec(" + seccion.value + ")");
        tupla.setAttribute("hidden", "hidden");
        ++index;
    }
}