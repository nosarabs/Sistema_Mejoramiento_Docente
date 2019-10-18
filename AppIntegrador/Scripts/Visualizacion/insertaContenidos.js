class InsertaContenidos {

    constructor() {

        this.graficos = new Graficos();
        this.textoLibre = new TextoLibre();
        this.estadisticas = new Estadisticas();

    }

    insertarGraficoEscala(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.graficos.generarGraficoEscala(baseDosCol.getCanvas(), codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarGraficoSeleccionUnica(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.graficos.generarGraficoSeleccionUnica(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarGraficoSeleccionMultiple(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.graficos.generarGraficoSeleccionMultiple(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarGraficoSeleccionCerrada(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.graficos.generarGraficoSeleccionCerrada(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarEstadisticas(baseEstadisticas, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.estadisticas.generarEstadisticas(baseEstadisticas.getElementoEstadisticas(), codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarJustificaciones(baseDosCol, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.textoLibre.generarJustificaciones(baseDosCol.getElementoJustificacion(), codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

    insertarTextoAbierto(baseTexto, codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta) {

        this.textoLibre.generarTextoAbierto(baseTexto.getTextoAbierto(), codigoFormulario, siglaCurso, numeroGrupo, semestre, ano, codigoPregunta);

    }

}