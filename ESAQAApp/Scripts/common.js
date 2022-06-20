function getKey(e) {
    if (window.event)
        return window.event.keyCode;
    else if (e)
        return e.which;
    else
        return null;
}

function restrictChars(e, obj) {
    var CHAR_AFTER_DP = 3;  // number of decimal places
    var validList = "0123456789.";  // allowed characters in field
    var key, keyChar;
    key = getKey(e);
    if (key == null) return true;
    // control keys
    // null, backspace, tab, carriage return, escape
    if (key == 0 || key == 8 || key == 9 || key == 13 || key == 27)
        return true;
    // get character
    keyChar = String.fromCharCode(key);
    // check valid characters
    if (validList.indexOf(keyChar) != -1) {
        // check for existing decimal point
        var dp = 0;
        if ((dp = obj.value.indexOf(".")) > -1) {
            if (keyChar == ".")
                return false;  // only one allowed
            else {
                // room for more after decimal point?
                if (obj.value.length - dp <= CHAR_AFTER_DP)
                    return true;
            }
        }
        else return true;
    }
    // not a valid character
    return false;
}

//using
//<input type="text" name="money" onKeyPress="return restrictChars(event, this)">