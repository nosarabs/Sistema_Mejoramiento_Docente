class Botones {

    constructor() {
    }

    /* COD-64: Yo como administrador quiero poder visualizar la información detallada
    de los resultados de una pregunta*/
    crearBoton(listaPreguntas, codigoPregunta, textoPregunta, idPreg) {

        var boton = document.createElement("button");
        boton.className = "btn-azulUCR";
        boton.id = codigoPregunta;
        boton.innerText = "Ver más" + codigoPregunta;

        boton.addEventListener("click", (e) => {
            this.rellenarModal(listaPreguntas, idPreg);
        });

        return boton;
    }

    // Método encargado de rellenar el modal de la vista con los gráficos, estadísticas
    // y respuestas de una pregunta específica.
    rellenarModal(listaPreguntas, idPreg) {
        
        var contadorPreguntasSeccion = 0;
        var insertaContenidos = new InsertaContenidos();

        var codigoSeccion = listaPreguntas[idPreg].codigoSeccion;
        var codigoPregunta = listaPreguntas[idPreg].codigoPregunta;
        var textoPregunta = String(contadorPreguntasSeccion + 1) + ". " + listaPreguntas[idPreg].textoPregunta;
        var tipoPregunta = listaPreguntas[idPreg].tipoPregunta;
        //cuerpoPrincipal.appendChild(boton);

        contadorPreguntasSeccion += 1;

        var base;

        switch (tipoPregunta) {

            case "texto_abierto":
                base = new BaseTexto(tipoPregunta);
                insertaContenidos.insertarTextoAbierto(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                break;

            case "escala":
                base = new BaseConEstadisticas(tipoPregunta);
                insertaContenidos.insertarGraficoEscala(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                insertaContenidos.insertarEstadisticas(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                break;

            case "seleccion_unica":
                base = new BaseDosCol(tipoPregunta);
                insertaContenidos.insertarGraficoSeleccionUnica(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                break;

            case "seleccion_multiple":
                base = new BaseDosCol(tipoPregunta);
                insertaContenidos.insertarGraficoSeleccionMultiple(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                break;

            case "seleccion_cerrada":
                base = new BaseDosCol(tipoPregunta);
                insertaContenidos.insertarGraficoSeleccionCerrada(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
                break;

        }

        if (tipoPregunta != "texto_abierto") {

            insertaContenidos.insertarJustificaciones(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, fechaInicio, fechaFin, codigoSeccion, codigoPregunta);
        }

        // Borra del modal el viejo componente e introduce el nuevo
        var modalTemp = document.getElementById("Ejemplo");;

        // Esto se usaría para dejar el modal en blanco
        modalTemp.firstChild.remove();

        // Esto se usaría para hacerle append de la respuesta de la pregunta
        modalTemp.appendChild(base.getBase());
        
        //document.getElementById("Ejemplo").innerHTML = idPreg;
        $("#ModalResultados").modal();
    }


    /* COD-66: Yo como administrador quiero visualizar las preguntas de una sección
     * en forma de cuadrícula para facilitar la visualización de las mismas
     * Tarea: - Incluir en cada campo de la cuadrícula un botón para mas detalles acerca de la pregunta.
    */
    // Método encargado de crear los flexboxes, labels y botones para cada
    // una de las preguntas
    crearBotones(idSeccion, listaPreguntas) {

        var cuerpoPrincipal = document.getElementById("contenedorPreguntas");

        cuerpoPrincipal.innerHTML = "";

        for (var i = 0; i < listaPreguntas.length; ++i) {

            if (listaPreguntas[i].codigoSeccion == idSeccion) {

                var contenedorParcial = document.createElement("div");
                //contenedorParcial.className += "col-md-2";
                contenedorParcial.style.textAlign = "center";

                var flexBonito = document.createElement("p-2");
                flexBonito.className += "col-md-4";
                flexBonito.className += " border";
                flexBonito.id = "contenedor" + listaPreguntas[i].codigoPregunta;

                var codigoPregunta = listaPreguntas[i].codigoPregunta;
                var textoPregunta = String(i + 1) + ". " + listaPreguntas[i].textoPregunta;
                var boton = this.crearBoton(listaPreguntas, codigoPregunta, textoPregunta, i);

                // Incluir un label!!
                var labelcito = document.createElement("p");
                labelcito.innerHTML = textoPregunta;

                // Appends
                contenedorParcial.appendChild(labelcito);
                contenedorParcial.appendChild(boton);
                flexBonito.appendChild(contenedorParcial);
                cuerpoPrincipal.appendChild(flexBonito);
            }
        }

    }

}

function seccion_cambiada(seccion, listaPreguntas) {
    var botones = new Botones();
    botones.crearBotones(seccion, listaPreguntas);
}

