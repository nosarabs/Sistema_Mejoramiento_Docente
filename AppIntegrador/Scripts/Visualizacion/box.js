function createBullets(parnt, answers) {

    var list = document.createElement("ul");

    for(var i = 0; i < answers.length; ++i) {
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

function addBox(cnt, box_data) {
	
	var box = document.createElement("div");
	box.setAttribute("class", "myBox" );
	var list = document.createElement("div");
	createBullets(list, box_data);
	box.appendChild(list);
	cnt.appendChild(box);
	
}