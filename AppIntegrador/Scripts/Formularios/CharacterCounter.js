// Counts characters used in an input box and displays it in a text div.
// The input box and the text needs to be inside a div and with the attribute 'maxlength' set. Otherwise, this function will not work.
// Adittionally, the text that tells how many characters are used needs to have the class counter set.
//
// To call this method, use the following html attribute inside the input tag: onkeyup = charCount(this)
//
// Example of the document's html structure:
//
// <div>
//     <input type="text" onkeyup="charCount(this)" maxlength="50"></input>
//     <div class="counter">0/50 caracteres usados</div>
// </div>

function charCount(inputBox) {
    // Get the current length of the input box
    var len = inputBox.value.length;
    // Get the maxlength attribute of the input box
    var max = $(inputBox).attr("maxlength")
    // Get the div with class counter
    var counterText = $(inputBox).parent().find(".counter");
    // Set the text to the div
    counterText.text(len + "/" + max + " caracteres usados");
}