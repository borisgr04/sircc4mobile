var theme;
var RegSolicitud = (function () {
    //Adding event listeners
    var _addHandlers = function () {

        $('#CboTip').on('select', function (event) {
            RegSolicitud.RefreshCboSubTip();
        });
        $('#BtnAbrirEP').click(function () {

            if (!$('#BtnAbrirEP').jqxButton('disabled')) {
                RegSolicitud.AbrirEP();
            } else
            { alert("Enable false"); }
        });

    };

    _iniElements = function () {
        //Inicializar Usuario
        var user = RegSolicitud.getUser();
        $("#HeadLoginName").html(user);
        RegSolicitud.RefreshCboSubTip("00");

    };

    _createElements = function () {
        //Crea Tabs y Botones de Control
        tema = RegSolicitud.config.theme;
        $('#jqxTabs').jqxTabs({ height: 1000, width: '100%', theme: RegSolicitud.config.theme, keyboardNavigation: false, scrollPosition: 'both' });
        $("#txtObjCon").jqxInput({ placeHolder: "Descripción ", height: 100, width: '95%', minLength: 1, theme: tema });
        //Crea Barra de Herramientas
        $('#BtnGuardar').jqxButton({ width: 80, theme: tema });
        $('#BtnAbrir').jqxButton({ width: 80, theme: tema });
        //, source: countries, Listado de elementos

        $("#txtFecRec").jqxDateTimeInput({ width: '150px', height: '25px', theme: tema, culture: 'es-CO' });

        $("#txtIDEP").jqxNumberInput({ decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', theme: tema });
        $("#txtNSol").jqxNumberInput({ decimalDigits: 0, width: '100px', height: '25px', inputMode: 'simple', theme: tema });

        $("#txtValTot").jqxNumberInput({ width: '200px', height: '25px', decimalDigits: 2, min: 0, max: 9999999999999, digits: 20, symbol: '$', theme: tema });


        var sourceEst = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvEP_ESTADOS');
        $("#CboEstEP").jqxDropDownList({
            disabled: true, selectedIndex: 0, source: sourceEst, displayMember: "NOM_EST", valueMember: "COD_EST", dropDownWidth: '200', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        var sourceEst = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvVIGENCIAS');
        $("#CboVig").jqxDropDownList({
            disabled: true, selectedIndex: 0, source: sourceEst, displayMember: "YEAR_VIG", valueMember: "YEAR_VIG", dropDownWidth: '200', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });


        $('#BtnAbrirEP').jqxButton({ width: 80, theme: tema });
        var sourceDep = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvDEPENDENCIAT');
        $("#CboDepSup").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });
        var sourceDep = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvDEPENDENCIA');
        $("#CboDepSol").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependencia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });
        var sourceDep = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvDEPENDENCIAD');
        $("#CboDepDel").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });
        var sourceTip = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvTIPOSCONT');
        $("#CboTip").jqxDropDownList({
            source: sourceTip, placeHolder: 'Seleccione Tipo de Contrato:', displayMember: "NOMC_TIP", valueMember: "COD_TIP", dropDownWidth: '300', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });

        var sourceMod = byaPage.getSource('/EstPrev/wfRgEstPrev.aspx/GetvModalidad');
        $("#CboMod").jqxDropDownList({
            selectedIndex: 1, source: sourceMod, placeHolder: 'Seleccione:', displayMember: "NOM_TPROC", valueMember: "COD_TPROC", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });


    };


    return {
        disabled: true,
        TxtID: null,
        isEP: null,
        config: {
            dragArea: null,
            theme: null,
            oper: null
        },

        getUser: function () {
            return byaSite.getUsuario();
        }
        ,
        ValidarEL: function () {
            $('#FrmSolicitud').jqxValidator('validate');
        },
        _createValidacionEL: function (fnOk) {
            $('#FrmSolicitud').jqxValidator(
            {
                onSuccess: fnOk,
                hintType: 'label',
                onError: function () { $(msgPpal).html('Los datos no son válidos!!!'); },
                rules: [
                    { input: '#txtObjCon', message: 'Debe especificar el objeto!', action: 'keyup, blur', rule: 'required' },
					{ input: '#CboTip', message: 'Debe Identificar el tipo de contrato', action: 'select', rule: function (input) {
					    var val = $("#CboTip").jqxDropDownList('val');
					    return (val != "");
					}
					}
                    ]
            });
        },
        GetID: function () {
            return $(RegSolicitud.TxtID).val();
        },
        SetID: function (value) {
            $(RegSolicitud.TxtID).val(value);
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
        RefreshCboSubTip: function () {
            var cod_tip = $('#CboTip').val();
            var source = byaPage.getSource('/Servicios/wsDatosBasicos.asmx/GetvSUBTIPOS', { cod_tip: "'" + cod_tip + "'" });
            $("#CboSubTip").jqxDropDownList({
                source: source, placeHolder: 'Seleccione Sub Tipo:', displayMember: "NOMC_STIP", valueMember: "COD_STIP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 250, height: 25, theme: tema
            });
        },
        init: function () {
            //this.TxtID = $("<div id='TxtID'></div>");
            this.TxtID = $("<input type='text' id='TxtID' style='float: left; margin-left: 5px;' ></input>");

            _createElements();
            _iniElements();
            _addHandlers();
            //this.validate();
            this.Deshabilitar();
            

        },
        validar: function () {
            alert("Debe Guardar los Datos Iniciales del Estudio Previo para continuar....");
        },
        Deshabilitar: function () {
            //Deshabilita todos los controles
            $('.formC :input').attr('disabled', true);
            $("#CboDepSup").jqxDropDownList({ disabled: true });
            $("#CboDepSol").jqxDropDownList({ disabled: true });
            $("#CboTip").jqxDropDownList({ disabled: true });
            $("#CboSubTip").jqxDropDownList({ disabled: true });
            //$("#TxtFecElab").jqxDateTimeInput({ disabled: true });
            $("#CboMod").jqxDropDownList({ disabled: true });
            $("#CboDepDel").jqxDropDownList({ disabled: true });
            $("#txtObjCon").jqxInput({ disabled: true });
            $('#BtnAbrirEP').jqxButton({ disabled: true });
            $("#txtIDEP").jqxNumberInput({ disabled: true });
            $("#txtNSol").jqxNumberInput({ disabled: true });
            $("#txtFecRec").jqxDateTimeInput({ disabled: true });
            $('#txtValTot').jqxNumberInput('disabled', true);
            this.disabled = true;
        },
        Limpiar: function () {
            //Colocar Valores Por Defecto
            $('#FrmSolicitud')[0].reset();
            //$('#FrmEstPrev :input').attr('value', '');
            $("#CboDepSup").jqxDropDownList({ selectedIndex: -1 });
            $("#CboDepSol").jqxDropDownList({ selectedIndex: -1 });
            $("#CboTip").jqxDropDownList({ selectedIndex: -1 });
            $("#CboSubTip").jqxDropDownList({ selectedIndex: -1 });
            $("#CboMod").jqxDropDownList({ selectedIndex: -1 });
            $("#CboDepDel").jqxDropDownList({ selectedIndex: -1 });
            byaPage.msgLimpiar($("#LbMsg"));
        },
        HabilitarN: function () {
            $('.formC :input').attr('disabled', false);

            $("#CboDepSup").jqxDropDownList({ disabled: false });
            $("#CboDepSol").jqxDropDownList({ disabled: false });
            $("#CboTip").jqxDropDownList({ disabled: false });
            $("#CboSubTip").jqxDropDownList({ disabled: false });
            $("#TxtFecElab").jqxDateTimeInput({ disabled: false });
            $("#CboMod").jqxDropDownList({ disabled: false });
            $("#CboDepDel").jqxDropDownList({ disabled: false });
            $("#BtnAbrirEP").jqxButton({ disabled: false });
            $("#txtIDEP").jqxNumberInput({ disabled: false });

        },
        HabilitarE: function () {
            $('.formC :input').attr('disabled', false);
            $('#txtValOtros').attr('disabled', true);
            $("#CboDepSup").jqxDropDownList({ disabled: false });
            $("#CboDepSol").jqxDropDownList({ disabled: false });
            $("#CboTip").jqxDropDownList({ disabled: false });
            $("#CboSubTip").jqxDropDownList({ disabled: false });
            //$("#TxtFecElab").jqxDateTimeInput({ disabled: false });
            $("#CboMod").jqxDropDownList({ disabled: false });
            $("#CboDepDel").jqxDropDownList({ disabled: false });
            this.disabled = false;
        },

        Nuevo: function (GuardarNuevo) {
            RegSolicitud.config.oper = 'nuevo';
            RegSolicitud.TxtID.jqxInput({ disabled: true });
            RegSolicitud.HabilitarN(); //Habilitar para nuevo
            RegSolicitud.Limpiar(); //Limpiar los input
            RegSolicitud._createValidacionEL(GuardarNuevo); //Configurar el Validador
            RegSolicitud.RefreshCboSubTip("00");

            //Colocar en Valores Por Defecto
            var user = RegSolicitud.getUser();
        },
        AbrirEP: function () {
            var sw = false;
            var nep = $("#txtIDEP").val();
            if (nep == "") {
                $("#LbMsg").html("Por favor Digite un Número de Estudio Previo...!!!");
                return false;
            }
            var parametrosJSON = {
                "id_ep": $("#txtIDEP").val(),
                "tipo": 'CN'
            };
            //byaPage.msgJson(parametrosJSON);
            $.ajax({
                type: "POST",
                url: "/Servicios/wsEstPrev.asmx/GetEstPrev",
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
                        $("#LbMsg").html("Estudio Previo N° " + RegSolicitud.GetID() + " no encontrado...!!!");
                        byaPage.msgResult($("#LbMsg"));
                    }
                    else {

                        /*
                        //Deshabilita todos los controles
                        $('.formC :input').attr('disabled', true);
                        */
                        $("#txtObjCon").val(ep.OBJE_EP);
                        $("#CboDepSup").val(ep.DEP_SUP_EP);
                        $("#CboDepSol").val(ep.DEP_NEC_EP);
                        $("#CboTip").val(ep.TIP_CON_EP);
                        RegSolicitud.RefreshCboSubTip(ep.TIP_CON_EP);
                        $("#CboSubTip").val(ep.STIP_CON_EP);
                        $("#CboMod").val(ep.MOD_SEL_EP);
                        $("#CboDepDel").val(ep.DEP_DEL_EP);
                        $("#txtValTot").val(ep.VAL_ENT_EP + ep.VAL_OTR_EP);
                        RegSolicitud.isEP = nep;
                        RegSolicitud.Deshabilitar();
                        $("#LbMsg").html("Listo...!!!"); // Si desea modificar debe presionar el boton Editar, realizar los cambios y guardar.");
                        byaPage.msgResult($("#LbMsg"));
                        sw = true;
                    }
                },
                error: function (jqXHR, status, error) {
                    alert(error + "-" + jqXHR.responseText);
                }
            });
            return sw;
        },
        AbrirSol: function () {
            var sw = false;
            var nep = $("#txtID").val();
            if (nep == "") {
                $("#LbMsg").html("Por favor Digite un Número de Solicitud...!!!");
                return false;
            }
            var parametrosJSON = {
                "codsol": RegSolicitud.GetID()
            };
            //byaPage.msgJson(parametrosJSON);
            //byaPage.msgJson(parametrosJSON);
            $.ajax({
                type: "POST",
                url: "/Servicios/wsSolicitudes.asmx/GetSolicitud",
                data: byaPage.JSONtoString(parametrosJSON),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                beforeSend: function () {
                    $("#LbMsg").html("Procesando.. espere por favor...!!!");
                },
                success: function (result) {
                    //alert("Hola...");
                    var e = byaPage.retObj(result.d);

                    if (e.NUM_SOL == 0) {
                        $("#LbMsg").html("Estudio Previo N° " + RegSolicitud.GetID() + " no encontrado...!!!");
                        byaPage.msgResult($("#LbMsg"));
                    }
                    else {
                        byaPage.msgJson(e);
                        $("#txtObjCon").val(e.OBJ_SOL);
                        //e.FEC_RECIBIDO =$('#txtFecRec').jqxDateTimeInput('value');
                        $("#CboDepSol").val(e.DEP_SOL);
                        $("#CboDepSup").val(e.DEP_SUP);
                        $("#CboTip").val(e.TIP_CON);
                        $("#CboMod").val(e.COD_TPRO);
                        RegSolicitud.RefreshCboSubTip(ep.TIP_CON_EP);
                        $("#txtNSol").val(e.COD_SOL);
                        $("#CboSubTip").val(e.STIP_CON);
                        //e.VIG_SOL = 2013;  //$("#CboVig").val();
                        $("#CboDepDel").val(e.DEP_PSOL);
                        $("#txtValTot").val(e.VAL_CON);
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
    _createToolBarEL();
    //if (pag.tipo == "EL") {

    /*}
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
    */

}
//Elaboración
function _createToolBarEL() {
    divBtnGrid = "#divToolEP";

    tema = theme;
    ancho = 80;
    alto = 20;

    var ToolElab = byaPage.container();

    var nuevoButton = byaPage.idButton("../jqwidgets/images/new.png", "Nuevo");
    var abrirButton = byaPage.idButton("../jqwidgets/images/open.png", "Abrir");
    var editarButton = byaPage.idButton("../jqwidgets/images/edit.png", "Editar");
    var guardarButton = byaPage.updButton();
    var cancelarButton = byaPage.idButton("../jqwidgets/images/undo.png", "Cancelar");

    ToolElab.append(nuevoButton);
    ToolElab.append(RegSolicitud.TxtID);
    ToolElab.append(abrirButton);
    ToolElab.append(editarButton);
    ToolElab.append(guardarButton);
    ToolElab.append(cancelarButton);

    $(divToolElab).html(ToolElab);

    RegSolicitud.TxtID.jqxInput({ width: '90px', height: '25px', theme: tema });
    nuevoButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: false });
    abrirButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: false });
    editarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    guardarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });
    cancelarButton.jqxButton({ theme: tema, width: ancho, height: alto, disabled: true });

    RegSolicitud.TxtID.on('change', function (event) {
        AbrirSol();
    });


    if (pag.EP) {
        RegSolicitud.TxtID.val(pag.EP);
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
            AbrirSol();
        }
    });

    function AbrirSol() {
        if (RegSolicitud.AbrirSol()) {
            nuevoButton.jqxButton({ disabled: false });
            abrirButton.jqxButton({ disabled: false });
            editarButton.jqxButton({ disabled: false });
            guardarButton.jqxButton({ disabled: true });
            cancelarButton.jqxButton({ disabled: false });

            $("#LbMsg").html("Listo...!!! Si desea modificar debe presionar el boton Editar, realizar los cambios y presione el Botón Guardar.");
        }

    }
    function AbrirEP() {
        RegSolicitud.config.oper = 'abrir';
        if (RegSolicitud.AbrirEP()) {
            nuevoButton.jqxButton({ disabled: false });
            abrirButton.jqxButton({ disabled: false });
            editarButton.jqxButton({ disabled: false });
            guardarButton.jqxButton({ disabled: true });
            cancelarButton.jqxButton({ disabled: false });

            $("#LbMsg").html("Listo...!!! Si desea modificar debe presionar el boton Editar, realizar los cambios y presione el Botón Guardar.");
        }
    }
    function Editar() {

        nuevoButton.jqxButton({ disabled: true });
        abrirButton.jqxButton({ disabled: true });
        editarButton.jqxButton({ disabled: true });
        guardarButton.jqxButton({ disabled: false });
        cancelarButton.jqxButton({ disabled: false });
        RegSolicitud.TxtID.jqxInput({ disabled: true });
        RegSolicitud.config.oper = 'editar';
        RegSolicitud.HabilitarE();
        RegSolicitud.disabled = false;
        RegSolicitud._createValidacionEL(GuardarMod);
        $("#LbMsg").html("después de modificar los datos y presione el botón guardar...!!!");
        byaPage.msgResult($("#LbMsg"));

    }



    guardarButton.click(function (event) {
        if (!guardarButton.jqxButton('disabled')) {
            RegSolicitud.ValidarEL();
        }
    });

    function GuardarNuevo() {


        jsonData = "{'Reg':" + JSON.stringify(getDatos()) + "}";
        alert(jsonData);
        urlTo = "/Servicios/wsSolicitudes.asmx/InsertPSolicitud";
        msgPpal = "#LbMsg";

        byaPage.POST_Sync(urlTo, jsonData, function (result) {
            byaRpta = byaPage.retObj(result.d);
            //Mensaje arriba de la grid
            msg = $(msgPpal); //referencia msg
            msg.html(byaRpta.Mensaje); //mostrar msg en div 

            byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
            if (!byaRpta.Error) {
                alert("A asignar....");
                RegSolicitud.SetID(byaRpta.id);
                alert(RegSolicitud.TxtID.val());
                Editar();
            }
        });
    }
    function GuardarMod() {
        alert("Modificacion");
        byaRpta = {};
        jsonData = "{'Reg':" + JSON.stringify(getDatos()) + "}";
        urlTo = "/Servicios/wsSolicitudes.asmx/UpdatePSolicitud";
        msgPpal = "#LbMsg";

        byaPage.POST_Sync(urlTo, jsonData, function (result) {
            byaRpta = byaPage.retObj(result.d);
            //Mensaje arriba de la grid
            msg = $(msgPpal); //referencia msg
            msg.html(byaRpta.Mensaje); //mostrar msg en div 
            byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
        });

        //RegSolicitud.TxtID.jqxInput({ disabled: false });
    }

    function nuevo() {
        nuevoButton.jqxButton({ disabled: true });
        abrirButton.jqxButton({ disabled: true });
        editarButton.jqxButton({ disabled: true });
        guardarButton.jqxButton({ disabled: false });
        cancelarButton.jqxButton({ disabled: false });
        RegSolicitud.Nuevo(GuardarNuevo);

    }


    getDatos = function () {
        var e = {};
        e.COD_SOL = RegSolicitud.GetID();
        e.OBJ_SOL = $("#txtObjCon").val();
        e.FEC_RECIBIDO = $('#txtFecRec').jqxDateTimeInput('value');
        e.DEP_SOL = $("#CboDepSol").val();
        e.TIP_CON = $("#CboTip").val();
        e.COD_TPRO = $("#CboMod").val();
        e.STIP_CON = $("#CboSubTip").val();
        e.VIG_SOL = 2013;  //$("#CboVig").val();
        e.DEP_PSOL = $("#CboDepDel").val();
        e.VAL_CON = $("#txtValTot").val();


        return e;
    };

    // delete selected row.
    cancelarButton.click(function (event) {
        if (!cancelarButton.jqxButton('disabled')) {

            RegSolicitud.config.oper = 'cancelar';

            if (confirm("Desea cancelar el proceso?")) {

                nuevoButton.jqxButton({ disabled: false });
                abrirButton.jqxButton({ disabled: false });
                guardarButton.jqxButton({ disabled: true });
                editarButton.jqxButton({ disabled: true });
                cancelarButton.jqxButton({ disabled: true });
                // imprimirButton.jqxButton({ disabled: true });
                RegSolicitud.TxtID.jqxInput({ disabled: false });

                RegSolicitud.Deshabilitar();
                RegSolicitud.Limpiar();
                //$('#FrmEstPrev').jqxValidator('hide');
                //$('#FrmEstPrev').jqxValidator();
            }
        }
    });



}
function LoadJqWidget() {
    RegSolicitud.config.theme = theme;
    RegSolicitud.init();
    RegSolicitud.Deshabilitar(); //Deshabilita los controles y tabs
    _createToolBar();
    
    if (pag.Cod_Sol != null) {
        alert("Enviaron Solicitud");
        RegSolicitud.SetID(pag.Cod_Sol);
        RegSolicitud.AbrirSol();
    }
    

}

//Agregar factorias de jquery al update panel.
//Sys.WebForms.PageRequestManager.getInstance().add_endRequest(LoadJqWidget);
function pagActual() {
    this.Cod_Sol = Principal.Params['Cod_Sol'];
    alert(this.Cod_Sol);
}
var pag = new pagActual();

//Factoria de jquery(pagina lista)
$(document).ready(function () {
    LoadJqWidget();
});
