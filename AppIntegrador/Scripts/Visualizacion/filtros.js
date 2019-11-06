class Filtros {
    constructor() { }

    //Método que permite crear el desplegable para filtrar las unidades académicas 
    //Permite construir parte del cshtml de la vista
    agregarFiltroUA(container)
    {
        //Título de la categoría
        var categoria = document.createElement("h5");
        categoria.textContent = "Unidades Academicas";
        container.appendChild(categoria);

        //Se agrega el select que contendrá los elementos (opciones)
        var seleccion = document.createElement("select");
        seleccion.id = "filtroUA";
        seleccion.className = "select";
        var option = document.createElement("option");

        option.value = "";
        option.text = "Seleccione una opción...";
        option.disabled= true;

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
                    option.value = resultados[i].codigo;
                    option.text = resultados[i].nombre;
                    seleccion.appendChild(option);                    
                }
            }
        });
        //Se agrega el elemento select a la vista
        container.appendChild(seleccion);
    }

    //Método que construye el panel de filtros
    crearFiltros()
    {
        //Se crea un contenedo para agregarlo luego a la vista
        var container = document.createElement("container");
        container.className = "container";
        container.id = "containerFiltros";

        //Llamado al método que crear el desplegable para las Unidades Académicas
        this.agregarFiltroUA(container);

        var panelfiltros = document.getElementById("panelFiltros");
        panelfiltros.appendChild(container);
    }
}