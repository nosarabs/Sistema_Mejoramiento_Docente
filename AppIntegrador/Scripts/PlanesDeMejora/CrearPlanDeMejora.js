$(document).ready(function () {
    cantidadProfes = new Counter();
    cantidadForm = new Counter();
    correosProfes = [];
    codigosFormularios = [];
    codigosSecciones = [];
    SeccionConObjetivoDict = {};

    currentPlan = new PlanMejora();
    currentObjective = null;
    currentAccMej = null;
});

function getPlan() {
    let campoNombre = document.getElementById("campoNombrePlanMejora");
    let campoFechaInicio = document.getElementById("campoFechaInicioPlanMejora");
    let campoFechaFin = document.getElementById("campoFechaFinPlanMejora");

    currentPlan.setPlan(campoNombre.value, campoFechaInicio.value, campoFechaFin.value);
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
    /*console.log(element);
    console.log(variable);
    console.log(key);
    console.log(counter);*/


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

function agregarSeccionesSeleccionadas() {
    SeccionConObjetivoDict[currentObjective.nombre] = [];
    $("input:checkbox[name=checkBoxSeccionObjetivo]:checked").each(function () {
        console.log($(this).val());
        SeccionConObjetivoDict[currentObjective.nombre].push($(this).val());
    });
}

function agregarObjetivo() {
    let campoNombre = document.getElementById("campoNombreObjetivo");
    let campoDescripcion = document.getElementById("campoDescripcionObjetivo");
    let campoTipoObjetivo = document.getElementById("campoTipoObjetivo");
    let campoFechaInicio = document.getElementById("campoFechaInicioObjetivo");
    let campoFechaFin = document.getElementById("campoFechaFinObjetivo");

    currentPlan.pushObjetivo(new Objetivo(campoNombre.value, campoTipoObjetivo.value, campoDescripcion.value, campoFechaInicio.value, campoFechaFin.value));
    campoNombre.value = "";
    campoDescripcion.value = "";
    campoFechaInicio.value = document.getElementById("campoFechaInicioPlanMejora").value;
    campoFechaFin.value = document.getElementById("campoFechaFinPlanMejora").value;
    console.log(JSON.stringify(currentPlan.Objetivo));
    $.ajax({
        type: 'POST',
        url: '/Objetivos/AnadirObjetivos',
        data: JSON.stringify(currentPlan.Objetivo),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        accept: 'json',
        success: function (result) {
            $('#divTablaObjetivos').html(result.message);
        }
    });
}

function mostrarTablaAccionMejora() {
    let tabla = document.getElementById("seccionAccionesMejora");
    tabla.removeAttribute("hidden");
    $.ajax({
        type: 'POST',
        url: '/AccionDeMejora/AnadirAccionesDeMejora',
        data: JSON.stringify(currentObjective.AccionDeMejora),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        accept: 'json',
        success: function (result) {
            $('#divTablaAccionesDeMejora').html(result.message);
        }
    });
}

function seleccionaObjetivo(element) {
    let val = element.value;
    currentObjective = currentPlan.Objetivo[val];
    console.log(`${currentObjective.nombre}: ${currentObjective.descripcion}`);
    mostrarTablaAccionMejora();
}

function agregarAccionMejora() {
    let campoDescripcion = document.getElementById("campoDescripcionAccionMejora");
    let campoFechaInicio = document.getElementById("campoFechaInicioAccionMejora");
    let campoFechaFin = document.getElementById("campoFechaFinAccionMejora");
    currentObjective.addAccionDeMejora(new AccionDeMejora(currentObjective.nombre, campoDescripcion.value, campoFechaInicio.value, campoFechaFin.value));

    campoDescripcion.value = "";

    let campoFechaInicioObj = document.getElementById("campoFechaInicioObjetivo");
    let campoFechaFinObj = document.getElementById("campoFechaFinObjetivo");
    campoFechaInicio.value = campoFechaInicioObj.value;
    campoFechaFin.value = campoFechaFinObj.value;

    mostrarTablaAccionMejora();
}

function mostrarTablaAccionable() {
    let tabla = document.getElementById("seccionAccionables");
    tabla.removeAttribute("hidden");
    $.ajax({
        type: 'POST',
        url: '/Accionables/AnadirAccionables',
        data: JSON.stringify(currentAccMej.Accionable),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        accept: 'json',
        success: function (result) {
            $('#divTablaAccionables').html(result.message);
        }
    });
}

function seleccionaAccion(element) {
    let val = element.value;
    currentAccMej = currentObjective.AccionDeMejora[val];
    console.log(`${currentAccMej.nombre}: ${currentAccMej.descripcion}`);
    mostrarTablaAccionable();
}

function agregarAccionable() {
    let campoDescripcion = document.getElementById("campoDescripcionAccionable");
    let campoFechaInicio = document.getElementById("campoFechaInicioAccionable");
    let campoFechaFin = document.getElementById("campoFechaFinAccionable");

    currentAccMej.addAccionable(new Accionable(currentObjective.nombre, currentAccMej.descripcion, campoDescripcion.value, campoFechaInicio.value, campoFechaFin.value));

    campoDescripcion.value = "";

    let campoFechaInicioObj = document.getElementById("campoFechaInicioObjetivo");
    let campoFechaFinObj = document.getElementById("campoFechaFinObjetivo");
    campoFechaInicio.value = campoFechaInicioObj.value;
    campoFechaFin.value = campoFechaFinObj.value;

    mostrarTablaAccionable();
}

function getSecciones() {
    modalGen('#ModalSecciones');
    console.log(codigosFormularios.length);

    if (codigosFormularios.length > 0) {
        let formularios = { FormularioSeleccionado: codigosFormularios };
        console.log(JSON.stringify(formularios));

        $.ajax({
            type: 'POST',
            url: '/Objetivos/ObtenerSecciones',
            data: JSON.stringify(formularios),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            accept: 'application/json',
            traditional: true,
            success: function (result) {
                console.log("and i oop");
                $('#ModalAgregarSeccionesInterno').html(result.message);
            }
        });
    }
}

function enviarDatosPlan() {
    getPlan();
    currentPlan.setCorreosProfes(correosProfes);
    currentPlan.setCodigosFormularios(codigosFormularios);
    currentPlan.setSeccionConObjetivo(SeccionConObjetivoDict);
    console.log(JSON.stringify(currentPlan));

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

/*
 * Funcion encargada de tomar los profesores a los que se les queire asignar un Plan de Mejora
 */
function getProfesoresOfPlan() {
    var table = document.getElementById("TablaMostrarProfesores");
    for (var i = 0, row; row = table.rows[i]; i++) {
        //iterate through rows
        //rows would be accessed using the "row" variable assigned in the for loop
        for (var j = 0, col; col = row.cells[j]; j++) {
            console.log(col);
        }
    }
}

/*
 * Funcion encargada de tomar los formularios a los que se les quiere asiganar a un Plan de Mejora
 */
function getFormulariosOfPlan() {
    var table = document.getElementById("TablaMostrarFormularios");
    for (var i = 0, row; row = table.rows[i]; i++) {
        //iterate through rows
        //rows would be accessed using the "row" variable assigned in the for loop
        for (var j = 0, col; col = row.cells[j]; j++) {
            console.log(col);
        }
    }
}

function modalGen(modal) {
    $(`${modal}`).modal();
}

function manejarFechasPlan(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton) {
    validarCampos(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton);
    let campoFechaInicioObj = document.getElementById("campoFechaInicioObjetivo");
    let campoFechaFinObj = document.getElementById("campoFechaFinObjetivo");
    let campoFechaInicioPlan = document.getElementById("campoFechaInicioPlanMejora");
    let campoFechaFinPlan = document.getElementById("campoFechaFinPlanMejora");
    campoFechaInicioObj.value = campoFechaInicioPlan.value;
    campoFechaFinObj.value = campoFechaFinPlan.value;
}

function manejarFechasObjetivo(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton) {
    validarCampos(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton);
    let campoFechaInicioAcMej = document.getElementById("campoFechaInicioAccionMejora");
    let campoFechaFinAcMej = document.getElementById("campoFechaFinAccionMejora");
    let campoFechaInicioObj = document.getElementById("campoFechaInicioObjetivo");
    let campoFechaFinObj = document.getElementById("campoFechaFinObjetivo");
    campoFechaInicioAcMej.value = campoFechaInicioObj.value;
    campoFechaFinAcMej.value = campoFechaFinObj.value;
}

function validarCampos(campoFechaInicio, campoFechaFin, campoNombre, campoDescripcion, campoFechaInferior, campoFechaSuperior, nombreBoton) {
    let fechaInicio = document.getElementById(campoFechaInicio);
    let fechaFinal = document.getElementById(campoFechaFin);
    let nombre = document.getElementById(campoNombre);
    let totalValidations = 3;
    if (campoNombre == null || campoDescripcion == null) {
        totalValidations = 2;
    }


    // Dejando el limite superior de las fechas a 10 años en el caso de la creacion de los planes de mejora
    let minDate = null;
    let topDate = null;
    if (campoFechaInferior != null && campoFechaSuperior != null) {
        let temp = document.getElementById(campoFechaInferior).value;
        minDate = new Date(temp + 'CST');
        temp = document.getElementById(campoFechaSuperior).value
        topDate = new Date(temp + 'CST');
    } else {
        minDate = new Date(); // Todays Date
        topDate = new Date(minDate.getFullYear() + 10, minDate.getMonth(), minDate.getDate()); //10 years from now
    }

    let validator = new Validador(50, 250, minDate, topDate, nombreBoton);

    //Definimos la cantidad de validaciones
    validator.setTotalValidations(totalValidations);

    // Ahora haciendo las validaciones
    if (campoNombre != null) {
        validator.validateSomethingInTextInput(nombre, 50);
    }
    validator.validateDates(fechaInicio, fechaFinal);

    if (campoDescripcion != null) {
        validator.validateSomethingInTextInput(campoDescripcion, 50);
    }


    validator.validityOfForm();
}

class Base {
    nombre = null;
    fechaInicio = null;
    fechaFin = null;
    constructor(nombre, fechaInicio, fechaFin) {
        this.nombre = nombre;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }
}

class PlanMejora extends Base {

    ProfeSeleccionado = null;
    FormularioSeleccionado = null;
    Objetivo = [];
    SeccionConObjetivo = {};

    constructor() {
        super(null, null, null);
    }

    setPlan(nombre, fechaInicio, fechaFin) {
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

    pushObjetivo(objetivo) {
        this.Objetivo.push(objetivo);
    }

    setObjetivos(objetivos) {
        this.Objetivo = objetivos;
    }

    setSeccionConObjetivo(nuevaSeccionConObjetivo) {
        this.SeccionConObjetivo = nuevaSeccionConObjetivo;
    }
}

class Objetivo extends Base {
    nombTipoObj = null;
    descripcion = null;
    AccionDeMejora = null;

    constructor(nombre, tipo, descripcion, fechaInicio, fechaFin) {
        super(nombre, fechaInicio, fechaFin);
        this.nombTipoObj = tipo;
        this.descripcion = descripcion;
        this.AccionDeMejora = [];
    }

    addAccionDeMejora(accionDeMejora) {
        this.AccionDeMejora.push(accionDeMejora);
    }

    setAccionesDeMejora(accionesDeMejora) {
        this.AccionDeMejora = accionesDeMejora;
    }
}

class AccionDeMejora {
    nombreObj = null;
    descripcion = null;
    Accionable = null;
    fechaInicio = null;
    fechaFin = null;
    constructor(nombreObj, descripcion, fechaInicio, fechaFin) {
        this.nombreObj = nombreObj;
        this.descripcion = descripcion;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
        this.Accionable = [];
    }

    addAccionable(accionable) {
        this.Accionable.push(accionable);
    }
}

class Accionable {
    descripcion = null;
    nombreObj = null;
    descripAcMej = null;
    fechaInicio = null;
    fechaFin = null;
    constructor(nombre, descripcion, descripcionAcc, fechaInicio, fechaFin) {
        this.nombreObj = nombre;
        this.descripAcMej = descripcion;
        this.descripcion = descripcionAcc;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }
}