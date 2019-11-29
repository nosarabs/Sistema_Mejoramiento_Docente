agregarsecciones = almacenador();

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
    console.log(agregarsecciones);
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