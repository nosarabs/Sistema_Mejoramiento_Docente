// Tiene que hacérsele el evento a document para que las futuras opciones
// que agrega AJAX sean incluidas también en el trigger del evento
$(document).on("keyup", ".validar", function () {
    validarEntradas();
})

$("#txtJustificationNumber").on("keyup", function () {
    validarEntradas();
})

function validarEntradas() {
    var todoLleno = true;
    if ($("#showField").is(':checked') === true) {
        console.log("Fuck");
        if ($("#txtJustificationNumber").val() == '') {
            console.log("Fuck2");
            todoLleno = false;
        }
    }

    if (todoLleno == true) {
        $(".validar").each(function () {
            if ($(this).val() == '') {
                todoLleno = false;
                // Equivalente a un break, pero JQuery ocupa que sea 
                // con un return por la nueva función que se "crea"
                return false;
            }
        })
    }

    $('#Create').prop('disabled', !todoLleno);
}