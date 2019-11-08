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

function GuardarSeccion() {
    $.ajax({
        url: '@Url.Action("CreateSeccion", "Formularios")',
        type: "post",
        //data: AddAntiForgeryToken({ id: parseInt($(this).attr("title")) }),
        data: $("form").serialize(), //if you need to post Model data, use this
        success: function (result) {
            $("#ModalCrearSeccion").html(result);
            CerrarCrearSeccion();
        }
    });
}

/* Permite que los checkboxes sean persistentes a través del refrescamiento de la página.*/

var checkboxValues = JSON.parse(localStorage.getItem('checkboxValues')) || {},
    $checkboxes = $("#checkbox-container :checkbox");

$checkboxes.on("change", function () {
    $checkboxes.each(function () {
        checkboxValues[this.id] = this.checked;
        console.log(this.id + "Checked")
    });

    localStorage.setItem("checkboxValues", JSON.stringify(checkboxValues));
});

// On page load
$(document).ready(function () {
    $(checkboxValues).each(function (key, value) {
        $("#" + key).prop('checked', value);
    });
});

