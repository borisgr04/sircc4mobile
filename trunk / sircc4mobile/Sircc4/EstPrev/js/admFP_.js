var admFP = (function () {
    var ventana = '#wFP';
    var msgPopup = "#wHFP";
    var msgPpal = "#msgFPEditar";
    var grid = "#jqxgridFP";

    var urlToInsert = "wfRgEstPrev.aspx/GuardarFP";
    var urlToGrid = "wfRgEstPrev.aspx/GetEP_FORMA_PAGO";
    var urlToUpdate = "wfRgEstPrev.aspx/GuardarModEPList";
    var urlToDelete = "wfRgEstPrev.aspx/deleteEP";


    var divBtnGrid = "#divBtnGridFP";

    //Adding event listeners
    function _addEventListeners() {

        $('#txtValUni').on('valuechanged', function (event) {
            calcularParcial();
        });
        $('#txtCan').on('valuechanged', function (event) {
            calcularParcial();
        });
        $('#txtIVA').on('valuechanged', function (event) {
            calcularParcial();
        });

        var GuardarNuevo = function () {

            //var result = $('#testForm').jqxValidator('validate');
            //alert(result);
            var row = {};
            row["DESC_ITEM"] = $('#txtDesc').val();
            row["CANT_ITEM"] = $('#txtCan').val();
            row["UNI_ITEM"] = $('#txtUni').val();
            row["VAL_UNI_ITEM"] = $('#txtValUni').val();
            row["PORC_IVA"] = $('#txtIVA').val();
            row["VAL_PAR"] = $('#txtValPar').val();
            row["GRUPO"] = $('#txtGrupo').val();


            var commit = $(grid).jqxGrid('addrow', null, row);

        }
        $("#BtnFPGuardar").click(function () {
            GuardarNuevo();

        });

        $("#BtnFPCancelar").click(function () {
            $(ventana).jqxWindow('close');
        });

        calcularParcial = function () {
            //var value = event.args.value;
            var iva = 1 + $("#txtIVA").val() / 100;
            var parcial = $('#txtValUni').val() * $('#txtCan').val() * iva;
            $('#txtValPar').val(parcial);
        }


    };

    //Creating all page elements which are jqxWidgets
    function _createElements() {
        var tema = admFP.config.theme;

        /*
        $("#txtCond").jqxInput({ placeHolder: "Descripción ", height: 100, width: 300, minLength: 1, theme: tema });

        $("#txtOrden").jqxNumberInput({ width: '100px', height: '25px',decimalDigits: 0, inputMode: 'simple', spinButtons: true, theme: tema });
        //GetTIPO_PAGO

        // Create currency Input.
        $("#txtValPag").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 9999999999, digits: 10, symbol: '$', theme: tema });
        $("#txtPorPag").jqxNumberInput({ width: '150px', height: '25px', decimalDigits: 0, min: 0, max: 99, digits: 2, symbolPosition: 'right', symbol: '%', theme: tema });
        */

        $("#CboTipPag").jqxDropDownList({
            selectedIndex: 0, source: byaPage.getSource('wfRgEstPrev.aspx/GetTIPO_PAGO'), placeHolder: 'Seleccione:', displayMember: "DES_PAGO", valueMember: "ID_PAGO", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 300, width: 300, height: 25, theme: tema
        });
        


        //var sourceTip = [{ cod: "S", val: "S" }, { cod: "N", val: "N"}];
        //$("#CboApoEnt").jqxDropDownList({ selectedIndex: 0, source: sourceTip, width: '70px', height: '25px', displayMember: "cod", valueMember: "val", placeHolder: 'Seleccione:', theme: tema });

        $("#BtnFPGuardar").jqxButton({ theme: tema, width: 100, height: 25 });
        $("#BtnFPCancelar").jqxButton({ theme: tema, width: 100, height: 25 });



        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 500, width: 550,
            theme: tema
        });
        _createGrid();
        _createToolGrid();

    };

    var actualizarGrid = function () {

        jsonData = "{'Reg':" + JSON.stringify(admFP.editedRows) + "}";

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

        //Completar Datos para mostrar en la grid   

        admFP.editedRows.splice(0, admFP.editedRows.length);
        //alert(editedRows.length);
        $(grid).jqxGrid("updatebounddata");
        //Confirmar si no hay errror
        return !byaRpta.Error;

    };

    function _createToolGrid() {
        var container = byaPage.container();
        var addButton = byaPage.addButton();
        var updButton = byaPage.updButton();
        var deleteButton = byaPage.deleteButton();
        var reloadButton = byaPage.reloadButton();
        var xlsButton = byaPage.xlsButton();

        container.append(addButton);
        container.append(deleteButton);
        container.append(updButton);
        container.append(reloadButton);
        container.append(xlsButton);

        $(divBtnGrid).append(container);
        //statusbar.append(container);
        addButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20, disabled: admFP.disabled });
        updButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20, disabled: admFP.disabled });
        deleteButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20, disabled: admFP.disabled });
        reloadButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: admFP.config.theme, width: 80, height: 20 });

        if (!admFP.disabled) {
            updButton.click(function (event) {
                actualizarGrid();
            });
            addButton.click(function (event) {
                $(ventana).jqxWindow('open');
            });

            // delete selected row.
            deleteButton.click(function (event) {
                var selectedrowindex = $("#jqxgridEFP").jqxGrid('getselectedrowindex');
                var rowscount = $("#jqxgridFP").jqxGrid('getdatainformation').rowscount;
                if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
                    var id = $("#jqxgridFP").jqxGrid('getrowid', selectedrowindex);
                    $("#jqxgridFP").jqxGrid('deleterow', id);
                }
            });
        }
        // reload grid data.
        reloadButton.click(function (event) {
            admFP.editedRows.splice(0, admFP.editedRows.length);
            $(grid).jqxGrid({ source: getAdapter() });
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', 'FormaPago_EP');
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
                    //id = TxtID.value == "" ? 0 : TxtID.value;
                    id = admFP.id_ep;
                    rowdata["ID_EP"] = id;
                    //alert(JSON.stringify(rowdata));
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

                    //Completar Datos para mostrar en la grid   
                    rowdata["ID"] = byaRpta.id;
                    //Confirmar si no hay errror
                    commit(!byaRpta.Error);

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
                    for (var i = 0; i < admFP.editedRows.length; i++) {
                        if (admFP.editedRows[i].index == rowid) {
                            admFP.editedRows[i].splice(i, 1);
                        }
                    }
                    var iva = 1 + newdata["PORC_IVA"] / 100;
                    newdata["VAL_PAR"] = newdata["CANT_ITEM"] * newdata["VAL_UNI_ITEM"] * iva;
                    admFP.editedRows.push({ index: rowindex, data: newdata });
                    commit(true);
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
        var editar = !admFP.disabled;
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
                editable: editar,
                localization: byaPage.getLocalization(),
                groupable: true,
                editmode: byaPage.editGrid,
                columns: [
                  { text: 'ORDEN', datafield: 'ORD_FPAG', width: 50, cellclassname: cellclass },
                  { text: 'TIPO DE PAGO', datafield: 'NOM_TIP_FPAG', width: 100, cellclassname: cellclass },
                  { text: 'VALOR', datafield: 'VAL_FPAG', width: 80, cellsalign: 'right', cellsformat: 'f2', columntype: 'numberinput', cellclassname: cellclass },
                  { text: 'PORCENTAJE', datafield: 'POR_FPAG', width: 70, columntype: 'numberinput', cellsalign: 'right', cellsformat: "p2", cellclassname: cellclass },
                  { text: 'CONDICIÓN', datafield: 'CON_FPAG', cellclassname: cellclass },
                  { text: 'APORTE PROPIO', datafield: 'PGEN_FPAG', width: 100, cellclassname: cellclass }
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
    admFP.disabled = wizard.disabled;

    admFP.init();
});
