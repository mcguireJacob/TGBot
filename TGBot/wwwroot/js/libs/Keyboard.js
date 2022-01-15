NK.Keyboard = {
    //This can be used to limit allowed key presses on number fields
    //use as onkeypress="return isNumber(event)"
    numbersOnlyInput: function () {
        var _evt = (evt) ? evt : window.event;
        var charCode = (_evt.which) ? _evt.which : _evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
};