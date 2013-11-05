var jqbyaQuitar = true;

$.datepicker.regional['es'] = {
    closeText: 'Cerrar',
    prevText: '<Ant',
    nextText: 'Sig>',
    currentText: 'Hoy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
    dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
    weekHeader: 'Sm',
    dateFormat: 'dd/mm/yy',
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: ''
};

$.datepicker.setDefaults($.datepicker.regional['es']);

function updateTips(IdMsg, t) {
    IdMsg.text(t).addClass("ui-state-highlight");
    setTimeout(function () {
        IdMsg.removeClass("ui-state-highlight", 1500);
    }, 500);
}
function checkRequired(o, n, IdMsg) {
    if (o.val() == "") {
        o.addClass("error");
        //updateTipsAlert(IdMsg, "El " + n + " es requerido.");
        MsgError(IdMsg, "El Campo [" + n + "] es requerido.");
        return false;
    } else {
        return true;
    }
}

function checkLength(o, n, min, max, IdMsg) {
    if (o.val().length > max || o.val().length < min) {
        o.addClass("error");
        //updateTips(IdMsg, "Largo de " + n + " debe ser entre " + min + " y " + max + ".");
        MsgError(IdMsg, "Largo de " + n + " debe ser entre " + min + " y " + max + ".");
        return false;
    } else {
        return true;
    }
}

function checkRegexp(o, regexp, n, IdMsg) {
    if (!(regexp.test(o.val()))) {
        o.addClass("error");
        //updateTips(IdMsg,n);
        MsgError(IdMsg, n);
        return false;
    } else {
        return true;
    }
}

function MsgBoxR(Id, Error, Mensaje) {
    if (Error) {
        MsgError(Id, Mensaje);
    }
    else {
        MsgOK(Id, Mensaje);
    }
}

function MsgError(Id, Mensaje) {
    $(Id).addClass('error');
    $("#spanIcon", $(Id)).addClass('ui-icon-alert');
    $(Id).html(Mensaje);
    //$("#tit", $(Id)).html("Error:");
    //$("#msg", $(Id)).html(Mensaje);
    
    if (jqbyaQuitar) {
        setTimeout(function () {
            $(Id).removeClass('error', 1500);
        }, 500);
    }
    
}
function MsgLimpiar(Id){
    $(Id).addClass('informacion');
    $(Id).removeClass('error');
    
}
function MsgOK(Id, Mensaje) {
    $(Id).addClass('informacion');
    $(Id).html(Mensaje);
    if (jqbyaQuitar) {
        setTimeout(function () {
            $(Id).removeClass('informacion', 1500);
        }, 500);
    }
    
}


function fillCombo(IdCombo, datos) {
    $(IdCombo).children().remove().end();
    $.each(datos, function (index, item) {
        IdCombo.get(0).options[IdCombo.get(0).options.length] = new Option(item.Descripcion, item.Codigo);
    });
}

function QuitarFmtNumerico(str) {
    return Number(str.replace(/[^0-9\.]+/g, ""));
}

function pad(str, max) {
    return str.length < max ? pad("0" + str, max) : str;
}

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});