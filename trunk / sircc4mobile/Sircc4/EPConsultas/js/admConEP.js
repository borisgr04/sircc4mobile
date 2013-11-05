//Agregar referencias a

var admConEP = (function () {

    var grid = "#jqxgridResult";
    var msgPpal = "#msgResult";
    var docExportXLS = 'EstudioPrevios';
    var urlToGrid = "/EPConsultas/conEP.aspx/GetConsulta";
    var divBtnGrid = "#divBtnGridConEP";
    var docExportXLS = "ResultadoConsutaEP";
    //var grid = '#jqxgridResult';

    //Adding event listeners
    var _addHandlers = function () {

        //Botones Siguiente


        $("#btnBuscarFunEP").click(function () {
            ModTer.showWindow(function (ter) {
                txtNomFunEP.value = ter.NOMBRE;
                txtIdFunEP.value = ter.IDE_TER;
            });

        });


        $('#txtIdFunEP').blur(function () {
            admConEP.BuscarTercero(txtIdFunEP.value, txtNomFunEP);
        });
    };

    function _createToolBar() {
        divBtn = "#divBotonesConEP";
        tema = theme;
        ancho = 100;
        alto = 20;
        var ToolElab = byaPage.container();
        var filtrarButton = byaPage.idButton("/jqwidgets/images/filter.png", "Filtrar");
        ToolElab.append(filtrarButton);
        $(divBtn).append(ToolElab);
        filtrarButton.jqxButton({ theme: tema, width: ancho, height: alto });
        filtrarButton.click(function (event) {
            _createGrid();
        });

        //        var xlsButton = byaPage.xlsButton();
        //        $(divBtn).append(xlsButton);
        //        xlsButton.jqxButton({ theme: tema, width: ancho, height: alto });
        //        xlsButton.click(function (event) {
        //            alert("Hola");
        //            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        //        });


    }

    function _createElements() {
        tema = admConEP.config.theme;
        $("#txtNroEP").jqxInput({ placeHolder: "Número EP ", height: 25, width: 100, minLength: 1, theme: tema });
        $("#txtIdFunEP").jqxInput({ placeHolder: "id del Funcionario ", height: 25, width: 100, minLength: 1, theme: tema });
        $("#txtFecEla").jqxDateTimeInput({ min: new Date(2012, 0, 1), max: new Date(2012, 11, 31), value: new Date(2000, 0, 1), width: '250px', height: '25px', theme: tema, selectionMode: 'range', culture: 'es-CO' });
        //$("#txtFecEla").jqxDateTimeInput({ width: '250px', height: '25px', theme: tema, selectionMode: 'range', culture: 'es-CO' });
        $("#txtValIniEP").jqxNumberInput({ width: '150px', height: '25px', symbol: '$', theme: tema, spinButtons: true });
        $("#txtValFinEP").jqxNumberInput({ width: '150px', height: '25px', symbol: '$', theme: tema, spinButtons: true });
        $("#jqxExpander").css('visibility', 'visible');
        $("#jqxExpander").jqxExpander({ expanded: false, width: '100%', theme: theme });
        $("#txtObjEP").jqxInput({ placeHolder: "Escriba la(s) palabras que Busca en el Objeto ", height: 25, width: 400, minLength: 1, theme: tema });


        //ActualizarFechas();

        $('#CmbVigEP').on('change', function (event) {
            var vig = 0;
            vig = $('#CmbVigEP').val();
            var date1 = new Date();
            date1.setFullYear(vig, 0, 1);
            var date2 = new Date();
            date2.setFullYear(vig, 0, 30);
            $("#txtFecEla").jqxDateTimeInput('setRange', date1, date2);
            $("#txtFecEla").jqxDateTimeInput({ min: new Date(vig, 0, 1), max: new Date(vig, 11, 31) });
        });



        $('#jqxExpander').on('collapsed', function () {
            $('.txtInp').val("");
            $('.cmbo').jqxDropDownList({ selectedIndex: -1 });
            var vig = 0;
            vig = $('#CmbVigEP').val();
            $('#cmbSTipConEP').jqxDropDownList({ selectedIndex: -1 });
            $('#txtValIniEP').val("");
            $('#txtValFinEP').val("");
        });

        var sourceMod = byaPage.getSource('/EPConsultas/conEP.aspx/GetvModalidad');
        $("#cmbModconEP").jqxDropDownList({
            source: sourceMod, placeHolder: 'Seleccione Modalidad', displayMember: "NOM_TPROC", valueMember: "COD_TPROC", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        var sourceEst = byaPage.getSource('/EPConsultas/conEP.aspx/GetvEP_ESTADOS');
        $("#CmbEstEP").jqxDropDownList({
            source: sourceEst, placeHolder: 'Seleccione el Estado', displayMember: "COD_EST", valueMember: "NOM_EST", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        var sourceVig = byaPage.getSource('/EPConsultas/conEP.aspx/GetvVIGENCIAS');
        $("#CmbVigEP").jqxDropDownList({
            source: sourceVig, placeHolder: 'Seleccione la Vigencia', selectedIndex: 0,
            displayMember: "YEAR_VIG", valueMember: "YEAR_VIG", dropDownWidth: '300', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });


        var sourceDep = byaPage.getSource('/EPConsultas/conEP.aspx/GetvDEPENDENCIAT');
        $("#cmbDepEP").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        var sourceTip = byaPage.getSource('/EPConsultas/conEP.aspx/GetvTIPOSCONT');
        //alert(byaPage.msgJson(sourceTip));
        $("#cmbTipConEP").jqxDropDownList({
            source: sourceTip, placeHolder: 'Seleccione Tipo de Contrato',
            displayMember: "NOMC_TIP", valueMember: "COD_TIP", dropDownWidth: '300', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        admConEP.RefreshCboSubTip();



        var vig = 0;
        vig = $('#CmbVigEP').val();
        var date1 = new Date();
        date1.setFullYear(vig, 0, 1);
        var date2 = new Date();
        date2.setFullYear(vig, 0, 30);
        $("#txtFecEla").jqxDateTimeInput('setRange', date1, date2);
        $("#txtFecEla").jqxDateTimeInput({ min: new Date(vig, 0, 1), max: new Date(vig, 11, 31) });



        $("#cmbTipConEP").on('select', function (event) {
            admConEP.RefreshCboSubTip();
        });

        //Datos Locales
        _createToolBar();
    }

    function NVL(val, defaultt) {
        return val ? val : defaultt;
    }

    //crea GridTipos
    function _createGrid() {

        var filtro = {};
        id = $("#txtNroEP").val();
        filtro.ID = id ? id : 0;
        //        var selection = $("#txtFecEla").jqxDateTimeInput('getRange');
        //        filtro.FEC_ELA_EP_INI = byaPage.converJSONDate(selection.from.toLocaleDateString());
        //        filtro.FEC_ELA_EP_FIN = byaPage.converJSONDate(selection.to.toLocaleDateString());
        //        alert($("#txtValIniEP").val());
        //        var NumProy = $("#txtNumProEp").val();
        //        filtro.COD_PRO = NumProy ? NumProy : 0;

        filtro.tipo = NVL(pag.tipo, "");
        if (filtro.tipo != "") {
            alert("Hola");
            $("#CmbEstEP").jqxDropDownList({ disabled: false });
        }
        alert(filtro.tipo);
        filtro.IDE_DIL_EP = NVL($("#txtIdFunEP").val(), "");
        filtro.STIP_CON_EP = NVL($("#cmbSTipConEP").val(), "");
        filtro.DEP_NEC_EP = NVL($("#cmbDepEP").val(), "");
        filtro.VIG_EP = NVL($("#CmbVigEP").val(), "");
        filtro.EST_EP = NVL($("#CmbEstEP").val(), "");
        filtro.OBJE_EP = NVL($("#txtObjEP").val(), "");
        filtro.VAL_ENT_EP_INI = NVL($("#txtValIniEP").val(), 0);
        filtro.VAL_ENT_EP_FIN = NVL($("#txtValFinEP").val(), 0);
        if ($('#txtValIniEP').val() > $('#txtValFinEP').val()) {
            alert("Verifique el filtro por Valores, el Valor Inicial debe ser menor que el final");
            return;
        }

        //        



        jsonData = "{'filtro':" + JSON.stringify(filtro) + "}";


        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'ID' },
                    { name: 'NECE_EP' },
                    { name: 'OBJE_EP' },
                    { name: 'DESC_EP' },
                    { name: 'PLAZ1_EP' },
                    { name: 'PLA1_EP' },
                    { name: 'PLAZ2_EP' },
                    { name: 'TPLA2_EP' },
                    { name: 'LUGE_EP' },
                    { name: 'PLIQ_EP' },
                    { name: 'FJUR_EP' },
                    { name: 'VAL_ENT_EP' },
                    { name: 'VAL_OTR_EP' },
                    { name: 'JFAC_SEL_EP' },
                    { name: 'CAP_FIN_EP' },
                    { name: 'CON_EXP_EP' },
                    { name: 'CAP_RES_EP' },
                    { name: 'FAC_ESC_EP' },
                    { name: 'ANA_EXI_EP' },
                    { name: 'IDE_DIL_EP' },
                    { name: 'CAR_DIL_EP' },
                    { name: 'DEP_NEC_EP' },
                    { name: 'STIP_CON_EP' },
                    { name: 'FEC_ELA_EP' },
                    { name: 'FEC_REG_EP' },
                    { name: 'FEC_MOD_EP' },
                    { name: 'USAP_REG_EP' },
                    { name: 'USAP_MOD_EP' },
                    { name: 'FEC_REV_EP' },
                    { name: 'USAP_REV_EP' },
                    { name: 'USAP_ELA_EP' },
                    { name: 'FEC_ELAS_EP' },
                    { name: 'USAP_APR_EP' },
                    { name: 'FEC_APR_EP' },
                    { name: 'USAP_ANU_EP' },
                    { name: 'FEC_ANU_EP' },
                    { name: 'USAP_DAN_EP' },
                    { name: 'FEC_DAN_EP' },
                    { name: 'DEP_SUP_EP' },
                    { name: 'CAR_SUP_EP' },
                    { name: 'VIG_EP' },
                    { name: 'IDE_APTE_EP' },
                    { name: 'CAR_APTE_EP' },
                    { name: 'CODIGO_EP' },
                    { name: 'GRUPOS_EP' },
                    { name: 'NUM_EMP_EP' }

                 ],
            async: false,
            record: 'Table',
            url: urlToGrid,
            data: { filtro: JSON.stringify(filtro) }
        };
        //,data: jsonData
        var dataAdapter = new $.jqx.dataAdapter(source,
        	            {
        	                contentType: 'application/json; charset=utf-8',
        	                loadError: function (jqXHR, status, error) {
        	                    alert(error + jqXHR.responseText);
        	                }
        	            });


        var linkrendered = function (row, column, value) {
            return '<a href="/EstPrev/wfRgEstPrev.aspx?tip=' + pag.tipo + '&nep=' + value + '"  target=_blank/>' + value + '</a>';
        };

        // initialize jqxGrid
        $(grid).jqxGrid(
            {
                width: '100%',
                source: dataAdapter,
                theme: admConEP.config.theme,
                autoheight: true,
                sortable: true,
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                enabletooltips: true,
                showstatusbar: true,
                columns: [
                  { text: 'Numero Estudio Previo', datafield: 'ID', width: 150, cellsrenderer: linkrendered },
                  { text: 'Objeto', datafield: 'OBJE_EP', width: 150 },
                  { text: 'Plazo', datafield: 'PLAZO', width: 150 },
                  { text: 'Lugar de Ejecución', datafield: 'LUGE_EP', width: 150 },
                  { text: 'Plazo de Liquidación', datafield: 'PLIQ_EP', width: 150 },
                  { text: 'Valor Aportes Entidad', datafield: 'VAL_ENT_EP', width: 150 },
                  { text: 'Valor Otros Aportes', datafield: 'VAL_OTR_EP', width: 150 },
                  { text: 'Funcionario que Diligencia', datafield: 'IDE_DIL_EP', width: 150 },
                  { text: 'Cargo del Funcionario', datafield: 'CAR_DIL_EP', width: 150 },
                  { text: 'Dependencia Necesidad', datafield: 'DEP_NEC_EP', width: 150 },
                  { text: 'Subtipo de Contrato', datafield: 'STIP_CON_EP', width: 150 },
                  { text: 'Fecha de Elaboración', datafield: 'FEC_ELA_EP', width: 150 },
                  { text: 'Fecha de Revisión', datafield: 'FEC_REV_EP', width: 150 },
                  { text: 'Usuario que Revisó', datafield: 'USAP_REV_EP', width: 150 },
                  { text: 'Usuario que Aprobó', datafield: 'USAP_APR_EP', width: 150 },
                  { text: 'Fecha de Aprobación', datafield: 'FEC_APR_EP', width: 150 },
                  { text: 'Usuario que Anula', datafield: 'USAP_ANU_EP', width: 150 },
                  { text: 'Fecha de Anulación', datafield: 'FEC_ANU_EP', width: 150 },
                  { text: 'Usuario que Desanula', datafield: 'USAP_DAN_EP', width: 150 },
                  { text: 'Fecha de Desanulación', datafield: 'FEC_DAN_EP', width: 150 },
                  { text: 'Dependencia que Supervisa', datafield: 'DEP_SUP_EP', width: 150 },
                  { text: 'Cargo del Supervisor', datafield: 'CAR_SUP_EP', width: 150 },
                  { text: 'Vigencia', datafield: 'VIG_EP', width: 150 },
                  { text: 'Apoyo Tecnico', datafield: 'IDE_APTE_EP', width: 150 },
                  { text: 'Cargo Apoyo Tecnico', datafield: 'CAR_APTE_EP', width: 150 },
                  { text: 'Codigo del EStudio previo', datafield: 'CODIGO_EP', width: 150 },
                  { text: 'Grupo de Estudio Previo', datafield: 'GRUPOS_EP', width: 150 },

                ]
            });
    }

    return {
        disabled: null,
        id_ep: null,
        editedRows: null,
        config: {
            dragArea: null,
            theme: null
        },


        ActualizarFechas: function () {
            var vig = 0;
            vig = $('#CmbVigEP').val();
            var date1 = new Date();
            date1.setFullYear(vig, 0, 1);
            var date2 = new Date();
            date2.setFullYear(vig, 0, 30);
            $("#txtFecEla").jqxDateTimeInput('setRange', date1, date2);
            $("#txtFecEla").jqxDateTimeInput({ min: new Date(vig, 0, 1), max: new Date(vig, 11, 31) });

        },
        BuscarTercero: function (ide_ter, txtNom) {
            var dato = "{ide_ter:'" + ide_ter + "'}";
            if (ide_ter != "") {

                $.ajax({
                    type: "POST",
                    url: "/EPConsultas/ConEP.aspx/GetTercerosPk",
                    data: "{ide_ter:'" + ide_ter + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        if (response.d == "0") {
                            alert("Tercero no Encontrado");
                            txtNom.value = "";
                        } else {
                            txtNom.value = response.d;
                        }

                    },
                    error: byaPage.ajaxError
                });
            }

        },

        RefreshCboSubTip: function () {
            var cod_tip = $("#cmbTipConEP").val();
            var source = byaPage.getSource('/EPConsultas/conEP.aspx/GetvSUBTIPOS', { cod_tip: "'" + cod_tip + "'" });
            $("#cmbSTipConEP").jqxDropDownList({
                source: source, placeHolder: 'Seleccione SubTipo', displayMember: "NOMC_STIP", valueMember: "COD_STIP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25,
                theme: tema
            });

        },


        init: function () {
            _createElements();
            _addHandlers();
            //Adding jqxWindow

        }
    };
} ());

function pagActual() {
    this.tipo = 'EL';  //$.getUrlVar('tip');
}

var pag = new pagActual();
$(function () {
    //alert(pag.tipo);
    admConEP.disabled = true;
    admConEP.config.theme = theme;
    admConEP.init();
});
