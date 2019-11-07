$('#ExampleModal').on('hide.bs.modal', function () {
    console.log('Todo es trivial')

    var seccionesAsociadas = []
    var index = 0;
    var beforeElement = true;
    while (index < agregarsecciones.current() && beforeElement) {
        var secc = document.getElementById("secciones[" + index + "].Codigo")
        seccionesAsociadas.push(secc.value);
        ++index;
    }

    var codigo = document.getElementById("textCode").value
    var nombre = document.getElementById("textName").value

    var resultado = { codigo, nombre, seccionesAsociadas };
    console.log(JSON.stringify(resultado));
    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/AsociarSesionesAFormulario",
        data: JSON.stringify(resultado),
        dataType: "html",
        traditional: true,
        success: function (data) {
            resultado = [];
            console.log(data);
            $('#seccionesActuales').html(data);
        },
        error: function () {

        }
    });
});
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
function CrearModal() {
    $('#ExampleModal').modal();
    $('#ModalAgregarSecciones').show();
}
$('.CrearSeccionModal').click(function () {
    $('#ModalCrearSeccion').show("fast");
    $('#ModalAgregarSecciones').hide("fast");
});
$('#ExampleModal').on('show.bs.modal', function (event) {
    $('#ModalCrearSeccion').hide();
})
function CerrarCrearSeccion() {
    $('#ModalCrearSeccion').hide("fast");
    $('#ModalAgregarSecciones').show("fast");
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

function mostrarSecciones() {
    var Codigo = document.getElementById("textCode").value;
    var Nombre = document.getElementById("textName").value;

    resultado = { Codigo, Nombre };
    console.log(JSON.stringify(resultado));

    $.ajax({
        contentType: "application/json; charset=utf-8",
        type: "POST",
        url: "/Formularios/AgregarFormulario",
        data: JSON.stringify(resultado),
        dataType: "json",
        traditional: true,
        success: function (data) {
            if (data.guardadoExitoso) {
                $("#secciones").removeAttr("hidden");
                $(".validar-agregar-secciones").each(function () {
                    this.disabled = "disabled";
                    return true;
                });
                $('#CrearFormulario').disabled = "disabled";

                $("#secciones").removeAttr("hidden");

                console.log("Todo es trivial");
                document.getElementById("validacion-codigo").textContent = "";
                $("#textCode").removeClass("error");
                document.getElementById("formularioCreado").setAttribute("value", "1");

                // document.getElementById("CreateFormulario").value = "Guardar";
            }
            else {
                console.log("Cmamo");
                document.getElementById("validacion-codigo").textContent = "Código en uso";
                $("#textCode").addClass("error");
            }
        }
    });
}