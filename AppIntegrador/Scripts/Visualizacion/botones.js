class Botones {

    constructor() {}

    crearBoton(codigoPregunta, textoPregunta) {

        var boton = document.createElement("button");
        boton.className = "collapsible";
        boton.id = codigoPregunta;
        boton.innerText = textoPregunta;
        boton.addEventListener("click", function () {

            this.classList.toggle("activeCollapsible");
            var content = this.nextElementSibling;

            if (content.style.maxHeight) {

                content.style.maxHeight = null;

            } else {

                content.style.maxHeight = content.scrollHeight + "px";

            }

        });

        return boton;

    }

    crearBotones(idContenedor, listaPreguntas) {

        var insertaContenidos = new InsertaContenidos();

        var cuerpoPrincipal = document.createElement("div");
        cuerpoPrincipal.className = "cuerpoPrincipal";

        var contadorPreguntasSeccion = 0;

        for (var i = 0; i < listaPreguntas.length; ++i) {

            // Chequea si la pregunta evaluada pertenece o no a la sección
            if (("Sec " + listaPreguntas[i].codigoSeccion) == idContenedor) {

                var codigoSeccion = listaPreguntas[i].codigoSeccion;
                var codigoPregunta = listaPreguntas[i].codigoPregunta;
                var textoPregunta = String(contadorPreguntasSeccion + 1) + ". " + listaPreguntas[i].textoPregunta;
                var tipoPregunta = listaPreguntas[i].tipoPregunta;
                var boton = this.crearBoton(codigoPregunta, textoPregunta);
                cuerpoPrincipal.appendChild(boton);

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

                cuerpoPrincipal.appendChild(base.getBase());

                var contenedor = document.getElementById(idContenedor);
                contenedor.appendChild(cuerpoPrincipal);
            }
            else {
                contadorPreguntasSeccion = 0;
            }
            
        }

    }

}