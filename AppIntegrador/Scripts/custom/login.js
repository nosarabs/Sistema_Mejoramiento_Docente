$('[type=password]').keypress(function (e) {
    var $password = $(this),
        tooltipVisible = $('.tooltip').is(':visible'),
        s = String.fromCharCode(e.which);

    //Check if capslock is on. No easy way to test for this
    //Tests if letter is upper case and the shift key is NOT pressed.
    if (s.toUpperCase() === s && s.toLowerCase() !== s && !e.shiftKey) {
        if (!tooltipVisible)
            $password.tooltip('show');
    } else {
        if (tooltipVisible)
            $password.tooltip('hide');
    }

    //Hide the tooltip when moving away from the password field
    $password.blur(function (e) {
        $password.tooltip('hide');
    });
});


////< !--UserStory TAM - 1.1.3 - Capslock detection-- >
//var capsLockEnabled = null;

//function getChar(e) {

//    if (e.which == null) {
//        return String.fromCharCode(e.keyCode); // IE
//    }
//    if (e.which != 0 && e.charCode != 0) {
//        return String.fromCharCode(e.which); // rest
//    }

//    return null;
//}

//document.onkeydown = function (e) {
//    e = e || event;

//    if (e.keyCode == 20 && capsLockEnabled !== null) {
//        capsLockEnabled = !capsLockEnabled;
//    }
//}

//document.onkeypress = function (e) {
//    e = e || event;

//    var chr = getChar(e);
//    if (!chr) return; // special key

//    if (chr.toLowerCase() == chr.toUpperCase()) {
//        // caseless symbol, like whitespace
//        // can't use it to detect Caps Lock
//        return;
//    }

//    capsLockEnabled = (chr.toLowerCase() == chr && e.shiftKey) || (chr.toUpperCase() == chr && !e.shiftKey);
//}

///**
// * Check caps lock
// */
//function checkCapsWarning() {
//    document.getElementById('caps').style.display = capsLockEnabled ? 'block' : 'none';
//}

//function removeCapsWarning() {
//    document.getElementById('caps').style.display = 'none';
//}
//< !-- End of UserStory -- >

////Eye script to show password

//var pass = document.getElementById('Password');
//var eye = document.getElementById('Eye');

//eye.addEventListener('click', TogglePassword);

//function TogglePassword() {

//    eye.classList.toggle('active');

//    (pass.type == 'password') ? pass.type = 'text' : pass.type = 'password';
//}

$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});