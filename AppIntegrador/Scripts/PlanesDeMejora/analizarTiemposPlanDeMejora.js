let msjEspaciosSobrantes = "Te quedan ";
let msjFechaInicioMuyAntes = "No puedes definir una fecha anterior al dia de hoy."

let msjFechaInicioMuyAdelanteDeFinal = "Fecha Inválida."
let msjFechaFinalMuyAtrasDeInicio = "Fecha Inválida."

let msjRegularFechaInicio = "Recuerda que no puede ser antes del día de hoy.";
let msjRegularFechaFinal = "Recuerda que no puede ser antes del día de inicio.";

let listo = "Fecha Válidos";

const maximoCaracteresNombrePlan = 50;

// Seteando el valor minimo de la fecha de inicio de los planes de mejora
var today = Date.now();

// Validacion de la fecha de inicio de plan de Mejora
function validarInicioPDM() {
    // Tomando la fecha que el usuario ingresa en el formulario
    let element = document.getElementById("fechaInicioPlanDM");
    //Ahora viendo los segundo de la fecha ingresada por el usuario
    let secondsInicio = hmsToSecondsOnly(element.value);
    // Viendo los segundos de la fecha actual
    let secondsToday = hmsToSecondsOnly(todayStringDate());

    console.log(secondsToday);
    console.log(secondsInicio);


    if (secondsInicio >= secondsToday) {
        var msj = document.getElementById("fechaInicioPDM_error");
        msj.classList.remove("redError");
        msj.classList.add("correct");
        msj.innerHTML = listo;
    } else {
        var msj = document.getElementById("fechaInicioPDM_error");
        msj.classList.remove("correct");
        msj.classList.add("redError");
        msj.innerHTML = msjFechaInicioMuyAntes;
    }
}

function validacionFechas() {
    let fechaInicoPlan = document.getElementById("fechaInicioPlanDM");
    let fechaFinalPlan = document.getElementById("fechaFinalPlanDM");

    if (fechaInicoPlan) {
        validarInicioPDM();
    }

    //Hasta que las dos fechas esten ingredas es que las validamos
    if (fechaInicoPlan.value && fechaFinalPlan.value)
    {
        // tomando los segundos de las fechas del plan y del dia en que se 
        // hace el plan de mejora
        let secondsToday = hmsToSecondsOnly(todayStringDate());
        let secondsInicio = hmsToSecondsOnly(fechaInicoPlan.value);
        let secondsFin = hmsToSecondsOnly(fechaFinalPlan.value);

        if ((secondsInicio >= secondsToday) && (secondsInicio <= secondsFin)) {
            var msj = document.getElementById("fechaInicioPDM_error");
            msj.innerHTML = listo;
            var msj = document.getElementById("fechaFinalPDM_error");
            msj.innerHTML = listo;
        }
        else
        {
            if (secondsInicio < secondsToday) {
                var errorMessage = document.getElementById("fechaInicioPDM_error");
                errorMessage.innerHTML = msjFechaInicioMuyAntes;
                errorMessage.classList.remove('regularMsj');
                errorMessage.classList.add("redError");
            } else {
                var errorMessage1 = document.getElementById("fechaInicioPDM_error");
                errorMessage1.innerHTML = msjFechaInicioMuyAdelanteDeFinal;
                errorMessage1.classList.add("redError");
                var errorMessage2 = document.getElementById("fechaFinalPDM_error");
                errorMessage2.innerHTML = msjFechaFinalMuyAtrasDeInicio;
                errorMessage2.classList.add("redError");
            }
        }
    }
}

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

// Funcion que se encarga de decir la cantidad de letras que le quedan al usuario
// para agregar en el nombre del plan de mejora
function cambioNombrePlan(nombrePlan) {
    let cantidadCaracteres = nombrePlan.value.length;

    var errorMessage = document.getElementById("nombrePDM_eror");
    let espaciosSobrantes = maximoCaracteresNombrePlan - cantidadCaracteres;

    errorMessage.innerHTML = msjEspaciosSobrantes + espaciosSobrantes + " letras."
}

// Metodo que se encarga de mostrar o ocualtar el segundo elemento que se le envia como parametro
function activarDesactivar(checkbox, elemento)
{
    if (checkbox.checked) {
        elemento.style.display = "block";
    } else { //If it has been unchecked.
        elemento.style.display = "none";
    }
}





function enableSubmitPDM() {
    document.getElementById("enviarFormularioCompletPDM").disabled = false;
}