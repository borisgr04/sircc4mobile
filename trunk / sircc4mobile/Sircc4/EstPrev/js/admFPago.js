var admFP = (function () {

    var ventana = '#wFP';
    var msgPopup = "#msgFP";
    var msgPpal = "#msgFPEditar";
    var grid = "#jqxgridFP";

    var urlToGrid = "wfRgEstPrev.aspx/GetEP_FORMA_PAGO";

    var urlToInsert = "wfRgEstPrev.aspx/InsertFP";
    var urlToUpdate = "wfRgEstPrev.aspx/UpdateFP";
    var urlToDelete = "wfRgEstPrev.aspx/DeleteFP";

    var oper;
    var editrow = -1;
    var docExportXLS = 'FormaPago_EP';

    var divBtnGrid = "#divBtnGridFP";

    //Adding event listeners
    function _addEventListeners() {

        var GuardarNuevo = function () {
            var dataRecord = {};
            dataRecord.ORD_FPAG = $("#txtOrden").val();
            dataRecord.TIP_FPAG = $("#CboTipPag").val();
            dataRecord.VAL_FPAG = $("#txtValPag").val();
            dataRecord.POR_FPAG = $("#txtPorPag").val();
            dataRecord.CON_FPAG = $("#txtCond").val();
            dataRecord.PGEN_FPAG = $("#CboApoEnt").val();

            var commit = $(grid).jqxGrid('addrow', null, dataRecord);
        }

        var GuardarMod = function () {
            if (editrow >= 0) {

                var rowID = $(grid).jqxGrid('getrowid', editrow);
                var dataRecord = $(grid).jqxGrid('getrowdata', editrow);

                dataRecord.ORD_FPAG = $("#txtOrden").val();
                dataRecord.TIP_FPAG = $("#CboTipPag").val();
                dataRecord.VAL_FPAG = $("#txtValPag").val();
                dataRecord.POR_FPAG = $("#txtPorPag").val();
                dataRecord.CON_FPAG = $("#txtCond").val();
                dataRecord.PGEN_FPAG = $("#CboApoEnt").val();

                var commit = $(grid).jqxGrid('updaterow', rowID, dataRecord);
            }
        }
        $("#BtnFPGuardar").click(function () {
            if (oper == "add") {
                GuardarNuevo();
            }
            if (oper == "edit") {
                GuardarMod();
            }
        });
        $("#txtValPag").on('change', function () {
            var por = $("#txtValPag").val() / admFP.getValTotal()*100;
            $("#txtPorPag").val(por);
        });
        $("#txtValPag").on('keypress', function () {
            
            var por = $("#txtValPag").val() / admFP.getValTotal();
            $("#txtPorPag").val(por);

        });
        



        $("#BtnFPCancelar").click(function () {
            editrow = -1;
            $(ventana).jqxWindow('close');
        });

    };

    //Creating all page elements which are jqxWidgets
    function _createElements() {


        var tema = admFP.config.theme;

        $("#txtCond").jqxInput({ placeHolder: "Descripción ", height: 100, width: 300, minLength: 1, theme: tema });

        $("#txtOrden").jqxNumberInput({ width: '100px', height: '25px', min: 0, decimalDigits: 0, inputMode: 'simple', spinButtons: true, theme: tema });
        //GetTIPO_PAGO

        // Create currency Input.
        $("#txtValPag").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 9999999999999999999, digits: 20, symbol: '$', theme: tema });
        $("#txtPorPag").jqxNumberInput({ width: '150px', height: '25px', decimalDigits: 0, min: 0, max: 99, digits: 2, symbolPosition: 'right', symbol: '%', theme: tema });


        $("#CboTipPag").jqxDropDownList({
            selectedIndex: 0, source: byaPage.getSource('wfRgEstPrev.aspx/GetTIPO_PAGO'), placeHolder: 'Seleccione:', displayMember: "DES_PAGO", valueMember: "ID_PAGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, width: 300, height: 25, theme: tema
        });



        var sourceTip = [{ cod: "S", val: "S" }, { cod: "N", val: "N"}];
        $("#CboApoEnt").jqxDropDownList({ selectedIndex: 0, source: sourceTip, width: '70px', height: '25px', displayMember: "cod", valueMember: "val", placeHolder: 'Seleccione:', theme: tema });

        $("#BtnFPGuardar").jqxButton({ theme: tema, width: 100, height: 25 });
        $("#BtnFPCancelar").jqxButton({ theme: tema, width: 100, height: 25 });

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


        addButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20, disabled: admFP.disabled });
        deleteButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20, disabled: admFP.disabled });

        reloadButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20 });

        if (!admFP.disabled) {
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
            admFP.editedRows.splice(0, admFP.editedRows.length);
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
            id = admFP.id_ep;

            var source = {
                datatype: "xml",
                datafields: [
	                { name: 'ID_EP' },
                    { name: 'ID' },
                    { name: 'ORD_FPAG', type: 'number' },
                    { name: 'TIP_FPAG', type: 'string' },
                    { name: 'VAL_FPAG', type: 'float' },
                    { name: 'POR_FPAG', type: 'number' },
                    { name: 'CON_FPAG', type: 'string' },
                    { name: 'PGEN_FPAG', type: 'string' },
                    { name: 'NOM_TIP_FPAG', type: 'string' }

                 ],
                addrow: function (rowid, rowdata, position, commit) {
                    var byaRpta = {};
                    id = admFP.id_ep;
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
                        alert(urlToDelete);
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
                    id = admFP.id_ep;
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
            for (var i = 0; i < admFP.editedRows.length; i++) {
                if (admFP.editedRows[i].index == row) {
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
                theme: admFP.config.theme,
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
                  { text: 'ORDEN', datafield: 'ORD_FPAG', width: 50, cellclassname: cellclass },
                  { text: 'TIPO DE PAGO', datafield: 'NOM_TIP_FPAG', width: 100, cellclassname: cellclass },
                  { text: 'VALOR', datafield: 'VAL_FPAG', width: 80, cellsalign: 'right', cellsformat: 'f2', columntype: 'numberinput', cellclassname: cellclass },
                  { text: 'PORCENTAJE', datafield: 'POR_FPAG', width: 70, columntype: 'numberinput', cellsalign: 'right', cellsformat: "p2", cellclassname: cellclass },
                  { text: 'CONDICIÓN', datafield: 'CON_FPAG', cellclassname: cellclass },
                  { text: 'APORTE PROPIO', datafield: 'PGEN_FPAG', width: 100, cellclassname: cellclass },
                   { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 120, cellsrenderer: function () {
                       return "Editar";
                   }, buttonclick: function (row) {
                       oper = "edit";
                       // open the popup window when the user clicks a button.
                       editrow = row;
                       // get the clicked row's data and initialize the input fields.
                       var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                       $("#txtOrden").val(dataRecord.ORD_FPAG);
                       $("#CboTipPAg").val(dataRecord.TIP_FPAG);
                       $("#txtValPag").val(dataRecord.VAL_FPAG);
                       $("#txtPorPag").val(dataRecord.POR_FPAG);
                       $("#txtCond").val(dataRecord.CON_FPAG);
                       $("#CboApoEnt").val(dataRecord.PGEN_FPAG);

                       alert($("#CboTipPag").jqxDropDownList('disabled'));

                       // show the popup window.
                       $(ventana).jqxWindow('open');
                   }
                   }

                ]
            });
    }

    return {
        disabled: null,
        valOtros: null,
        valProp: null,
        id_ep: null,
        Grupo: null,
        editedRows: null,
        config: {
            dragArea: null,
            theme: null
        },
        getValTotal: function () {
            return this.valOtros + this.valProp;
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

    admFP.config.theme = theme;
    admFP.id_ep = wizard.TxtID.val();
    admFP.Grupo = wizard.GetGrupos();
    admFP.disabled = wizard.disabled;
    admFP.valOtros = wizard.GetValOtros();
    admFP.valProp = wizard.GetValProp();
    admFP.init();

});