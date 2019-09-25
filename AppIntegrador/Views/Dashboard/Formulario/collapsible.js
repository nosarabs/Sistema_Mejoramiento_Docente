//Functions

const classes = {

    ESCALA: "escala",
    SELECCION_UNICA: "seleccion_unica",
    SELECCION_MULTIPLE: "seleccion_multiple",
    SELECCION_CERRADA: "seleccion_cerrada",
    TEXTO_ABIERTO: "texto_abierto"

}

const types = {

    ESCALA: "bar",
    SELECCION_UNICA: "pie",
    SELECCION_MULTIPLE: "bar",
    SELECCION_CERRADA: "pie",
    TEXTO_ABIERTO: "texto_abierto"

}

function createCollapsible(question, type) {

    var btn = document.createElement("button");   	// Create a <button> element
    btn.className = type;							//Set its class as a collapsible
    btn.innerHTML = question;						// Insert text
    var cnt = document.createElement("div");
    cnt.className = "content";

    btn.addEventListener("click", function () {		//Add an event listener to the button

        this.classList.toggle("active");
        var content = this.nextElementSibling;

        if (content.style.maxHeight) {

            content.style.maxHeight = null;

        } else {

            content.style.maxHeight = content.scrollHeight + "px";

        }

    });

    document.body.appendChild(btn);
    document.body.appendChild(cnt);

}

function createGraphic(cvs, graphic_data, type) {
    new Chart(cvs, {
        type: type,
        data: {
            labels: graphic_data.LABELS,
            datasets: [
                {
                    label: "Cantidad de estudiantes",
                    backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                    data: graphic_data.DATA
                }
            ]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: "Resultados"
            },
            responsive: false,
            maintainAspectRatio: false
        }
    });
}

function addBullets(parnt, answers) {

    var list = document.createElement("ul");

    for (var i = 0; i < answers.length; ++i) {
        // Create the list item:
        var item = document.createElement("li");

        // Set its contents:
        item.appendChild(document.createTextNode(answers[i]));

        // Add it to the list:
        list.appendChild(item);

        if (i < (answers.length - 1)) {
            //Add a line break between items.
            var br = document.createElement("br");
        }
    }

    parnt.appendChild(list);

}

function addContent(btn, graphic_data, box_data) {

    var cvs = document.createElement("canvas");
    cvs.setAttribute("width", "900");
    cvs.setAttribute("height", "550");
    btn.nextElementSibling.appendChild(cvs);
    switch (btn.className) {
        case classes.ESCALA:
            createGraphic(cvs, graphic_data, types.ESCALA);
            break;
        case classes.SELECCION_UNICA:

            break;
        case classes.SELECCION_MULTIPLE:

            break;
        case classes.SELECCION_CERRADA:

            break;
        case classes.TEXTO_ABIERTO:

            break;
    }

    var box = document.createElement("div");
    box.setAttribute("class", "myBox");
    var list = document.createElement("div");
    addBullets(list, box_data);
    box.appendChild(list);
    btn.nextElementSibling.appendChild(box);

}

//Main

var questions = ['¿Qué calificación le daría la profesor?', '¿Qué calificación le daría al curso?'];
createCollapsible(questions[0], classes.ESCALA);
createCollapsible(questions[1], classes.ESCALA);

var buttons = document.getElementsByClassName(classes.ESCALA);
var graphic_data = {
    DATA: [1100, 784, 2200, 5267, 4333],
    LABELS: ["1", "2", "3", "4", "5"]
}

var box_data = ["Hola", "esto", "es", "una", "prueba"];

for (var i = 0; i < buttons.length; ++i) {
    addContent(buttons[i], graphic_data, box_data);
}