
async function callForPrice() {
var ok =  $.ajax({
    url: "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=EURUSD%3DX",
        headers: { 'x-api-key': '6CxtVp2Ng73DYJsDMlwQi7e7TMo9LTjB5QXTlmG7' },
        paramsObj: { modules: 'defaultKeyStatistics,assetProfile' }
})

    await ok;
    getThePrices(ok)
 
}

function getThePrices(param) {
   
    
    console.log(JSON.parse(param.responseText).quoteResponse.result[0].regularMarketPrice);
    
    var priceOfSelected = JSON.parse(param.responseText).quoteResponse.result[0].regularMarketPrice;

    var selected = $("#tradingPair").val();

    if (selected == 1) {
        $("#TextOfSelected").text("🔻EurUsd " +  priceOfSelected)
    }
    
}


function fuckingTelegram() {
    var tradeType = $("#TradeType").text();
    var pair = $("#TextOfSelected").text()
    var sl = $("#StopLoss").text()
    var tp = $("#TakeProfit").text()
    var message = tradeType + "%0D%0A" + pair + "%0D%0A %0D%0A" + sl + "%0D%0A" + tp;
    postAjax('https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&text=' + message);

}




function tradeTypeChange() {
    var selected = $("#TradeTypeSelect").val();

    if (selected == 1) {
        $("#TradeType").text("Buy Now");
    }
}




function StopLoss() {
    var typedSL = $("#StopLossInput").val()



    if (typedSL != "") {
        $("#StopLoss").text("🔸Stop Loss: " + typedSL);
    }
}


function TakeProfit() {
    var typedTP = $("#TakeProfitInput").val()



    if (typedTP != "") {
        $("#TakeProfit").text("🔹Take Profit: " + typedTP);
    }
}










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