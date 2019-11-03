let msjEspaciosSobrantes = "Te quedan ";
let msjFechaInicioMuyAntes = "No puedes definir una fecha anterior al dia de hoy."
let msjFechaInicioMuyAdelanteDeFinal = "Esta fecha no puede ser despues de la fecha de finalizacion."
let msjFechaFinalMuyAtrasDeInicio = "Esta fecha no puede ser antes de la fecha de inicion."

const maximoCaracteresNombrePlan = 50;


// Seteando el valor minimo de la fecha de inicio de los planes de mejora
var today = Date.now();


function validacionFechas() {
    let fechaInicoPlan = document.getElementById("fechaInicioPlanDM");
    let fechaFinalPlan = document.getElementById("fechaFinalPlanDM");

    //Hasta que las dos fechas esten ingredas es que las validamos
    if (fechaInicoPlan.value && fechaFinalPlan.value)
    {
        // tomando los segundos de las fechas del plan y del dia en que se 
        // hace el plan de mejora
        let secondsToday = hmsToSecondsOnly(todayStringDate());
        let secondsInicio = hmsToSecondsOnly(fechaInicoPlan.value);
        let secondsFin = hmsToSecondsOnly(fechaFinalPlan.value);

        if ((secondsInicio >= secondsToday) && (secondsInicio <= secondsFin)) {
            console.log("todo bien");
        }
        else
        {
            if (secondsInicio < secondsToday) {
                var errorMessage = document.getElementById("fechaInicioPDM_eror");
                errorMessage.innerHTML = msjFechaInicioMuyAntes;
            } else {
                var errorMessage1 = document.getElementById("fechaInicioPDM_eror");
                errorMessage1.innerHTML = msjFechaInicioMuyAdelanteDeFinal;
                var errorMessage2 = document.getElementById("fechaFinalPDM_eror");
                errorMessage2.innerHTML = msjFechaFinalMuyAtrasDeInicio;
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