function customAlert(type, message) {
    if (type === "confirm") {
        //sweetalerts
    } else {
        $.notify({
            // options
            message: message
        }, {
            // settings
                type: type,
            placement: {
                from: "top",
                align: "right"
            }
        });
    }
}