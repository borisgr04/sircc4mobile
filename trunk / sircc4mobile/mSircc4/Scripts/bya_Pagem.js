Number.prototype.format = function () {
    return this.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
};

var byaPageM = new Object();

var byaPageM = {
    NomApp: function () {
        return "SIRCC 4 Mobile.";
    },
    PiePagina: function () {
        return "SIRCC 4 Mobile. Desarrollado por B&A Systems S.A.S.";
    },
    JSONtoString: function (json) {
        // aqui desarrollo una funcionalidad concreta
        return JSON.stringify(json);
    },
    msgJson: function (json) {
        alert(this.JSONtoString(json));
    },
    ajaxError: function (jqXHR, status, error) {
        //alert(error + "-" + jqXHR.responseText);
        alert("Ajax " + error + " - " + jqXHR.responseText);
        //alert(status);
    },
    retObj: function (d) {
        return (typeof d) == 'string' ? eval('(' + d + ')') : d;
    },
    
    converJSONDate: function (dateTime) {
        //alert(dateTime);
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = date.getFullYear() + "-" +
                                        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                                            ("0" + date.getDate()).slice(-2);

            return formatted;
        }
    },

    converJSONDateTime: function (dateTime) {
        if (dateTime != null) {
            var date = new Date(parseInt(dateTime.substr(6)));
            var formatted = date.getFullYear() + "-" +
                                        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                                            ("0" + date.getDate()).slice(-2) + " " + date.getHours() + ":" +
                                        ("0" + date.getMinutes()).slice(-2);
            return formatted;
        }
    }
}