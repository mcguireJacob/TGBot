const { log } = require("node:console");

$(function () {
    disableAll()
});

function disableAll() {
    $("#tradingPair").attr('disabled', true)
    $("#StopLossInput").attr('disabled', true)
    $("#TakeProfitInput").attr('disabled', true)
    $("#LimitAt").attr('disabled', true)
}


function callForPrice() {

    var tradeType = $("#TradeTypeSelect").val()

    console.log("hi")
    switch (tradeType) {
        
        case '1':
        case '2':
            if ($("#tradingPair").val() != '') {
                $("#StopLossInput").attr('disabled', false)
                $("#TakeProfitInput").attr('disabled', false)
                getPriceFromAPI();
            } else {
                $("#StopLossInput").attr('disabled', true)
                $("#TakeProfitInput").attr('disabled', true)
            }
            break;
        case '3':
        case '4':
            if ($("#tradingPair").val() != '') {
                $("#TextOfSelected").text("🔻" + $("#tradingPair option:selected").text())
            }
            
            if ($("#tradingPair").val() != '' && $("#LimitAt").val() != '') {
                
                $("#StopLossInput").attr('disabled', false)
                $("#TakeProfitInput").attr('disabled', false)
            } else {
                $("#StopLossInput").attr('disabled', true)
                $("#TakeProfitInput").attr('disabled', true)
            }
            
            break;
        
    }
}


function getPriceFromAPI() {
    var selected = $("#tradingPair").val()
    NK.Ajax.post($("#getPricey").val(),
        { id: selected },
        function (data) {
            getThePrices(data)
        },
        function () {
            console.log("DIDNT WORK")
        }
    )
}





function getThePrices(param) {

    $("#TextOfSelected").text("🔻" + $("#tradingPair option:selected").text() + " "  + param)
            
    $("#currentPrice").val(param)

}


function SendTelegramMessage() {
    var tradeType = $("#TradeType").text();
    var pair = $("#TextOfSelected").text()
    var sl = $("#StopLoss").text()
    var tp = $("#TakeProfit").text()
    var additional = $("#additionalText").text()
    var limitOne = $("#LimitAt").val()
    var message = ''
    if (additional != 'Additional Text Here') {
        message = tradeType + "%0D%0A" + pair + "%0D%0A %0D%0A" + sl + "%0D%0A" + tp + "%0D%0A %0D%0A" + additional;
    } else {
        message = tradeType + "%0D%0A" + pair + "%0D%0A %0D%0A" + sl + "%0D%0A" + tp + "%0D%0A %0D%0A";
    }


    /*message = tradeType + "%0D%0A" + pair + "%0D%0A %0D%0A" + "At: " + limitOne + "%0D%0A %0D%0A" + sl + "%0D%0A" + tp;*/

    postAjax('https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&text=' + message, {},
        function (data) {
            console.log(data.result.message_id);
            saveForm(data.result.message_id)
        }
    );




}


function testAjaxStuff() {
    postAjax('https://tradingview.com/api/authorize?login=user1&password=dfkjhoijogpoi')

}

function clearInputsAndMessage() {
    $("#TextOfSelected").text("")
    $("#StopLoss").text("")
    $("#TakeProfit").text("")
    $("#tradingPair").val("")
    $("#StopLossInput").val("")
    $("#TakeProfitInput").val("")
    $("#LimitAt").val("")
    $("#LimitAtMessage").text("")

}



function tradeTypeChange() {
    var selected = $("#TradeTypeSelect").val();
    var tradeType = $("#TradeType")
    clearInputsAndMessage()
    disableAll()

    $("#LimitDiv").css("display", "none")
    switch (selected) {
        case "Please Select":
            tradeType.text("");
            break;
        case '1':
            tradeType.text("📈Buy Now");
            $("#tradingPair").attr('disabled', false)
            break;
        case '2':
            tradeType.text("📉Sell Now");
            $("#tradingPair").attr('disabled', false)
            break;
        case '3':
            $("#LimitDiv").css("display", "")
            tradeType.text("📈Buy Limit");
            $("#tradingPair").attr('disabled', false)
            $("#LimitAt").attr('disabled', false)
            break;
        case '4':
            $("#LimitDiv").css("display", "")
            tradeType.text("📉Sell Limit");
            $("#tradingPair").attr('disabled', false)
            $("#LimitAt").attr('disabled', false)
            break;
    }

}




function Limit() {
    
    $("#LimitAtMessage").text("At: " + $("#LimitAt").val())
    $("#limitOne").val($("#LimitAt").val())

    if ($("#tradingPair").val() != '' && $("#LimitAt").val() != '') {
        $("#StopLossInput").attr('disabled', false)
        $("#TakeProfitInput").attr('disabled', false)
    } else {
        $("#StopLossInput").attr('disabled', true)
        $("#TakeProfitInput").attr('disabled', true)
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













function saveForm(messageID) {
    var tTradeType = $("#TradeTypeSelect").val()
    var tTradingPair = $("#tradingPair").val()
    var tCurrentPrice = $("#currentPrice").val()
    var tSL = $("#StopLossInput").val()
    var tTp = $("#TakeProfitInput").val()
    var tLimitOne = $("#limitOne").val()




    var payload = {
        tTradeType: tTradeType,
        tTradingPair: tTradingPair,
        tCurrentPrice: tCurrentPrice,
        tSL: tSL,
        tTp: tTp,
        tTelegramMessageID: messageID,
        tLimitOne: tLimitOne
    }
    NK.Ajax.post($("#TradingForm").attr("action"),
        payload,
        function (path) {
            console.log("it worked")
            console.log(path)
        },
        function () {
            console.log("DIDNT WORK")
        }
    )
}



