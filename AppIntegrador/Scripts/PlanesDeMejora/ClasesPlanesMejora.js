export class PlanMejora {
    nombre = null;
    fechaInicio = null;
    fechaFin = null;

    constructor(nombre, fechaInicio, fechaFin) {
        this.nombre = nombre;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }
}

export class Objetivo extends PlanMejora {
    tipo = null;
    descripcion = null;

    constructor(nombre, tipo, descripcion, fechaInicio, fechaFin) {
        super(nombre, fechaInicio, fechaFin);
        this.tipo = tipo;
        this.descripcion = descripcion;
    }
}

export class AccionDeMejora extends PlanMejora{
    descripcion = null;
    constructor(nombre, descripcion, fechaInicio, fechaFin) {
        super(nombre, fechaInicio, fechaFin);
        this.descripcion = descripcion;
    }
}

export class Accionable extends AccionDeMejora{
    descripcionAcc = null;
    constructor(nombre, descripcion, descripcionAcc, fechaInicio, fechaFin) {
        super(nombre, descripcion, fechaInicio, fechaFin);
        this.descripcionAcc = descripcionAcc;
    }
}