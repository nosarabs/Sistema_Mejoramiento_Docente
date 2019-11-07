class GraficosDashboard {

    constructor() { }

    recuperarPromedioProfesor() {
        var correo = "@HttpContext.Current.User.Identity.Name";

        $.ajax({
            url: '/Dashboard/ObtenerPromedioProfesor',
            data: { correo: correo },
            type: 'post',
            dataType: 'json',
            async: false,
            success: function (resultados) {
                alert(resultados.promedio);
                alert(resultados.cantidad);
            }
        });
    }
}