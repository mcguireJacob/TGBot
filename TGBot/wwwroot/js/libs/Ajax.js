//Depends on AjaxCall.js. That file must load first.
//#region Ajax Implementations
NK.Ajax = {
  util: {
    //I don't expect this will work all the time. It will probably not work with files.
    //However, I think I can use it later to get the form info and create the objects I need to post with files.
    getFormData: function($form) {
      return $($form).serialize();
    },
    getFormDataWithFiles: function($form) {
      var formFields = NKAjax.util.getFormData($form);
      var fileInputs = $('input[type="file"]');
      //Build form post
      throw "Not implemented";
    }
  },
  get: function(url, callback) {
    new NKAjaxCall({
      method: "GET",
      endPoint: url,
      events: {
        onSuccess: callback
      }
    }).send();
  },
  post: function(url, data, success, fail, divSelector) {
    new NKAjaxCall({
      method: "POST",
      parameters: data,
      endPoint: url,
      events: {
        onBeforeSend: () => {
          if (divSelector) {
            KTApp.block(divSelector, {
              overlayColor: "#000000",
              state: "primary",
              message: "Loading..."
            });
          }
        },
        onAfterSend: () => {
          if (divSelector) {
            KTApp.unblock(divSelector);
          }
        },
        onSuccess: success,
        onFail: fail
      }
    }).send();
  },
  sendForm: function($form, success, fail) {
    NK.Ajax.post(
      $form.attr("action"),
      NK.Ajax.util.getFormData($form),
      success,
      fail
    );
  },
  sendFormWithSpinner: function($form, divSelector, success, fail) {
    new NKAjaxCall({
      method: "POST",
      parameters: NK.Ajax.util.getFormData($form),
      endPoint: $form.attr("action"),

      events: {
        onBeforeSend: () => {
          if (divSelector) {
            KTApp.block(divSelector, {
              overlayColor: "#000000",
              state: "primary",
              message: "Loading..."
            });
          }
        },
        onAfterSend: () => {
          if (divSelector) {
            KTApp.unblock(divSelector);
          }
        },
        onSuccess: success,
        onFail: fail
      }
    }).send();
  }
};
//#endregion
