var admProy = (function () {
    var ventana = '#winConProy';
    var grid = "#jqxgridProy";
    var msgPopup = "#msgConProy";
    var msgPpal = "#msgProy";

    var oper;
    var editrow = -1;
    var docExportXLS = 'Proyectos_EP';

    var urlToInsert = "wfRgEstPrev.aspx/GuardarNuevoProyecto";
    var urlToDelete = "wfRgEstPrev.aspx/deleteProyecto";

    var urlToGrid = "wfRgEstPrev.aspx/GetEP_Proyectos";
    var divBtnGrid = "#divBtnGridProy";

    var gridCon = '#jqxgridConProy';
    var urlToGridCon = "wfRgEstPrev.aspx/GetProyectos";

    function verVentana() {
        $(ventana).jqxWindow('open');
        _createGridCon();
    }
    //Creating the demo window
    function _createWindow() {
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 700, maxWidth: 900, minHeight: 300, minWidth: 200, height: 500, width: 750,
            theme: admProy.config.theme
        });
    };
    //crea GridTipos
    function _createGridCon() {
        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'Nro_Proyecto' },
                    { name: 'Nombre_Proyecto' }
                 ],
            async: false,
            record: 'Table',
            url: urlToGridCon,
            data: { filtro: "''" }
        };
        var dataAdapter = new $.jqx.dataAdapter(source, { contentType: 'application/json; charset=utf-8' });
        // initialize jqxGrid
        $(gridCon).jqxGrid(
                    {
                        width: '100%',
                        source: dataAdapter,
                        theme: admProy.config.theme,
                        height: 350,
                        sortable: true,
                        altrows: true,
                        localization: byaPage.getLocalization(),
                        showfilterrow: true,
                        filterable: true,
                        pageable: true,
                        enabletooltips: true,
                        columns: [
                      { text: 'Código', datafield: 'Nro_Proyecto', width: 150 },
                      { text: 'Descripción', datafield: 'Nombre_Proyecto'}
                    ]
                    });

        $(gridCon).bind('rowselect', function (event) {
            var selectedRowIndex = event.args.rowindex;
            var cod, nom;
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'Nro_Proyecto');
            cod = cell.value;
            var cell = $(gridCon).jqxGrid('getcell', selectedRowIndex, 'Nombre_Proyecto');
            nom = cell.value;

            var datarow = {};
            datarow["COD_PRO"] = cod;
            datarow["NOMBRE_PROYECTO"] = nom;
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

        tema = admProy.config.theme;

        addButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admProy.disabled });
        deleteButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admProy.disabled });

        reloadButton.jqxButton({ theme: tema, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: tema, width: 80, height: 20 });

        
            addButton.click(function (event) {
                if (!admProy.disabled) {
                verVentana();
                }
            });
            // delete selected row.
            deleteButton.click(function (event) {
            if (!admProy.disabled) {
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
        id = admProy.id_ep;

        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'COD_PRO' },
                    { name: 'NOMBRE_PROYECTO' }
                 ],
            addrow: function (rowid, rowdata, position, commit) {
                var byaRpta = {};
                id = admProy.id_ep;
                jsonData = "{Cod:'" + rowdata["COD_PRO"] + "','Id_EP':" + id + "}";
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

            },
            deleterow: function (rowid, commit) {
                
                var byaRpta = {};

                var rowdata = $(grid).jqxGrid('getrowdatabyid', rowid);
                
                if (confirm("Desea eliminar el proyecto:" + rowdata["COD_PRO"])) {
                
                    id = admProy.id_ep;
                    jsonData = "{Cod:'" + rowdata["COD_PRO"] + "','Id_EP':" + id + "}";
                    
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
                    commit(!byaRpta.Error);
                    $(grid).jqxGrid("updatebounddata");

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
                width: 800,
                source: dataAdapter,
                theme: admProy.config.theme,
                autoheight: true,
                sortable: true,
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                enabletooltips: true,
                showstatusbar: true,
                columns: [
                  { text: 'Código', datafield: 'COD_PRO', width: 150 },
                  { text: 'Descripción', datafield: 'NOMBRE_PROYECTO', width: 800 }
                ]
            });
        //Manejo de Captura de Datos del GRid 
        var generaterow = function () {
            var row = {};
            row["COD_PRO"] = txtCodPro.value;
            row["NOMBRE_PROYECTO"] = txtNomPro.value;
            return row;
        }
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
    admProy.id_ep = wizard.TxtID.val();
    admProy.disabled = wizard.disabled;
    admProy.config.theme = theme;
    admProy.init();
});