
//Obtenido de: https://www.bootply.com/P8lWXlkW1O en 2019.10.17
$('[type=password]').keypress(function (e) {
    //Solo se ocupa mencionar si el capslocks esta prendido cuando el usuario NO esta viendo la contrasenna en texto plano
    if (document.getElementById("password-eye").classList.contains("fa-eye")) 
    {
        var $password = $(this),
            tooltipVisible = $('.tooltip').is(':visible'),
            s = String.fromCharCode(e.which);

        //Check if capslock is on. No easy way to test for this
        //Tests if letter is upper case and the shift key is NOT pressed.
        if (s.toUpperCase() === s && s.toLowerCase() !== s && !e.shiftKey) {
            if (!tooltipVisible)
                $password.tooltip('show');
        } else {
            if (tooltipVisible)
                $password.tooltip('hide');
        }

        //Hide the tooltip when moving away from the password field
        $password.blur(function (e) {
            $password.tooltip('hide');
        });
    }
});


$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});


//Validacion de correo obtenido de https://stackoverflow.com/a/46181 en 2019.10.17
function verificarSiCorreoBienFormateado(username) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(username);
}

//Si tiene un @ del todo la entrada
function verificarSiContieneArroba(username) {
    var re = /.*@.*/;
    return re.test(username);
}

//Si no tiene un @ entonces se le agrega @ucr.ac.cr, despues se confirma si la entrada tiene un formato adecuado de correo y se presenta un mensaje de error si no.
function validateEmail(input, warningOutput)
{
    var username = input.value;
    if (!verificarSiContieneArroba(username)) {
        username += "@ucr.ac.cr";
        input.value = username;
    }
    if (verificarSiCorreoBienFormateado(username)) {
        warningOutput.innerHTML = "";
        return true;
    }
    else
    {
        warningOutput.innerHTML = "<p>El correo no posee un formato valido</p>";
        return false;
    }
}

//$("#validate-email").on("click", validateEmail);

//Se verifica el tamanno en tiempo real y se da un mensaje de error si se llega a los maximos, el primero sin @ y el segundo con (usuario vs correo)
function verificarTamanno(control, maxnoat, maxtotal, warningOutput) {
    size = control.value.length;
    username = control.value;
    if (!verificarSiContieneArroba(username)) {
        if (size > maxnoat)
        {
            control.value = control.value.substring(0, maxnoat);
            size = control.value.length;
            warningOutput.innerHTML = "<p>Un nombre de usario debe ser menor a " + maxnoat + " caracteres</p>";
        }
        else
        {
            warningOutput.innerHTML = "";
        }
    }
    else 
    {
        if (size > maxtotal)
        {
            control.value = control.value.substring(0, maxtotal);
            size = control.value.length;
            warningOutput.innerHTML = "<p>Un correo debe ser menor a " + maxtotal + " caracteres</p>";
        }
        else
        {
            warningOutput.innerHTML = "";
        }
    }
}

