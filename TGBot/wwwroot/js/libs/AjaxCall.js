//#region Base Ajax. All other AJAX constructs will use this.
//Note, A Get is when all parameters are in the URL. A Post is when we have form data.
//I am going to automate this decision.

"use strict";

var NKAjaxCall = function(obj) {
  //Where are we contacting
  this.endPoint = null;
  //What method are we using
  (this.method = null),
    //What parameters are we sending
    (this.parameters = {});
  //The last results will be stored here.
  this.data = [];

  this._origTxt = null;
  this._origPosition = null;
  this._origHeight = null;
  this._origWidth = null;

  //Events
  this.events = {
    onBeforeSend: null,
    onAfterSend: null,
    onResultReceived: null,
    onUnexpectedResult: null,
    onSuccess: null,
    onFail: null,
    always: null
  };

  //For now, I am only allowing obj sets on construct.
  //I might allow this to be the URL, or an obj
  if (obj) {
    this.setObj(obj);
  }
};

//Set all of the properties to this instance, if the property exists
NKAjaxCall.prototype.setObj = function(obj) {
  //Set all of the properies that fit this object
  for (var i in obj) {
    this.set(i, obj[i]);
  }
};

//Set the value of a property based on a key, if the property exists.
NKAjaxCall.prototype.set = function(key, value) {
  //I am not going to allow properties to be added via set.
  //To make a.n assignment, you have to adress a property I have added to the object constructor.
  if (this.hasOwnProperty(key)) {
    this[key] = value;
  }

  //Return this for chaining.
  return this;
};

//Return the value of a property of this instance
NKAjaxCall.prototype.get = function(key) {
  return this[key];
};

//Check an error result for usable information
NKAjaxCall.prototype.parseFail = function(response, callback) {
  //401 would happen if someone tried to access a controller they do not have permission to. This does not cause a logout.
  //503 would happen if the website is taken off line.
  if (response.status == 401 || response.status == 503) {
    //Redirect to login. Note, if this is a 503, the server may be off line and the result would be an error page indicating so.
    //If the site is down due to inactivity, this request should bring it back up.
    document.location = "/login/?returnUrl=" + document.location;
    return;
  }
  if (response.status == 404) {
    Swal.fire("Error!", "Endpoint not found", "error");
    console.error("Endpoint not found", response);
    return;
  }
  var isJson = !!response.responseJSON;
  var isText =
    !!response.responseText && response.responseText != "" && !isJson;
  if (!isJson && !isText) {
    return;
  }
  if (callback) {
    callback(response);
  } else {
    var message = "";
    if (isJson && response.responseJSON.JsonResult) {
      message = response.responseJSON.JsonResult;
    } else if (isText && response.responseText) {
      message = response.responseText;
    } else {
      console.error("Ajax Failed!", response);
    }
    if (message.trim().length > 0) {
      Swal.fire("Error!", message, "error");
    }
  }
};

NKAjaxCall.prototype.send = function() {
  //The dev can say to do stuff before the send. If they did, call that function
  if (this.events.onBeforeSend) {
    this.events.onBeforeSend(this);
  }

  $.ajax({
    url: this.endPoint,
    type: this.method,
    cache: false,
    data: this.parameters
  })
    //If it worked, call success
    .done(data => this.onSuccess(data))
    //Only the jsXHR contains useful information.
    //If this request fails, parse the ReturnArgs obj and send it to their fail function, if supplied.
    //Otherwise, put the result in a standard error modal.
    .fail(jsXHR => this.parseFail(jsXHR, this.events.onFail));

  //If it is a successs, call the sucess function with the data passed back from the server.
  //.always((data) => this.onSuccess(data));

  //The dev can say to do stuff after the send. If they did, call that function
  if (this.events.onAfterSend) {
    this.events.onAfterSend(this);
  }

  return this;
};

NKAjaxCall.prototype.onSuccess = function(data) {
  //Whatever came back, assign it to the current data
  this.data = data;

  //The dev can choose to do stuff after we get the result, but before processing any data.
  //If they provided an event, call it.
  if (this.events.onResultReceived) {
    this.events.onResultReceived(this.data);
  }

  //If the user doe snot block the onResultReceived call, success will fire immediately after. We could block that with a scoped callback on that function.

  //The data we passed back above may have been manipulated.
  //Call the success handler with the data as it is now.
  if (this.events.onSuccess) {
    this.events.onSuccess(this.data);
  }
};

//This is only meant to be a default value. I think you could assign ()=>{return $([your layout container]).html();};
NKAjaxCall.prototype.spinner =
  '<i style="position:relative;" class="fa fa-spinner fa-pulse fa-lg"></i>';

//IMO, we should maybe consider adding a button array [$elem] parameter so that we can disable more than just the $elem button
//Maybe an attribute on other buttons we could scan for, instead?
//We should also eval $elem for 'function'. That way it can be overridden easily.
NKAjaxCall.prototype.handleButton = function($elem, isDisabled) {
  if (isDisabled) {
    $elem.prop("disabled", true);
    this.origTxt = $elem.html();
    //I think we are gathering the height and width here so that
    //we can set it back after we change the button inner html.
    //(I was tempted to put them in the assignments below)
    this._origHeight = $elem.height();
    this._origWidth = $elem.width();
    this._origPosition = $elem.css("position");

    //Change the button layout
    $elem.html(this.spinner);
    $elem.css("position", "relative");
    //Resset the size despite the content change
    $elem.height(this._origHeight);
    $elem.width(this._origWidth);
    //Disable more clicks
    $elem.prop("disabled", true);
  } else {
    //These were in the old code, but I don't 'think' they do anything since we are about to replace the entire html of the element.
    //$elem.find('i').removeClass('fa-spinner');
    //$elem.find('i').removeClass('fa-pulse');
    //Replace the html back to the original content
    $elem.html(this.origTxt);
    //Return it to it's original position if it moved when we changed it back.
    $elem.css("position", this._origPosition);
    //Enable further clicks
    $elem.prop("disabled", null);
  }
};
//#endregion
