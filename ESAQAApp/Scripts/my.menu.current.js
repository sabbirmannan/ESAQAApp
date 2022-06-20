$(document).ready(function () {
    if (!readCookie('levelOneID'))
        createCookie("levelOneID", "", 1);
    if (!readCookie('levelTwoID'))
        createCookie("levelTwoID", "", 1);
    if (!readCookie('levelThreeID'))
        createCookie("levelThreeID", "", 1);
});

var levelOneID, levelTwoID, levelThreeID;
function SetMyOwnID(id, level) {
    if (level == 1) {
        createCookie('levelOneID', id, 1);
    }
    if (level == 2) {
        createCookie('levelTwoID', id, 1);
    }
    if (level == 3) {
        createCookie('levelThreeID', id, 1);
    }
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }

    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}