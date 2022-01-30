function getMessages() {
    var fromDate = $("#fromDate").val()
    var toDate = $("#toDate").val()
    var payload = { from: fromDate, to: toDate };
    if (fromDate != "" && toDate != "") {
        
        NK.Ajax.post($("#urlToGetMessages").val(),
            payload,
            function (data) {
                
                $("#trades").html(data)
                if ($("#trades").html() == "\n\n\n") {
                    $("#weeklyMessages").css("display", "none")
                } else {
                    $("#weeklyMessages").css("display", "")
                }
            }
            
        )
    } else {
        $("#weeklyMessages").css("display", "none")
    }
    
}


function constructWeeklyMessage() {
    var winRatio = $("#winRatio").text()

    var tradesInRangeMessage = ""
    var tradesInRange = $(".tradeWinLoss")
    for (var i = 0; i < tradesInRange.length; i++) {
        tradesInRangeMessage += tradesInRange[i].innerHTML + "%0D%0A"
    }

    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    const d = new Date($("#fromDate").val().replace('-', '/'));
    let fromMonth = months[d.getMonth()];


    const tod = new Date($("#toDate").val().replace('-', '/'));
    let toMonth = months[tod.getMonth()];

    var monthDateMessage = "";

    if (fromMonth != toMonth) {
        monthDateMessage = fromMonth + " " + d.getDate() + " - " + toMonth + " " + tod.getDate()
    } else {
        monthDateMessage = fromMonth + " " + d.getDate() + "-" +  tod.getDate()
    }

    var totalPips = $("#totalPips").text()


    var message = d.getFullYear() + " " + monthDateMessage + "%0D%0A %0D%0A" + winRatio + "%0D%0A %0D%0A" + tradesInRangeMessage + "%0D%0A %0D%0A" + totalPips

    postAjax('https://api.telegram.org/bot5074478768:AAGgm7gcHeySMXo13qhw3fwwYHxx1F7S6eg/sendMessage?chat_id=@ShitTradinBot&text=' + message, {});
}


