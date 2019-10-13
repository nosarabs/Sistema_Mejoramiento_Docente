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
        agregarsecciones.add();

        console.log(element.name);
    }
    else {
        element.name = "";
        agregarsecciones.remove();
        // Cambiar ids de las secciones siguientes
    }
}