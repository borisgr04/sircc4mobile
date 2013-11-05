var theme;
var wizard = (function () {
    //Adding event listeners
    var _addHandlers = function () {

        //Botones Siguiente
        $('.nextButton').click(function () {
            wizard.validar();
            $('#jqxTabs').jqxTabs('next');
        });
        //Manejo de Modal Popup
        $("#btnBuscarC").click(function () {
            ModTer.showWindow(function (ter) {
                $("#TxtNomFun").val(ter.NOMBRE);
                $("#TxtIdeFun").val(ter.IDE_TER);
            });
        });

        $("#btnBuscarR").click(function () {

            ModTer.showWindow(function (ter) {
                $("#TxtNomRes").val(ter.NOMBRE);
                $("#TxtIdeRes").val(ter.IDE_TER);
            });

        });

        $("#btnBuscarApoTec").click(function () {

            ModTer.showWindow(function (ter) {
                $("#TxtNomApoTec").val(ter.NOMBRE);
                $("#TxtIdeApoTec").val(ter.IDE_TER);
            });

        });

        $('#CboTPlazo1').on('select', function (event) {
            var args = event.args;
            var item = $('#CboTPlazo1').jqxDropDownList('getItem', args.index);
            if (item != null) {
                wizard.RefreshCboTPlazo2($('#CboTPlazo1').val());
            }
        });


        //Al seleccionar
        $('#CboTip').on('select', function (event) {
            wizard.RefreshCboSubTip();
        });


        $('#TxtIdeFun').on('change', function () {
            wizard.BuscarTercero(TxtIdeFun, TxtNomFun);
        });

        $('#TxtIdeRes').on('change', function () {
            wizard.BuscarTercero(TxtIdeRes, TxtNomRes);
        });

        $('#TxtIdeApoTec').on('change', function () {
            wizard.BuscarTercero(TxtIdeApoTec, TxtNomApoTec);
        });

        $('#jqxTabs').on('selected', function (event) {
            var pageIndex = event.args.item + 1;

            wizard.loadPageTabs(pageIndex);
        });

        $('#txtValTot').on('change', function () {
            $("#txtValProp").jqxNumberInput({ max: $('#txtValTot').val() });
            $('#txtValProp').val($('#txtValTot').val());
            $('#txtValOtros').val(0);

        });
        $('#txtValProp').on('change', function () {
            var tot = $('#txtValTot').val();
            var prop = $('#txtValProp').val();
            if (prop <= tot) {
                $('#txtValOtros').val(tot - prop)
            }
            else {
                $('#txtValProp').val(0);
            }
        });

    };

    _createToolTips = function () {
        /*var tema = wizard.config.theme;
        alert("crea tool");
        $("#lbGrupo").jqxTooltip({ content: '<b>Grupo:</b> <i>Especificar Cantidad de Grupos del Contrato.</i><br /><b>Grupo:</b> 0, Indica que no se realizará el contrato por grupos', position: 'mouse', name: 'movieTooltip', theme: tema });
        $("#lbGrupo").jqxTooltip({ position: 'top', content: 'This is a jqxButton.', theme: tema });
        $("#lbGrupo").append("Juepa");
        */
    };

    _iniElements = function () {

        //Inicializar Usuario
        var user = wizard.getUser();
        $("#HeadLoginName").html(user);

        //$("#TxtIdeFun").val(user);
        //wizard.BuscarTercero(TxtIdeFun, TxtNomFun);
        wizard.RefreshCboSubTip("00");
        wizard.RefreshCboTPlazo2("M");
    };

    _createElements = function () {
        //Crea Tabs y Botones de Control
        tema = wizard.config.theme;
        $('#jqxTabs').jqxTabs({ height: 1000, width: '100%', theme: wizard.config.theme, keyboardNavigation: false, scrollPosition: 'both' });
        $('#nextButton1').jqxButton({ width: 80, theme: tema });
        $('#nextButton2').jqxButton({ width: 80, theme: tema });
        $('#nextButton3').jqxButton({ width: 80, theme: tema });
        //$('#nextButton4').jqxButton({ width: 80, theme: wizard.config.theme });
        //$('#nextButton5').jqxButton({ width: 80, theme: wizard.config.theme });
        $('#nextButton6').jqxButton({ width: 80, theme: tema });
        $('#nextButton7').jqxButton({ width: 80, theme: tema });
        $('#nextButton8').jqxButton({ width: 80, theme: tema });
        $('#nextButton9').jqxButton({ width: 80, theme: tema });

        //Crea Barra de Herramientas
        $('#BtnGuardar').jqxButton({ width: 80, theme: tema });
        $('#BtnAbrir').jqxButton({ width: 80, theme: tema });
        //, source: countries, Listado de elementos

        $("#TxtFecElab").jqxDateTimeInput({ width: '150px', height: '25px', theme: tema, culture: 'es-CO' });

        $("#TxtIdeFun").jqxNumberInput({ decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', theme: tema });
        $("#TxtNomFun").jqxInput({ height: 25, width: 300, minLength: 1, theme: tema });

        $("#TxtIdeRes").jqxNumberInput({ decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', theme: tema });
        $("#TxtNomRes").jqxInput({ height: 25, width: 300, minLength: 1, theme: tema });

        $("#TxtIdeApoTec").jqxNumberInput({ decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', theme: tema });
        $("#TxtNomApoTec").jqxInput({ height: 25, width: 300, minLength: 1, theme: tema });
        $("#TxtGrupos").jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
        $("#TxtNEmpleos").jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
        $("#TxtPlazoLiq").jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
        $("#txtValTot").jqxNumberInput({ width: '200px', height: '25px', decimalDigits: 2, min: 0, max: 9999999999999, digits: 20, symbol: '$', theme: tema });
        $("#txtValProp").jqxNumberInput({ width: '200px', height: '25px', decimalDigits: 2, min: 0, max: 9999999999999, digits: 20, symbol: '$', theme: tema });
        $("#txtValOtros").jqxNumberInput({ width: '200px', height: '25px', decimalDigits: 2, min: 0, max: 9999999999999, digits: 20, symbol: '$', theme: tema });

        var sourceEst = byaPage.getSource('wfRgEstPrev.aspx/GetvEP_ESTADOS');
        $("#CboEstEP").jqxDropDownList({
            disabled: true, selectedIndex: 0, source: sourceEst, displayMember: "NOM_EST", valueMember: "COD_EST", dropDownWidth: '200', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        var sourceEst = byaPage.getSource('wfRgEstPrev.aspx/GetvVIGENCIAS');
        $("#CboVig").jqxDropDownList({
            disabled: true, selectedIndex: 0, source: sourceEst, displayMember: "YEAR_VIG", valueMember: "YEAR_VIG", dropDownWidth: '200', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        var sourceCargo = byaPage.getSource('wfRgEstPrev.aspx/GetvEP_CARGO');
        $("#CboCarDilJq").jqxDropDownList({

            source: sourceCargo, placeHolder: 'Seleccione su Cargo :', displayMember: "DES_CARGO", valueMember: "COD_CARGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        $("#CboCarRes").jqxDropDownList({
            source: sourceCargo, placeHolder: 'Seleccione Cargo del Responsable:', displayMember: "DES_CARGO", valueMember: "COD_CARGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        $("#CboCarApoTec").jqxDropDownList({
            source: sourceCargo, placeHolder: 'Seleccione Cargo del Apoyo Técnico: ', displayMember: "DES_CARGO", valueMember: "COD_CARGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        $("#CboCarSup").jqxDropDownList({
            source: sourceCargo, placeHolder: 'Seleccione Cargo del Supervisor:', displayMember: "DES_CARGO", valueMember: "COD_CARGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        var sourceDep = byaPage.getSource('wfRgEstPrev.aspx/GetvDEPENDENCIAT');
        $("#CboDepSup").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });
        var sourceDep = byaPage.getSource('wfRgEstPrev.aspx/GetvDEPENDENCIA');
        $("#CboDepSol").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependencia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });
        var sourceDep = byaPage.getSource('wfRgEstPrev.aspx/GetvDEPENDENCIAD');
        $("#CboDepDel").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });
        var sourceTip = byaPage.getSource('wfRgEstPrev.aspx/GetvTIPOSCONT');
        $("#CboTip").jqxDropDownList({
            source: sourceTip, placeHolder: 'Seleccione Tipo de Contrato:', displayMember: "NOMC_TIP", valueMember: "COD_TIP", dropDownWidth: '300', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        var sourceTip = byaPage.getSource('wfRgEstPrev.aspx/GetvTIPO_PLAZOS');
        $("#CboTPlazo1").jqxDropDownList({
            selectedIndex: 1, source: sourceTip, placeHolder: 'Seleccione:', displayMember: "NOM_PLA", valueMember: "COD_TPLA", dropDownWidth: '100', dropDownHorizontalAlignment: 'left', width: 100, height: 25, theme: tema
        });

        var sourceMod = byaPage.getSource('wfRgEstPrev.aspx/GetvModalidad');
        $("#CboMod").jqxDropDownList({
            selectedIndex: 1, source: sourceMod, placeHolder: 'Seleccione:', displayMember: "NOM_TPROC", valueMember: "COD_TPROC", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        $("#TxtPlazo1").jqxNumberInput({ min: 1, decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
        $("#TxtPlazo2").jqxNumberInput({ min: 0, decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });


    };


    return {
        disabled: true,
        TxtID: null,
        config: {
            dragArea: null,
            theme: null,
            oper: null
        },

        getUser: function () {
            return byaSite.getUsuario();
        }
        ,
        RefreshCboTPlazo2: function (cod) {

            var source = byaPage.getSource('wfRgEstPrev.aspx/GetvTIPO_PLAZOS2', { cod_tpla: "'" + cod + "'" });

            $("#CboTPlazo2").jqxDropDownList({
                source: source, placeHolder: 'Seleccione:', displayMember: "NOM_PLA", valueMember: "COD_TPLA", dropDownWidth: '100', dropDownHorizontalAlignment: 'left', width: 100, height: 25, theme: tema
            });
        },
        loadPageTabs: function (tabIndex) {
            //content11: "admFPago.htm",
            var pagTab = {
                content4: "admProy.htm",
                content5: "admEspTec.htm",
                content7: "admObligC.htm",
                content8: "admObligE.htm",
                content10: "admCDP.htm",

                content13: "admCapJur.htm",
                content15: "admPol.htm",
                content16: "admMun.htm"
            };

            var pajAjax = pagTab['content' + tabIndex];

            if (typeof (pajAjax) === "undefined") {

            } else {
                $.get(pajAjax, function (data) {
                    $('#content' + tabIndex).html(data);
                });
            }
        }
        ,
        ValidarEL: function () {
            $('#FrmEstPrev').jqxValidator('validate');
        },
        _createValidacionEL: function (fnOk) {
            $('#FrmEstPrev').jqxValidator(
            {
                onSuccess: fnOk,
                onError: function () { $(msgPpal).html('Los datos no son válidos!!!'); },
                rules: [
                    { input: '#TxtFecElab', message: 'Fecha de Elaboración requerida 1/1/1900 and 1/1/2013.', action: 'valuechanged', rule: function (input, commit) {
                        var date = $('#TxtFecElab').jqxDateTimeInput('value');
                        var result = date.getFullYear() >= 1900 && date.getFullYear() <= 2013;
                        // call commit with false, when you are doing server validation and you want to display a validation error on this field. 
                        return result;
                    }
                    },
                    { input: '#TxtIdeFun', message: 'Debe especificar un responsable del Estudio Previo!', action: 'keyup, blur', rule: 'length=3,12' },
					{ input: '#CboTip', message: 'Debe Identificar el tipo de contrato', action: 'select', rule: function (input) {
					    var val = $("#CboTip").jqxDropDownList('val');
					    return (val != "");
					}
					}
                    ]
            });
        },
        BuscarTercero: function (txtIde, txtNom) {
            var ide_ter = $(txtIde).val();
            if (ide_ter != "") {

                $.ajax({
                    type: "POST",
                    url: "wfRgEstPrev.aspx/GetTercerosPk",
                    data: "{ide_ter:'" + ide_ter + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "0") {
                            //alert("Tercero no Encontrado");
                            $(txtIde).val(0);
                            $(txtNom).val("");
                        } else {
                            $(txtNom).val(response.d);
                        }

                    },
                    error: function (response) {
                        if (response.length != 0)
                            alert('Error:' + response);
                    }
                });
            }

        },
        GetID: function () {
            return $(wizard.TxtID).val();
        },
        SetID: function (value) {
            $(wizard.TxtID).val(value);
        },
        GetGrupos: function () {
            return $("#TxtGrupos").val();
        },
        GetValProp: function () {
            return $("#txtValProp").val();
        },
        GetValOtros: function () {
            return $("#txtValOtros").val();
        },

        //Initializing the wizzard - creating all elements, adding event handlers and starting the validation
        RefreshCboSubTip: function () {
            var cod_tip = $('#CboTip').val();
            var source = byaPage.getSource('wfRgEstPrev.aspx/GetvSUBTIPOS', { cod_tip: "'" + cod_tip + "'" });
            $("#CboSubTip").jqxDropDownList({
                source: source, placeHolder: 'Seleccione Sub Tipo:', displayMember: "NOMC_STIP", valueMember: "COD_STIP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
            });
        },
        init: function () {

            this.TxtID = $("<div style='float: left; margin-left: 5px;' id='TxtID'></div>");

            _createElements();
            _createToolTips();
            _iniElements();

            //$('#backButtonReview').jqxButton({ width: 50, theme: theme });
            _addHandlers();

            //this.validate();
            this.Deshabilitar();

        },
        validar: function () {
            alert("Debe Guardar los Datos Iniciales del Estudio Previo para continuar....");
        },
        Deshabilitar: function () {
            $('#jqxTabs').jqxTabs({ disabled: true });
            //Deshabilita todos los controles
            $('.formC :input').attr('disabled', true);

            $("#CboCarDilJq").jqxDropDownList({ disabled: true });
            $("#CboCarRes").jqxDropDownList({ disabled: true });
            $("#CboCarApoTec").jqxDropDownList({ disabled: true });
            $("#CboCarSup").jqxDropDownList({ disabled: true });
            $("#CboDepSup").jqxDropDownList({ disabled: true });
            $("#CboDepSol").jqxDropDownList({ disabled: true });
            $("#CboTip").jqxDropDownList({ disabled: true });
            $("#CboSubTip").jqxDropDownList({ disabled: true });
            $("#TxtFecElab").jqxDateTimeInput({ disabled: true });
            $("#CboMod").jqxDropDownList({ disabled: true });
            $('#txtValOtros').attr('disabled', true);
            $("#CboTPlazo1").jqxDropDownList({ disabled: true });
            $("#CboTPlazo2").jqxDropDownList({ disabled: true });

            $("#TxtGrupos").jqxNumberInput({ disabled: true });
            $("#TxtNEmpleos").jqxNumberInput({ disabled: true });

            $("#CboDepDel").jqxDropDownList({ disabled: true });



            this.disabled = true;
        },
        Limpiar: function () {
            //Colocar Valores Por Defecto
            //$('#FrmEstPrev')[0].reset();
            //$('#FrmEstPrev :input').attr('value', '');


            $("#CboCarDilJq").jqxDropDownList({ selectedIndex: -1 });
            $("#CboCarRes").jqxDropDownList({ selectedIndex: -1 });
            $("#CboCarApoTec").jqxDropDownList({ selectedIndex: -1 });
            $("#CboCarSup").jqxDropDownList({ selectedIndex: -1 });
            $("#CboDepSup").jqxDropDownList({ selectedIndex: -1 });
            $("#CboDepSol").jqxDropDownList({ selectedIndex: -1 });
            $("#CboTip").jqxDropDownList({ selectedIndex: -1 });
            $("#CboSubTip").jqxDropDownList({ selectedIndex: -1 });
            $("#CboMod").jqxDropDownList({ selectedIndex: -1 });

            $("#CboTPlazo1").jqxDropDownList({ selectedIndex: -1 });
            $("#CboTPlazo2").jqxDropDownList({ selectedIndex: -1 });

            $("#CboDepDel").jqxDropDownList({ selectedIndex: -1 });
            byaPage.msgLimpiar($("#LbMsg"));

            //_iniElements();
            //this.BuscarTercero(TxtIdeFun, TxtIdeNom);


        },
        HabilitarN: function () {
            var Tabs = $('#jqxTabs');
            Tabs.jqxTabs('enableAt', 0);
            Tabs.jqxTabs('disableAt', 1);
            Tabs.jqxTabs('disableAt', 2);
            Tabs.jqxTabs('disableAt', 3);
            Tabs.jqxTabs('disableAt', 4);
            Tabs.jqxTabs('disableAt', 5);
            Tabs.jqxTabs('disableAt', 6);
            Tabs.jqxTabs('disableAt', 7);
            Tabs.jqxTabs('disableAt', 8);
            Tabs.jqxTabs('disableAt', 9);
            Tabs.jqxTabs('disableAt', 10);
            Tabs.jqxTabs('disableAt', 11);
            Tabs.jqxTabs('disableAt', 12);
            Tabs.jqxTabs('disableAt', 13);
            Tabs.jqxTabs('disableAt', 14);

            $('.formC :input').attr('disabled', false);

            $('#TxtNomApoTec').attr('disabled', true);
            $('#TxtNomFun').attr('disabled', true);
            $('#TxtIdeFun').attr('disabled', true);

            $('#TxtNomRes').attr('disabled', true);
            $('#txtValOtros').attr('disabled', true);

            $("#CboCarDilJq").jqxDropDownList({ disabled: false });
            $("#CboCarRes").jqxDropDownList({ disabled: false });
            $("#CboCarApoTec").jqxDropDownList({ disabled: false });
            $("#CboCarSup").jqxDropDownList({ disabled: false });
            $("#CboDepSup").jqxDropDownList({ disabled: false });
            $("#CboDepSol").jqxDropDownList({ disabled: false });
            $("#CboTip").jqxDropDownList({ disabled: false });
            $("#CboSubTip").jqxDropDownList({ disabled: false });
            $("#TxtFecElab").jqxDateTimeInput({ disabled: false });
            $("#CboMod").jqxDropDownList({ disabled: false });
            $("#TxtGrupos").jqxNumberInput({ disabled: false });
            $("#TxtNEmpleos").jqxNumberInput({ disabled: false });



            $("#CboTPlazo1").jqxDropDownList({ disabled: false });
            $("#CboTPlazo2").jqxDropDownList({ disabled: false });
            $("#TxtPlazo1").jqxNumberInput({ disabled: false });
            $("#TxtPlazo2").jqxNumberInput({ disabled: false });

            $("#CboDepDel").jqxDropDownList({ disabled: false });


        },
        HabilitarE: function () {
            var Tabs = $('#jqxTabs');
            Tabs.jqxTabs('enableAt', 0);
            Tabs.jqxTabs('enableAt', 1);
            Tabs.jqxTabs('enableAt', 2);
            Tabs.jqxTabs('enableAt', 3);
            Tabs.jqxTabs('enableAt', 4);
            Tabs.jqxTabs('enableAt', 5);
            Tabs.jqxTabs('enableAt', 6);
            Tabs.jqxTabs('enableAt', 7);
            Tabs.jqxTabs('enableAt', 8);
            Tabs.jqxTabs('enableAt', 9);
            Tabs.jqxTabs('enableAt', 10);
            Tabs.jqxTabs('enableAt', 11);
            Tabs.jqxTabs('enableAt', 12);
            Tabs.jqxTabs('enableAt', 13);
            Tabs.jqxTabs('enableAt', 14);

            $('.formC :input').attr('disabled', false);

            $('#TxtNomApoTec').attr('disabled', true);
            $('#TxtNomFun').attr('disabled', true);
            $('#TxtIdeFun').attr('disabled', true);
            $('#TxtNomRes').attr('disabled', true);
            $('#txtValOtros').attr('disabled', true);

            $("#CboCarDilJq").jqxDropDownList({ disabled: false });
            $("#CboCarRes").jqxDropDownList({ disabled: false });
            $("#CboCarApoTec").jqxDropDownList({ disabled: false });
            $("#CboCarSup").jqxDropDownList({ disabled: false });
            $("#CboDepSup").jqxDropDownList({ disabled: false });
            $("#CboDepSol").jqxDropDownList({ disabled: false });
            $("#CboTip").jqxDropDownList({ disabled: false });
            $("#CboSubTip").jqxDropDownList({ disabled: false });
            $("#TxtFecElab").jqxDateTimeInput({ disabled: false });
            $("#CboMod").jqxDropDownList({ disabled: false });
            $("#TxtGrupos").jqxNumberInput({ disabled: false });
            $("#TxtNEmpleos").jqxNumberInput({ disabled: false });

            $("#CboTPlazo1").jqxDropDownList({ disabled: false });
            $("#CboTPlazo2").jqxDropDownList({ disabled: false });

            $("#TxtPlazo1").jqxNumberInput({ disabled: false });
            $("#TxtPlazo2").jqxNumberInput({ disabled: false });

            $("#CboDepDel").jqxDropDownList({ disabled: false });



            this.disabled = false;

            wizard.loadPageTabs($("#jqxTabs").jqxTabs('val') + 1);
        },
        imprimir: function () {
            byaSite.AbrirPagina("../ashx/descEP.ashx?id_ep=" + wizard.TxtID.val());
        }
		,
        Nuevo: function (GuardarNuevo) {
            wizard.config.oper = 'nuevo';
            wizard.TxtID.jqxNumberInput({ disabled: true });
            wizard.HabilitarN(); //Habilitar para nuevo
            wizard.Limpiar(); //Limpiar los input
            wizard._createValidacionEL(GuardarNuevo); //Configurar el Validador
            wizard.RefreshCboSubTip("00");
            wizard.RefreshCboTPlazo2("M");
            //Colocar en Valores Por Defecto
            var user = wizard.getUser();

            $("#TxtIdeFun").val(user);

            wizard.BuscarTercero(TxtIdeFun, TxtNomFun);



        },
        AbrirEP: function () {

            var sw = false;
            if (wizard.GetID() == "") {
                $("#LbMsg").html("Por favor Digite un Número de Estudio Previo...!!!");
                wizard.TxtID.focus();
                wizard.disabled = true;
                return false;
            }
            var parametrosJSON = {
                "id_ep": wizard.GetID(),
                "tipo": pag.tipo
            };
            //byaPage.msgJson(parametrosJSON);
            $.ajax({
                type: "POST",
                url: "wfRgEstPrev.aspx/GetEstPrev",
                data: byaPage.JSONtoString(parametrosJSON),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                beforeSend: function () {
                    $("#LbMsg").html("Procesando.. espere por favor...!!!");
                },
                success: function (result) {
                    var ep = byaPage.retObj(result.d);

                    if (ep.ID == 0) {
                        $("#LbMsg").html("Estudio Previo N° " + wizard.GetID() + " no encontrado...!!!");
                        byaPage.msgResult($("#LbMsg"));
                    }
                    else {

                        $('#jqxTabs').jqxTabs({ disabled: false });
                        //Deshabilita todos los controles
                        $('.formC :input').attr('disabled', true);
                        TxtDesNec.value = ep.NECE_EP;
                        TxtObjCon.value = ep.OBJE_EP;
                        TxtDesObj.value = ep.DESC_EP;
                        TxtPlazo1.value = ep.PLAZ1_EP;
                        TxtPlazo2.value = ep.PLAZ2_EP;
                        TxtLugar.value = ep.LUGE_EP;
                        TxtPlazoLiq.value = ep.PLIQ_EP;
                        TxtFundJur.value = ep.FJUR_EP;
                        TxtJFacSel.value = ep.JFAC_SEL_EP;
                        TxtFacEsc.value = ep.FAC_ESC_EP;
                        TxtCapFin.value = ep.CAP_FIN_EP;
                        TxtCapRes.value = ep.CAP_RES_EP;
                        //TxtAnaExig.Text = ep.ANA_EXI_EP; era la poliza
                        TxtCodExp.value = ep.CON_EXP_EP;
                        $("#CboEstEP").val(ep.EST_EP);

                        $("#TxtFecElab").val(byaPage.parseJsonDate(ep.FEC_ELA_EP));
                        //Quien Diligencia
                        $("#TxtIdeFun").val(ep.IDE_DIL_EP);
                        wizard.BuscarTercero(TxtIdeFun, TxtNomFun);
                        $("#CboCarDilJq").val(ep.CAR_DIL_EP);
                        $("#TxtIdeRes").val(ep.IDE_RES_EP);
                        wizard.BuscarTercero(TxtIdeRes, TxtNomRes);
                        //CboCarDil.SelectedValue = ep.CAR_DIL_EP;
                        $("#TxtIdeApoTec").val(ep.IDE_APTE_EP);
                        wizard.BuscarTercero(TxtIdeApoTec, TxtNomApoTec);
                        $("#CboCarApoTec").val(ep.CAR_APTE_EP);

                        //$("#CboCarRes").val(ep.CAR_DIL_EP);
                        $("#CboCarSup").val(ep.CAR_SUP_EP);
                        $("#CboDepSup").val(ep.DEP_SUP_EP);

                        $("#CboDepSol").val(ep.DEP_NEC_EP);
                        $("#CboTip").val(ep.TIP_CON_EP);

                        $("#CboMod").val(ep.MOD_SEL_EP);

                        $("#TxtNEmpleos").val(ep.NUM_EMP_EP);
                        $("#TxtGrupos").val(ep.GRUPOS_EP);


                        $("#TxtPlazo1").val(ep.PLAZ1_EP);
                        $("#CboTPlazo1").val(ep.TPLA1_EP);
                        $("#TxtPlazo2").val(ep.PLAZ2_EP);
                        $("#CboTPlazo2").val(ep.TPLA2_EP);

                        $("#CboDepSup").val(ep.DEP_SUP_EP);

                        wizard.RefreshCboSubTip(ep.TIP_CON_EP);
                        $("#CboDepDel").val(ep.DEP_DEL_EP);

                        $("#txtValTot").val(ep.VAL_ENT_EP + ep.VAL_OTR_EP);

                        $("#txtValProp").val(ep.VAL_ENT_EP);
                        $("#txtValOtros").val(ep.VAL_OTR_EP);


                        //Mostrar Primer Tab
                        $('#jqxTabs').jqxTabs('select', 0);


                        $("#LbMsg").html("Listo...!!!");// Si desea modificar debe presionar el boton Editar, realizar los cambios y guardar.");
                        byaPage.msgResult($("#LbMsg"));
                        sw = true;
                    }
                },
                error: function (jqXHR, status, error) {
                    alert(error + "-" + jqXHR.responseText);
                }
            });
            return sw;
        }

    };
} ());


function _createToolBar() {
    //byaSite.getUsuario();
    if (pag.tipo == "EL") {
        _createToolBarEL();
    }
    if (pag.tipo == "RV") {
        _createToolBarRV();
    }
    if (pag.tipo == "AP") {
        _createToolBarAP();
    }
	if (pag.tipo == "DA") {
        _createToolBarDA();
    }
    if (pag.tipo == "CN") {
        _createToolBarCN();
    }
    
}
//Elaboración
function _createToolBarEL() {
		divBtnGrid = "#divToolEP";

		tema = theme;
		ancho = 85;
		alto = 20;

		var ToolElab = byaPage.container();

        var nuevoButton = byaPage.idButton("../jqwidgets/images/new.png", "Nuevo");
        var abrirButton = byaPage.idButton("../jqwidgets/images/open.png", "Abrir");
        var editarButton = byaPage.idButton("../jqwidgets/images/edit.png", "Editar");
        var guardarButton = byaPage.updButton();
        var cancelarButton = byaPage.idButton("../jqwidgets/images/undo.png", "Cancelar");
        var imprimirButton = byaPage.idButton("../jqwidgets/images/print.png", "Imprimir");
        
                    
        ToolElab.append(nuevoButton);
        ToolElab.append(wizard.TxtID);
        ToolElab.append(abrirButton);
        ToolElab.append(editarButton);
        ToolElab.append(guardarButton);
        ToolElab.append(cancelarButton);
        ToolElab.append(imprimirButton);

        $(divToolElab).html(ToolElab);

        wizard.TxtID.jqxNumberInput({ decimalDigits: 0,min:0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
        nuevoButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: false });
        abrirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: false });
        editarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
        guardarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
        cancelarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
        imprimirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });

        wizard.TxtID.on('change', function (event) {
            AbrirEP();
        });
		
		
		if( pag.EP ){
		 wizard.TxtID.val(pag.EP);
		 AbrirEP();
		}

	

        editarButton.click(function (event) {
            if (!editarButton.jqxButton('disabled')) {
                Editar();
            }
        });

        nuevoButton.click(function (event) {
            if (!nuevoButton.jqxButton('disabled')) {
                nuevo();
            }
        });

        abrirButton.click(function (event) {

            if (!abrirButton.jqxButton('disabled')) {
                AbrirEP();
            }
        });

        function AbrirEP() {
            wizard.config.oper = 'abrir';
            if (wizard.AbrirEP()) {
                nuevoButton.jqxButton({ disabled: false });
                abrirButton.jqxButton({ disabled: false });
                editarButton.jqxButton({ disabled: false });
                guardarButton.jqxButton({ disabled: true });
                cancelarButton.jqxButton({ disabled: false });
                imprimirButton.jqxButton({ disabled: false });
                $("#LbMsg").html("Listo...!!! Si desea modificar debe presionar el boton Editar, realizar los cambios y presione el Botón Guardar.");
            }
        }
        function Editar() {

            nuevoButton.jqxButton({ disabled: true });
            abrirButton.jqxButton({ disabled: true });
            editarButton.jqxButton({ disabled: true });
            guardarButton.jqxButton({ disabled: false });
            cancelarButton.jqxButton({ disabled: false });
            imprimirButton.jqxButton({ disabled: false });
            wizard.TxtID.jqxNumberInput({ disabled: true });

            wizard.config.oper = 'editar';

            
            wizard.HabilitarE();
            wizard.disabled = false;
            wizard._createValidacionEL(GuardarMod);
            $("#LbMsg").html("después de modificar los datos y presione el botón guardar...!!!");
            byaPage.msgResult($("#LbMsg"));

        }

        

        guardarButton.click(function (event) {
            if (!guardarButton.jqxButton('disabled')) {
                wizard.ValidarEL();
            }
        });

        function GuardarNuevo() {

            jsonData = "{'Reg':" + JSON.stringify(datosEP()) + "}";
            urlTo = "wfRgEstPrev.aspx/insertESTPREV";
            msgPpal = "#LbMsg";

            byaPage.POST_Sync(urlTo, jsonData, function (result) {
                byaRpta = byaPage.retObj(result.d);
                //Mensaje arriba de la grid
                msg = $(msgPpal); //referencia msg
                msg.html(byaRpta.Mensaje); //mostrar msg en div 

                byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
                if (!byaRpta.Error) {
                    wizard.SetID(byaRpta.id);
                    AbrirEP();
                    Editar();
                }
            });
        }
        function GuardarMod() {
            byaRpta = {};
            jsonData = "{'Reg':" + JSON.stringify(datosEP()) + "}";
            urlTo = "wfRgEstPrev.aspx/updateESTPREV";
            msgPpal = "#LbMsg";

            byaPage.POST_Sync(urlTo, jsonData, function (result) {
                byaRpta = byaPage.retObj(result.d);
                //Mensaje arriba de la grid
                msg = $(msgPpal); //referencia msg
                msg.html(byaRpta.Mensaje); //mostrar msg en div 
                byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
            });

            //wizard.TxtID.jqxNumberInput({ disabled: false });
        }

        function nuevo() {
            nuevoButton.jqxButton({ disabled: true });
            abrirButton.jqxButton({ disabled: true });
            editarButton.jqxButton({ disabled: true });
            guardarButton.jqxButton({ disabled: false });
            cancelarButton.jqxButton({ disabled: false });
            imprimirButton.jqxButton({ disabled: false });
            wizard.Nuevo();
        }

        datosEPNuevo = function () {
            var ep = {};

            ep.FEC_ELA_EP = $('#TxtFecElab').jqxDateTimeInput('value');

            ep.IDE_DIL_EP = $("#TxtIdeFun").val();
            ep.CAR_DIL_EP = $("#CboCarDilJq").val();

            ep.IDE_RES_EP = $("#TxtIdeRes").val();
            ep.CAR_RES_EP = $("#CboCarRes").val();

            ep.IDE_APTE_EP = $("#TxtIdeApoTec").val();

            ep.CAR_APTE_EP = $("#CboCarApoTec").val();

            ep.DEP_NEC_EP = $("#CboDepSol").val();
            ep.DEP_SUP_EP = $("#CboDepSup").val();
            ep.CAR_SUP_EP = $("#CboCarSup").val();

            ep.TIP_CON_EP = $("#CboTip").val();
            ep.STIP_CON_EP = $("#CboSubTip").val();

            ep.MOD_SEL_EP = $("#CboMod").val();

            ep.NUM_EMP_EP = $("#TxtNEmpleos").val();
            ep.GRUPOS_EP = $("#TxtGrupos").val();

            ep.DEP_DEL_EP = $("#CboDepDel").val();
            
            ep.VAL_ENT_EP = $("#txtValProp").val();
            ep.VAL_OTR_EP = $("#txtValOtros").val();
            
            return ep;
        };

        datosEP = function () {
            var ep = {};
            ep.ID = wizard.GetID();
            ep.NECE_EP = TxtDesNec.value;
            ep.OBJE_EP = TxtObjCon.value;

            ep.DESC_EP = TxtDesObj.value;

            ep.LUGE_EP = TxtLugar.value;
            ep.PLIQ_EP = TxtPlazoLiq.value;

            ep.FJUR_EP = TxtFundJur.value;
            ep.JFAC_SEL_EP = TxtJFacSel.value;
            ep.FAC_ESC_EP = TxtFacEsc.value;
            ep.CAP_FIN_EP = TxtCapFin.value;
            ep.CAP_RES_EP = TxtCapRes.value;

            ep.CON_EXP_EP = TxtCodExp.value;

            ep.FEC_ELA_EP = $('#TxtFecElab').jqxDateTimeInput('value');

            ep.IDE_RES_EP = $("#TxtIdeRes").val();
            ep.CAR_RES_EP = $("#CboCarRes").val();

            ep.IDE_DIL_EP = $("#TxtIdeFun").val();
            ep.CAR_DIL_EP = $("#CboCarDilJq").val();

            ep.IDE_APTE_EP = $("#TxtIdeApoTec").val();

            //$("#CboCarRes").val();
            ep.CAR_APTE_EP = $("#CboCarApoTec").val();
            ep.CAR_SUP_EP = $("#CboCarSup").val();

            ep.DEP_SUP_EP = $("#CboDepSup").val();
            ep.DEP_NEC_EP = $("#CboDepSol").val();
            ep.TIP_CON_EP = $("#CboTip").val();

            ep.MOD_SEL_EP = $("#CboMod").val();

            ep.NUM_EMP_EP = $("#TxtNEmpleos").val();
            ep.GRUPOS_EP = $("#TxtGrupos").val();

            ep.PLAZ1_EP = $("#TxtPlazo1").val();
            ep.TPLA1_EP = $("#CboTPlazo1").val();

            ep.PLAZ2_EP = $("#TxtPlazo2").val();
            ep.TPLA2_EP = $("#CboTPlazo2").val();

            ep.TIP_CON_EP = $("#CboTip").val();
            ep.STIP_CON_EP = $("#CboSubTip").val();
            

            ep.VIG_EP = $("#CboVig").val();
            
            ep.DEP_DEL_EP = $("#CboDepDel").val();

            ep.VAL_ENT_EP = $("#txtValProp").val();
            ep.VAL_OTR_EP = $("#txtValOtros").val();

            return ep;
        };

        // delete selected row.
        cancelarButton.click(function (event) {
            if (!cancelarButton.jqxButton('disabled')) {

                wizard.config.oper = 'cancelar';

                if (confirm("Desea cancelar el proceso?")) {

                    nuevoButton.jqxButton({ disabled: false });
                    abrirButton.jqxButton({ disabled: false });
                    guardarButton.jqxButton({ disabled: true });
                    editarButton.jqxButton({ disabled: true });
                    cancelarButton.jqxButton({ disabled: true });
                    imprimirButton.jqxButton({ disabled: true });
                    wizard.TxtID.jqxNumberInput({ disabled: false });

                    wizard.Deshabilitar();
                    wizard.Limpiar();
                    $('#FrmEstPrev').jqxValidator('hide');
                    $('#FrmEstPrev').jqxValidator();
                }
            }
        });
        
        imprimirButton.click(function (event) {
            if (!imprimirButton.jqxButton('disabled')) {
                alert("imprimirButton ..." + wizard.TxtID.val());
                AbrirPagina("../ashx/descEP.ashx?id_ep="+wizard.TxtID.val());
            }
        });
        
        function AbrirPagina(url) {
            self.location.href = url;
        }
}

//Revisión 
function _createToolBarRV() {
    divBtnGrid = "#divToolEP";
    tema = theme;
    ancho = 85;
    alto = 20;
    
	var ToolRevApr = byaPage.container();

    var abrirButton = byaPage.idButton("../jqwidgets/images/open.png", "Abrir");
	var revisarButton = byaPage.idButton("../jqwidgets/images/valid.png", "Revisar");
	var cancelarButton = byaPage.idButton("../jqwidgets/images/undo.png", "Cancelar");
    var imprimirButton = byaPage.idButton("../jqwidgets/images/print.png", "Imprimir");
	
	ToolRevApr.append(wizard.TxtID);
	ToolRevApr.append(abrirButton);
    ToolRevApr.append(revisarButton);
    ToolRevApr.append(cancelarButton);
    ToolRevApr.append(imprimirButton);

    $(divToolRevApr).html(ToolRevApr);

	wizard.TxtID.jqxNumberInput({ decimalDigits: 0,min:0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
    abrirButton.jqxButton({ theme: tema, width: ancho, height: alto });
	revisarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    cancelarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
	imprimirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
	
    if( pag.EP ){
		 wizard.TxtID.val(pag.EP);
		 AbrirEP();
	}
	revisarButton.click(function (event) {
	    if (!revisarButton.jqxButton('disabled')) {
	        admHEstadosEP.AbrirRevisar(wizard.TxtID.val());
	    }
	});


	abrirButton.click(function (event) {
	    AbrirEP();
	});
	function AbrirEP() {

	    if (wizard.AbrirEP()) {
	        imprimirButton.jqxButton({ disabled: false });
	        revisarButton.jqxButton({ disabled: false });
	        cancelarButton.jqxButton({ disabled: false });
	        abrirButton.jqxButton({ disabled: true });
	    }
	    else {
	        imprimirButton.jqxButton({ disabled: true });
	        revisarButton.jqxButton({ disabled: true });
	        cancelarButton.jqxButton({ disabled: true });
	        abrirButton.jqxButton({ disabled: false });
	    }
    }

    cancelarButton.click(function (event) {
        
        if (!cancelarButton.jqxButton('disabled')) {

            wizard.config.oper = 'cancelar';

            if (confirm("Desea cancelar el proceso?")) {

                imprimirButton.jqxButton({ disabled: true });
                revisarButton.jqxButton({ disabled: true });
                cancelarButton.jqxButton({ disabled: true });
                abrirButton.jqxButton({ disabled: false });

                wizard.Deshabilitar();
                wizard.Limpiar();
                
            }
        }
    });
    imprimirButton.click(function (event) {
        if (!imprimirButton.jqxButton('disabled')) {
            wizard.imprimir();
        }
    });
            
}

//Aprobación
function _createToolBarAP() {
    divBtnGrid = "#divToolEP";
    tema = theme;
    ancho = 85;
    alto = 20;

    var ToolRevApr = byaPage.container();

    var abrirButton = byaPage.idButton("../jqwidgets/images/open.png", "Abrir");
    var aprobarButton = byaPage.idButton("../jqwidgets/images/check.png", "Aprobar");
    var cancelarButton = byaPage.idButton("../jqwidgets/images/undo.png", "Cancelar");
    var imprimirButton = byaPage.idButton("../jqwidgets/images/print.png", "Imprimir");

    ToolRevApr.append(wizard.TxtID);
    ToolRevApr.append(abrirButton);
    ToolRevApr.append(aprobarButton);
    ToolRevApr.append(cancelarButton);
    ToolRevApr.append(imprimirButton);

    $(divToolRevApr).html(ToolRevApr);

    wizard.TxtID.jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
    abrirButton.jqxButton({ theme: tema, width: ancho, height: alto });
    aprobarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    cancelarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    imprimirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });

    if (pag.EP) {
        wizard.TxtID.val(pag.EP);
        AbrirEP();
    }
    aprobarButton.click(function (event) {
        if (!aprobarButton.jqxButton('disabled')) {
            admHEstadosEP.AbrirAprobar(wizard.TxtID.val());
        }
    });

    abrirButton.click(function (event) {
        AbrirEP();
    });
    function AbrirEP() {
        if (wizard.AbrirEP()) {
            imprimirButton.jqxButton({ disabled: false });
            aprobarButton.jqxButton({ disabled: false });
            cancelarButton.jqxButton({ disabled: false });
            abrirButton.jqxButton({ disabled: true });
        }
        else {
            imprimirButton.jqxButton({ disabled: true });
            aprobarButton.jqxButton({ disabled: true });
            cancelarButton.jqxButton({ disabled: true });
            abrirButton.jqxButton({ disabled: false });
        }
    }

    cancelarButton.click(function (event) {

        if (!cancelarButton.jqxButton('disabled')) {

            wizard.config.oper = 'cancelar';

            if (confirm("Desea cancelar el proceso?")) {

                imprimirButton.jqxButton({ disabled: true });
                revisarButton.jqxButton({ disabled: true });
                cancelarButton.jqxButton({ disabled: true });
                abrirButton.jqxButton({ disabled: false });

                wizard.Deshabilitar();
                wizard.Limpiar();

            }
        }
    });
    imprimirButton.click(function (event) {
        if (!imprimirButton.jqxButton('disabled')) {
            wizard.imprimir();
        }
    });

}

//DesAprobación
function _createToolBarDA() {
    divBtnGrid = "#divToolEP";
    tema = theme;
    ancho = 85;
    alto = 20;

    var ToolRevApr = byaPage.container();

    var abrirButton = byaPage.idButton("../jqwidgets/images/open.png", "Abrir");
    var desaprobarButton = byaPage.idButton("../jqwidgets/images/uncheck.png", "Desaprobar");
    var cancelarButton = byaPage.idButton("../jqwidgets/images/undo.png", "Cancelar");
    var imprimirButton = byaPage.idButton("../jqwidgets/images/print.png", "Imprimir");

    ToolRevApr.append(wizard.TxtID);
    ToolRevApr.append(abrirButton);
    ToolRevApr.append(desaprobarButton);
    ToolRevApr.append(cancelarButton);
    ToolRevApr.append(imprimirButton);

    $(divToolRevApr).html(ToolRevApr);

    wizard.TxtID.jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
    desaprobarButton.jqxButton({ theme: tema, width: ancho + 10, height: alto, disabled: true });
    abrirButton.jqxButton({ theme: tema, width: ancho, height: alto });
    cancelarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    imprimirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });

    if (pag.EP) {
        wizard.TxtID.val(pag.EP);
        AbrirEP();
        alert("Ho....");
    }
    desaprobarButton.click(function (event) {
        if (!desaprobarButton.jqxButton('disabled')) {
            admHEstadosEP.AbrirDesAprobar(wizard.TxtID.val());
        }
    });

    abrirButton.click(function (event) {
        AbrirEP();
    });

    function AbrirEP() {
        if (wizard.AbrirEP()) {
            imprimirButton.jqxButton({ disabled: false });
            desaprobarButton.jqxButton({ disabled: false });
            cancelarButton.jqxButton({ disabled: false });
            abrirButton.jqxButton({ disabled: true });
            
        }
        else {
            imprimirButton.jqxButton({ disabled: true });
            desaprobarButton.jqxButton({ disabled: true });
            cancelarButton.jqxButton({ disabled: true });
            abrirButton.jqxButton({ disabled: false });
        }
    }

    cancelarButton.click(function (event) {

        if (!cancelarButton.jqxButton('disabled')) {

            wizard.config.oper = 'cancelar';

            if (confirm("Desea cancelar el proceso?")) {

                imprimirButton.jqxButton({ disabled: true });
                desaprobarButton.jqxButton({ disabled: true });
                cancelarButton.jqxButton({ disabled: true });
                abrirButton.jqxButton({ disabled: false });

                wizard.Deshabilitar();
                wizard.Limpiar();

            }
        }
    });
    imprimirButton.click(function (event) {
        if (!imprimirButton.jqxButton('disabled')) {
            wizard.imprimir();
        }
    });

}

//Consulta
function _createToolBarCN() {
    divBtnGrid = "#divToolEP";
    tema = theme;
    ancho = 85;
    alto = 20;

    var ToolRevApr = byaPage.container();
    var imprimirButton = byaPage.idButton("../jqwidgets/images/print.png", "Imprimir");
    ToolRevApr.append(imprimirButton);
    $(divToolRevApr).html(ToolRevApr);
    wizard.TxtID.jqxNumberInput({ decimalDigits: 0, min: 0, width: '100px', height: '25px', inputMode: 'simple', spinButtons: true, theme: tema });
    imprimirButton.jqxButton({ theme: tema, width: ancho, height: alto});
    if (pag.EP) {
        wizard.TxtID.val(pag.EP);
        wizard.AbrirEP();
     
    }
    imprimirButton.click(function (event) {
        if (!imprimirButton.jqxButton('disabled')) {
            wizard.imprimir();
        }
    });

}