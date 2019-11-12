﻿//Berta Sánchez Jalet
//COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
//Tarea técnica: Mostrar las nota resultantes.
//Cumplimiento: 5/10
class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor(correo) {

        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: { correo: correo },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //alert(resultados.promedio);
                //alert(resultados.cantidad);
            }

        });

    }

    recuperarPromedioCursos(correo) {

        $.ajax({
            url: '/Dashboard/ObtenerPromedioCursos',
            data: { correo: correo },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //alert(resultados.promedio);
                //alert(resultados.cantidad);
            }
        });
    }

}