var admHEstadosEP = (function () {
    var ventana = '#winRAI';
    var msgPopup = "#winConRAI";


    var urlToInsert = "wfRgEstPrev.aspx/InsertHEstadoEP";

    //Adding event listeners
    function _addEventListeners() {

        var Guardar = function () {
            var dataRecord = {};
            var byaRpta = {};
            alert("Guardar");
            dataRecord.ID_EP = admHEstadosEP.id_ep;
            dataRecord.OBS_EP = $("#txtObs").val();
            dataRecord.FEC_EP = $("#txtFec").jqxDateTimeInput('value');
            dataRecord.TIP_EP = admHEstadosEP.tipo;
            jsonData = "{'Reg':" + JSON.stringify(dataRecord) + "}";

            byaPage.POST_Sync(urlToInsert, jsonData, function (result) {
                byaRpta = byaPage.retObj(result.d);
                //Mensaje del popup
                msg = $(msgPopup); //referencia msg
                msg.html(byaRpta.Mensaje); //mostrar msg en div 
                byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
                if (!byaRpta.Error) {
                    $("#BtnRAIGuardar").jqxButton({ disabled: true });
                    $("#txtObs").jqxInput({ disabled: true });
                    $("#txtFec").jqxDateTimeInput({ disabled: true });
                }
            });
        }
        $("#BtnRAIGuardar").click(function (event) {
            Guardar();
        });
        $("#BtnRAICancelar").click(function (event) {
            alert("Cancelar");
        });
    };

    //Creating all page elements which are jqxWidgets
    function _createElements() {

        var tema = admHEstadosEP.config.theme;
        $("#winConRAI").html("Especifique Fecha de Revisión y presione Guardar..");
        $("#BtnRAIGuardar").jqxButton({ theme: tema, width: 100, height: 25 });
        $("#BtnRAICancelar").jqxButton({ theme: tema, width: 100, height: 25 });
        $("#txtFec").jqxDateTimeInput({ width: '150px', height: '25px', theme: tema, culture: 'es-CO' });
        $("#txtObs").jqxInput({ placeHolder: "Escriba observación ", height: 100, width: 300, minLength: 1, theme: tema });
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 400, width: 550,
            theme: theme
        });

    };


    return {
        disabled: null,
        tipo: null,
        id_ep: null,
        config: {
            theme: null
        },
        AbrirRevisar: function (id_ep) {
            admHEstadosEP.id_ep = id_ep;
            this.tipo = "RV";
            $(ventana).jqxWindow('open');
        }
        ,
        AbrirAprobar: function (id_ep) {
            admHEstadosEP.id_ep = id_ep;
            this.tipo = "AP";
            $(ventana).jqxWindow('open');
        },
        AbrirDesAprobar: function (id_ep) {
            admHEstadosEP.id_ep = id_ep;
            this.tipo = "DA";
            $(ventana).jqxWindow('open');
        },
        init: function () {
            this.editedRows = new Array();
            //Creating all jqxWindgets except the window
            _createElements();
            //Attaching event listeners
            _addEventListeners();
            //Adding jqxWindow
            //_createWindow();
        }
    };
} ());

$(function () {
    admHEstadosEP.config.theme = theme;
    admHEstadosEP.id_ep = wizard.TxtID.val();
    admHEstadosEP.init();
});