agregarsecciones = function() {
    var seccionesSeleccionadas = null;
    // Inicializador que crea el arreglo
    function init() {
        seccionesSeleccionadas = [];
    }
    // Agrega el elemento especificado por id
    function add(id) {
        seccionesSeleccionadas.push(id);
    }
    // Remueve la primera aparición de id en el arreglo
    // (En buena teoría solo debería existir un elemento)
    function remove(id) {
        seccionesSeleccionadas.filter(function (ele) {
            return ele != id;
        });
    }
    // Borra todo el arreglo
    function removeAll() {
        seccionesSeleccionadas = [];
    }
    // Verifica si el elemento está contenido en el arreglo
    function containsElement(id) {
        return seccionesSeleccionadas.find(function (element) {
            return element == id;
        });
    }

    return {
        init: init,
        add: add,
        remove: remove,
        removeAll: removeAll,
        containsElement: containsElement,
        // Función para obtener el arreglo. Se usa para poder pasárselo al controlador
        getArray: function () { return seccionesSeleccionadas; }
    }
}();

// Cuando se marca o desmarca un checkbox del banco,
// se agrega o elimina el elemento del arreglo de secciones.
function selectSeccion(element) {
    if (element.checked) {
        agregarsecciones.add(element.value);
    }
    else {
        agregarsecciones.remove(element.value);
    }
}

// Función que llama al controlador para agregar las secciones selecionadas al formulario
function addSeccionToFormulario() {
    // Obtener el nombre y el codigo del formulario
    var codigo = document.getElementById("textCode").value
    var nombre = document.getElementById("textName").value
    var seccionesAsociadas = agregarsecciones.getArray();
    // Construir el objeto que recibe la vista
    var resultado = { codigo, nombre, seccionesAsociadas };
    // Llamado al método del controlador
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/AsociarSeccionesAFormulario",
        data: JSON.stringify(resultado),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            // La vista es actualizada con los datos recibidos del controlador  
            $('#seccionesActuales').html(data);
        }
    });
}