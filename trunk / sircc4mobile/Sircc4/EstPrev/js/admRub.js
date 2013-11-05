var admRub = (function () {
//CDP
	var ventana = '#wadmRub';
    var grid = "#jqxgridRub";
	
	var msgPopup = "#msgRub";
    var msgPpal = "#msgRubEditar";

    var oper;
    var editrow = -1;
    var docExportXLS = 'Rubros_EP';

    var urlToInsert = "wfRgEstPrev.aspx/InsertRub";
    var urlToUpdate = "wfRgEstPrev.aspx/UpdateRub";
    var urlToDelete = "wfRgEstPrev.aspx/deleteRub";

    var urlToGrid = "wfRgEstPrev.aspx/GetEP_RUBROS_CDP";
	
    var divBtnGrid = "#divBtnGridRub";
	
	function _addEventListeners() {

        var GuardarNuevo = function () {
            var dataRecord = {};
			dataRecord.COD_RUB = $("#txtCodRub").val();
            dataRecord.DES_RUB = $("#txtDesRub").val();
            dataRecord.VALOR = $("#txtValRub").val();
			
			dataRecord.ID_EP= admRub.id_ep;
			dataRecord.ID_EP_CDP = admRub.id_ep_cdp;
			dataRecord.NRO_CDP = admCDP.NRO_CDP;
			dataRecord.VIG_CDP = admCDP.VIG_CDP;
			
			
					
					
            var commit = $(grid).jqxGrid('addrow', null, dataRecord);
        }

        var GuardarMod = function () {
            if (editrow >= 0) {

            var rowID = $(grid).jqxGrid('getrowid', editrow);
            var dataRecord = $(grid).jqxGrid('getrowdata', editrow);

            dataRecord.COD_RUB = $("#txtCodRub").val();
            dataRecord.NOM_RUB = $("#txtDesRub").val();
            dataRecord.VALOR = $("#txtValRub").val();
			
			dataRecord.ID_EP= admRub.id_ep;
			dataRecord.ID_EP_CDP = admRub.id_ep_cdp;
			dataRecord.NRO_CDP = admCDP.NRO_CDP;
			dataRecord.VIG_CDP = admCDP.VIG_CDP;
			
			

            var commit = $(grid).jqxGrid('updaterow', rowID, dataRecord);
            }
        }
        $("#BtnEPGuardarRub").click(function () {
            if (oper == "add") {
                GuardarNuevo();
            }
            if (oper == "edit") {
                GuardarMod();
            }
        });

        $("#BtnEPCancelarRub").click(function () {
            editrowRub = -1;
            $(ventana).jqxWindow('close');
        });

    };
	
    function _createElements() {
        var tema = admRub.config.theme;

		//RUB
		$("#txtCodRub").jqxInput({ width: '200px', height: '25px',  theme: tema });
        $("#txtDesRub").jqxInput({ width: '300px', height: '25px',  theme: tema });
        $("#txtValRub").jqxNumberInput({ width: '150px', height: '25px', min: 0, max: 999999999999, digits: 10, symbol: '$', theme: tema });
        $("#BtnEPGuardarRub").jqxButton({ theme: tema, width: 100, height: 25, disabled:admRub.disabled });
        $("#BtnEPCancelarRub").jqxButton({ theme: tema, width: 100, height: 25 });
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, maxHeight: 400, maxWidth: 650, minHeight: 300, minWidth: 200, height: 300, width: 550,
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

        var tema = admRub.config.theme;
        var disabled = admRub.disabled;
        addButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: disabled });
        deleteButton.jqxButton({ theme: tema, width: 80, height: 20, disabled: disabled });

        reloadButton.jqxButton({ theme: tema, width: 80, height: 20 });
        xlsButton.jqxButton({ theme: tema, width: 80, height: 20 });


        addButton.click(function (event) {
            if (!disabled) {
                oper = "add";
                abrirVentana();
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
            admRub.editedRows.splice(0, admRub.editedRows.length);
            //$(grid).jqxGrid({ source: getAdapter() });
            $(grid).jqxGrid("updatebounddata");
        });
        xlsButton.click(function (event) {
            $(grid).jqxGrid('exportdata', 'xls', docExportXLS);
        });
    }
	
	var getAdapterRub = function () {
            var source = {
                datatype: "xml",
                datafields: [
	                { name: 'ID_EP' },
					{ name: 'ID_EP_CDP' },
					{ name: 'NRO_CDP' },
					{ name: 'VIG_CDP' },
                    { name: 'ID' },
					{ name: 'COD_RUB', type: 'string' },
					{ name: 'NOM_RUB', type: 'string' },
                    { name: 'VALOR', type: 'number' }
                 ],
                addrow: function (rowid, rowdata, position, commit) {
                    var byaRpta = {};
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
                    
					var rowdata = $(grid).jqxGrid('getrowdatabyid', rowid);
                    var byaRpta = {};
                    
                    jsonData = "{'Reg':" + JSON.stringify(newdata) + "}";
                    alert(jsonData);
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
                data: { 'id_ep_cdp': admRub.id_ep_cdp }
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

	function abrirVentana(){
					/*var offset = $(grid).offset();
					$(ventana).jqxWindow({ position: { x: parseInt(offset.left) + 60, y: parseInt(offset.top) + 60 } });*/
                    $(ventana).jqxWindow('open');
	}
	function _createGrid() {
        //autorowheight: true,
        $(grid).jqxGrid(
            {
				source: getAdapterRub(),
                width: 1000,
                theme: admRub.config.theme,
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
                  { text: 'Rubro', datafield: 'COD_RUB', width: 280},
                  { text: 'Descripción ', datafield: 'NOM_RUB', width: 370},
				  { text: 'Valor', datafield: 'VALOR', width: 120, columntype: 'numberinput', cellsformat: 'c2',cellsalign: 'right'},
                  { text: 'Edit', datafield: 'Edit', columntype: 'button', width: 120, cellsrenderer: function () {
                       return "Editar";
                   }, buttonclick: function (row) {
                       oper = "edit";

                       // open the popup window when the user clicks a button.
                       editrow = row;
                       // get the clicked row's data and initialize the input fields.
                       var dataRecord = $(grid).jqxGrid('getrowdata', editrow);
                       
						$("#txtCodRub").val(dataRecord.COD_RUB);
						$("#txtDesRub").val(dataRecord.NOM_RUB);
						$("#txtValRub").val(dataRecord.VALOR);
                       // show the popup window.
					   abrirVentana();
                   }
                   }
                ]
            });
    }
	
	 return {
        disabled: null,
        id_ep: null,
		id_ep_cdp: null,
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
	
	} 
	());


$(function () {
	admRub.config.theme = theme;
	admRub.id_ep_cdp = admCDP.id_ep_cdp;
	admRub.id_ep = wizard.TxtID.val();
    admRub.Grupo = wizard.GetGrupos();
    admRub.disabled = wizard.disabled;
    admRub.init();
});
