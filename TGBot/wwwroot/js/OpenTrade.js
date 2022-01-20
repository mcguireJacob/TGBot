function CloseTradeManually(elem) {

    var tID = $(elem).parent().find(".tID").val();
    
    var payload = { tID: tID}
    NK.Ajax.post($("#ActionToClose").val(),
        payload,
        function () {

            document.location.href = "OpenTrades"
        }
    )
}