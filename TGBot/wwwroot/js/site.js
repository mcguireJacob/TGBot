"use strict";

var custSpinner = ' <i style="position:relative;" class="fa fa-spinner fa-pulse fa-lg"></i>';

//This is used to serialize the data of a form
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        if (typeof indexed_array[n['name']] === "undefined") {
            indexed_array[n['name']] = n['value'];
        }
        else if (typeof indexed_array[n['name']] === "object") {
            indexed_array[n['name']].push(n['value']);
        } else {
            var oldVal = indexed_array[n['name']];
            indexed_array[n['name']] = [];
            indexed_array[n['name']].push(oldVal);
            indexed_array[n['name']].push(n['value']);
        }
    });

    return indexed_array;
}

if (!Array.from) {
    Array.from = (function () {
        var toStr = Object.prototype.toString;
        var isCallable = function (fn) {
            return typeof fn === 'function' || toStr.call(fn) === '[object Function]';
        };
        var toInteger = function (value) {
            var number = Number(value);
            if (isNaN(number)) { return 0; }
            if (number === 0 || !isFinite(number)) { return number; }
            return (number > 0 ? 1 : -1) * Math.floor(Math.abs(number));
        };
        var maxSafeInteger = Math.pow(2, 53) - 1;
        var toLength = function (value) {
            var len = toInteger(value);
            return Math.min(Math.max(len, 0), maxSafeInteger);
        };

        // The length property of the from method is 1.
        return function from(arrayLike/*, mapFn, thisArg */) {
            // 1. Let C be the this value.
            var C = this;

            // 2. Let items be ToObject(arrayLike).
            var items = Object(arrayLike);

            // 3. ReturnIfAbrupt(items).
            if (arrayLike == null) {
                throw new TypeError("Array.from requires an array-like object - not null or undefined");
            }

            // 4. If mapfn is undefined, then let mapping be false.
            var mapFn = arguments.length > 1 ? arguments[1] : void undefined;
            var T;
            if (typeof mapFn !== 'undefined') {
                // 5. else
                // 5. a If IsCallable(mapfn) is false, throw a TypeError exception.
                if (!isCallable(mapFn)) {
                    throw new TypeError('Array.from: when provided, the second argument must be a function');
                }

                // 5. b. If thisArg was supplied, let T be thisArg; else let T be undefined.
                if (arguments.length > 2) {
                    T = arguments[2];
                }
            }

            // 10. Let lenValue be Get(items, "length").
            // 11. Let len be ToLength(lenValue).
            var len = toLength(items.length);

            // 13. If IsConstructor(C) is true, then
            // 13. a. Let A be the result of calling the [[Construct]] internal method of C with an argument list containing the single item len.
            // 14. a. Else, Let A be ArrayCreate(len).
            var A = isCallable(C) ? Object(new C(len)) : new Array(len);

            // 16. Let k be 0.
            var k = 0;
            // 17. Repeat, while k < len… (also steps a - h)
            var kValue;
            while (k < len) {
                kValue = items[k];
                if (mapFn) {
                    A[k] = typeof T === 'undefined' ? mapFn(kValue, k) : mapFn.call(T, kValue, k);
                } else {
                    A[k] = kValue;
                }
                k += 1;
            }
            // 18. Let putStatus be Put(A, "length", len, true).
            A.length = len;
            // 20. Return A.
            return A;
        };
    }());
}

