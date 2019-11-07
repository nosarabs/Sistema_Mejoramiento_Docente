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

function ValidarCodigo() {
    var Codigo = document.getElementById("textCode").value;
    var Nombre = document.getElementById("textName").value;

    resultado = { Codigo, Nombre };
    console.log(JSON.stringify(resultado));

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
                    $(".validar-agregar-secciones").each(function () {
                        this.disabled = "disabled";
                        return true;
                    })

                    console.log("Todo es trivial");
                    document.getElementById("validacion-codigo").textContent = "";
                    $("#textCode").removeClass("error");
                    document.getElementById("formularioCreado").setAttribute("value", "1");

                    CrearModal();
                }
                else {
                    console.log("Cmamo");
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