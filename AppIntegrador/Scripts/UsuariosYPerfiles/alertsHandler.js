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
                from: "bottom",
                align: "center"
            }
        });
    }
}