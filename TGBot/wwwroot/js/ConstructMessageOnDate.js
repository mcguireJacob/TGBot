function getMessages() {
    var fromDate = $("#fromDate").val()
    var toDate = $("#toDate").val()
    var payload = { from: fromDate, to: toDate };
    if (fromDate != "" && toDate != "") {
        NK.Ajax.post($("#urlToGetMessages").val(),
            payload,
            function (data) {
                console.log(data)
                $("#trades").html(data)
                
            }
            
        )
    }
    
}


