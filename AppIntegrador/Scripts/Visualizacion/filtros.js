class Filtros {
    constructor() { }

    agregarFiltroUA(container)
    {
        var categoria = document.createElement("h5");
        categoria.textContent = "Unidades Academicas";
        container.appendChild(categoria);
        var seleccion = document.createElement("select");
        seleccion.id = "filtroUA";
        
        var option = document.createElement("option");

        $.ajax({
            url: '/Dashboard/getUnidadesAcademicas',
            data: {
            },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                for (var i = 0; i < resultados.length; ++i) {
                    option.value = resultados[i].codigo;
                    option.text = resultados[i].nombre;
                    seleccion.appendChild(option);                    
                }
            }
        });
        container.appendChild(seleccion);
    }

    crearFiltros()
    {
        var container = document.createElement("container");
        container.className = "container";
        container.id = "containerFiltros";


        this.agregarFiltroUA(container);
        var p = document.getElementById("panelFiltros");
        p.appendChild(container);
    }
}