NK.Formatting = {
    //This is done in another way also. 
    //This is a more robust version of the above function: isNumber
    //It will condition the value of a text input based on attributes added to the element that define the allowed precision of the number being inputed.
    formatCurrencyInputs: function (skipEmptyValues) {
        var _skipEmptyValues = skipEmptyValues;
        setTimeout(() => {
            $('[currency]').each(function () {
                //Grab the value from the attribute
                var attr = $(this).attr('currency');

                //Split on the commas to get our values
                var settings = attr.split(',');

                //We might not have a full array. Correct if not
                var addElements = 3 - settings.length;

                for (var i = 0; i < addElements; i++) {
                    //I am pushing false because the plugin will then ignore the settings. 
                    //We just need the elements to exist do we can address the function parameters.
                    settings.push[false];
                }

                var format = (e) => {
                    $(e.target).formatCurrency(settings[0].trim(), settings[1].trim(), settings[2].trim(), _skipEmptyValues);
                };

                var unformat = (e) => {
                    $(e.target).unformatCurrency(settings[0].trim(), _skipEmptyValues);
                };

                $(this).off('blur', format).on('blur', format).off('focus', unformat).on('focus', unformat);
                $(this).trigger('blur');
            });
        }, 60);
    },
    numericInputMasker: {
        keyPressHandler: function (evt) {

            var el = this;

            var wholePrecision = $(el).attr("wholePrecision");
            var decimalPrecision = $(el).attr("decimalPrecision");

            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');

            //dissallow anything thats not a number
            if ((charCode < 48 || charCode > 57) && decimalPrecision === 0) {
                return false;
            }

            //dissallow anything thats not a number, a dot
            if (charCode !== 46 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            //allowing only # of chars after the dot
            var caratPos = numericInputMasker.getSelectionStart(el);
            var dotPos = el.value.indexOf(".");

            if (number[0]) {
                if (number[0].length >= wholePrecision && charCode !== 46 && number[1] === undefined) {
                    return false;
                }
            }

            if (number[1]) {
                if (number[1].length >= decimalPrecision && caratPos > dotPos) {
                    return false;
                }

                if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                    return false;
                }
            }

            return true;
        },
        getSelectionStart: function (o) {
            //http://javascript.nwbox.com/cursor_position/
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text === '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }
    },
    bindNumericOnlyInputs: function () {
        var numericInput = function (e) {
            var $elem = $(e.target);
            var domElem = $elem.get()[0]
            var s = domElem.selectionStart;
            var e = domElem.selectionEnd;
            var v = $elem.val();
            var f = numeral(v).value();
            if (f != null) {
                f = f.toString().replace('-', '');
                $elem.val(f);
            }
            else {
                $elem.val('');
            }
            domElem.selectionStart = s;
            domElem.selectionEnd = e;
        }

        var $elems = $('[numericonly]');
        $elems.off('input', numericInput).on('input', numericInput);
    }
};
