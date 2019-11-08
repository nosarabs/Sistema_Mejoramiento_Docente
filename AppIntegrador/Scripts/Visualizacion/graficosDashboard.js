//Berta Sánchez Jalet
//COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
//Tarea técnica: Mostrar las nota resultantes.
//Cumplimiento: 5/10
class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor(correo) {
        var promedioP;
        var cantidadP;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: { correo: correo },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {

                promedioP = resultados.promedio;
                cantidadP = resultados.cantidad;

            }

        });
        return [promedioP, cantidadP];
    }

     recuperarPromedioCursos(correo) {
        var promedioP;
        var cantidadP;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioCursos',
            data: { correo: correo },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedioP = resultados.promedio;
                cantidadP = resultados.cantidad;

            }

        });
        return [promedioP, cantidadP];
     }

}