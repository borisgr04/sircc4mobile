var NuevoEP = (function () {
    var ventana = '#winNuevoEP';
    var msgPopup = "#msgConNuevoEP";
    var gridCon = '#jqxgridNuevoEP';
    var urlToGridCon = "/Servicios/wsEstPrev.asmx/GetEstudiosPrevios";
    var id;
    var idEP;
    function _createButton() {
        $("#btnCrearNuevo").jqxButton({ width: '150', theme: NuevoEP.config.theme });
        $("#btnAbrir").jqxButton({ width: '150', theme: NuevoEP.config.theme, disabled: true });

        $("#btnCrearNuevo").click(function () {

            jsonData = "{'id_plantilla':" + id + "}";
            urlTo = "/Servicios/wsEstPrev.asmx/NuevodePlantilla";

            byaPage.POST_Sync(urlTo, jsonData, function (result) {
                byaRpta = byaPage.retObj(result.d);
                idEP = byaRpta.id;
                //Mensaje arriba de la grid
                msg = $(msgPopup); //referencia msg
                msg.html(byaRpta.Mensaje); //mostrar msg en div 
                byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error
                $("#btnAbrir").jqxButton({ width: '150', theme: NuevoEP.config.theme, disabled: byaRpta.Error });


            });

        });
        $("#btnAbrir").click(function () {
            byaPage.pantallacompleta("/EstPrev/wfRgEstPrev.aspx?tip=EL&nep=" + idEP);
        });
    }
    //Creating the demo window
    function _createWindow() {
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, height: 300, width: 350, isModal: true,
            theme: NuevoEP.config.theme
        });
    };
    //crea GridTipos
    function _createGridCon() {
        jsonData = "{'vig':" + 2013 + "}";
        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'ID' },
                    { name: 'VIG_EP' },
                    { name: 'OBJE_EP' },
                    { name: 'NOM_TIP_CON_EP' },
                    { name: 'MODALIDAD' },
                    { name: 'CLASE' },
                    { name: 'DEP_NEC_EP' },
                    { name: 'MOD_SEL_EP' }
                 ],
            async: false,
            record: 'Table',
            url: urlToGridCon,
            data: { 'vig': '2013' }
        };
        var cellsrendererNOM = function (row, column, value) {
            return '<div style="margin-left: 5px;margin-top: 5px; font-size: 11px">' + value + '</div>';
        }

        var dataAdapter = new $.jqx.dataAdapter(source, { contentType: 'application/json; charset=utf-8' });
        // initialize jqxGrid

        $(gridCon).jqxGrid(
                    {
                        width: '100%',
                        source: dataAdapter,
                        theme: NuevoEP.config.theme,
                        localization: byaPage.getLocalization(),
                        height: 350,
                        sortable: true,
                        altrows: true,
                        showfilterrow: true,
                        filterable: true,
                        pageable: true,
                        enabletooltips: true,
                        columns: [
                        { text: 'ID', datafield: 'ID', width: 40 },
                        { text: 'Vigencia', datafield: 'VIG_EP', width: 70 },
                        { text: 'Objeto', datafield: 'OBJE_EP', width: 150 },
                        { text: 'Modalidad', datafield: 'MODALIDAD', width: 150 },
                        { text: 'Clase', datafield: 'CLASE', width: 150 },
                        { text: 'Crear ', datafield: 'Crear', columntype: 'button', width: 70, cellsrenderer: function () {
                            return "Crear ";
                        }, buttonclick: function (row) {
                            oper = "edit";
                            // open the popup window when the user clicks a button.
                            editrow = row;
                            // get the clicked row's data and initialize the input fields.
                            var dataRecord = $(gridCon).jqxGrid('getrowdata', editrow);
                            id = dataRecord.ID;
                            // show the popup window.
                            $(ventana).jqxWindow('open');
                        }
                    },
                        { text: 'Ver', datafield: 'Ver', columntype: 'button', width: 70, cellsrenderer: function () {
                            return "Ver";
                        }, buttonclick: function (row) {
                            oper = "edit";
                            // open the popup window when the user clicks a button.
                            editrow = row;
                            // get the clicked row's data and initialize the input fields.
                            var dataRecord = $(gridCon).jqxGrid('getrowdata', editrow);
                            id = dataRecord.ID;
                            byaPage.pantallacompleta("/EstPrev/wfRgEstPrev.aspx?tip=CN&nep=" + id);
                        }
                    }
                        
                    ]
                    });
        //{ text: 'ID', datafield: 'ID', width: 40, cellsformat: 'd', cellsalign: 'right' },
       /* $(gridCon).bind('rowselect', function (event) {
            var selectedRowIndex = event.args.rowindex;
            var datarow = {};
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'IDE_TER');
            datarow["IDE_TER"] = cell.value;
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'NOMBRE');
            datarow["NOMBRE"] = cell.value;
            NuevoEP.fnresultado(datarow);
            $(ventana).jqxWindow('close');
        });*/

    }
    return {
        id_ep: null,
        fnresultado: null,
        editedRows: null,
        config: {
            dragArea: null,
            theme: null
        },
        init: function () {
            _createWindow();
            _createButton();
            _createGridCon();
        },
        showWindow: function (fnresultado) {
            //$(ventana).jqxWindow('open');
        }
    };
} ());

function pagActual() {
    this.tipo = 'EL';  //$.getUrlVar('tip');
}
var pag = new pagActual();
$(function () {
    NuevoEP.config.theme = theme;
    NuevoEP.init();
});