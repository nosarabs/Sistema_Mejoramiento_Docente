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
            url: '/Dashboard/ObtenerUnidadesAcademicas',
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
                    option.value = resultados[i].CodigoUA;
                    option.text = resultados[i].NombreUA;
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
            url: '/Dashboard/ObtenerCarrerasEnfasis',
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
                    option.value = resultados[i].CodCarrera + "/" + resultados[i].CodEnfasis;
                    option.text = resultados[i].NomCarrera  + " - " + resultados[i].NomEnfasis;
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
            url: '/Dashboard/ObtenerGrupos',
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
                    option.value = resultados[i].SiglaCurso + "/" + resultados[i].NumGrupo + "/" + resultados[i].Semestre + "/" + resultados[i].Anno;
                    option.text = resultados[i].SiglaCurso + " - " + resultados[i].NombreCurso + " - " + resultados[i].NumGrupo + " - " + resultados[i].Semestre + " - " + resultados[i].Anno ;
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
            url: '/Dashboard/ObtenerProfesores',
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
                    option.value = resultados[i].Correo;
                    var nombreCompleto = "";
                    if (resultados[i].Apellido1 != null)
                        nombreCompleto+=resultados[i].Apellido1+" ";
                    if (resultados[i].Apellido2 != null)
                        nombreCompleto+=resultados[i].Apellido2+" ";  
                    if (resultados[i].Nombre1 != null)
                        nombreCompleto+=resultados[i].Nombre1+" ";
                    if (resultados[i].Nombre2 != null)
                        nombreCompleto+=resultados[i].Nombre2;
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

                    var stringFechaInicio = new Date(fechaInicio).toLocaleDateString('en-GB', { day: 'numeric', month: 'numeric', year: 'numeric' });
                    var stringFechaFin = new Date(fechaFin).toLocaleDateString('en-GB', { day: 'numeric', month: 'numeric', year: 'numeric' });

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