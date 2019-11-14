$(document).ready(function () {
    cantidadProfes.init();
});


cantidadProfes = function () {
    var current = null;
    function init() {
        current = 0;
    }
    function add() {
        current++;
    }
    function remove() {
        current--;
    }

    return {
        init: init,
        add: add,
        remove: remove,
        current: function () { return current; }
    }
}();

function seleccionaCheckBoxProfe(element) {
    if (element.checked) {
        element.id = "profes[" + cantidadProfes.current() + "].correo";
        cantidadProfes.add();
    }
    else {
        deseleccionarProfe(element.value);
        cantidadProfes.remove();
    }
}

function deseleccionarProfe(correo) {
    let index = 0;
    let found = true;
    for (; index < cantidadProfes.current() && found; ++index) {
        var profe = document.getElementById("profes[" + index + "].correo");
        if (profe.value.localeCompare(correo) == 0) {
            profe.setAttribute("name", "");
            profe.id = null;
            found = false;
        }
    }
    for (; index < cantidadProfes.current(); ++index) {
        let newIndex = index - 1;
        let profe = document.getElementById("profes[" + index + "].correo");
        profe.id = "profes[" + newIndex + "].correo";
    }
}

function agregarProfesores() {
    for (let index = 0; index < cantidadProfes.current(); ++index) {
        let profe = document.getElementById("profes[" + index + "].correo");
        profe.setAttribute("name", "ProfeSeleccionado");
        console.log(profe.value);
    }
}


function modalProfes() {
    $('#ModalProfesores').modal();
}

function validarPlanDeMejora() {
    let fechaInicioPlan = document.getElementById('campoFechaInicioPlanMejora');
    let fechaFinalPlan = document.getElementById('campoFechaFinPlanMejora');
    let nombrePlan = document.getElementById('campoNombrePlanMejora');

    // Dejando el limite superior de las fechas a 10 años en el caso de la creacion de los planes de mejora
    let minDate = new Date(); // Todays Date
    let topDate = new Date(minDate.getFullYear() + 10, minDate.getMonth(), minDate.getDate()); //10 years from now
    let validator = new Validador(50, 50, minDate, topDate, 'CrearPlanBoton');

    //Definimos la cantidad de validaciones
    validator.setTotalValidations(2);

    // Ahora haciendo las validaciones
    validator.validateSomethingInTextInput(nombrePlan, 50);
    validator.validateDates(fechaInicioPlan, fechaFinalPlan);

    validator.validityOfForm();
}