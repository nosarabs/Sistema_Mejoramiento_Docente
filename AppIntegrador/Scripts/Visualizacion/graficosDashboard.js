//Berta Sánchez Jalet
//COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
//Tarea técnica: Mostrar las nota resultantes.
//Cumplimiento: 5/10
class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: {
                unidadesAcademicas: unidadesAcademicas,
                carrerasEnfasis: carrerasEnfasis,
                grupos: grupos,
                profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;
            }

        });
        return [promedio, cantidad]
    }

    recuperarPromedioCursos(unidadesAcademicas, carrerasEnfasis, grupos, profesores) {
        var promedio;
        var cantidad;
        $.ajax({
            url: '/Dashboard/ObtenerPromedioCursos',
            data: {
                    unidadesAcademicas: unidadesAcademicas,
                    carrerasEnfasis: carrerasEnfasis,
                    grupos: grupos,
                    profesores: profesores
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                promedio = resultados.promedio;
                cantidad = resultados.cantidad;
            }
        });
        return [promedio, cantidad]
    }

}