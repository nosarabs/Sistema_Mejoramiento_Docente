 //this can be called on individual forms by id or on "form" to target all forms
 //simultaneously
$('#dirrty-form').dirrty({
    preventLeaving:false

    // this function is called when the form.trigger's "dirty"
}).on("dirty", function () {
    $("#dirrty-save").attr("disabled", false);
    $("#dirrty-cancel").toggleClass("btn-danger btn-default");
    $("#dirrty-cancel").text('Cancelar');

    // this function is called when the form.trigger's "clean"
}).on("clean", function () {
    $("#dirrty-save").attr("disabled", true);
    $("#dirrty-cancel").toggleClass("btn-danger btn-default");
    $("#dirrty-cancel").text('Salir');
});