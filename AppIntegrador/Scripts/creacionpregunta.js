﻿function pregunta() {
    swal({
        title: "Pregunta creada con éxito!",
        text: "Puede ser vista en el banco de preguntas",
        type: "success",
        timer: 10000,
        showConfirmButton: false
    },
        function () {
            window.location.href = "Create";
        });
} 