//This is used to serialize an entire form and post it to the path in the action attribute of the form
function saveFormData($form, success_callback, error_callback) {

    var passData = JSON.stringify(getFormData($form));
    var urlref = $form.attr('action');
    if (!urlref) {
        console.error("Error in saving form data. no valid action found on the form");
        return;
    }
    var jqxhr = $.ajax({
        url: urlref,
        type: 'post',
        cache: false, //enable this line if you want jquery to add a parameter to not cache this request. Usually post requests are not cached by IE
        data: { 'passData': passData }

    })
        .done(function (data, textStatus) {
            if (success_callback) {
                success_callback(data);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

            var jsonError = "";

            try {
                jsonError = JSON.parse(jqXHR.responseText);
            }
            catch (err) {
                if (error_callback) {
                    error_callback('Unable to parse error response');
                }
                else {
                    console.error('Unable to parse error response: ' + err);
                }
                return;
            }

            if (error_callback) {
                error_callback(jsonError);
            }
            else {
                //If JsonResult does not exist then you are not using the ReturnArgs object on the .Net side and should use the fail callback to handle the message on your own.
                ErrorMessage.show(jsonError.JsonResult);
            }
        });
}

var isSubmitting = false;

function saveFormDataSpinner($form, $elem, success_callback, error_callback) {

    if (isSubmitting) {
        return false;
    }
    else {
        isSubmitting = true;
    }

    var passData = JSON.stringify(getFormData($form));
    var origTxt = $elem.html();
    var origPosition = $elem.css("position");
    var origHeight = $elem.height();
    var origWidth = $elem.width();

    $elem.html(custSpinner);
    $elem.css("position", "relative");
    $elem.height(origHeight);
    $elem.width(origWidth);
    $elem.prop('disabled', true);
    var urlref = $form.attr('action');
    if (!urlref) {
        console.error("Error in saving form data. no valid action found on the form");
        return;
    }
    var jqxhr = $.ajax({
        url: urlref,
        type: 'post',
        cache: false, //enable this line if you want jquery to add a parameter to not cache this request. Usually post requests are not cached by IE
        data: { 'passData': passData }

    })
        //success
        .done(function (data, textStatus) {
            $elem.find('i').removeClass('fa-spinner');
            $elem.find('i').removeClass('fa-pulse');
            $elem.html(origTxt);
            try {
                if (data) {
                    if (success_callback) {
                        setTimeout(function () { success_callback(data); }, 100);

                    }
                    //initJqDatePicker();
                    if (data.success) {
                        //show a success status if we receive that response from the server
                        $('#nk-data-success').show();
                    }
                }
            } catch (err) {
                console.error("error in ajax success:", err.message);
                return;
            }
        })
        //error
        .fail(function (jqXHR, textStatus, errorThrown) {
            $elem.find('i').removeClass('fa-spinner');
            $elem.find('i').removeClass('fa-pulse');
            $elem.html(origTxt);
            var jsonError = "";
            try {
                jsonError = JSON.parse(jqXHR.responseText);
            } catch (err) {
                ErrorMessage.show('An unexpected error has occurred');
                console.error("error parsing error message as JSON:", err.message);
                return;
            }
            if (error_callback) {
                error_callback(jsonError);
            }
            else {
                //If JsonResult does not exist then you are not using the ReturnArgs object on the .Net side and should use the fail callback to handle the message on your own.
                ErrorMessage.show(jsonError.JsonResult);
            }
        })
        //Always
        .always(function () {
            //$elem.removeAttr('disabled', true);
            //$elem.html(origTxt);
            $elem.css("position", origPosition);
            $elem.prop('disabled', null);
            isSubmitting = false; // allows search button to be enabled
        });
}

//This now works. You can parse you object from passData, and also user Request.Files to manage your files.
function saveFormDataWithFile($form, success_callback, error_callback) {
    var passData = JSON.stringify(getFormData($form));

    var fd = new FormData();
    fd.append("passData", passData);

    $form.find('input[type="file"]').each(function (i, ui) {
        $.each(ui.files, function (j, ui) {
            fd.append('file_' + i + '_' + j, this);
        });
    });

    var urlref = $form.attr('action');
    if (!urlref) {
        console.error("Error in saving form data. no valid action found on the form");
        return;
    }
    $.ajax({
        url: urlref,
        type: 'post',
        processData: false, //this needs to be false if you are including a file
        contentType: false, //this needs to be false if you are including a file
        //cache: false, //enable this line if you want jquery to add a parameter to not cache this request. Usually post requests are not cached by IE
        data: fd
    })
        .done(function (data, textStatus) {
            if (success_callback) {
                success_callback(data);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

            var jsonError = "";

            try {
                jsonError = JSON.parse(jqXHR.responseText);
            }
            catch (err) {
                if (error_callback) {
                    error_callback('Unable to parse error response');
                }
                else {
                    console.error('Unable to parse error response: ' + err);
                }
                return;
            }

            if (error_callback) {
                error_callback(jqXHR.responseText);
            }
        });
}

//This now works. You can parse you object from passData, and also user Request.Files to manage your files.
function saveCustomDataWithFile($form, customData, success_callback, error_callback) {

    var passData = JSON.stringify(customData);

    var fd = new FormData();
    fd.append("passData", passData);

    $form.find('input[type="file"]').each(function (i, ui) {
        $.each(ui.files, function (j, ui) {
            fd.append('file_' + i + '_' + j, this);
        });
    });

    var urlref = $form.attr('action');
    if (!urlref) {
        console.error("Error in saving form data. no valid action found on the form");
        return;
    }
    $.ajax({
        url: urlref,
        type: 'post',
        processData: false, //this needs to be false if you are including a file
        contentType: false, //this needs to be false if you are including a file
        //cache: false, //enable this line if you want jquery to add a parameter to not cache this request. Usually post requests are not cached by IE
        data: fd
    })
        .done(function (data, textStatus) {
            if (success_callback) {
                success_callback(data);
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {

            var jsonError = "";

            try {
                jsonError = JSON.parse(jqXHR.responseText);
            }
            catch (err) {
                if (error_callback) {
                    error_callback('Unable to parse error response');
                }
                else {
                    console.error('Unable to parse error response: ' + err);
                }
                return;
            }

            if (error_callback) {
                error_callback(jqXHR.responseText);
            }
        });
}

function saveFormDataWithFileSpinner($form, $elem, success_callback, error_callback) {
    var passData = JSON.stringify(getFormData($form));

    var fd = new FormData();
    fd.append("passData", passData);

    $form.find('input[type="file"]').each(function (i, ui) {
        $.each(ui.files, function (j, ui) {
            fd.append('file_' + i + '_' + j, this);
        });
    });

    var origTxt = $elem.html();
    var origPosition = $elem.css("position");
    var origHeight = $elem.height();
    var origWidth = $elem.width();

    $elem.html(custSpinner);
    $elem.css("position", "relative");
    $elem.height(origHeight);
    $elem.width(origWidth);
    $elem.prop('disabled', true);

    var urlref = $form.attr('action');
    if (!urlref) {
        console.error("Error in saving form data. no valid action found on the form");
        return;
    }
    $.ajax({
        url: urlref,
        type: 'post',
        processData: false, //this needs to be false if you are including a file
        contentType: false, //this needs to be false if you are including a file
        //cache: false, //enable this line if you want jquery to add a parameter to not cache this request. Usually post requests are not cached by IE
        data: fd
    })
        .done(function (data, textStatus) {
            $elem.find('i').removeClass('fa-spinner');
            $elem.find('i').removeClass('fa-pulse');
            $elem.html(origTxt);
            try {
                if (data) {
                    if (success_callback) {
                        setTimeout(function () { success_callback(data); }, 100);

                    }
                    //initJqDatePicker();
                    if (data.success) {
                        //show a success status if we receive that response from the server
                        $('#nk-data-success').show();
                    }

                }
            } catch (err) {
                console.error("error in ajax success:", err.message);
                return;
            }
        })
        //error
        .fail(function (jqXHR, textStatus, errorThrown) {
            $elem.find('i').removeClass('fa-spinner');
            $elem.find('i').removeClass('fa-pulse');
            $elem.html(origTxt);
            var jsonError = "";
            try {
                jsonError = JSON.parse(jqXHR.responseText);
            } catch (err) {
                ErrorMessage.show('An unexpected error has occurred');
                console.error("error parsing error message as JSON:", err.message);
                return;
            }
            if (error_callback) {
                error_callback(jsonError);
            }
            else {
                //If JsonResult does not exist then you are not using the ReturnArgs object on the .Net side and should use the fail callback to handle the message on your own.
                ErrorMessage.show(jsonError.JsonResult);
            }
        })
        //Always
        .always(function () {
            //$elem.removeAttr('disabled', true);
            //$elem.html(origTxt);
            $elem.css("position", origPosition);
            $elem.prop('disabled', null);
        });
}

function HandleExportDownload(url) {

    var link = document.createElement('a');

    link.setAttribute("target", "_blank");

    link.href = url;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}



//This is used when you would like to post to the server, but do not have a form.
function postAjax(url, paramsObj, successCallback, failCallback) {
    $.ajax({
        url: url,
        type: "post",
        cache: false,
        data: { passData: JSON.stringify(paramsObj) }
    }).done(function (data) {
        if (successCallback) {
            if (typeof data === 'string') {
                if (data.trim().substring(0, 1) !== '<') {
                    data = JSON.parse(data);
                }
            }
            successCallback(data);
        }
    }).fail(function (jqXHR, textStatus) {
        if (failCallback) {
            var retVal = jqXHR.responseText;
            if (typeof retVal === 'string') {
                retVal = JSON.parse(retVal);
            }
            if (failCallback) {
                failCallback(retVal);
            }
        } else {
            //If JsonResult does not exist then you are not using the ReturnArgs object on the .Net side and should use the fail callback to handle the message on your own.
            ErrorMessage.show(JSON.parse(jqXHR.responseText).JsonResult);
        }
    });
}

var isAjaxSubmitting = false;
function postAjaxSpinner(url, paramsObj, $elem, successCallback, failCallback) {

    $elem.prop('disabled', true);
    var origTxt = $elem.html();
    var origPosition = $elem.css("position");
    var origHeight = $elem.height();
    var origWidth = $elem.width();

    $elem.html(custSpinner);
    $elem.css("position", "relative");
    $elem.height(origHeight);
    $elem.width(origWidth);

    $.ajax({
        url: url /*+ '?_=' + new Date().getTime()*/, //We can not assume there will not be parameters added to the URL. The time stamp fails if there are.
        type: "post",
        cache: false,
        //contentType: 'application/json',
        data: { passData: JSON.stringify(paramsObj) }
    }).done(function (data) {
        $elem.find('i').removeClass('fa-spinner');
        $elem.find('i').removeClass('fa-pulse');
        $elem.html(origTxt);
        //Why not parse the data here so it is not a string in the callback?
        //Just check for < starting the result.
        if (successCallback) { successCallback(data); }
    }).fail(function (jqXHR, textStatus) {
        $elem.find('i').removeClass('fa-spinner');
        $elem.find('i').removeClass('fa-pulse');
        $elem.html(origTxt);
        var jsonError = "";
        try {
            jsonError = JSON.parse(jqXHR.responseText);
        } catch (err) {
            ErrorMessage.show('An unexpected error has occurred');
            console.error("error parsing error message as JSON:", err.message);
            return;
        }
        if (failCallback) {
            failCallback(jsonError);
        }
        else {
            //If JsonResult does not exist then you are not using the ReturnArgs object on the .Net side and should use the fail callback to handle the message on your own.
            ErrorMessage.show(jsonError.JsonResult);
        }
    }).always(function () {
        $elem.css("position", origPosition);
        $($elem).prop('disabled', null);
    });

}

//This ca be used to select a specific parameter from the current document href
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

//This is used to open a modal where the layout is to be parsed on the server using razor.
//Use the callback to set up form validation or any other after action setup
function openModal(modalURL, callback) {
    var success = false;
    var
        jqxhr = $.ajax({
            url: modalURL,
            type: 'get',
            cache: false
        })
            .done(function (data, textStatus) {
                $("#modalAddEdit").html(data); //replace the modal with received content
                //addRequiredMarks();
                success = true;
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                var jsonError = "";
                try {
                    jsonError = JSON.parse(jqXHR.responseText);
                } catch (err) {
                    console.error("Unable to parse ajax error response as JSON", err.message);
                    ErrorMessage.show('There was an error retrieving your data!: ' + err.message);
                    return;
                }
                if (typeof jsonError !== 'undefined' && jsonError.JsonResult) {
                    ErrorMessage.show(jsonError.JsonResult);
                    return;
                }
                else {
                    alert('There was an error retrieving your data!');
                }
            })
            .always(function (data, textStatus) {
                if (textStatus !== "error") {
                    $('#modalAddEdit').modal();
                    if (callback) { callback(success); }
                }
            });

}

// Very similar to the openModal but allows for object to pushed with modal call
function openModalWithData(modalURL, paramsObj, callback) {
    var success = false;

    var
        jqxhr = $.ajax({
            url: modalURL,
            type: "post",
            cache: false,
            data: { passData: JSON.stringify(paramsObj) }
        }).done(function (data) {
            $("#modalAddEdit").html(data); //replace the modal with received content
            //addRequiredMarks();
            success = true;
        }).fail(function (jqXHR, textStatus, errorThrown) {
            var jsonError = "";
            try {
                jsonError = JSON.parse(jqXHR.responseText);
            } catch (err) {
                console.error("Unable to parse ajax error response as JSON", err.message);
                ErrorMessage.show('There was an error retrieving your data!: ' + err.message);
                return;
            }
            if (typeof jsonError !== 'undefined' && jsonError.JsonResult) {
                ErrorMessage.show(jsonError.JsonResult);
                return;
            }
            else {
                alert('There was an error retrieving your data!');
            }
        })
            .always(function (data, textStatus) {
                if (textStatus !== "error") {
                    $('#modalAddEdit').modal();
                    if (callback) { callback(success); }
                }
            });
}

//This is names poorly since any message can be sent to is. It does not need to be an error.
var ErrorMessage = {
    show: function (message, title) {
        var selector = 'errormessage';

        $('#errormessageTitle').html(title || "Error:");
        $('#errormessageContent').html(message || "");
        $('#' + selector).modal();

    }

};

var DynamicMessage = function (message, title, buttonText, openCallback, closeCallback) {

    var selector = 'dynamicMessage';

    $('#dynamicmessageTitle').html(title);
    $('#dynamicmessageContent').html(message || "");

    $('#' + selector + ' button').html(buttonText || "Close");

    $('#' + selector + ' button').unbind('click');

    if (closeCallback) {
        $('#' + selector + ' button').click(function (evt) { closeCallback(evt) });
    }

    $('#' + selector).modal();

    if (openCallback) {
        openCallback();
    }

};





//This is used to replace the browsers built in confirmation
var Confirm = function (message, title, callback) {
    $.confirm({
        animation: 'top',
        title: title,
        content: message,
        buttons: {
            confirm: {
                btnClass: 'btn-primary',
                action: function () {
                    callback(true);
                }
            },
            cancel: {
                btnClass: 'btn-secondary',
                action: function () {
                    callback(false);
                }
            }
        }
    });
};

var YesNoConfirm = function (message, title, callback) {
    $.confirm({
        animation: 'top',
        title: title,
        content: message,
        buttons: {
            yes: {
                btnClass: 'btn-primary',
                action: function () {
                    callback(true);
                }
            },
            no: {
                btnClass: 'btn-secondary',
                action: function () {
                    callback(false);
                }
            }
        }
    });
};

// This is used to show and simulate a mobile success message using jquery
var SuccessMessage = {
    show: function (message, timeout, header, callback) {

        if (timeout === undefined) {
            timeout = 4000;
        }
        if (header === undefined) {
            header = 'Success';
        }

        $('#mobileSuccessMessage .inner-header').html('').html(header);
        $('#mobileSuccessMessage .inner-message').html('').html(message);
        $('#mobileSuccessMessage').fadeIn();
        setTimeout(function () {
            $('#mobileSuccessMessage').fadeOut();
            if (callback) {
                callback();
            }
        }, timeout);
    }
};
//This can be used to limit allowed key presses on number fields
//use as onkeypress="return isNumber(event)"
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

//This is a jQuery plugin that allows for the tablesorter to be unset from a table.
//Use like $('table').unbindTablesorter().tablesorter()
(function ($) {
    $.fn.unbindTablesorter = function () {
        $(this).unbind('appendCache applyWidgetId applyWidgets sorton update updateCell')
            //.removeClass('tablesorter')
            .find('thead th')
            .unbind('click mousedown')
            .removeClass('header headerSortDown headerSortUp');
        return this;
    };

    $.fn.bindTablesorter = function () {
        $(this).unbindTablesorter().tablesorter();
        return this;
    };
})(jQuery);

//This will take just about anything and return a treue or false.
//Note that if an object or array is passed, the result will always be false.
//Usage: if(val && parseBool(val)){ /*Do some stuff*/ }
function parseBool(val) { return val === true || val.toLowercase() === "true" }

//This function will replace all instance of found text, instead of the default javascript behavior of just replacing the first occurance it locates.
String.prototype.replaceAll = function (target, value) {
    var exit = false;
    var retVal = this;
    if (target) {
        while (!exit) {
            if (retVal.indexOf(target) > -1) {
                retVal = retVal.replace(target, value);
            }
            else {
                exit = true;
            }
        }
        return retVal;
    }
};


//It will filter the value of a text input based on attributes added to the element that define the allowed precision of the number being inputed.
var numericInputMasker = {
    KeyPressHandler: function (evt) {

        var el = this;

        var wholePrecision = $(el).attr("wholePrecision");
        var decimalPrecision = $(el).attr("decimalPrecision");

        var charCode = (evt.which) ? evt.which : event.keyCode;
        var number = el.value.split('.');

        //dissallow anything thats not a number if (charCode != 44 && charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        if ((charCode < 48 || charCode > 57) && decimalPrecision === 0) {
            return false;
        }

        //dissallow anything thats not a number, a dot
        if (charCode !== 46 && charCode !== 44 && (charCode < 48 || charCode > 57)) {
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

};

//This can be used when you need to validate a single field using Parsley.
function validateSingle(id, caller, callback) {
    var elem = $('#' + id);
    if (!$(elem).length) {
        var err = 'No ID Sent To Validate';
        console.error(err);
        throw err;
    }
    if ($(elem).parsley().isValid()) { callback(caller); }
    else { $(elem).parsley().validate(); }
}


//This can be used when you need to validate a collection of inputs outside of a clean form using Parsley.
function validateMultiple(inputs, callback) {

    var isValid = true;

    $(inputs).each(function (index, elem) {
        $(elem).parsley().validate();
        if (!$(elem).parsley().isValid()) {
            isValid = false;
        }
    });

    if (isValid && callback) {
        callback(inputs);
    }

    if (isValid) {
        return true;
    }
    else {
        return false;
    }

}

//I am not really sure why this was added, but it could be useful. 
//This will redirect to the url passed in as the parameter if it is not empty.
function goToPage(location) {
    if (!location || location.length < 1) {
        console.error("invalid redirect url");
        return;
    }
    var url = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : ''); //port is empty for default port

    url = url + location;
    window.location = url;
}

//Keepalive
var keepAliveUrl = '';
var loginUrl = '';
var keepAlive = function (keepAliveUrl, loginUrl) {
    keepAliveUrl = keepAliveUrl; loginUrl = loginUrl;
    setInterval(function () { _keepalive(keepAliveUrl, loginUrl); }, 60000);//Call this function every 60 seconds
};

var _keepalive = function (keepAliveUrl, loginUrl) {
    var pathExceptions = [
        "/login/"//This should cover anything pertaining to login
        /*Add Exceptions Here*/
    ];

    function _isPathException() {
        for (var i in pathExceptions) {
            var p = pathExceptions[i];
            if (document.location.href.toLowerCase().indexOf(p.toLowerCase()) > -1) {
                return true;
            }
        }
        return false;
    }

    $.ajax({
        url: keepAliveUrl,
        type: 'post',
        cache: false
    })
        .done(function (data, textStatus) {
            if (_isPathException()) { return; }
            if (data) {
                //If we have a session, success is returned. Otherwise fail.
                if (data === 'fail') {
                    document.location = loginUrl;
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            if (_isPathException()) { return; }
            document.location = loginUrl;
        });
};

//var addRequiredMarks = function () {
//    var reqArr = $('[required]');

//    for (var i in reqArr) {
//        var elem = reqArr[i];
//        if (i === 'length') { break; }

//        var lbl = $(elem).prev('label');
//        if (!lbl) {
//            lbl = $(elem).after('label');
//        }

//        if (lbl) {
//            var spn = $(lbl).find('.requiredMark');
//            var hasLbl = $(spn).length && $(spn).length > 0;
//            if (!hasLbl) {
//                $(lbl).append('<span class="requiredMark" title="A value is required">&nbsp;*</span>');
//            }
//        }
//    }

//};

//$(function () { addRequiredMarks(); });

//var cleanRequiredMarks = function () {
//    $('.requiredMark').remove();
//    addRequiredMarks();
//};

//Trying out this function to pad leading zeroes onto user entered code to fit required string length on Local Government Add.
function paddy(n, p, c) {
    var pad_char = typeof c !== 'undefined' ? c : '0';
    var pad = new Array(1 + p).join(pad_char);
    return (pad + n).slice(-pad.length);
}

/*
 * Use Like:
    //Create a 1 minute timer
    var timy = null
    $(function () {
        timy = new Timer(6000, function (args) {
            alert(args);
            timy.stop();
        });
        timy.start('One minute has passed. Timer will now stop');
        console.log(t);
    });
    
 */
var Timer = function (interval, event) {
    this.timer = null;
    this.interval = interval;
    this.start = function (args) {
        this.args = args;
        this._startTimer();
    };
    this._startTimer = function () {
        this.timer = setInterval((function (args) {
            if (this.eventHandler) {
                this.eventHandler(this.args);
            }
        }).bind(this), this.interval);
    },
        this.stop = function () {
            clearInterval(this.timer);
        };
    this.reset = function (interval, args, eventHandler) {
        this.stop();
        if (interval) {
            this.interval = interval;
        }
        if (args) {
            this.args = args;
        }
        if (eventHandler) {
            this.eventHandler = eventHandler;
        }
        this._startTimer();
    };
    this.eventHandler = event;
    this.args = null;
};

/*
 * Auto fade in page content
 * Create Date Pickers
 */

$(function () {
    $('.auto-fade-in').css({ 'visibility': 'visible', 'display': 'none' }).fadeIn();
    
    /* NetK - JAL - NOT SURE BUT PRODUCING ERROR - 2018-10-26
    $('input[datetimepicker]').datetimepicker({ format: 'yyyy-mm-dd hh:ii' }).on('changeDate', function () {
        $(this).datetimepicker('hide');
    });
    //*/
});

// Credit David Walsh (https://davidwalsh.name/javascript-debounce-function)

// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.
function debounce(func, wait, immediate) {
    var timeout;

    // This is the function that is actually executed when
    // the DOM event is triggered.
    return function executedFunction() {
        // Store the context of this and any
        // parameters passed to executedFunction
        var context = this;
        var args = arguments;

        // The function to be called after 
        // the debounce time has elapsed
        var later = function () {
            // null timeout to indicate the debounce ended
            timeout = null;

            // Call function now if you did not on the leading end
            if (!immediate) func.apply(context, args);
        };

        // Determine if you should call the function
        // on the leading or trail end
        var callNow = immediate && !timeout;

        // This will reset the waiting every function execution.
        // This is the step that prevents the function from
        // being executed because it will never reach the 
        // inside of the previous setTimeout  
        clearTimeout(timeout);

        // Restart the debounce waiting period.
        // setTimeout returns a truthy value (it differs in web vs node)
        timeout = setTimeout(later, wait);

        // Call immediately if you're dong a leading
        // end execution
        if (callNow) func.apply(context, args);
    };
}

//Shows a spinner center screen
//Spinner.show()/Spinner.hide()
//If true is passed on show, no mask is shown: Spinner.show(true) no mask is shown.
var Spinner = {
    state: {
        IsShowing: false
    },
    show: function (noMask) {
        var s = '.spinner';
        if (!noMask) {
            s = '.spinnerMask, ' + s;
        }
        $(s).fadeIn();
        $(s).focus();
        this.state.IsShowing = true;
    },
    hide: function () {
        $('.spinnerMask, .spinner').fadeOut();
        this.state.IsShowing = false;
    }
};

function getCurrentPlantID() {
    return parseInt($('#currentPlant').val());
}


//Ajax File Upload
var Upload = function (url, id, file) {
    this.file = file;
    this.id = id;
    this.url = url;
};
Upload.prototype.getType = function () {
    return this.file.type;
};
Upload.prototype.getSize = function () {
    return this.file.size;
};
Upload.prototype.getName = function () {
    return this.file.name;
};
Upload.prototype.doUpload = function (callback) {
    $('#progress-wrp').fadeIn();
    var that = this;
    var formData = new FormData();

    // add assoc key values, this will be posts values
    formData.append("file", this.file, this.getName());
    formData.append("id", this.id);
    formData.append("upload_file", true);

    $.ajax({
        type: "POST",
        url: this.url,
        xhr: function () {
            var myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) {
                myXhr.upload.addEventListener('progress', that.progressHandling, false);
            }
            return myXhr;
        },
        success: function (data) {
            $('#progress-wrp').fadeOut();
            if (callback) {
                callback(data);
            }
        },
        error: function (jsonError) {
            ErrorMessage.show(jsonError.JsonResult);
            $('#progress-wrp').hide();
        },
        async: true,
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        timeout: 60000
    });
};
Upload.prototype.progressHandling = function (event) {
    var percent = 0;
    var position = event.loaded || event.position;
    var total = event.total;
    var progress_bar_id = "#progress-wrp";
    if (event.lengthComputable) {
        percent = Math.ceil(position / total * 100);
    }
    // update progressbars classes so it fits your code
    $(progress_bar_id + " .progress-bar").css("width", +percent + "%");
    $(progress_bar_id + " .status").text(percent + "%");
};


var InputFormatter = {

    thouSeparator: ",",

    re: new RegExp("[,]", "g"),
    format: function (val, DecPlaces) {
        var decPlaces = DecPlaces === null ? 2 : DecPlaces,
            decSeparator = ".",
            thouSeparator = InputFormatter.thouSeparator,
            n = val.replace(thouSeparator.re, ''),
            i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
            sign = val < 0 ? "-" : "",
            j = (j = i.length) > 3 ? j % 3 : 0;
        return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
    },
    onKeyPress: function (evt) {
        var el = this;
        var charCode = (evt.which) ? evt.which : event.keyCode;
        var number = el.value.split('.');
        //dissallow anything thats not a number, a dot or a comma
        if (charCode !== 44 && charCode !== 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        //dissallow typing 2 chars after the dot
        var caratPos = InputFormatter.getSelectionStart(el);
        var dotPos = el.value.indexOf(".");
        if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
            return false;
        }
        return true;
    },
    onKeyUp: function () {
        var el = this;
        el.value = InputFormatter.format(el.value);
    },
    getSelectionStart: function (o) {
        //http://javascript.nwbox.com/cursor_position/
        if (o.createTextRange) {
            var r = document.selection.createRange().duplicate();
            r.moveEnd('character', o.value.length);
            if (r.text === '') return o.value.length;
            return o.value.lastIndexOf(r.text);
        } else return o.selectionStart;
    },
    toFloat: function (el) {
        var val = el.value,
            n = val.replace(InputFormatter.re, ''),
            f = parseFloat(n);
        return isNaN(f) ? 0 : f;
    }

};

// BEGIN - NumeralJS Number formatting for Calculate Discretionary and Entitlement 

function cleanseNum(dirtyValue, fixPrecision) {
    var myNumeral = numeral(dirtyValue);

    if (myNumeral.value() === null) {
        return 0;
    }

    if (fixPrecision !== undefined && isNaN(fixPrecision) === true) {
        return 'Precision Value Invalid';
    }

    if (fixPrecision === undefined) {
        return parseFloat(myNumeral.value());
    }
    else {
        return parseFloat(myNumeral.value().toFixed(fixPrecision));
    }

}

function ToCurrency(cleanValue) {
    var myNumeral = numeral(cleanValue);
    return myNumeral.format('0,0.00');
}

function blurFormatter(cleanValue, precision) {
    var formatString = '';
    var myNumeral
    if (precision === 0) {
        myNumeral = numeral(cleanValue);
        return myNumeral.format('0,0');
    }
    else {
        myNumeral = numeral(cleanValue);
        return myNumeral.format('0,0.' + ''.padEnd(precision, '0'));
    }
}

$("input").on('focus', function () {
    this.select();
});

function bindBlur(elem) {
    if (elem === undefined) {
        elem = '*';
    }

    $(elem).find("input[data-blur-format]").off('blur').on('blur', function (blurEvent) {
        var $elem = $(this);
        var precision = $elem.data('blurFormat');

        if ($elem.val() === '') {
            return;
        }

        var cleasedNum = cleanseNum($elem.val(), precision);

        $elem.val(blurFormatter(cleasedNum, precision));

        //console.log('blurFormat-ing');
    });

}

// END - NumeralJS Number formatting for Calculate Discretionary and Entitlement 

// BEGIN - Handling Cookies for Passthrough Login

function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin === -1) {
        begin = dc.indexOf(prefix);
        if (begin !== 0) return null;
    }
    else {
        begin += 2;
        var end = document.cookie.indexOf(";", begin);
        if (end === -1) {
            end = dc.length;
        }
    }
    // because unescape has been deprecated, replaced with decodeURI
    //return unescape(dc.substring(begin + prefix.length, end));
    return decodeURI(dc.substring(begin + prefix.length, end));
}



// END - Handling Cookies for Passthrough Login


//TableSorter Supplemental Function - Begin

function getSorts(tableSelector) {

    var tableSortClasses = [{ className: "tablesorter-headerAsc", sort: 'a' }, { className: "tablesorter-headerDesc", sort: 'd' }];

    var arry = $(tableSelector + " th").map(function (index, elem) {

        var sortDirection = '';

        $.each(elem.classList, function (index, value) {

            for (var i = 0; i < tableSortClasses.length; i++) {
                if (tableSortClasses[i].className === value) {
                    sortDirection = tableSortClasses[i].sort;
                }
            }

        });

        if (sortDirection !== '') {
            return new Array([
                index,
                sortDirection
            ]);
        }

    });

    return arry;
}

function setSorts(tableSelector, sortArray) {

    if (sortArray && sortArray.length > 0) {
        $(tableSelector).trigger('sorton', [sortArray]);
    }
    else {
        $(tableSelector).trigger('sortReset');
    }

}

function applyTablesorter() {
    var $newTables = $("table:not(.tablesorter)");
    $newTables.tablesorter({

        theme: "bootstrap",
        widthFixed: false,
        //headerTemplate: '{icon}{content}', // new in v2.7. Needed to add the bootstrap icon!
        emptyTo: 'emptyMin',
        //widgets:['uitheme']

    });

    //update the tablesorter internal cache with the new rows
    var $hasSorting = $('table.tablesorter');
    $hasSorting.trigger("update");

}

//TableSorter Supplemental Function - End

