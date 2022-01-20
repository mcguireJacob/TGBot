

 function callForPrice() {
     var selected = $("#tradingPair").val();
     //postAjax($("#getPricey").val(), {id: selected},
     //   function (data) {
     //       getThePrices(data)
     //    })

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
  
    var selected = $("#tradingPair").val();

    if (selected == 1) {
        $("#TextOfSelected").text("🔻EurUsd " +  param)
    }

   

    $("#currentPrice").val(param)
    
}


function fuckingTelegram() {
    var tradeType = $("#TradeType").text();
    var pair = $("#TextOfSelected").text()
    var sl = $("#StopLoss").text()
    var tp = $("#TakeProfit").text()
    var message = tradeType + "%0D%0A" + pair + "%0D%0A %0D%0A" + sl + "%0D%0A" + tp;
    var telegram = postAjax('https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&text=' + message, {},
        function (data) {
            console.log(data.result.message_id);
            saveForm(data.result.message_id)
        }
    );
    
    
    

}


function testAjaxStuff() {
    postAjax('https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&reply_to_message_id=862&text=hello')
}



function tradeTypeChange() {
    var selected = $("#TradeTypeSelect").val();

    

    switch (selected) {
        case "Please Select":
            $("#TradeType").text("");
            break;
        case '1':
            $("#TradeType").text("📈Buy Now");
            break;
        case '2':
            $("#TradeType").text("📉Sell Now");
            break;
        case '3':
            $("#TradeType").text("📈Buy Limit");
            break;
        case '4':
            $("#TradeType").text("📉Sell Limit");
            break;
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
    

    var payload = {
        tTradeType: tTradeType,
        tTradingPair: tTradingPair,
        tCurrentPrice: tCurrentPrice,
        tSL: tSL,
        tTp: tTp,
        tTelegramMessageID: messageID
    }
    NK.Ajax.post($("#TradingForm").attr("action"),
        payload,
        function () {
            console.log("it worked")
        },
        function () {
            console.log("DIDNT WORK")
        }
    )
}



