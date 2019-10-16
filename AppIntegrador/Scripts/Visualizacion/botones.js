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

    crearBotones(listaPreguntas) {

        var insertaContenidos = new InsertaContenidos();

        for (var i = 0; i < listaPreguntas.length; ++i) {

            var codigoPregunta = listaPreguntas[i].codigoPregunta;
            var textoPregunta = String(i + 1) + ". " + listaPreguntas[i].textoPregunta;
            var tipoPregunta = listaPreguntas[i].tipoPregunta;
            var boton = this.crearBoton(codigoPregunta, textoPregunta);
            document.body.appendChild(boton);

            var base;

            switch (tipoPregunta) {

                case "texto_abierto":
                    base = new BaseTexto(tipoPregunta);
                    insertaContenidos.insertarTextoAbierto(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    break;

                case "escala":
                    base = new BaseConEstadisticas(tipoPregunta);
                    insertaContenidos.insertarGraficoEscala(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    insertaContenidos.insertarEstadisticas(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    break;

                case "seleccion_unica":
                    base = new BaseDosCol(tipoPregunta);
                    insertaContenidos.insertarGraficoSeleccionUnica(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    break;

                case "seleccion_multiple":
                    base = new BaseDosCol(tipoPregunta);
                    insertaContenidos.insertarGraficoSeleccionMultiple(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    break;

                case "seleccion_cerrada":
                    base = new BaseDosCol(tipoPregunta);
                    insertaContenidos.insertarGraficoSeleccionCerrada(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
                    break;

            }

            if (tipoPregunta != "texto_abierto")
            {

                insertaContenidos.insertarJustificaciones(base, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);
            }

            document.body.appendChild(base.getBase());

        }

    }

}