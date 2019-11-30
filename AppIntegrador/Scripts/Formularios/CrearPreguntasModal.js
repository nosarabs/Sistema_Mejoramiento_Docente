//Hola
//Esta es una prueba
//De comentario de varias líneas
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

