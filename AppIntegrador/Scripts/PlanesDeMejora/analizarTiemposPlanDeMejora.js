﻿let msjEspaciosSobrantes = "Te quedan ";
let msjFechaMuyAntes = "No puedes definir una fecha anterior al dia de hoy."

let msjFechaInicioMuyAdelanteDeFinal = "Inválida, no puede ser después de la fecha final."
let msjFechaFinalMuyAtrasDeInicio = "Inválida, no puede ser antes de la fecha de inicio."

let msjRegularFechaInicio = "Recuerda que no puede ser antes del día de hoy.";
let msjRegularFechaFinal = "Recuerda que no puede ser antes del día de inicio.";

let listo = "Fecha Válida";
let today = Date.now();
const maximoCaracteresNombrePlan = 50;
let validacionPorFechas = false;
enableDisableSubmitPDM();

// Funcion que se encarga de devolver un valor numerico que representa el str que se le manda 
// como parametro, se espera que el mismo sea una fecha
function hmsToSecondsOnly(str) {
    var p = str.split('-'),
        s = 0, m = 1;
    while (p.length > 0) {
        s += m * parseInt(p.pop(), 10);
        m *= 60;
    }
    return s;
}

// Funcion que convierte la fecha de hoy a formato anno-mes-dia
function todayStringDate() {
    var dateObj = new Date(today);
    var month = dateObj.getUTCMonth() + 1; //months from 1-12
    var day = dateObj.getUTCDate();
    var year = dateObj.getUTCFullYear();
    newdate = year + "-" + month + "-" + day;
    return newdate;
}


// Viendo los segundos de la fecha actual
var numToday = hmsToSecondsOnly(todayStringDate());

// Metodo que me dice los segundos del elemento con el id que se pide en el parametro
function getSeconds(valorId) {
    // Buscando el elemento en el DOM
    let elemento = document.getElementById(valorId);
    if (elemento.value) {
        // Si el elemento existe
        let seconds = hmsToSecondsOnly(elemento.value);
        // Retornando los segundo que implica la fecha del elemento
        return seconds;
    } else {
        //En caso de que el elemento no exista
        return -1;
    }
}

//Metodo que se encarga de asignarle el error del parametro al segundo elemento del Dom que 
//viene por parametro, el tercer parametro es si es un error o no
function setDOMvalues(htmlValues, element, error) {
    document.getElementById(element.id).innerHTML = htmlValues;
    if (error) {
        document.getElementById(element.id).classList.remove('correct');
        document.getElementById(element.id).classList.add('redError');
    } else {
        document.getElementById(element.id).classList.remove('redError');
        document.getElementById(element.id).classList.add('correct');
    }
}

// Metodo que se encarga de ver si la fecha enviada es valida si no es anterior 
// al dia de hoy, se asume que el elemento es un input de fecha
// La fecha puede ser igual al dia de hoy
function esValidaPorHoy(elemento) {
    let numElemento = hmsToSecondsOnly(elemento.value);
    if (numElemento >= numToday) {
        return true;
    } else {
        return false;
    }
}


//Funcion que se encarga de ver si la fechaA es anterior a la fechaB
//El proposito principal de la función es la reutilización del código
//Y tambien da verdadero si las fechas son las mismas
function esAntes(elementoA, elementoB) {
    let timeElementoA = hmsToSecondsOnly(elementoA.value);
    let timeElementoB = hmsToSecondsOnly(elementoB.value);
    return timeElementoA <= timeElementoB ? true : false;
}

// Metodo que se encarga de validar los ids de las fechas que se ingresan como parametro
// La primer fecha se espera que sea antes de la segunda
function validarFechas(idFechaUno, idFechaDos) {
    let fechaUno = document.getElementById(idFechaUno);
    let msjUno = document.getElementById(idFechaUno + '_msj');
    let fechaDos = document.getElementById(idFechaDos);
    let msjDos = document.getElementById(idFechaDos + '_msj');

    // Si existen ambas fechas
    if (fechaUno.value && fechaDos.value) {
        // Ahora viendo que la primera fecha sea validad por el dia de hoy
        // Ahora viendo que la segunda fecha sea validad por el dia de hoy
    } else {
        //Solo existe la primera
        if (fechaUno.value) {

        } else {
            // Solo existe la segunda
            if (fechaDos.value) {

            } else {
                //No existe ninguna
            }
        }

    }
}


// Metodo que se encarga de validar la fecha de inicio de un plan de mejora
function validarInicioPDM() {
    let resultado = false;
    // Toma del elemento del DOM que corresponde a la fecha de inicio de un plan
    let elementoFechaInicio = document.getElementById('fechaInicioPlanDM');
    let elementoFechaFinal = document.getElementById('fechaFinalPlanDM');
    validadoPorFechas(false);

    // Validando con respecto a la fecha de hoy
    let fechaValida = esValidaPorHoy(elementoFechaInicio);
    if (fechaValida && elementoFechaFinal.value) {
        // La fecha de inicio es validada por la fecha de hoy
        // Y la fechaFinal de los planes de mejora ya tiene un valor
        let fechaFinalValida = esValidaPorHoy(elementoFechaFinal);
        let elem1 = document.getElementById(elementoFechaInicio.id + '_msj');
        let elem2 = document.getElementById(elementoFechaFinal.id + '_msj');
        if (fechaFinalValida) {
            // Analizando el caso en el que la fecha final es válida por el dia de hoy
            if (esAntes(elementoFechaInicio, elementoFechaFinal)) {
                //Analizando el caso de exito de fechas
                setDOMvalues(listo, elem1, false);
                setDOMvalues(listo, elem2, false);
                validadoPorFechas(true);
                resultado = true;
            } else {
                //Caso en que la fecha de inicio no es antes de la final
                setDOMvalues(msjFechaInicioMuyAdelanteDeFinal, elem1, true);
                setDOMvalues(msjFechaFinalMuyAtrasDeInicio, elem2, true);
            }
        } else {
            // Caso en que la fecha final es menor que hoy
            setDOMvalues(listo, elem1, true);
            setDOMvalues(msjFechaMuyAntes, elem2, true);
        }
    } else { // Analizando el caso en el que solo se ha ingresado la fecha de inicio
        if (fechaValida) {
            let elem = document.getElementById(elementoFechaInicio.id + '_msj');
            setDOMvalues(listo, elem, false);
        } else {
            // Caso en el que la fecha de inicio no es válida
            let elem = document.getElementById(elementoFechaInicio.id + '_msj');
            setDOMvalues(msjFechaMuyAntes, elem, true);
        }
    }
    return resultado;   
}

