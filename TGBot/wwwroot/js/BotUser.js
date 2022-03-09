$(function () {
    
    $("#AccountServer").select2();
    $(".select2-selection--single").css("height", "2.2em")
    $(".select2-selection__rendered").css("font-size", "1rem")
    $(".select2-selection__rendered").css("margin-top", "0.2rem")
    $(".select2-selection__arrow").css("margin-top", "0.3rem")

});


function saveTradeInfo() {
    var aID = $("#aID").val()
    var accountLogin = $("#accountLogin").val()
    var accountPassword = $("#accountPassword").val()
    var accountServer = $("#accountServer").val()
    var riskPct = $("#riskPct").val()
    var fixedLotSize = $("#fixedLotSize").val()

    var payload = {
        aID: aID,
        aAccountLogin : accountLogin,
        aAccountPassword : accountPassword,
        aAccountServer : accountServer,
        aRiskPct : riskPct,
        aFixedLot : fixedLotSize

    }


    NK.Ajax.post($("#saveAccount").val(),
        payload
    )
    toastr.success("Account Details Saved")


}

function clearOther(pctOrFixed) {
    if (pctOrFixed == "pct") {
        $("#fixedLotSize").val("")
    }
    if (pctOrFixed == "fixed") {
        $("#riskPct").val("")
    }

}

var countDecimals = function (value) {
    if (Math.floor(value) === value) return 0;
    if (value.toString().split(".")[1] != null)
    return value.toString().split(".")[1].length || 0;
}

function setTwoNumberDecimal(el) {
    
    var decimalCount = countDecimals(el.value)

    if (decimalCount > 2) {
        el.value = parseFloat(el.value).toFixed(2);
    }

    
}