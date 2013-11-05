var admCDP = (function () {
    //CDP
    var ventana = '#wadmCDP';
    var grid = "#jqxgridCDP";

    var msgPopup = "#msgCDP";
    var msgPpal = "#msgCDPEditar";

    var oper;
    var editrow = -1;
    var docExportXLS = 'CDP_EP';

    var urlToInsert = "wfRgEstPrev.aspx/InsertCDP";
    var urlToUpdate = "wfRgEstPrev.aspx/UpdateCDP";
    var urlToDelete = "wfRgEstPrev.aspx/deleteCDP";

    var urlToGrid = "wfRgEstPrev.aspx/GetEP_CDP";

    var divBtnGrid = "#divBtnGridCDP";

    //Adding event listeners
    function _addEventListeners() {

        var GuardarNuevo = function () {
            var dataRecord = {};
            dataRecord.GRUPO = $("#txtGrupoCDP").val();
            dataRecord.NRO_CDP = $("#txtNroCDP").val();
            //dataRecord.FEC_CDP = byaPage.parseJsonDate($("#txtFecCDP").val());
            dataRecord.FEC_CDP = $("#txtFecCDP").jqxDateTimeInput('value'); ;
            dataRecord.VAL_CDP = $("#txtValCDP").val();
            dataRecord.VIG_FUT = $("#cboVigFut").val();
            var commit = $(grid).jqxGrid('addrow', null, dataRecord);
        }

        var GuardarMod = function () {
            if (editrow >= 0) {

                var rowID = $(grid).jqxGrid('getrowid', editrow);
                var dataRecord = $(grid).jqxGrid('getrowdata', editrow);

                dataRecord.GRUPO = $("#txtGrupoCDP").val();
                dataRecord.NRO_CDP = $("#txtNroCDP").val();
                //dataRecord.FEC_CDP = byaPage.parseJsonDate($("#txtFecCDP").val());
                dataRecord.FEC_CDP = $("#txtFecCDP").jqxDateTimeInput('value'); ;
                dataRecord.VAL_CDP = $("#txtValCDP").val();
                dataRecord.VIG_FUT = $("#cboVigFut").val();


                var commit = $(grid).jqxGrid('updaterow', rowID, dataRecord);
            }
        }
        $("#BtnEPGuardarCDP").click(function () {
            if (oper == "add") {
                GuardarNuevo();
            }
            if (oper == "edit") {
                GuardarMod();
            }
        });

        $("#BtnEPCancelarCDP").click(function () {
            editrow = -1;
            $(ventana).jqxWindow('close');
        });

    };

    function _createElements() {
        var tema = admCDP.config.theme;
        $("#txtGrupoCDP").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 0, max: admCDP.Grupo, theme: tema });
        $("#txtNroCDP").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 0, theme: tema });
        $("#txtFecCDP").jqxDateTimeInput({ width: '150px', height: '25px', theme: tema, culture: 'es-CO' });
        $("#txtValCDP").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 9999999999, digits: 10, symbol: '$', theme: tema, disabled: true });

        var source = [{ cod: "NO", val: "NO" }, { cod: "SI", val: "SI"}];
        byaPage.JSONtoString(source)
        //byaPage.msgJson(source);
        $("#cboVigFut").jqxDropDownList({ selectedIndex: 0, source: source, width: '70px', height: '25px', displayMember: "cod", valueMember: "val", placeHolder: 'Seleccione:', theme: tema });
        // Load the data from the Select html element.

        $("#BtnEPGuardarCDP").jqxButton({ theme: tema, width: 100, height: 25 });
        $("#BtnEPCancelarCDP").jqxButton({ theme: tema, width: 100, height: 25 });

        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 400, width: 550,
            theme: tema
        });
        _createGrid();
        _createToolGrid();
    }


    function _createToolGrid() {
        var container = byaPage.container();
        var addButton = byaPage.addButton();
        var reloadButton = byaPage.reloadButton();
        var deleteButton = byaPage.deleteButton();
        var xlsButton = byaPage.xlsButton();

        container.append(addButton);
        container.append(deleteButton);

        container.append(reloadButton);
        container.append(xlsButton);

        $(divBtnGrid).append(container);

        var tema = admCDP.config.theme;
        var disabled = admCDP.disabled;
        alert(disabled);
        addButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: disabled });
        deleteButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: disabled });

        reloadButton.jqxButton({ theme: tema, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: tema, width: 80, height: 20 });


        addButton.click(function (event) {
            if (!disabled) {
                oper = "add";
                $("#txtNroCDP").jqxNumberInput({ disabled: false });
                $(ventana).jqxWindow('open');
            }
        });

        // delete selected row.
        deleteButton.click(function (event) {
            if (!disabled) {
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
            admCDP.editedRows.splice(0, admCDP.editedRows.length);
            //$(grid).jqxGrid({ source: getAdapter() });
            $(grid).jqxGrid("updatebounddata");
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        });
    }
    //crea Grid



    ///
    function _createGrid() {


        var getAdapter = function () {
            id = admCDP.id_ep;

            var source = {
                datatype: "xml",
                datafields: [
	                { name: 'ID_EP' },
                    { name: 'ID' },
                    { name: 'NRO_CDP', type: 'number' },
                    { name: 'FEC_CDP', type: 'date' },
                    { name: 'VAL_CDP', type: 'number' },
                    { name: 'VIG_FUT', type: 'string' },
                    { name: 'GRUPO', type: 'number' }
                 ],
                addrow: function (rowid, rowdata, position, commit) {
                    var byaRpta = {};
                    id = admCDP.id_ep;
                    rowdata["ID_EP"] = id;
                    jsonData = "{'Reg':" + JSON.stringify(rowdata) + "}";
                    byaPage.POST_Sync(urlToInsert, jsonData, function (result) {
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
                    $(grid).jqxGrid("updatebounddata");

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
                updaterow: function (rowid, newdata, commit) {
                    //byaPage.msgJson(newdata);
                    var rowindex = $(grid).jqxGrid('getrowboundindexbyid', rowid);
                    var byaRpta = {};
                    id = admCDP.id_ep;
                    newdata["ID_EP"] = id;
                    jsonData = "{'Reg':" + JSON.stringify(newdata) + "}";
                    //alert(jsonData);
                    byaPage.POST_Sync(urlToUpdate, jsonData, function (result) {//Enviar Datos Sincronicamente
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
                    //Confirmar si no hay errror
                    commit(!byaRpta.Error);
                    $(grid).jqxGrid("updatebounddata");

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
            return dataAdapter;
        }

        var cellclass = function (row, datafield, value, rowdata) {
            for (var i = 0; i < admCDP.editedRows.length; i++) {
                if (admCDP.editedRows[i].index == row) {
                    return "editedRow";
                }
            }
        }


        // initialize jqxGrid
        //autorowheight: true,
        $(grid).jqxGrid(
            {
                width: '100%',
                source: getAdapter(),
                theme: admCDP.config.theme,
                sortable: true,
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                enabletooltips: true,
                showaggregates: true,
                showstatusbar: true,
                autoheight: true,
                localization: byaPage.getLocalization(),
                groupable: true,
                columns: [
                  { text: 'N° CDP', datafield: 'NRO_CDP', columntype: 'numberinput', cellsalign: 'right', cellclassname: cellclass },
                  { text: 'Fecha CDP', datafield: 'FEC_CDP', columntype: 'datetimeinput', cellsformat: 'd', align: 'right', cellsalign: 'right', cellclassname: cellclass },
                  { text: 'Valor CDP', datafield: 'VAL_CDP', columntype: 'numberinput', cellsformat: 'c2', cellsalign: 'right', cellclassname: cellclass },
                  { text: 'Vig. Futura', datafield: 'VIG_FUT', cellclassname: cellclass },
                  { text: 'Grupo', datafield: 'GRUPO', cellclassname: cellclass },
                   { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 70, cellsrenderer: function () {
                       return "Editar";
                   }, buttonclick: function (row) {
                       oper = "edit";

                       // open the popup window when the user clicks a button.
                       editrow = row;
                       // get the clicked row's data and initialize the input fields.
                       var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                       $("#txtGrupoCDP").val(dataRecord.GRUPO);
                       $("#txtNroCDP").jqxNumberInput({ disabled: true });
                       $("#txtNroCDP").val(dataRecord.NRO_CDP);
                       //alert(dataRecord.FEC_CDP);

                       $("#txtFecCDP").val(dataRecord.FEC_CDP);
                       $("#txtValCDP").val(dataRecord.VAL_CDP);
                       $("#cboVigFut").val(dataRecord.VIG_FUT);
                       // show the popup window.
                       /* var offset = $(grid).offset();
                       $(ventana).jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });
                       */
                       $(ventana).jqxWindow('open');
                   }
                   }
                ]
            });

        $(grid).on('rowselect', function (event) {
            admCDP.id_ep_cdp = event.args.row.ID;
            admCDP.VIG_CDP = byaSite.getVigencia();
            admCDP.NRO_CDP = event.args.row.NRO_CDP;
            $.get("admRub.htm", function (data) {
                $('#divRubros').html(data);
            });

        });
    }


    return {
        disabled: null,
        id_ep: null,
        id_ep_cdp: null,
        VIG_CDP: null,
        NRO_CDP: null,
        Grupo: null,
        editedRows: null,
        config: {
            dragArea: null,
            theme: null
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
    admCDP.config.theme = theme;
    admCDP.id_ep = wizard.TxtID.val();
    admCDP.Grupo = wizard.GetGrupos();
    admCDP.disabled = wizard.disabled;
    admCDP.init();
});
