function insert(num) {
    document.form.textarea.value = document.form.textarea.value + num
}

function equal() {
    var text = document.form.textarea.value;
    if (text) {
        document.form.textarea.value = eval(text)
    }
}

function clean() {
    document.form.textarea.value = "";
}

function backspace() {
    var text = document.form.textarea.value;
    document.form.textarea.value = text.substring(0, text.length - 1);
}

document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e && e.keyCode == 32) { // space 键
        equal();
    }


};
