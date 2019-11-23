$(document).ready(function () {
    cantidadProfes = new Counter();
    cantidadForm = new Counter();
});

class Counter {
    current = null;
    constructor() {
        this.current = 0;
    }
    add() {
        this.current++;
    }
    remove() {
        this.current--;
    }
    getCurrent() {
        return this.current;
    }
}

function seleccionaCheckBoxGen(element, variable, key, counter) {
    if (element.checked) {
        element.id = `${variable}[${counter.getCurrent()}].${key}`;
        counter.add();
    }
    else {
        deseleccionarGen(element.value, key, counter);
        counter.remove();
    }
}

function deseleccionarGen(variable, key, counter) {
    let index = 0;
    let found = true;
    for (; index < counter.getCurrent() && found; ++index) {
        var gen = document.getElementById(`${variable}[${index}].${key}`);
        if (gen.value.localeCompare(key) == 0) {
            gen.setAttribute("name", "");
            gen.id = null;
            found = false;
        }
    }
    for (; index < counter.getCurrent(); ++index) {
        let newIndex = index - 1;
        let gen = document.getElementById(`${variable}[${index}].${key}`);
        gen.id = `${variable}[${newIndex}].${key}`;
    }
}

function agregarGen(variable, key, counter, url, attribute, div) {
    console.log(counter.getCurrent());
    for (let index = 0; index < counter.getCurrent(); ++index) {
        let gen = document.getElementById(`${variable}[${index}].${key}`);
        gen.setAttribute("name", `${attribute}`);
    }
    $.ajax({
        type: 'POST',
        url: `${url}`,
        data: $('form').serialize(),
        dataType: 'html', //dataType - html
        success: function (result)
        {
            $(`${div}`).html(result);
        }
    });
}

function modalGen(modal) {
    $(`${modal}`).modal();
}

function validarCampos(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton) {
    let fechaInicio = document.getElementById(campoFechaInicio);
    let fechaFinal = document.getElementById(campoFechaFin);
    let nombre = document.getElementById(campoNombre);
    let totalValidations = campoDescripcion == null ? 2 : 3;

    // Dejando el limite superior de las fechas a 10 años en el caso de la creacion de los planes de mejora
    let minDate = null;
    let topDate = null; 
    if (campoFechaInferior != null && campoFechaSuperior != null) {
        let temp = document.getElementById(campoFechaInferior.id).value;
        minDate = new Date(temp + 'CST');
        temp = document.getElementById(campoFechaSuperior.id).value
        topDate = new Date(temp + 'CST');
    } else {
        minDate = new Date(); // Todays Date
        topDate = new Date(minDate.getFullYear() + 10, minDate.getMonth(), minDate.getDate()); //10 years from now
    }

    let validator = new Validador(50, 250, minDate, topDate, nombreBoton);

    //Definimos la cantidad de validaciones
    validator.setTotalValidations(totalValidations);

    // Ahora haciendo las validaciones
    validator.validateSomethingInTextInput(nombre, 50);
    validator.validateDates(fechaInicio, fechaFinal);

    if (campoDescripcion != null) {
        validator.validateSomethingInTextInput(campoDescripcion, 50);
    }


    validator.validityOfForm();
}