//$(".dropdown-menu li").on("click", function () {
//    cambiarTipo(this);
//})

//function cambiarTipo(node) {
//    var valorSeleccionado = $(node).attr("value")
//    // Buscar el boton "combobox"
//    var botonCombobox = $(node).parent().parent()
//    // Asignar valor al dropdown
//    $(botonCombobox).find("#Tipo").attr("value", valorSeleccionado)
//    // Cambiar nombre
//    $(botonCombobox).find(".texto-btn").text($(node).text())
//}

function deshabilitarCamposBasicos(booleano) {
    $('#Codigo').prop('disabled', booleano);
    $('#Enunciado').prop('disabled', booleano);
    $('#Create').prop('disabled', booleano);

}

$("#Tipo").on("change", function () {
    if (this.value === '-') {
        deshabilitarCamposBasicos(true)
    }
    else {
        deshabilitarCamposBasicos(false)
    }

    cargarVista(this.value)
})

function cargarVista(valor) {
    $('#contenedor-preguntas').empty();
    if (valor === 'U') {
        $.ajax({
            url: 'PreguntaConOpciones',
            success: function (data) {
                $(data).appendTo('#contenedor-preguntas')
                opcionUnicaInit()

            }
        })
    }
}

function opcionUnicaInit() {
    // Hide the text input box by default
    $('#justificationField').css('display', 'none');
    $("#sortable").disableSelection();
    $("#Add").trigger('click');

}

$(document).on("click", "#Add", function () {
    var i = fixIndexes();
    $.ajax({
        url: 'OpcionesDeSeleccion',
        data: {i: i },
        success: function (data) {
            $(data).appendTo('#sortable').height(0).animate({ 'height': 75 }, 200);
            $("#sortable").find(".texto").focus();
            disableRemoveButton();
            $('#Create').prop('disabled', true);
            // Make options inside sortable id sortable
            $("#sortable").sortable({
                axis: "y",
                // Fix indexes of each input box when an input box is moved
                update: function () {
                    fixIndexes();
                },
            });
        }
    })
})

        function EnableDisableTextBox(justificationField) {
            var txtPassportNumber = document.getElementById("txtJustificationNumber");
    txtJustificationNumber.disabled = justificationField.checked ? false : true;
            if (!txtJustificationNumber.disabled) {
        txtJustificationNumber.focus();
}
validarEntradas();
        }