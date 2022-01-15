//Keepalive
var keepAliveUrl = '';
var loginUrl = '';
var keepAlive = function (keepAliveUrl, loginUrl) {
    keepAliveUrl = keepAliveUrl; loginUrl = loginUrl;
    setInterval(function () { _keepalive(keepAliveUrl, loginUrl); }, 60000);//Call this function every 60 seconds
};
var _keepalive = function (keepAliveUrl, loginUrl) {
    var pathExceptions = [
        "/login/"//This should cover anything pertaining to login
        /*Add Exceptions Here*/
    ];

    function _isPathException() {
        for (var i in pathExceptions) {
            var p = pathExceptions[i];
            if (document.location.href.toLowerCase().indexOf(p.toLowerCase()) > -1) {
                return true;
            }
        }
        return false;
    }

    $.ajax({
        url: keepAliveUrl,
        type: 'post',
        cache: false
    })
        .done(function (data, textStatus) {
            if (_isPathException()) { return; }
            if (data) {
                //If we have a session, success is returned. Otherwise fail.
                if (data === 'fail') {
                    document.location = loginUrl;
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            if (_isPathException()) { return; }
            document.location = loginUrl;
        });
};