var admObligC = (function () {
    var ventana = '#wadmObligC';
    var grid = "#jqxgridObligC";
    var msgPopup = "#msgObligC";
    var msgPpal = "#msgObligCEditar";

    var oper;
    var editrow = -1;
    var docExportXLS = 'Obligaciones_EP';

    var urlToInsert = "wfRgEstPrev.aspx/InsertObligacionesC";
    var urlToUpdate = "wfRgEstPrev.aspx/UpdateObligacionesC";
    var urlToDelete = "wfRgEstPrev.aspx/deleteObligacionesC";
    var urlToGrid = "wfRgEstPrev.aspx/GetEP_OBLIGACIONESC";

    var divBtnGrid = "#divBtnGridObligC";

    //Adding event listeners
    function _addEventListeners() {

        var GuardarNuevo = function () {
            var dataRecord = {};
            dataRecord.GRUPO = $("#txtGrupo").val();
            dataRecord.DES_OBLIG = $("#txtObligC").val();

            var commit = $(grid).jqxGrid('addrow', null, dataRecord);
            
            if (commit) {
                $("#txtGrupo").val(0);
                $("#txtObligC").val('');
            }
        }

        var GuardarMod = function () {
            if (editrow >= 0) {

                var rowID = $(grid).jqxGrid('getrowid', editrow);
                var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                dataRecord.GRUPO = $("#txtGrupo").val();
                dataRecord.DES_OBLIG = $("#txtObligC").val();
                var commit = $(grid).jqxGrid('updaterow', rowID, dataRecord);

                
            }
        }
        $("#BtnOCGuardar").click(function () {

            if (oper == "add") {
                GuardarNuevo();
            }
            if (oper == "edit") {
                GuardarMod();
            }
        });

        $("#BtnOCCancelar").click(function () {
            editrow = -1;
            $(ventana).jqxWindow('close');
        });

    };

    //Creating all page elements which are jqxWidgets
    function _createElements() {

        var tema = admObligC.config.theme;

        $("#txtGrupo").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 0, max: admObligC.Grupo, theme: tema });

        $("#txtObligC").jqxInput({ placeHolder: "Digitar o Pegar con Ctrl+V ", height: 200, width: 300, minLength: 1, theme: tema });

        $("#BtnOCGuardar").jqxButton({ theme: tema, width: 100, height: 25, disabled: admObligC.disabled });
        $("#BtnOCCancelar").jqxButton({ theme: tema, width: 100, height: 25 });

        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 400, width: 550,
            theme: tema
        });
        _createGrid();
        _createToolGrid();
    };

    function _createToolGrid() {
        var container = byaPage.container();
        var addButton = byaPage.addButton();
        var updButton = byaPage.updButton();
        var reloadButton = byaPage.reloadButton();
        var deleteButton = byaPage.deleteButton();
        var xlsButton = byaPage.xlsButton();

        container.append(addButton);
        container.append(deleteButton);
        //container.append(updButton);
        container.append(reloadButton);
        container.append(xlsButton);

        $(divBtnGrid).append(container);
        //statusbar.append(container);
        tema = admObligC.config.theme;
        addButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admObligC.disabled });
        updButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admObligC.disabled });
        deleteButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: admObligC.disabled });
        reloadButton.jqxButton({ theme: tema, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: tema, width: 80, height: 20 });

        if (!admObligC.disabled) {
            updButton.click(function (event) {
                actualizarGrid();
            });
            addButton.click(function (event) {
                oper = "add";
                $(ventana).jqxWindow('open');
            });

            // delete selected row.
            deleteButton.click(function (event) {
                var selectedrowindex = $(grid).jqxGrid('getselectedrowindex');
                var rowscount = $(grid).jqxGrid('getdatainformation').rowscount;
                if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
                    var id = $(grid).jqxGrid('getrowid', selectedrowindex);
                    $(grid).jqxGrid('deleterow', id);
                }
            });
        }
        // reload grid data.
        reloadButton.click(function (event) {
            admObligC.editedRows.splice(0, admObligC.editedRows.length);
            $(grid).jqxGrid({ source: getAdapter() });
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        });
    }
    //crea Grid
    function _createGrid() {

        var getAdapter = function () {
            id = admObligC.id_ep;

            var source = {
                datatype: "xml",
                datafields: [
	                { name: 'ID_EP' },
                    { name: 'ID' },
                    { name: 'DES_OBLIG' },
                    { name: 'GRUPO' }
                 ],
                addrow: function (rowid, rowdata, position, commit) {
                    var byaRpta = {};
                    id = admObligC.id_ep;
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

                    var rowindex = $(grid).jqxGrid('getrowboundindexbyid', rowid);
                    var byaRpta = {};
                    id = admObligC.id_ep;
                    newdata["ID_EP"] = id;
                    jsonData = "{'Reg':" + JSON.stringify(newdata) + "}";

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
        	                    alert("Error en Grid:" + error + jqXHR.responseText);
        	                }
        	            });
            return dataAdapter;
        }

        var cellclass = function (row, datafield, value, rowdata) {
            for (var i = 0; i < admObligC.editedRows.length; i++) {
                if (admObligC.editedRows[i].index == row) {
                    return "editedRow";
                }
            }
        }

        // initialize jqxGrid
        //autorowheight: true,
        var editar = !admObligC.disabled;
        $(grid).jqxGrid(
            {
                width: '100%',
                source: getAdapter(),
                theme: admObligC.config.theme,
                sortable: true,
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                enabletooltips: true,
                showaggregates: true,
                showstatusbar: true,
                autoheight: true,
                editable: editar,
                localization: byaPage.getLocalization(),
                groupable: true,
                editmode: byaPage.editGrid,
                renderstatusbar: function (statusbar) {


                },
                columns: [
                  { text: 'Obligación', datafield: 'DES_OBLIG', cellclassname: cellclass },
                  { text: 'Grupo', datafield: 'GRUPO', width: 80, cellclassname: cellclass },
                  { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 80, cellsrenderer: function () {
                      return "Editar";
                  }, buttonclick: function (row) {
                      oper = "edit";
                      // open the popup window when the user clicks a button.
                      editrow = row;
                      // get the clicked row's data and initialize the input fields.
                      var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                      $("#txtGrupo").val(dataRecord.GRUPO);
                      $("#txtObligC").val(dataRecord.DES_OBLIG);
                      // show the popup window.
                      $(ventana).jqxWindow('open');
                  }
                  }
                ]
            });
    }

    return {
        disabled: null,
        id_ep: null,
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
    admObligC.config.theme = theme;
    admObligC.id_ep = wizard.TxtID.val();
    admObligC.Grupo = wizard.GetGrupos();
    admObligC.disabled = wizard.disabled;
    admObligC.init();

});