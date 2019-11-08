class Filtros {
    constructor() { }

    //Método que permite crear el desplegable para filtrar las unidades académicas 
    //Permite construir parte del cshtml de la vista
    agregarFiltroUA(container)
    {
        //Título de la categoría
        var categoria = document.createElement("h5");
        categoria.textContent = "Unidad Académica";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroUA";
        seleccion.className = "select";

        //seleccion.onclick = this.recuperarFiltros(container);

        var option = document.createElement("option");
        option.className = "option";
        option.value = "null";
        option.text = "Seleccione una opción...";

        seleccion.appendChild(option);   

        //Se llama al método del controlador que obtiene los nombres y los códigos de la unidades académicas guardadas en la BD
        $.ajax({
            url: '/Dashboard/getUnidadesAcademicas',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //Ciclo que crea cada option para luego agregarlo al select
                for (var i = 0; i < resultados.length; ++i) {
                    var option = document.createElement("option");
                    option.className = "option";
                    option.value = resultados[i].codigo;
                    option.text = resultados[i].nombre;
                    seleccion.appendChild(option);                    
                }
            }
        });
        //Se agrega el elemento select a la vista
        seleccion.onchange = function () { recuperarFiltros() };
        container.appendChild(seleccion);
    }

    //Método que permite crear el desplegable para filtrar por los énfasis de una carrera
    //Permite construir parte del cshtml de la vista
    agregarFiltroCarreraEnfasis(container)
    {
        //Título de la categoría
        container.appendChild(document.createElement("hr"));
        var categoria = document.createElement("h5");
        categoria.textContent = "Énfasis de Carrera";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroCarreraEnfasis";
        seleccion.className = "select";
        var option = document.createElement("option");
        option.className = "option";

        option.value = "null";
        option.text = "Seleccione una opción...";

        seleccion.appendChild(option);

        //Se llama al método del controlador que obtiene los nombres y los códigos de la unidades académicas guardadas en la BD
        $.ajax({
            url: '/Dashboard/getCarreraEnfasis',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //Ciclo que crea cada option para luego agregarlo al select
                for (var i = 0; i < resultados.length; ++i) {
                    var option = document.createElement("option");
                    option.className = "option";
                    option.value = resultados[i].codigoCarrera + "/" + resultados[i].codigoEnfasis;
                    option.text = resultados[i].nombreCarrera  + " - " + resultados[i].nombreEnfasis;
                    seleccion.appendChild(option);
                }
            }
        });
        //Se agrega el elemento select a la vista
        seleccion.onchange = function () { recuperarFiltros() };
        container.appendChild(seleccion);
    }

    //Método que permite deslpegar el filtro para seleccionar el grupo de un curso en un período específico
    agregarFiltroCursoGrupo(container) {
        //Título de la categoría
        container.appendChild(document.createElement("hr"));
        var categoria = document.createElement("h5");
        categoria.textContent = "Grupos de Curso";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroCursoGrupo";
        seleccion.className = "select";
        var option = document.createElement("option");
        option.className = "option";

        option.value = "null";
        option.text = "Seleccione una opción...";

        seleccion.appendChild(option);

        //Se llama al método del controlador que obtiene los nombres y los códigos de la unidades académicas guardadas en la BD
        $.ajax({
            url: '/Dashboard/getCursoGrupo',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //Ciclo que crea cada option para luego agregarlo al select
                for (var i = 0; i < resultados.length; ++i) {
                    var option = document.createElement("option");
                    option.className = "option";
                    option.value = resultados[i].siglaCurso + "/" + resultados[i].numGrupo + "/" + resultados[i].semestre + "/" + resultados[i].anno;
                    option.text = resultados[i].siglaCurso + " - " + resultados[i].nombreCurso + " - " + resultados[i].numGrupo + " - " + resultados[i].semestre + " - " + resultados[i].anno ;
                    seleccion.appendChild(option);
                }
            }
        });
        //Se agrega el elemento select a la vista
        seleccion.onchange = function () { recuperarFiltros() };

        container.appendChild(seleccion);
    }

    //Método que permite deslpegar el filtro para seleccionar el grupo de un curso en un período específico
    agregarFiltroProfesores(container) {
        //Título de la categoría
        container.appendChild(document.createElement("hr"));
        var categoria = document.createElement("h5");
        categoria.textContent = "Profesor/a";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroProfesores";
        seleccion.className = "select";
        var option = document.createElement("option");
        option.className = "option";

        option.value = "null";
        option.text = "Seleccione una opción...";

        seleccion.appendChild(option);

        //Se llama al método del controlador que obtiene los nombres y los códigos de la unidades académicas guardadas en la BD
        $.ajax({
            url: '/Dashboard/getProfesores',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                //Ciclo que crea cada option para luego agregarlo al select
                for (var i = 0; i < resultados.length; ++i) {
                    var option = document.createElement("option");
                    option.className = "option";
                    option.value = resultados[i].correo;
                    var nombreCompleto = "";
                    if (resultados[i].apellido1 != null)
                        nombreCompleto+=resultados[i].apellido1+" ";
                    if (resultados[i].apellido2 != null)
                        nombreCompleto+=resultados[i].apellido2+" ";  
                    if (resultados[i].nombre1 != null)
                        nombreCompleto+=resultados[i].nombre1+" ";
                    if (resultados[i].nombre2 != null)
                        nombreCompleto+=resultados[i].nombre2;
                    option.text = nombreCompleto; 
                    seleccion.appendChild(option);
                }
            }
        });
        //Se agrega el elemento select a la vista
        seleccion.onchange = function () { recuperarFiltros() };
        container.appendChild(seleccion);
    }

    agregarFiltroFormularios(container) {
        //Título de la categoría
        container.appendChild(document.createElement("hr"));
        var categoria = document.createElement("h5");
        categoria.textContent = "Formulario";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroFormularios";
        seleccion.className = "select";
        var option = document.createElement("option");
        option.className = "option";

        option.value = "null";
        option.text = "Seleccione una opción...";

        seleccion.appendChild(option);

        //Se llama al método del controlador que obtiene los nombres y los códigos de la unidades académicas guardadas en la BD
        $.ajax({
            url: '/Dashboard/ObtenerFormularios',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {

                //Ciclo que crea cada option para luego agregarlo al select

                for (var i = 0; i < resultados.length; ++i) {

                    var fechaInicio = resultados[i].FechaInicio;
                    var fechaFin = resultados[i].FechaFin;
                    var parsedFechaInicio = new Date(parseInt(fechaInicio.substr(6)));
                    var parsedFechaFin = new Date(parseInt(fechaFin.substr(6)));
                    var newFechaInicio = new Date(parsedFechaInicio);
                    var newFechaFin = new Date(parsedFechaFin);
                    var stringFechaInicio = newFechaInicio.toLocaleDateString("es-ES");
                    var stringFechaFin = newFechaFin.toLocaleDateString("es-ES");

                    var option = document.createElement("option");
                    option.className = "option";
                    option.value = resultados[i].FCodigo + "*" + resultados[i].FNombre + "*" + resultados[i].CSigla + "*" + resultados[i].GNumero + "*" + resultados[i].GSemestre + "*" + resultados[i].GAnno + "*" + stringFechaInicio + "*" + stringFechaFin;
                    option.text = resultados[i].FCodigo + " - " + resultados[i].FNombre + " - " + resultados[i].CSigla + " - " + resultados[i].GNumero + " - " + resultados[i].GSemestre + " - " + resultados[i].GAnno + " - " + stringFechaInicio + " - " + stringFechaFin;
                    seleccion.appendChild(option);

                }

            }
        });
        //Se agrega el elemento select a la vista
        seleccion.onchange = function () { actualizarVistaParcial() };
        container.appendChild(seleccion);
    }

    //Método que construye el panel de filtros
    crearFiltros()
    {
        //Se crea un contenedor para agregarlo luego a la vista
        var container = document.createElement("container");
        container.className = "container";
        container.id = "containerFiltros";

        //Llamado al método que crear el desplegable para las Unidades Académicas
        this.agregarFiltroUA(container);
        this.agregarFiltroCarreraEnfasis(container);
        this.agregarFiltroCursoGrupo(container);
        this.agregarFiltroProfesores(container);
        this.agregarFiltroFormularios(container);

        var panelfiltros = document.getElementById("panelFiltros");
        panelfiltros.appendChild(container);

    }
}