var byaSite = new Object();

var byaSite = {
    usuario: 'sirccm_user',
    vigencia: 'sirccm_vig',
    cerrarSesion: function () {
        this.setVigencia('');
        this.setUsuario('');
    },
    setUsuario: function (username) {
        $.cookie(byaSite.usuario, username);
    },
    setVigencia: function (vigencia) {
        $.cookie(byaSite.vigencia, vigencia);
    },
    getUsuario: function () {
        return $.cookie(byaSite.usuario);
    },
    getVigencia: function () {
        return $.cookie(byaSite.vigencia);
    },
    getDepDel: function () {
        return "06";
    },
    NomApp: function () {
        return "SIRCC 4 Mobile.";
    },
    PiePagina: function () {
        return " B&A Systems S.A.S. ";
    },
    CargarVigencias: function (IdCbo) {
        $.ajax({
            type: "POST",
            url: "/wsSircc.asmx/Vigencias",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var IdCombo = $(IdCbo);
                IdCombo.children().remove().end();
                $.each(response.d, function (index, item) {
                    IdCombo.get(0).options[IdCombo.get(0).options.length] = new Option(item, item);
                });
                IdCombo.val(byaSite.getVigencia());
                IdCombo.selectmenu('refresh', true);
            },
            error: function (jqXHR, status, error) {
                alert("Ajax " + error + " - " + jqXHR.responseText);
            }
        });
        $(IdCbo).change(function () {
            byaSite.setVigencia($(IdCbo).val());
        });
    }
}