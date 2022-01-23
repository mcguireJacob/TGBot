function CloseTradeManually(elem) {

    var tID = $(elem).parent().find(".tID").val();
    $(elem).parent().parent().remove()
    var payload = { tID: tID}
    NK.Ajax.post($("#ActionToClose").val(),
        payload,
        function () {

            
        }
    )
}