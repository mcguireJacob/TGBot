NK.Modals = {
  //This is used to open a modal where the layout is to be parsed on the server using razor.
  //Use the callback to set up form validation or any other after action setup
  serverModal: (modalURL, caption, size, callback) => {
    var success = false;
    $.ajax({
      url: modalURL,
      type: "get",
      cache: false
    })
      .done(function(data, textStatus) {
        $("#modalAddEdit")
          .find(".modal-body")
          .html(data); //replace the modal with received content
        $("#modalAddEdit")
          .find(".modal-header > h3")
          .html(caption);
        $("#modalSize").addClass(size);

        NK.Validation.addRequiredMarks();
        /* Init.bindDatePickers($("#modalAddEdit"));*/
        success = true;
      })
      .fail(function(jqXHR, textStatus, errorThrown) {
        var jsonError = "";
        try {
          jsonError = JSON.parse(jqXHR.responseText);
        } catch (err) {
          console.error(
            "Unable to parse ajax error response as JSON",
            err.message
          );
          ErrorMessage.show(
            "There was an error retrieving your data!: " + err.message
          );
          return;
        }
        if (typeof jsonError !== "undefined" && jsonError.JsonResult) {
          ErrorMessage.show(jsonError.JsonResult);
          return;
        } else {
          alert("There was an error retrieving your data!");
        }
      })
      .always(function(data, textStatus) {
        if (textStatus !== "error") {
          $("#modalAddEdit").modal();
          callback && callback(success);
        }
      });
  },
  serverModalMd: (modalURL, caption, callback) => {
    return NK.Modals.serverModal(modalURL, caption, "modal-md", callback);
  },
  serverModalSm: (modalURL, caption, callback) => {
    return NK.Modals.serverModal(modalURL, caption, "modal-sm", callback);
  },
  serverModalLg: (modalURL, caption, callback) => {
    return NK.Modals.serverModal(modalURL, caption, "modal-lg", callback);
  },

  localModal: function($selector, callback) {
    $($selector).modal();
    callback && callback();
  },
  alert: function(message, title, closeCallback) {
    var selector = "errormessage";
    var btn = $("#" + selector).find('[type="button"]');
    if (closeCallback) {
      $(btn).on("click", function() {
        $("#errormessage").modal("hide");
        //If we have a callback, wait just a little longer than the fadeout time on the modal and then call it.
        setTimeout(closeCallback, 410);
      });
    } else {
      $(btn).off("click");
    }

    $("#errormessageTitle").html(title || "Error:");
    $("#errormessageContent").html(message || "");
    $("#errormessage").modal();
  },
  confirm: function(message, title, callback) {
    $.confirm({
      animation: "top",
      title: title,
      content: message,
      buttons: {
        confirm: {
          btnClass: "btn-primary",
          action: function() {
            callback(true);
          }
        },
        cancel: {
          btnClass: "btn-secondary",
          action: function() {
            callback(false);
          }
        }
      }
    });
  }
};
