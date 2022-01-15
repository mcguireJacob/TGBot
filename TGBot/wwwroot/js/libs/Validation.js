NK.Validation = {
  //This can be used when you need to validate a single field using Parsley.
  validateSingle: function(id, caller, callback) {
    var elem = $("#" + id);
    if (!$(elem).length) {
      var err = "No ID Sent To Validate";
      console.error(err);
      throw err;
    }
    if (
      $(elem)
        .parsley()
        .isValid()
    ) {
      callback(caller);
    } else {
      $(elem)
        .parsley()
        .validate();
    }
  },
  //I would like to see one that lets me validate an area of layout.
  addRequiredMarks: function() {
    var reqArr = $("[required]");

    for (var i in reqArr) {
      var lbl = null;
      var elem = reqArr[i];
      if (i === "length") {
        break;
      }

      // testing for input groups in forms, its a layer deeper than normal form elements
      var inputGroup = $(elem).parent(".input-group");
      if (inputGroup && inputGroup.length > 0) {
        lbl = inputGroup.siblings("label");
      } else {
        lbl = $(elem).prev("label");
        if (!lbl) {
          lbl = $(elem).after("label");
        }
      }

      if (lbl) {
        var spn = $(lbl).find(".requiredMark");
        var hasLbl = $(spn).length && $(spn).length > 0;
        if (!hasLbl) {
          $(lbl).append(
            '<span class="requiredMark" title="A value is required">&nbsp;*</span>'
          );
        }
      }
    }
  },
  cleanRequiredMarks: function() {
    $(".requiredMark").remove();
    NK.Validation.addRequiredMarks();
  },
  //I like this one. But, if we use bound blazor forms, we would not need it.
  //max length notification - turns the border a color and disallows further charecters from being entered.
  checkMaxLength: function() {
    $("[maxlength]")
      .on("keyup", function(e) {
        if ($(this).val().length >= $(this).attr("maxlength")) {
          $(this).addClass("border-warning");
          if ($(this).val().length > $(this).attr("maxlength")) {
            $(e).preventDefault();
          }
        } else {
          $(this).removeClass("border-warning");
        }
      })
      .on("focusout", function() {
        $(this).removeClass("border-warning");
      });
  }
};
