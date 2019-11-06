let msjEspaciosSobrantes = "Te quedan ";
let msjFechaMuyAntes = "No puede ser antes de hoy."

let msjFechaInicioMuyAdelanteDeFinal = "Inválida, no puede ser después de la fecha final."
let msjFechaFinalMuyAtrasDeInicio = "Inválida, no puede ser antes de la fecha de inicio."

let msjRegularFechaInicio = "Recuerda que no puede ser antes del día de hoy.";
let msjRegularFechaFinal = "Recuerda que no puede ser antes del día de inicio.";

let mensajeFechaInicioDefault = "Fecha de inicio.";
let mensajeFechaFinalDefault = "Fecha de finalización.";

let listo = "Fecha Válida";

const idBoton = "sendPDMListo"
const idPlanDeMejoraNombre = "nombrePlanDM";

let today = Date.now();
const maximoCaracteresNombrePlan = 50;
let validacionPorFechas = false;
let validacionPorNombrePlan = false;

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
function cambiarMensaje(mensaje, element, error) {
    document.getElementById(element.id).innerHTML = mensaje;
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
// La primer fecha se espera que sea antes de la segunda fecha ingeresadas como parametros
function validarFechas(idFechaUno, idFechaDos) {
    let fechaUno = document.getElementById(idFechaUno);
    let msjUno = document.getElementById(idFechaUno + '_msj');
    let fechaDos = document.getElementById(idFechaDos);
    let msjDos = document.getElementById(idFechaDos + '_msj');

    // Variable para la validación de fechas
    this.validacionPorFechas = false;

    // Si existen ambas fechas
    if (fechaUno.value && fechaDos.value) { // Ambas fechas estan definidas
        // Validando ambas fechas por el dia de hoy y que la de incio sea antes que la final
        let validaPorHoyInicio = esValidaPorHoy(fechaUno);
        let validaPorHoyFinal = esValidaPorHoy(fechaDos);
        let ordenValidoDeFechas = esAntes(fechaUno, fechaDos);

        if (validaPorHoyInicio && validaPorHoyFinal && ordenValidoDeFechas) {
            cambiarMensaje(listo, msjUno, false);
            cambiarMensaje(listo, msjDos, false);
            this.validacionPorFechas = true;
        } else {
            // Para la fecha de inicio
            if (!validaPorHoyInicio) {
                cambiarMensaje(msjFechaMuyAntes, msjUno, true);
            } else {
                if (ordenValidoDeFechas) {
                    cambiarMensaje(listo, msjUno, false);
                } else {
                    cambiarMensaje(msjFechaInicioMuyAdelanteDeFinal, msjUno, true);
                }
            }
            // Para la fecha final
            if (!validaPorHoyFinal) {
                cambiarMensaje(msjFechaMuyAntes, msjDos, true);
            } else {
                if (ordenValidoDeFechas) {
                    cambiarMensaje(listo, msjDos, false);
                } else {
                    cambiarMensaje(msjFechaFinalMuyAtrasDeInicio, msjDos, true);
                }
            }
        }

    } else { //Si solo existe una de las fechas o ninguna

        if (fechaUno.value) {
            //Solo existe la primera
            // Validando por el dia de hoy
            fechasValidas = esValidaPorHoy(fechaUno);
            if (fechasValidas) {
                cambiarMensaje(listo, msjUno, false);
            } else {
                cambiarMensaje(msjFechaMuyAntes, msjUno, true);
            }
        } else {
            if (fechaDos.value) {
                // Solo existe la segunda
                fechasValidas = esValidaPorHoy(fechaDos);
                if (fechasValidas) {
                    cambiarMensaje(listo, msjDos, false);
                } else {
                    cambiarMensaje(msjFechaMuyAntes, msjDos, true);
                }
            } else {
                //No existe ninguna
                cambiarMensaje(mensajeFechaInicioDefault, msjUno, false);
                cambiarMensaje(mensajeFechaFinalDefault, msjDos, false);
            }
        }
    }

    activarBotonSubmit();
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
    validarNombreDePlan(espaciosSobrantes);
    activarBotonSubmit();
}

// Funcion que se encarga de ver si el nombre del plan de mejora es valido
function validarNombreDePlan(espaciosSobrantes) {
    // La unica validacion que se hace es que tenga caracteres
    let cantidadCaracteres = document.getElementById(idPlanDeMejoraNombre).value.length;
    this.validacionPorNombrePlan = false;
    if (cantidadCaracteres > 0) {
        this.validacionPorNombrePlan = true;
    }
}

//// METODOS PARA LA ACTIVACION DEL BOTON DE ENVIO DE LOS PLANES DE MEJORA ////
function activarBotonSubmit() {
    let elementoBoton = document.getElementById(idBoton);
    if (this.validacionPorFechas && this.validacionPorNombrePlan) {
        elementoBoton.disabled = false;
    } else {
        elementoBoton.disabled = true;
    }
}