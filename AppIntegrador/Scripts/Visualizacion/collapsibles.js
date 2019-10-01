//Classes and types

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

function appendLineBreaks(elmnt, number) {
	
	for (var i = 0; i < number; ++i) {
		elmnt.appendChild(document.createElement("br"));
	}
	
}

function appendTitle(elmnt, txt) {
	var title = document.createElement("h3");
	title.innerHTML = txt;
	elmnt.appendChild(title);
}

function createCollapsible(question, type) {
	
	var btn = document.createElement("button");   	// Create a <button> element
	btn.className = type;							//Set its class as a collapsible
	btn.innerHTML = question;						// Insert text
	var cnt = document.createElement("div");
	cnt.className = "content";
	
	btn.addEventListener("click", function() {		//Add an event listener to the button
		
		if (this.nextElementSibling.childElementCount == 0) {
			
			appendLineBreaks(cnt, 2);
			addChart(cnt, {DATA: [1100, 784, 2200, 5267, 4333, 3500, 6000, 1700, 2900, 1000], LABELS: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"]}, this.className);
			appendLineBreaks(cnt, 6);
			appendTitle(cnt, "Justificación de los resultados");
			addBox(cnt, ["Hola", "esto", "es", "una", "prueba", "de", "que", "se", "puede", "scrollear"]);
			appendLineBreaks(cnt, 2);
			
		}
		
		this.classList.toggle("active");
		var content = this.nextElementSibling;
		
		if (content.style.maxHeight){
			
			content.style.maxHeight = null;
			
		} else {
			
			content.style.maxHeight = content.scrollHeight + "px";
			
		} 
		
	});
	
	document.body.appendChild(btn);
	document.body.appendChild(cnt);
	
}

//Main

var questions = ['¿Qué calificación le daría la profesor?', '¿Qué calificación le daría al curso?'];
var qTypes = [classes.ESCALA, classes.SELECCION_UNICA];

for (var i = 0; i < questions.length; ++i) {
	question = String(i + 1) + ". " + questions[i];
	createCollapsible(question, qTypes[i]);
}