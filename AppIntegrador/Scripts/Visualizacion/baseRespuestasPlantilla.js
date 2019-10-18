class BaseRespuesta {

    constructor(tipo) {

        this.base = document.createElement("div");
        this.base.className = "row content";
    
  
        this.base.id = "tipo";

    }

    getBase() {

        return this.base;

    }

}

class BaseDosCol extends BaseRespuesta{

    constructor(tipo) {

        super(tipo);
        this.leftCol = document.createElement("div");
        this.leftCol.className = "col";
        this.rightCol = document.createElement("div");
        this.rightCol.className = "col";
        this.rowCanvas = document.createElement("div");
        this.rowCanvas.className = "row";
        this.canvas = document.createElement("canvas");
        this.canvas.setAttribute("width", "250vw"); //Ancho canvas
        this.canvas.setAttribute("height", "150vh"); //Largo canvas
        this.justificacion = document.createElement("div");
        this.justificacion.className = "row myBox";

        var tituloJustificacion = document.createElement("h3");
        tituloJustificacion.innerText = "Justificación de los resultados";
        this.rightCol.appendChild(tituloJustificacion);    

        this.rightCol.appendChild(this.justificacion);
        this.rowCanvas.appendChild(this.canvas);
        this.leftCol.appendChild(this.rowCanvas);
        this.base.appendChild(this.leftCol);
        this.base.appendChild(this.rightCol);

    }

    getCanvas() {

        return this.canvas;

    }

    getElementoJustificacion() {

        return this.justificacion;

    }

}

class BaseConEstadisticas extends BaseDosCol {

    constructor(tipo) {

        super(tipo);
        this.estadisticas = document.createElement("div");
        this.estadisticas.className = "row estadisticas";
        this.media = document.createElement("div");
        this.media.className = "col";
        this.mediana = document.createElement("div");
        this.mediana.className = "col";
        this.desviacion = document.createElement("div");
        this.desviacion.className = "col";

        this.estadisticas.appendChild(this.media);
        this.estadisticas.appendChild(this.mediana);
        this.estadisticas.appendChild(this.desviacion);
        this.rightCol.insertAdjacentElement("afterbegin", this.estadisticas);

    }

    getElementoEstadisticas() {

        return this.estadisticas;

    }

}


class BaseTexto extends BaseRespuesta {

    constructor(tipo) {

        super(tipo);
        this.textoAbierto = document.createElement("div");
        this.textoAbierto.className = "myBox textoAbierto";

        var tituloJustificacion = document.createElement("h3");
        tituloJustificacion.innerText = "Respuestas";

        this.base.appendChild(tituloJustificacion);    
        this.base.appendChild(this.textoAbierto);


    }

    getTextoAbierto() {

        return this.textoAbierto;

    }
}