// Metodo que se encarga de validar la fecha de inicio de un plan de mejora
function validarFinPDM() {
    let resultado = false;
    // Toma del elemento del DOM que corresponde a la fecha de inicio de un plan
    let elementoFechaInicio = document.getElementById('fechaInicioPlanDM');
    let elementoFechaFinal = document.getElementById('fechaFinalPlanDM');
    validadoPorFechas(false);

    // Validando con respecto a la fecha de hoy
    let fechaValida = esValidaPorHoy(elementoFechaFinal);

    if (fechaValida && elementoFechaInicio.value) {
        // La fecha final es validada por la fecha de hoy
        // Y la fechaInicio de los planes de mejora ya tiene un valor
        let fechaInicioValida = esValidaPorHoy(elementoFechaInicio);
        let elem1 = document.getElementById(elementoFechaInicio.id + '_msj');
        let elem2 = document.getElementById(elementoFechaFinal.id + '_msj');
        if (fechaInicioValida) {
            // Analizando el caso en el que la fecha final es válida por el dia de hoy
            if (esAntes(elementoFechaInicio, elementoFechaFinal)) {
                //Analizando el caso de exito de fechas
                setDOMvalues(listo, elem1, false);
                setDOMvalues(listo, elem2, false);
                validadoPorFechas(false);
                resultado = true;
            } else {
                //Caso en que la fecha de inicio no es antes de la final
                setDOMvalues(msjFechaInicioMuyAdelanteDeFinal, elem1, true);
                setDOMvalues(msjFechaFinalMuyAtrasDeInicio, elem2, true);
            }
        } else {
            // Caso en que la fecha inicio es menor que hoy
            setDOMvalues(listo, elem2, true);
            setDOMvalues(msjFechaMuyAntes, elem1, true);
        }
    } else { // Analizando el caso en el que solo se ha ingresado la fecha final
        if (fechaValida) {
            let elem = document.getElementById(elementoFechaFinal.id + '_msj');
            setDOMvalues(listo, elem, false);
        } else {
            // Caso en el que la fecha final no es válida
            let elem = document.getElementById(elementoFechaFinal.id + '_msj');
            setDOMvalues(msjFechaMuyAntes, elem, true);
        }
    }
    return resultado;
}

//////// VALIDACIÓN DEL TEXTO QUE REPRESENTA EL NOMBRE DEL PLAN DE MEJORA ////////

// Funcion que se encarga de decir la cantidad de letras que le quedan al usuario
// para agregar en el nombre del plan de mejora
function cambioNombrePlan(nombrePlan) {
    let cantidadCaracteres = nombrePlan.value.length;

    var errorMessage = document.getElementById("nombrePDM_eror");
    let espaciosSobrantes = maximoCaracteresNombrePlan - cantidadCaracteres;

    errorMessage.innerHTML = msjEspaciosSobrantes + espaciosSobrantes + " letras.";
    revisarBotonSubmit();
}

// Funcion que se encarga de ver si el nombre del plan de mejora es valido
function validarNombreDePlan() {
    let resultado = false;
    let elem = document.getElementById('nombrePlanDM');
    //Esta validacion solo define que se tiene que escribir aldo en el campo
    if (elem.value.length > 0) {
        resultado = true;
    }
    return resultado;
}

//// METODOS PARA LA ACTIVACION DEL BOTON DE ENVIO DE LOS PLANES DE MEJORA ////

//Metodo que se fija en todos los campos que tiene que validar para la 
// activacion del boton de creacion de los planes de mejora
function validadoPorFechas(validado) {
    this.validacionPorFechas = validado;    
}

// Este metodo revisa todas las validaciones que se ocupan para activar el boton submit 
// de los planes de mejora
function revisarBotonSubmit() {
    let validacionPorNombrePlan = validarNombreDePlan();

    let elem = document.getElementById("sendPDMListo");
    if (validacionPorNombrePlan && this.validacionPorFechas) {
        elem.disabled = false;
    } else {
        elem.disabled = true;
    }
}



// Funcion que se encarga de habilitar o desabilitart el boton de envío del formulario PDM
function enableDisableSubmitPDM() {
    let elemSubmit = document.getElementById('sendPDMListo');
    console.log("Andres Navarrete");
    if (elemSubmit) {
        if (validacionPorFechas && validadoPorNombrePlan()) {
            document.getElementById('sendPDMListo').disabled = false;
        } else {
            document.getElementById('sendPDMListo').disabled = true;
        }
    }
}