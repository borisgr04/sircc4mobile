var admMun = (function () {
    var ventana = '#winConMun';
    var grid = "#jqxgridMun";
    var msgPopup = "#msgConMun";
    var msgPpal = "#msgMunPpal";

    var oper;
    var editrow = -1;
    var docExportXLS = 'Regiones_EP';

    var urlToInsert = "wfRgEstPrev.aspx/InsertMun";
    var urlToDelete = "wfRgEstPrev.aspx/deleteMun";

    var urlToGrid = "wfRgEstPrev.aspx/GetEP_MUN";


    var divBtnGrid = "#divBtnGridMun";

    var gridCon = '#jqxgridConMun';
    var urlToGridCon = "wfRgEstPrev.aspx/GetMun";

    function verVentana() {
        $(ventana).jqxWindow('open');
        _createGridCon();
    }
    //Creating the demo window
    function _createWindow() {
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 700, maxWidth: 900, minHeight: 300, minWidth: 200, height: 500, width: 750,
            theme: admMun.config.theme
        });
    };
    //crea GridTipos
    function _createGridCon() {
        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'COD_MUN' },
                    { name: 'NOM_MUN' }
                 ],
            async: false,
            record: 'Table',
            url: urlToGridCon,
            data: { id: admMun.id_ep }
        };
        var dataAdapter = new $.jqx.dataAdapter(source, { contentType: 'application/json; charset=utf-8' });
        // initialize jqxGrid
        $(gridCon).jqxGrid(
                    {
                        width: 700,
                        source: dataAdapter,
                        theme: admMun.config.theme,
                        localization: byaPage.getLocalization(),
                        height: 350,
                        sortable: true,
                        altrows: true,
                        showfilterrow: true,
                        filterable: true,
                        pageable: false,
                        enabletooltips: true,
                        columns: [
                      { text: 'ID', datafield: 'COD_MUN', width: 100 },
                      { text: 'Región', datafield: 'NOM_MUN', width: 550 }
                    ]
                    });

        $(gridCon).bind('rowselect', function (event) {
            var selectedRowIndex = event.args.rowindex;
            var datarow = {};

            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'COD_MUN');
            datarow["COD_MUN"] = cell.value;

            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'NOM_MUN');
            datarow["NOM_MUN"] = cell.value;

            var commit = $(grid).jqxGrid('addrow', null, datarow);
        });

    }
    function _createBarraGrid() {
        var container = byaPage.container();
        var addButton = byaPage.addButton();
        var deleteButton = byaPage.deleteButton();
        var reloadButton = byaPage.reloadButton();
        var xlsButton = byaPage.xlsButton();

        container.append(addButton);
        container.append(deleteButton);
        container.append(reloadButton);
        container.append(xlsButton);

        $(divBtnGrid).append(container);

        tema = admMun.config.theme;

        addButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admMun.disabled });
        deleteButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admMun.disabled });
        reloadButton.jqxButton({ theme: tema, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: tema, width: 80, height: 20 });

        addButton.click(function (event) {
            if (!admMun.disabled) {
                verVentana();
            }
        });

        // delete selected row.
        deleteButton.click(function (event) {
            if (!admMun.disabled) {
                var selectedrowindex = $(grid).jqxGrid('getselectedrowindex');
                var rowscount = $(grid).jqxGrid('getdatainformation').rowscount;
                if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
                    var id = $(grid).jqxGrid('getrowid', selectedrowindex);
                    $(grid).jqxGrid('deleterow', id);
                }
            }
        });

        // reload grid data.
        reloadButton.click(function (event) {
            $(grid).jqxGrid("updatebounddata");
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        });
    }

    function _createElements() {
        _createBarraGrid();
        _createGrid();
    }
    //crea GridTipos
    function _createGrid() {
        id = admMun.id_ep;

        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'ID' },
                    { name: 'ID_EP' },
                    { name: 'COD_MUN' },
                    { name: 'NOM_MUN' }
                 ],
            addrow: function (rowid, rowdata, position, commit) {
                var byaRpta = {};
                id = admMun.id_ep;
                rowdata["ID_EP"] = id;
                jsonData = "{'Reg':" + JSON.stringify(rowdata) + "}";

                
                byaPage.POST_Sync(urlToInsert, jsonData, function (result) {//Enviar Datos Sincronicamente
                
                    byaRpta = byaPage.retObj(result.d);

                    //Mensaje del popup
                    msg = $(msgPopup); //referencia msg
                    msg.html(byaRpta.Mensaje); //mostrar msg en div 
                    byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error

                    //Mensaje arriba de la grid
                    msg = $(msgPpal); //referencia msg
                    msg.html(byaRpta.Mensaje); //mostrar msg en div 
                    byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error

                });
                commit(!byaRpta.Error);
                $(grid).jqxGrid("updatebounddata");
                $(gridCon).jqxGrid("updatebounddata");
            },
            deleterow: function (rowid, commit) {
                var byaRpta = {};
                if (confirm("Desea eliminar el item")) {
                    var rowdata = $(grid).jqxGrid('getrowdatabyid', rowid);

                    jsonData = "{'ID':" + rowdata.ID + "}";
                    

                    byaPage.POST_Sync(urlToDelete, jsonData, function (result) {//Enviar Datos Sincronicamente

                        byaRpta = byaPage.retObj(result.d);

                        //Mensaje del popup
                        msg = $(msgPopup); //referencia msg
                        msg.html(byaRpta.Mensaje); //mostrar msg en div 
                        byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error

                        //Mensaje arriba de la grid
                        msg = $(msgPpal); //referencia msg
                        msg.html(byaRpta.Mensaje); //mostrar msg en div 
                        byaPage.msgResult(msg, byaRpta.Error); //colocar colores de acuerdo al error

                    });

                    //Completar Datos para mostrar en la grid   
                    rowdata["ID"] = byaRpta.id;
                    //Confirmar si no hay errror
                    commit(!byaRpta.Error);
                }
            },
            async: false,
            record: 'Table',
            url: urlToGrid,
            data: { 'id': id }
        };
        var dataAdapter = new $.jqx.dataAdapter(source,
        	            {
        	                contentType: 'application/json; charset=utf-8',
        	                loadError: function (jqXHR, status, error) {
        	                    alert(error + jqXHR.responseText);
        	                }
        	            });

        // initialize jqxGrid
        $(grid).jqxGrid(
            {
                width: '100%',
                source: dataAdapter,
                theme: admMun.config.theme,
                autoheight: true,
                sortable: true,
                localization: byaPage.getLocalization(),
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: false,
                enabletooltips: true,
                showstatusbar: true,
                columns: [
                  { text: 'Región', datafield: 'NOM_MUN' }
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
        init: function () {
            _createElements();
            //Adding jqxWindow
            _createWindow();
        }
    };
} ());


$(function () {
    admMun.id_ep = wizard.TxtID.val();
    admMun.config.theme = theme;
    admMun.disabled = wizard.disabled;
    admMun.init();
});