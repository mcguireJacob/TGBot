//This is meant for events that the entire project should observe, not just a page or an element.
NK.Events = {
    PageLoad: {
        load: function () {
            //Just run the functions.
            String.prototype.replaceAll = function (target, value) {
                var retVal = this;
                if (target) {
                    while (!retVal.indexOf(target) > -1) {
                        retVal = retVal.replace(target, value);
                    }
                    return retVal;
                }
            };
            NK.Events.PageLoad.loadJQuery();
            NK.Formatting.formatCurrencyInputs(true);
            NK.Events.PageLoad.selectFirstVisibleInput();
            NK.Validation.checkMaxLength();
            NK.Formatting.bindNumericOnlyInputs();
            NK.Events.PageLoad.fadeInContent();
			NK.Validation.addRequiredMarks();
            NK.Validation.checkMaxLength();								 
        },
        loadJQuery: function () {
            (function ($) {
                //This is a jQuery plugin that allows for the tablesorter to be unset from a table.
                //Use like $('table').unbindTablesorter().tablesorter()
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

                $.fn.scrollIntoView = function () {
                    var offset = $(this).offset();
                    $('html, body').animate({ scrollTop: offset.top - 50, scrollLeft: offset.left - 50 }, 700, 'swing');
                    return this;
                };

                $.fn.resetChildInputs = function () {
                    var domObj = null;
                    $(this).find(':input').each(function (i, ui) {
                        domObj = $(ui).get()[0];
                        if ($(ui).is(':checkbox') || $(ui).is(':radio')) {
                            domObj.checked = domObj.defaultChecked;
                        }
                        else if ($(ui).is('select')) {
                            for (var j = 0; j < domObj.length; j++) {
                                domObj.options[j].selected = domObj.options[j].defaultSelected
                            }
                        }
                        else {
                            //this works for both textareas and text boxes
                            domObj.value = domObj.defaultValue;
                        }
                    });
                };

                $.fn.serializeObject = function () {
                    var baseArr = $(this).serializeArray();
                    var obj = {};
                    if (baseArr) {
                        for (var i = 0; i < baseArr.length; i++) {
                            obj[baseArr[i].name] = baseArr[i].value;
                        }
                    }
                    return obj;
                };

                $.fn.unformatCurrency = function (precision, skipEmptyValues) {
                    if ($(this).length < 1) { return $(this); }
                    //This will return the text box back to standard decimal formatting
                    //Hmm. This could cause a loss of information. I added that as a parameter so the dev can specify.
                    if (!precision) {
                        precision = 2;
                    }

                    var formatString = '0.';
                    for (var i = 0; i < precision; i++) {
                        formatString += '0';
                    }

                    var rawVal = null;
                    //Set the value of the text box according to the format string and return the text box for chaining.		
                    if ($(this).length == 1) {
                        rawVal = $(this).val();
                        if (skipEmptyValues) {
                            if (rawVal.trim().length == 0) {
                                //
                            }
                            else {
                                $(this).val(numeral(rawVal).format(formatString));
                            }
                        }
                        else {
                            $(this).val(numeral(rawVal).format(formatString));
                        }
                    }
                    else {
                        $(this).each(function () {
                            rawVal = $(this).val();
                            if (skipEmptyValues) {
                                if (rawVal.trim().length == 0) {
                                    //
                                }
                                else {
                                    $(this).val(numeral(rawVal).format(formatString));
                                }
                            }
                            else {
                                $(this).val(numeral(rawVal).format(formatString));
                            }
                        });
                    }
                    return $(this);
                };

                $.fn.formatCurrency = function (precision, addCommas, addDollarSign, skipEmptyValues) {
                    //If no precision was given, assume 2
                    if (!precision) {
                        precision = 2
                    }

                    //Start off the string with a 0
                    var formatString = '0';

                    //If we are using dollar signs, perpend that to the format string
                    //$0
                    if (addDollarSign && addDollarSign == 'true' || addDollarSign == true) {
                        formatString = '$' + formatString;
                    }

                    //If we are using commas, append that to the format string
                    //$0,0
                    if (addCommas && addCommas == 'true' || addCommas == true) {
                        formatString += ',0';
                    }

                    //Add the decimal
                    //$0,0.
                    formatString += '.';

                    //Append how ever many points of precision after the decimal that was requested, or 2.
                    //$0,0.00
                    for (var i = 0; i < precision; i++) {
                        formatString += '0';
                    }

                    var rawVal = null;
                    //Set the value of the text box according to the format string and return the text box for chaining.		
                    if ($(this).length == 1) {
                        rawVal = $(this).val();
                        if (skipEmptyValues) {
                            if (rawVal.trim().length == 0) {
                                //
                            }
                            else {
                                $(this).val(numeral(rawVal).format(formatString));
                            }
                        }
                        else {
                            $(this).val(numeral(rawVal).format(formatString));
                        }
                    }
                    else {
                        $(this).each(function () {
                            rawVal = $(this).val();
                            if (skipEmptyValues) {
                                if (rawVal.trim().length == 0) {
                                    //
                                }
                                else {
                                    $(this).val(numeral(rawVal).format(formatString));
                                }
                            }
                            else {
                                $(this).val(numeral(rawVal).format(formatString));
                            }
                        });
                    }
                    return $(this);
                };
            })(jQuery);
        },
        //Put cursor in the first visible input that is not a bunch of things
        selectFirstVisibleInput: function ($elem) {
            var $elem = $elem;
            //A brief timeout while layout updates
            setTimeout(() => {
                var selector = 'input:not(.datepicker):not([datepicker]):not(button):not([type="checkbox"]):not([type="password"]):not([type="radio"]):visible';
                if ($elem) {
                    $elem.find(selector).first().focus();
                }
                else {
                    $(selector).first().focus();
                }
            }, 400);
        },
        fadeInContent: function () {
            $('#kt_content').fadeIn(400);
        }
    }
};