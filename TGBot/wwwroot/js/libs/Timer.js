
/*
    Use Like:
    Create a 1 minute timer
    
    var timy = null
    $(function () {
        timy = new Timer(6000, function (args) {
            alert(args);
            timy.stop();
        });
        timy.start('One minute has passed. Timer will now stop');
        console.log(t);
    });   
*/
NK.Timer = function (interval, event) {
    this.timer = null;
    this.interval = interval;
    this.start = function (args) {
        this.args = args;
        this._startTimer();
    };
    this._startTimer = function () {
        this.timer = setInterval((function (args) {
            if (this.eventHandler) {
                this.eventHandler(this.args);
            }
        }).bind(this), this.interval);
    },
    this.stop = function () {
        clearInterval(this.timer);
    };
    this.reset = function (interval, args, eventHandler) {
        this.stop();
        if (interval) {
            this.interval = interval;
        }
        if (args) {
            this.args = args;
        }
        if (eventHandler) {
            this.eventHandler = eventHandler;
        }
        this._startTimer();
    };
    this.eventHandler = event;
    this.args = null;
};