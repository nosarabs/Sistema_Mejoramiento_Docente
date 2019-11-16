$(function () {
    var includes = $('[data-include]');
    jQuery.each(includes, function () {
        var file = 'views/' + $(this).data('include') + '.html';
        $(this).load(file);
    });
});


var currentEnabled = null;
function enableElement(elem) {
    if (currentEnabled) {
        currentEnabled.disabled = true;
    }
    elem.disabled = false;
    currentEnabled = elem;
}


/* Historia RIP - CF5.Llama al método del controlador que genera la vista parcial filtrada y
vuelve a llamar al diálogo modal para mostrar los resultados.*/
$("#ActualizarVistaFiltros").click(function () {
    $.ajax({
        url: '@Url.Action("AplicarFiltro", "Formularios")',
        type: "post",
        //data: AddAntiForgeryToken({ id: parseInt($(this).attr("title")) }),
        data: $("form").serialize(), //if you need to post Model data, use this
        success: function (result) {
            $("#ModalSecciones").html(result);
            $('#ExampleModal').show();
        }
    });
})

function BorrarPregunta(Scod, Pcod) {
    console.log("Trivial")
    var SCodigo = Scod
    var PCodigo = Pcod
    var resultado = { SCodigo, PCodigo };
    console.log(JSON.stringify(resultado));
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/EliminarPregunta",
        data: JSON.stringify(resultado),
        dataType: "json",
        traditional: true,
        success: function (data) {
            if (data.eliminadoExitoso) {
                ActualizarSecciones();
            }
        },
        error: function () {

        }
    });
}

function BorrarSeccion(Scod) {
    // Arregla la vista cuando se deselecciona
    var seccionBanco = document.getElementById("sec(" + Scod + ")");
    $(seccionBanco).removeAttr("hidden");
    var checkboxBanco = $(seccionBanco).children().children();
    checkboxBanco = $(checkboxBanco).children();
    $(checkboxBanco).trigger("click");


    var FCodigo = document.getElementById("textCode").value
    var SCodigo = Scod
    var resultado = { FCodigo, SCodigo };

    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/EliminarSeccion",
        data: JSON.stringify(resultado),
        dataType: "json",
        traditional: true,
        success: function (data) {
            if (data.eliminadoExitoso) {
                ActualizarSecciones();
            }
        },
        error: function () {

        }
    });
}

function ActualizarSecciones() {
    var id = document.getElementById("textCode").value;
    var result = { id };
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/DesplegarFormulario",
        data: JSON.stringify(result),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            $('#seccionesActuales').html(data);
        },
        error: function () {

        }
    });
}

function ValidarCodigo() {
    var Codigo = document.getElementById("textCode").value;
    var Nombre = document.getElementById("textName").value;

    resultado = { Codigo, Nombre };

    if (document.getElementById("formularioCreado").value == 0) {
        $.ajax({
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: "/Formularios/AgregarFormulario",
            data: JSON.stringify(resultado),
            dataType: "json",
            traditional: true,
            success: function (data) {
                if (data.guardadoExitoso) {
                    /*$(".validar-agregar-secciones").each(function () {
                        this.disabled = "disabled";
                        return true;
                    })*/
                    document.getElementById("validacion-codigo").textContent = "";
                    $("#textCode").removeClass("error");
                    document.getElementById("formularioCreado").setAttribute("value", "1");

                    CrearModal();
                }
                else {
                    document.getElementById("validacion-codigo").textContent = "Código en uso";
                    $("#textCode").addClass("error");
                }
            }
        });
    }
    else {
        CrearModal();
    }
}
