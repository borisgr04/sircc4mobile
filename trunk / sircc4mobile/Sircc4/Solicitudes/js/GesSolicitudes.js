var gesSolicitudes = (function () {

    if (Principal == null) {
        console.log("Error: No existe el Objeto Principal");
    }

    var ventana = '#winNuevoEP';
    var msgPopup = "#msgConNuevoEP";
    var gridCon = '#jqxgridSol';
    var urlToGridCon = "/Servicios/wsSolicitudes.asmx/GetSolicitudes";
    var id;
    var idEP;

    var _addHandlers = function () {

        $('#BtnConsulta').click(function () {
            _createGridCon();
        });

        $('#BtnNuevo').click(function () {
            Principal.LoadPagina('/Solicitudes/RegSolicitud.htm', 'Nueva Solicitud');
        });

        $('#BtnEditar').click(function () {
            var selectedrowindex = $(gridCon).jqxGrid('getselectedrowindex');
            var dataRecord = $(gridCon).jqxGrid('getrowdata', selectedrowindex);

            var Params = {
                Cod_Sol: dataRecord.COD_SOL
            };
            Principal.Params = Params;
            Principal.LoadPagina('/Solicitudes/RegSolicitud.htm', 'Nueva Solicitud');
        });

    };
    _createElements = function () {
        tema = gesSolicitudes.config.theme;
        var sourceDep = byaPage.getSource('/Servicios/wsDatosBasicos.asmx/GetvDEPENDENCIAD');
        $("#CboDepDel").jqxDropDownList({
            source: sourceDep, placeHolder: 'Seleccione Dependecia:', displayMember: "NOM_DEP", valueMember: "COD_DEP", dropDownWidth: '600', dropDownHorizontalAlignment: 'left', width: 200, height: 25, theme: tema
        });
        $('#BtnConsulta').jqxButton({ width: 80, theme: tema });
        $('#BtnNuevo').jqxButton({ width: 80, theme: tema });
        $('#BtnEditar').jqxButton({ width: 80, theme: tema });

    }
    //Creating the demo window
    function _createWindow() {
        $(ventana).jqxWindow({ autoOpen: false,
            showCollapseButton: true, height: 300, width: 350, isModal: true,
            theme: gesSolicitudes.config.theme
        });
    };
    //crea GridTipos
    function _createGridCon() {
        var dep_sol = "'" + $("#CboDepDel").val() + "'";

        var source = {
            datatype: "xml",
            datafields: [
	                { name: 'COD_SOL' },
                    { name: 'OBJ_SOL' },
                    { name: 'COD_TPRO_NOM' },
                    { name: 'CLASE' },
                    { name: 'DEP_SOL_NOM' },
                    { name: 'NOM_ABOG_ENC' }
                 ],
            async: false,
            record: 'Table',
            url: urlToGridCon,
            data: { 'dep_psol': dep_sol }
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
                        theme: gesSolicitudes.config.theme,
                        localization: byaPage.getLocalization(),
                        height: 350,
                        sortable: true,
                        altrows: true,
                        showfilterrow: true,
                        filterable: true,
                        pageable: true,
                        enabletooltips: true,
                        columns: [
                        { text: 'Código ', datafield: 'COD_SOL', width: 150 },
                        { text: 'Objeto', datafield: 'OBJ_SOL', width: 150 },
                        { text: 'Modalidad', filtertype: 'checkedlist', datafield: 'COD_TPRO_NOM', width: 150 },
                        { text: 'Clase', filtertype: 'checkedlist', datafield: 'CLASE', width: 150 },
                        { text: 'Dependencia Solicitante', filtertype: 'checkedlist', datafield: 'DEP_SOL_NOM', width: 150 },
                        { text: 'Encargado', filtertype: 'checkedlist', datafield: 'NOM_ABOG_ENC', width: 150 }


                    ]
                    });

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
            _createElements();
            _addHandlers();
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
    gesSolicitudes.config.theme = theme;
    gesSolicitudes.init();
});