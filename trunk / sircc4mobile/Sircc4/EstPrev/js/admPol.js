var admPol = (function () {
    var ventana = '#wadmPol';
    var grid = "#jqxgridPol";
    var msgPopup = "#msgPol";
    var msgPpal = "#msgPolEditar";

    var oper;
    var editrow = -1;
    var docExportXLS = 'Amparos_EP';

    var urlToInsert = "wfRgEstPrev.aspx/InsertPolizas";
    var urlToUpdate = "wfRgEstPrev.aspx/UpdatePolizas";
    var urlToDelete = "wfRgEstPrev.aspx/deletePolizas";
    
    var urlToGrid = "wfRgEstPrev.aspx/GetEP_POLIZAS";
    var divBtnGrid = "#divBtnGridPol";

    //Adding event listeners
    function _addEventListeners() {

        var GuardarNuevo = function () {
            var dataRecord = {};
            dataRecord.COD_POL = $("#CboCodPol").val();
            dataRecord.GRUPO = $("#txtGrupo").val();
            dataRecord.CAL_APARTIRDE = $("#CboCalApartitDe").val();
            dataRecord.APARTIRDE = $("#CboApartitDe").val();
            dataRecord.VIGENCIA = $("#txtVig").val();
            dataRecord.TIPO = $("#CboTipPol").val();
            dataRecord.POR_SMMLV = $("#txtPorSM").val();
            var commit = $(grid).jqxGrid('addrow', null, dataRecord);
        }

        var GuardarMod = function () {
            if (editrow >= 0) {

                var rowID = $(grid).jqxGrid('getrowid', editrow);
                var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                dataRecord.COD_POL = $("#CboCodPol").val();
                dataRecord.GRUPO = $("#txtGrupo").val();
                dataRecord.CAL_APARTIRDE = $("#CboCalApartitDe").val();
                dataRecord.APARTIRDE = $("#CboApartitDe").val();
                dataRecord.VIGENCIA = $("#txtVig").val();
                dataRecord.TIPO = $("#CboTipPol").val();
                dataRecord.POR_SMMLV = $("#txtPorSM").val();

                var commit = $(grid).jqxGrid('updaterow', rowID, dataRecord);
            }
        }
        $("#BtnPolGuardar").click(function () {
            if (oper == "add") {
                GuardarNuevo();
            }
            if (oper == "edit") {
                GuardarMod();
            }
        });

        $("#BtnPolCancelar").click(function () {
            $(ventana).jqxWindow('close');
            editrow = -1;
        });

    };

    //Creating all page elements which are jqxWidgets
    function _createElements() {
        var getSourcePol = function (Servicio) {
            var source = {};
            $.ajax({
                type: "GET",
                url: Servicio,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (result) {
                    source = result.d;
                },
                error: function (jqXHR, status, error) {
                    alert(error + "-" + jqXHR.responseText);
                }
            });
            return source;
        }

        var tema = admPol.config.theme;

        $("#txtGrupo").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 0, max: admPol.Grupo, theme: tema });

        $("#txtVig").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 1, theme: tema });
        $("#txtPorSM").jqxNumberInput({ width: '100px', height: '25px', inputMode: 'simple', decimalDigits: 0, spinButtons: true, min: 1, theme: tema });

        //$("#CboCodPol").jqxComboBox({ selectedIndex: 0, source: getSourcePol(), displayMember: "NOM_POL", valueMember: "COD_POL", width: 200, height: 25, theme: tema });
        $("#CboCodPol").jqxDropDownList({
            selectedIndex: 0, source: getSourcePol('wfRgEstPrev.aspx/GetPOLIZAS'), displayMember: "NOM_POL", valueMember: "COD_POL", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });

        $("#CboCalApartitDe").jqxDropDownList({
            selectedIndex: 0, source: getSourcePol('wfRgEstPrev.aspx/GetCALCULOPOL'), placeHolder: 'Seleccione:', displayMember: "DESCRIPCION", valueMember: "COD_CAL", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, width: 300, height: 25, theme: tema
        });

        $("#CboApartitDe").jqxDropDownList({
            selectedIndex: 0, source: getSourcePol('wfRgEstPrev.aspx/GetCAL_VIG_POL'), placeHolder: 'Seleccione:', displayMember: "DESCRIPCION", valueMember: "COD_CAL", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, height: 25, theme: tema
        });
        var sourceTip = [{ cod: "%", val: "%" }, { cod: "SMMLV", val: "SMMLV"}];
        $("#CboTipPol").jqxDropDownList({ selectedIndex: 0, source: sourceTip, width: '70px', height: '25px', displayMember: "cod", valueMember: "val", placeHolder: 'Seleccione:', theme: tema });
        // Load the data from the Select html element.


        $("#BtnPolGuardar").jqxButton({ theme: tema, width: 100, height: 25, disabled: admPol.disabled });
        $("#BtnPolCancelar").jqxButton({ theme: tema, width: 100, height: 25 });

        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 500, width: 550,
            theme: tema
        });
        _createGrid();
        _createToolGrid();
    };

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


        addButton.jqxButton({ theme: admPol.config.theme, width: 80, height: 20, disabled: admPol.disabled });
        deleteButton.jqxButton({ theme: admPol.config.theme, width: 80, height: 20, disabled: admPol.disabled });

        reloadButton.jqxButton({ theme: admPol.config.theme, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: admPol.config.theme, width: 80, height: 20 });

        if (!admPol.disabled) {
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
            admPol.editedRows.splice(0, admPol.editedRows.length);
            //$(grid).jqxGrid({ source: getAdapter() });
            $(grid).jqxGrid("updatebounddata");
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        });

    }
    //crea Grid
    function _createGrid() {

        var getAdapter = function () {
            id = admPol.id_ep;

            var source = {
                datatype: "xml",
                datafields: [
	                { name: 'ID_EP' },
                    { name: 'ID' },
                    { name: 'COD_POL' },
                    { name: 'POR_SMMLV' },
                    { name: 'CAL_APARTIRDE' },
                    { name: 'VIGENCIA' },
                    { name: 'APARTIRDE' },
                    { name: 'TIPO' },
                    { name: 'GRUPO', type: 'string' },
                    { name: 'NOM_POL' },
                    { name: 'NOM_CALPOL' },
                    { name: 'NOM_CALVIGPOL' },
                    { name: 'DESCRIPCION' }
                 ],
                addrow: function (rowid, rowdata, position, commit) {
                    var byaRpta = {};
                    id = admPol.id_ep;
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
                    id = admPol.id_ep;
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
        	                    alert(error + jqXHR.responseText);
        	                }
        	            });
            return dataAdapter;
        }

        var cellclass = function (row, datafield, value, rowdata) {
            for (var i = 0; i < admPol.editedRows.length; i++) {
                if (admPol.editedRows[i].index == row) {
                    return "editedRow";
                }
            }
        }


        // initialize jqxGrid
        //autorowheight: true,
        $(grid).jqxGrid(
            {
                width: 1000,
                source: getAdapter(),
                theme: admPol.config.theme,
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
                  { text: 'Amparo', datafield: 'NOM_POL', width: 120, cellclassname: cellclass },
                  { text: 'Descripción', datafield: 'DESCRIPCION', width: 585, cellclassname: cellclass },
                  { text: 'Grupo', datafield: 'GRUPO', width: 70, cellclassname: cellclass },
                   { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 120, cellsrenderer: function () {
                       return "Editar";
                   }, buttonclick: function (row) {
                       oper = "edit";
                       // open the popup window when the user clicks a button.
                       editrow = row;
                       // get the clicked row's data and initialize the input fields.
                       var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                       $("#CboCodPol").val(dataRecord.COD_POL);
                       $("#txtGrupo").val(dataRecord.GRUPO);
                       $("#CboCalApartitDe").val(dataRecord.CAL_APARTIRDE);
                       $("#CboApartitDe").val(dataRecord.APARTIRDE);
                       $("#txtVig").val(dataRecord.VIGENCIA);
                       $("#CboTipPol").val(dataRecord.TIPO);
                       $("#txtPorSM").val(dataRecord.POR_SMMLV);
                       
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
        agregar: function (Cod, Nom) {
            var datarow = {};
            datarow["COD_PRO"] = Cod;
            datarow["NOMBRE_PROYECTO"] = Nom;
            var commit = $("#jqxgridProy").jqxGrid('addrow', null, datarow);
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
    admPol.config.theme = theme;
    admPol.id_ep = wizard.TxtID.val();
    admPol.Grupo = wizard.GetGrupos();
    admPol.disabled = wizard.disabled;
    admPol.init();

});