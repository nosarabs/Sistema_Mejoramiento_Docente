//Create the collapsible buttons.
questions = ['¿Qué calificación le daría la profesor?', '¿Qué calificación le daría al curso?'];

for (var i = 0; i < questions.length; ++i) {
	var btn = document.createElement("button");   	// Create a <button> element
	btn.setAttribute("class", "collapsible" );
	btn.innerHTML = questions[i];	                // Insert text
	var cnt = document.createElement("div");
	cnt.setAttribute("class", "content" );
	var cvs = document.createElement("canvas");
	cvs.setAttribute("id", "chart_" + i );
	cvs.setAttribute("width", "900" );
	cvs.setAttribute("height", "550" );
	var box = document.createElement("div");
	box.setAttribute("class", "myBox" );
	var list = document.createElement("div");
	list.setAttribute("id", "list_" + i );
	cnt.appendChild(cvs);
	box.appendChild(list);
	cnt.appendChild(box);
	document.body.appendChild(btn);
	document.body.appendChild(cnt);
}

//Retrieve the collapsible buttons and make them work.
var coll = document.getElementsByClassName("collapsible");

for (var i = 0; i < coll.length; ++i) {
  coll[i].addEventListener("click", function() {
    this.classList.toggle("active");
    var content = this.nextElementSibling;
    if (content.style.maxHeight){
      content.style.maxHeight = null;
    } else {
      content.style.maxHeight = content.scrollHeight + "px";
    } 
  });
}
