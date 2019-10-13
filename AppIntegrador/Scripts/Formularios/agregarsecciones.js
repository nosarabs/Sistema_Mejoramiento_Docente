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

function addSeccionToFormulario(element) {
    if (element.checked) {
        element.name = "secciones[" + agregarsecciones.current() + "].Codigo";
        element.id = element.name;
        agregarsecciones.add();
    }
    else {
        removeSeccion(element.value);
        agregarsecciones.remove();
    }
}

function removeSeccion(codigo) {
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
        seccion.setAttribute("name", "secciones[" + newIndex + "].Codigo");
        seccion.id = seccion.name;
        ++index;
    }
}