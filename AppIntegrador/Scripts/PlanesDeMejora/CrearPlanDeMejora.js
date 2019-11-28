$(document).ready(function () {
    cantidadProfes = new Counter();
    cantidadForm = new Counter();
    objetivos = [];
    accMejora = [];
    accionables = [];
    correosProfes = [];
    codigosFormularios = [];
    currentPlan = null;
});

function getPlan() {
    let campoNombre = document.getElementById("campoNombrePlanMejora");
    let campoFechaInicio = document.getElementById("campoFechaInicioPlanMejora");
    let campoFechaFin = document.getElementById("campoFechaFinPlanMejora");

    currentPlan = new PlanMejora(campoNombre.value, campoFechaInicio.value, campoFechaFin.value);
}

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
        deseleccionarGen(variable, key, counter, element.value);
        counter.remove();
    }
}

function deseleccionarGen(variable, key, counter, value) {
    let index = 0;
    let found = true;
    for (; index < counter.getCurrent() && found; ++index) {
        var gen = document.getElementById(`${variable}[${index}].${key}`);
        if (gen.value.localeCompare(value) == 0) {
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

function agregarGen(variable, key, counter, url, attribute, div, array) {
    console.log(counter.getCurrent());
    array.splice(0, array.length);
    for (let index = 0; index < counter.getCurrent(); ++index) {
        let gen = document.getElementById(`${variable}[${index}].${key}`);
        gen.setAttribute("name", `${attribute}`);
        array.push(gen.value);
    }

    array.forEach(element => {
        console.log(element);
    })

    $.ajax({
        type: 'POST',
        url: `${url}`,
        data: $(`[name=${attribute}]`).serialize(),
        dataType: 'html', //dataType - html
        success: function (result)
        {
            $(`${div}`).html(result);
        }
    });
}

function agregarObjetivo() {
    $.ajax({
        type: 'POST',
        url: '/Objetivos/AnadirObjetivo',
        data: $('#formObjetivos :input').serialize(),
        dataType: 'html',
        success: function () {
            console.log("and i oop");
        }
    })
}

function enviarDatosPlan() {
    getPlan();
    currentPlan.setCorreosProfes(correosProfes);
    currentPlan.setCodigosFormularios(codigosFormularios);
    $.ajax({
        type: 'POST',
        url: '/PlanDeMejora/Crear',
        data: JSON.stringify(currentPlan),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        accept: 'application/json',
        success: function () {
            console.log("and i oop");
            window.location.href = '/PlanDeMejora/Index';
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

class PlanMejora {
    nombre = null;
    fechaInicio = null;
    fechaFin = null;
    ProfeSeleccionado = null;
    FormularioSeleccionado = null;

    constructor(nombre, fechaInicio, fechaFin) {
        this.nombre = nombre;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }

    setCorreosProfes(correosProfes) {
        this.ProfeSeleccionado = correosProfes;
    }

    setCodigosFormularios(codigosFormularios) {
        this.FormularioSeleccionado = codigosFormularios;
    }
}

class Objetivo extends PlanMejora {
    tipo = null;
    descripcion = null;

    constructor(nombre, tipo, descripcion, fechaInicio, fechaFin) {
        super(nombre, fechaInicio, fechaFin);
        this.tipo = tipo;
        this.descripcion = descripcion;
    }
}

class AccionDeMejora extends PlanMejora {
    descripcion = null;
    constructor(nombre, descripcion, fechaInicio, fechaFin) {
        super(nombre, fechaInicio, fechaFin);
        this.descripcion = descripcion;
    }
}

class Accionable extends AccionDeMejora {
    descripcionAcc = null;
    constructor(nombre, descripcion, descripcionAcc, fechaInicio, fechaFin) {
        super(nombre, descripcion, fechaInicio, fechaFin);
        this.descripcionAcc = descripcionAcc;
    }
}

//class valoresArreglos {
//    elementos = null;

//    constructor() {
//        elementos = [];
//    }

//    limpiar() {
//        elementos.splice(0, elementos.length);
//    }

//    agregarElemento(elemento) {
//        elementos.push(elemento);
//    }

//    cantidad() {
//        return elementos.length;
//    }

//    imprimir() {
//        elementos.forEach(element => {
//            console.log(element);
//        });
//    }
//}