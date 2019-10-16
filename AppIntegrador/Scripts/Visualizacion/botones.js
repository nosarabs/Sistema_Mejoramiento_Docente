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

        for (var i = 0; i < listaPreguntas.length; ++i) {

            var codigoPregunta = listaPreguntas[i].codigoPregunta;
            var textoPregunta = String(i + 1) + ". " + listaPreguntas[i].textoPregunta;
            var tipoPregunta = listaPreguntas[i].tipoPregunta;
            var boton = this.crearBoton(codigoPregunta, textoPregunta);
            document.body.appendChild(boton);

            var base;

            if (tipoPregunta == "texto_abierto") {

                base = new BaseRespuesta("texto_abierto");

            }
            else if (tipoPregunta == "escala") {

                base = new BaseConEstadisticas("escala");

            }
            else {

                base = new BaseDosCol("seleccion_cerrada");

            }

            document.body.appendChild(base.getBase());

        }

    }

}