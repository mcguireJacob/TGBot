/* Custom Parsley validators */
/*
Usage: data-parsley-check-date-format="[0-9]{2}/[0-9]{2}/[0-9]{4}"
*/
window.Parsley.addValidator(
  "checkDateFormat",
  function(value, requirments) {
    var patt = new RegExp(requirments);
    var res = patt.test(value);
    if (res === true && !isNaN(Date.parse(value))) {
      return true;
    }
    return false;
  },
  22
).addMessage(
  "en",
  "checkDateFormat",
  "Invalid Date, use date picker for valid dates"
);

/*
Usage: data-parsley-atleast-one-checked="requirement"
*/
window.Parsley.addValidator(
  "atleastOneChecked",
  function(value, requirement) {
    var numChecked = 0;
    $(`[data-parsley-atleast-one-checked="${requirement}"]`).each(function() {
      console.log(numChecked);
      if ($(this).prop("checked")) {
        numChecked++;
      }
    });
    if (numChecked == 0) {
      return false;
    }
    return true;
  },
  22
).addMessage("en", "atleastOneChecked", "One of the options must be checked");

/*
Usage: data-parsley-check-time-span='input[name="tcdStartTime"]'
*/
//window.Parsley
//    .addValidator('checkTimeSpan', function (endTime, startSelector) {
//        var startTime = $(startSelector).val();
//        var startTime_parts = startTime.split(":");
//        var endTime_parts = endTime.split(":");
//        // numeric value we're going to use to validate against
//        var startTime_raw = 0;
//        var endTime_raw = 0;
//        if (startTime_parts[1].includes('pm') && startTime_parts[0] != '12') {
//            startTime_raw = parseInt(startTime_parts[0], 10) + 12;
//        } else if (startTime_parts[1].includes('am') && startTime_parts[0] == '12') {
//            startTime_raw = parseInt(startTime_parts[0], 10) - 12;
//        } else {
//            startTime_raw = parseInt(startTime_parts[0], 10);
//        }
//        if (endTime_parts[1].includes('pm') && endTime_parts[0] != '12') {
//            endTime_raw = parseInt(endTime_parts[0], 10) + 12;
//        } else if (endTime_parts[1].includes('am') && endTime_parts[0] == '12') {
//            endTime_raw = parseInt(endTime_parts[0], 10) - 12;
//        } else {
//            endTime_raw = parseInt(endTime_parts[0], 10);
//        }
//        startTime_raw = startTime_raw + (parseInt(startTime_parts[1].replace("am", "").replace("pm", ""), 10) / 60);
//        endTime_raw = endTime_raw + (parseInt(endTime_parts[1].replace("am", "").replace("pm", ""), 10) / 60);
//        if (endTime_raw >= startTime_raw) {
//            return true;
//        }
//        return false;
//    }, 22)
//    .addMessage('en', 'checkTimeSpan', 'Start time cannot be after the end time!');

// Usage: data-parsley-pin-check
//window.Parsley
//    .addValidator('pinCheck', function (value) {
//        if (value.length == 0 || value.length == 4) {
//            return true;
//        }
//        return false;
//    }, 22)
//    .addMessage('en', 'pinCheck', 'Invalid Pin, check Pin length');

// Usage: data-parsley-check-phone
//window.Parsley
//    .addValidator('checkPhone', function (value) {
//        if (value != '') {
//            var stripRegEx = /[\+\(\)\/\.\s\-]+/g;
//            var cleanPhone = value.replace(stripRegEx, "");
//            var posExtension = cleanPhone.indexOf('ext');
//            var extension = '';
//            if (posExtension !== false && posExtension > 0) {
//                extension = cleanPhone.substr(posExtension);
//                cleanPhone = cleanPhone.substr(0, posExtension);
//            }
//            var simplePattern = /^\d?\W*(\d{3})\W*(\d{3})\W*(\d{4})$/;
//            var matches = cleanPhone.match(simplePattern);
//            if (matches != null && matches !== false) {
//                //var returnRes = {};
//                //returnRes.match = matches[1]+'-'+matches[2]+'-'+matches[3]+' '+extension;
//                return true;
//            }
//        }
//        return false;
//    }, 22)
//    .addMessage('en', 'checkPhone', 'Phone is invalid!');

// Usage: data-parsley-check-zip
//window.Parsley
//    .addValidator('checkZip', function (value) {
//        if (!value.match(/^([0-9]{5}|[0-9]{5}\-[0-9]{4})$/)) {
//            return false;
//        }
//        return true;
//    }, 22)
//    .addMessage('en', 'checkZip', 'Zip is invalid!');

// Usage: data-parsley-check-date
//window.Parsley
//    .addValidator('checkDate', function (value) {
//        var timestamp = Date.parse(value);
//        return (isNaN(timestamp) ? false : true);
//    }, 22)
//    .addMessage('en', 'checkDate', 'Date is invalid');

// Usage: data-parsley-check-currency
//window.Parsley
//    .addValidator('checkCurrency', function (value) {
//        //var regex  = /^\d+(?:\.\d{0,2})$/;
//        var regex = /^\$?(([1-9][0-9]{0,2}(,[0-9]{3})*)|[0-9]+)?\.[0-9]{1,2}$/;
//        if (regex.test(value)) {
//            return true; // alert("Number is valid");
//        }
//        return false;
//    }, 22)
//    .addMessage('en', 'checkCurrency', 'Value needs to be a valid currency amount');

// Usage: data-parsley-check-blank="#ElementID"
//window.Parsley
//    .addValidator('checkBlank', function (value, requirement) {
//        if ($(requirement).val() != '' && value == '') {
//            return false;
//        } else if ($(requirement).val() == '' && value != '') {
//            return false;
//        }
//        return true;
//    }, 22)
//    .addMessage('en', 'checkBlank', 'Need to choose an option');

//has uppercase
//window.Parsley.addValidator('uppercase', {
//    requirementType: 'number',
//    validateString: function (value, requirement) {
//        var uppercases = value.match(/[A-Z]/g) || [];
//        return uppercases.length >= requirement;
//    },
//    messages: {
//        en: 'Your password must contain at least (%s) uppercase letter.'
//    }
//});

//has lowercase
//window.Parsley.addValidator('lowercase', {
//    requirementType: 'number',
//    validateString: function (value, requirement) {
//        var lowecases = value.match(/[a-z]/g) || [];
//        return lowecases.length >= requirement;
//    },
//    messages: {
//        en: 'Your password must contain at least (%s) lowercase letter.'
//    }
//});

//has number
window.Parsley.addValidator("maxNumber", {
  //requirementType: 'number',
  validateString: function(value, max) {
    var numeralValue = new numeral(value).value();

    if (max < numeralValue) {
      return false;
    }

    if (isNaN(numeralValue)) {
      return false;
    } else {
      return true;
    }
  }
});

//has special char
//window.Parsley.addValidator('special', {
//    requirementType: 'number',
//    validateString: function (value, requirement) {
//        var specials = value.match(/[^a-zA-Z0-9]/g) || [];
//        return specials.length >= requirement;
//    },
//    messages: {
//        en: 'Your password must contain at least (%s) special characters.'
//    }
//});
/*
data-parsley-minlength="8"
data-parsley-uppercase="1"
data-parsley-lowercase="1"
data-parsley-number="1"
data-parsley-special="1"
*/
