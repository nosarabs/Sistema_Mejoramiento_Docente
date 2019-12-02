
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

function CerrarCrearPregunta() {
    $('#ModalCrearPregunta').hide("fast");
    $('#BancoDePreguntas').show("fast");
}

function GuardarPregunta() {
    var Codigo = document.getElementById("PreguntaCodigo").value;
    var Enunciado = document.getElementById("PreguntaEnunciado").value;
    var Tipo = document.getElementById("Tipo").value;
    var Justificacion = document.getElementById("txtJustificationNumber").value;

    Pregunta = { Codigo, Enunciado, Tipo };
    OpcionesDeSeleccion = []

    if (Tipo == "U" || Tipo == "M") {

        var ListaOpciones = document.getElementsByClassName("opcion-de-seleccion");
        for (i = 0; i < ListaOpciones.length; i++) {
            OpcionesDeSeleccion.push(ListaOpciones[i].value);
        }
    }
    if (Tipo == "E"){
        var max = document.getElementById("max").value;
        var min = document.getElementById("min").value;
    }
    var result = { Pregunta, Justificacion, OpcionesDeSeleccion, min, max };
    $.ajax({
        contentType: "application/json; charset=utf8",
        type: "POST",
        url: "/Preguntas/GuardarPregunta",
        data: JSON.stringify(result),
        dataType: "json",
        traditional: true,
        success: function (data) {
            if (data.CodigoEnUso) {
                document.getElementById("validar-codigo-pregunta").textContent = "Codigo en uso";
                $("#PreguntaCodigo").addClass("error");
            }
            if (data.FaltaOpciones) {
                //document.getElementById("validar-codigo-pregunta").textContent = "Codigo en uso";
                $("#agregar-opcion").addClass("error");
            }
            if (data.MinMax) {
                document.getElementById("min").textContent = "El primer número debe ser menor al segundo";
                $("#min").addClass("error");
            }
            if (data.guardadoExitoso) {
                $.ajax({
                    url: "/Preguntas/CreateBase",
                    type: "post",
                    dataType: "html",
                    success: function (result) {
                        $('#ModalCrearPregunta').html(result);
                        CerrarCrearPregunta();
                    }
                })
                
            }
        }
    })
}

function MostrarCrearPregunta(){
    $('#ModalCrearPregunta').show("fast")
    $('#BancoDePreguntas').hide("fast")
}

function GenerarCrearPreguntas() {
    $.ajax({
        type: "post",
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

