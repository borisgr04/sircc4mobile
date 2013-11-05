        var admEspTec = (function () {
            var ventana = '#wEspTec';
            var msgEP = "#msgEP";
            var grid = "#jqxgridEspTec";
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

                var Validar = function () {
                    var row = {};
                    row["DESC_ITEM"] = $('#txtDesc').val();
                    row["CANT_ITEM"] = $('#txtCan').val();
                    row["UNI_ITEM"] = $('#txtUni').val();
                    row["VAL_UNI_ITEM"] = $('#txtValUni').val();
                    row["PORC_IVA"] = $('#txtIVA').val();
                    row["VAL_PAR"] = $('#txtValPar').val();
                    return row;
                }
                $("#BtnEPGuardar").click(function () {
                    //alert($(msgEP).text);
                    var datarow = Validar();
                    var commit = $(grid).jqxGrid('addrow', null, datarow);
                });

                $("#BtnEPCancelar").click(function () {

                    $(ventana).jqxWindow('close');
                });
                $("#BtnGuardarMod").click(function () {
                    actualizarGrid();
                });

                calcularParcial = function () {
                    //var value = event.args.value;
                    var iva = 1 + $("#txtIVA").val() / 100;
                    var parcial = $('#txtValUni').val() * $('#txtCan').val() * iva;
                    $('#txtValPar').val(parcial);
                }

                var actualizarGrid = function () {


                    //id = TxtID.value == "" ? 0 : TxtID.value;
                    jsonData = "{'Reg':" + JSON.stringify(editedRows) + "}";
                    msg = $('#msgEPEditar');
                    //msg.html(JSON.stringify(editedRows));
                    //alert(JSON.stringify(editedRows));
                    urlToHandler = 'wfRgEstPrev.aspx/GuardarModEPList';
                    $.ajax({
                        type: "POST",
                        url: urlToHandler,
                        data: jsonData,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (result) {
                            byaRpta = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
                            msg = $('#msgEPEditar');
                            //alert(byaRpta.Mensaje);
                            msg.html(byaRpta.Mensaje);
                            if (!byaRpta.Error) {
                                msg.removeClass('error');
                                msg.addClass('informacion');
                            }
                            else {
                                msg.removeClass('informacion');
                                msg.addClass('error');
                            }

                        },
                        error: function (jqXHR, status, error) {
                            alert(error + "-" + jqXHR.responseText);
                        }
                    });
                    //Completar Datos para mostrar en la grid   

                    editedRows.splice(0, editedRows.length);
                    //alert(editedRows.length);
                    $(grid).jqxGrid("updatebounddata");
                    //Confirmar si no hay errror
                    return !byaRpta.Error;

                }

            };

            //Creating all page elements which are jqxWidgets
            function _createElements() {
                var tema = admEspTec.config.theme;

                $("#txtDesc").jqxInput({ placeHolder: "Descripción ", height: 100, width: 300, minLength: 1, theme: tema });
                $("#txtCan").jqxNumberInput({ width: '100px', height: '25px', min: 0, theme: tema });
                $("#txtUni").jqxInput({ placeHolder: "Unidad", height: 25, width: 100, minLength: 1, theme: tema });
                // Create currency Input.
                $("#txtValUni").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 9999999999, digits: 10, symbol: '$', theme: tema });
                $("#txtIVA").jqxNumberInput({ width: '150px', height: '25px', decimalDigits: 0, min: 0, max: 99, digits: 2, symbolPosition: 'right', symbol: '%', theme: tema });
                $("#txtValPar").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 9999999999, readOnly: true, digits: 10, symbol: '$', theme: tema });

                //BtnEPGuardar
                /*var container = $("<div style='overflow: hidden; position: relative; margin: 5px; text-align:center'></div>");
                var addButton = $("<div style='float: right; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='../jqwidgets/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>Adicionar</span></div>");
                var deleteButton = $("<div style='float: right; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='../jqwidgets/images/close.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>Eliminar</span></div>");
                container.append(addButton);
                container.append(deleteButton);
                addButton.jqxButton({ theme: tema, width: 80, height: 20 });
                deleteButton.jqxButton({ theme: tema, width: 80, height: 20 });
                $("#divBotones").append(container);*/

                $("#BtnEPGuardar").jqxButton({ theme: tema, width: 100, height: 25 });
                $("#BtnEPCancelar").jqxButton({ theme: tema, width: 100, height: 25 });



                $(ventana).jqxWindow({ autoOpen: false,
                    showCollapseButton: true, maxHeight: 600, maxWidth: 650, minHeight: 300, minWidth: 200, height: 500, width: 550,
                    theme: tema
                });


                _createGrid();
            };



            //crea GridTipos
            function _createGrid() {
                //id = TxtID.value == "" ? 0 : TxtID.value;
                id = "4";
                //alert(id);
                var source = {
                    datatype: "xml",
                    datafields: [
	                { name: 'ID_EP' },
                    { name: 'ID' },
                    { name: 'DESC_ITEM', type: 'string' },
                    { name: 'CANT_ITEM', type: 'number' },
                    { name: 'UNI_ITEM', type: 'string' },
                    { name: 'VAL_UNI_ITEM', type: 'number' },
                    { name: 'PORC_IVA', type: 'number' },
                    { name: 'VAL_PAR', type: 'number' }
                 ],
                    addrow: function (rowid, rowdata, position, commit) {
                        var byaRpta = {};
                        //id = TxtID.value == "" ? 0 : TxtID.value;
                        id = "4";
                        rowdata["ID_EP"] = id;
                        //alert(JSON.stringify(rowdata));
                        jsonData = "{'Reg':" + JSON.stringify(rowdata) + "}";
                        urlToHandler = 'wfRgEstPrev.aspx/GuardarEP';
                        $.ajax({
                            type: "POST",
                            url: urlToHandler,
                            data: jsonData,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: false,
                            success: function (result) {
                                byaRpta = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
                                msg = $('#msgEP');
                                alert(byaRpta.Mensaje);
                                msg.html(byaRpta.Mensaje);
                                if (!byaRpta.Error) {
                                    msg.removeClass('error');
                                    msg.addClass('informacion');
                                }
                                else {
                                    msg.removeClass('informacion');
                                    msg.addClass('error');
                                }
                            },
                            error: function (jqXHR, status, error) {
                                alert(error + "-" + jqXHR.responseText);
                            }
                        });
                        //Completar Datos para mostrar en la grid   
                        rowdata["ID"] = byaRpta.id;
                        //Confirmar si no hay errror
                        commit(!byaRpta.Error);

                    },
                    deleterow: function (rowid, commit) {
                        // synchronize with the server - send delete command
                        //call commit with parameter true if the synchronization with the server is successful 
                        //and with parameter false if the synchronization failed.
                        alert("Falta Implementar sincronización con base de datos");
                        commit(true);
                    },
                    updaterow: function (rowid, newdata, commit) {
                        var rowindex = $(grid).jqxGrid('getrowboundindexbyid', rowid);
                        //alert(editedRows.length);
                        for (var i = 0; i < editedRows.length; i++) {
                            if (editedRows[i].index == rowid) {
                                //alert("Exite!!.. hay que borrarlo");
                                editedRows[i].splice(i,1);
                            }
                        }
                        //alert(editedRows.length);
                        editedRows.push({ index: rowindex, data: newdata });
                        commit(true);
                        /*
                        //id = TxtID.value == "" ? 0 : TxtID.value;
                        jsonData = "{'Reg':" + JSON.stringify(newdata) + "}";
                        urlToHandler = 'wfRgEstPrev.aspx/GuardarModEP';
                        $.ajax({
                        type: "POST",
                        url: urlToHandler,
                        data: jsonData,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (result) {
                        byaRpta = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
                        msg = $('#msgEPEditar');
                        //alert(byaRpta.Mensaje);
                        msg.html(byaRpta.Mensaje);
                        if (!byaRpta.Error) {
                        msg.removeClass('error');
                        msg.addClass('informacion');
                        }
                        else {
                        msg.removeClass('informacion');
                        msg.addClass('error');
                        }
                                
                        },
                        error: function (jqXHR, status, error) {
                        alert(error + "-" + jqXHR.responseText);
                        }
                        });
                        //Completar Datos para mostrar en la grid   
                        
                        //Confirmar si no hay errror
                        commit(!byaRpta.Error);
                        */
                    },
                    async: false,
                    record: 'Table',
                    url: 'wfRgEstPrev.aspx/GetEP_ESPTEC',
                    data: { 'id': id }
                };
                var dataAdapter = new $.jqx.dataAdapter(source,
        	            {
        	                contentType: 'application/json; charset=utf-8',
        	                loadError: function (jqXHR, status, error) {
        	                    alert(error + jqXHR.responseText);
        	                }
        	            });

                //var dataAdapter = new $.jqx.dataAdapter(source);
                var cellsrenderer = function (row, columnfield, value, defaulthtml, columnproperties) {
                    if (value < 20) {
                        return '<span style="margin: 4px; float: ' + columnproperties.cellsalign + '; color: #ff0000;">' + value + '</span>';
                    }
                    else {
                        return '<span style="margin: 4px; float: ' + columnproperties.cellsalign + '; color: #008000;">' + value + '</span>';
                    }
                }
                var cellclass = function (row, datafield, value, rowdata) {
                    for (var i = 0; i < editedRows.length; i++) {
                        if (editedRows[i].index == row) {
                            return "editedRow";
                        }
                    }
                }
                // initialize jqxGrid

                //autorowheight: true,

                $("#jqxgridEspTec").jqxGrid(
            {
                width: 800,
                source: dataAdapter,
                theme: admEspTec.config.theme,
                sortable: true,
                altrows: true,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                enabletooltips: true,
                showstatusbar: true,
                autoheight: true,
                editable: true,
                localization: byaPage.getLocalization(),
                editmode: byaPage.editGrid,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='../jqwidgets/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>Adicionar</span></div>");
                    var deleteButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='../jqwidgets/images/close.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>Eliminar</span></div>");
                    container.append(addButton);
                    container.append(deleteButton);
                    statusbar.append(container);
                    addButton.jqxButton({ theme: admEspTec.config.theme, width: 80, height: 20 });
                    deleteButton.jqxButton({ theme: admEspTec.config.theme, width: 80, height: 20 });

                    addButton.click(function (event) {
                        $(ventana).jqxWindow('open');
                    });
                    // delete selected row.
                    deleteButton.click(function (event) {
                        var selectedrowindex = $("#jqxgridEspTec").jqxGrid('getselectedrowindex');
                        var rowscount = $("#jqxgridEspTec").jqxGrid('getdatainformation').rowscount;
                        if (selectedrowindex >= 0 && selectedrowindex < rowscount) {
                            var id = $("#jqxgridEspTec").jqxGrid('getrowid', selectedrowindex);
                            $("#jqxgridEspTec").jqxGrid('deleterow', id);
                        }
                    });
                },
                columns: [
                  { text: 'Descripción', datafield: 'DESC_ITEM', width: 300, cellclassname: cellclass },
                  { text: 'Cantidad', datafield: 'CANT_ITEM', width: 80, cellsalign: 'right', cellsformat: 'f2', columntype: 'numberinput', cellclassname: cellclass },
                  { text: 'Unidad', datafield: 'UNI_ITEM', width: 100, columntype: 'combobox', cellclassname: cellclass },
                  { text: 'Valor Unitario', datafield: 'VAL_UNI_ITEM', width: 100, columntype: 'numberinput', cellsalign: 'right', cellsformat: 'c2', cellclassname: cellclass },
                  { text: '% IVA', datafield: 'PORC_IVA', width: 70, columntype: 'numberinput', cellsalign: 'right', cellsformat: "p2", cellclassname: cellclass },
                  { text: 'Valor Parcial', datafield: 'VAL_PAR', width: 150, cellsalign: 'right', cellsformat: 'c2', editable: false, cellclassname: cellclass }
                ]

                //{ text: 'Código', datafield: 'ID', width: 150 },
            });
                //Manejo de Captura de Datos del GRid 


            }

            return {
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
                    //Creating all jqxWindgets except the window
                    _createElements();
                    //Attaching event listeners
                    _addEventListeners();
                    //Adding jqxWindow
                    //_createWindow();
                }
            };
        } ());
    