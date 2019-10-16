class InsertaContenidos {

    constructor() {

    }

    insertarEstadisticas(baseEstadisticas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        var estadisticas = new Estadisticas();
        estadisticas.generarEstadisticas(baseEstadisticas.getElementoEstadisticas(), codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

}