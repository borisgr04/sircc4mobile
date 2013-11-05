<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GCRegActas.aspx.cs" Inherits="Sircc4.GestionContratos.GCRegActas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--<script src="TableTools.js" type="text/javascript"></script>
    <script src="ZeroClipboard.js" type="text/javascript"></script>
--%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script src="../Scripts/DataTables/media/js/jquery.dataTables.js" type="text/javascript"></script>
    <style type="text/css" title="currentStyle">
        @import "../Scripts/DataTables/media/css/demo_page.css";
        @import "../Scripts/DataTables/media/css/demo_table_jui.css";
        @import "../Scripts/themes/smoothness/jquery-ui-1.8.4.custom.css";
    </style>
    <script type="text/javascript" src="../Scripts/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="../Scripts/jquery_bya.js" type="text/javascript"></script>
    <script type="text/javascript">
        var oTable = $('#tbl-actas');
        var oActasTable = null;
        $(function () {
            $('#dialog-confirm').css("display", "none");

            //para Configurar Si el mensaje se quita Solo
            jqbyaQuitar = false;

            function FillComboActas() {
                $.ajax({
                    type: "POST",
                    url: "GCRegActas.aspx/GetRutaEst",
                    data: "{cod_con:'" + Cod_Con + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        fillCombo($("#cboTipo"), response.d);
                    },
                    error: function (response) {
                        if (response.length != 0)
                            alert(response);
                    }
                });
            }

            function LimpiarInput() {
                $('#txtAvance').val(0);
                $('#txtNroDoc').val(0);
                $('#txtNroDoc').attr('readonly', true);
                $('#txtNVisitas').val(0);
                $("#txtObs").val('');
                $("#txtValAut").val(0);
                $('#hdID').val(0);
            }

            //Iniicalizaciones

            $("#txtTip").css("display", "none");
            $('.currency').formatCurrency();
            $("#txtFecha").datepicker();
            var tips = $(".validateTips");
            var RegActas = {};
            var Cod_Con = $.getUrlVar('CodCon');
            //Inicializa todos los elementos de clase currency 
            $('.currency').blur(function () {
                $('.currency').formatCurrency();
            });
            $(function () {
                $("#tabs").tabs({
                    collapsible: true
                });
            });
            FillComboActas();
            FillGrid();
            LimpiarInput();
            //Fin 

            function EsValidoElForm() {
                var bValid = true;
                $('#txtFecha').removeClass("ui-state-error");
                $('#txtAvance').removeClass("ui-state-error");
                $('#txtNVisitas').removeClass("ui-state-error");
                $('#txtValAut').removeClass("ui-state-error");

                bValid = bValid && checkRequired($('#txtFecha'), "Fecha", $('#lbMsgResult'));
                bValid = bValid && checkRequired($('#txtAvance'), "Avance", $('#lbMsgResult'));
                bValid = bValid && checkRequired($('#txtNVisitas'), "Número de Visitas", $('#lbMsgResult'));
                bValid = bValid && checkRequired($('#txtValAut'), "Valor de Pago a Autorizar", $('#lbMsgResult'));

                if (bValid) {
                    RegActas.Avance = $('#txtAvance').val();
                    RegActas.NVisitas = $("#txtNVisitas").val();
                    RegActas.Fecha = $("#txtFecha").val();
                    RegActas.Observacion = $("#txtObs").val();
                    RegActas.NroDoc = $("#txtNroDoc").val();
                    RegActas.EstFin = $("#cboTipo").val();
                    RegActas.CodCon = Cod_Con;
                    RegActas.Id = $('#hdID').val();
                    RegActas.ValAut = QuitarFmtNumerico($("#txtValAut").val());
                }
                return bValid;
            }

            $("#btnGuardar").click(function () {
                if (EsValidoElForm()) {
                    jsonData = "{'regActas':" + JSON.stringify(RegActas) + "}";
                    urlToHandler = 'GCRegActas.aspx/guardarActa';
                    $.ajax({
                        type: "POST",
                        url: urlToHandler,
                        data: jsonData,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var byaRpta = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
                            MsgBoxR('#lbMsgResult', byaRpta.Error, byaRpta.Mensaje);
                            if (!byaRpta.Error) {
                                FillComboActas();
                                refresh();
                                $("#tabs").tabs({ active: 1 });
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);

                        }
                    });
                }
            });

            function Anular(Ide_Acta, row) {
                jsonData = "{'Ide_Acta':" + JSON.stringify(Ide_Acta) + "}";
                urlToHandler = 'GCRegActas.aspx/anularActas';
                $.ajax({
                    type: "POST",
                    url: urlToHandler,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var byaRpta = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;
                        alert(byaRpta.Mensaje);
                        //Borra Registro
                        FillComboActas();
                        refresh();
                    },
                    error: fnError
                });
            }

            function fnError(XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }
            function Resultado(result) {
                $('#messages').html(result.d);
            }

            //-----------------------------
            $("#tbl-actas2").on("click", "#Anular", function () {
                AnularFila(this);
            });

            $("#tbl-actas2").on("click", "#Editar", function () {
                EditarFila(this);
            });

            function EditarFila(lnk) {
                var row = lnk.parentNode.parentNode;

                $("#hdID").val(row.cells[0].innerHTML);
                $("#txtTip").val(row.cells[1].innerHTML);
                $("#txtTip").css("display", "block");
                $("#cboTipo").css("display", "none");
                $("#txtNroDoc").val(row.cells[2].innerHTML);
                $("#txtFecha").val(row.cells[3].innerHTML);

                $("#txtNVisitas").val(row.cells[4].innerHTML == 'null' ? 0 : row.cells[4].innerHTML);
                $("#txtAvance").val(row.cells[5].innerHTML == 'null' ? 0 : row.cells[5].innerHTML);
                $("#txtValAut").val(row.cells[6].innerHTML == 'null' ? 0 : row.cells[6].innerHTML);
                $("#tabs").tabs({ active: 0 });

            }

            function AnularFila(lnk) {
                var row = lnk.parentNode.parentNode;
                var Id = row.cells[0].innerHTML;
                $("#dialog-confirm").dialog({
                    resizable: false,
                    height: 200,
                    modal: true,
                    buttons: {
                        "Anular": function () {
                            Anular(Id, row);
                            $(this).dialog("close");
                        },
                        "Cancelar": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }

            function FillGrid() {
                oActasTable = $('#tbl-actas2').dataTable({
                    "bJQueryUI": true,
                    "bPaginate": false,
                    "oLanguage": {
                        "sUrl": "../Styles/datatablesES.txt"
                    },
                    "bInfo": true,
                    "sAjaxSource": "grdGesActas.ashx",
                    "bProcessing": true,
                    "bServerSide": true,
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "codcon", "value": '2011020025' });
                        $.getJSON(sSource, aoData, function (json) {
                            fnCallback(json)
                        });
                    },
                    "fnFooterCallback": function (nRow, aaData, iStart, iEnd, aiDisplay) {
                        var valpagoTotal = 0;
                        /*Calculate the total for all rows, even outside this page*/
                        for (var i = 0; i < aaData.length; i++) {
                            valpagoTotal += parseFloat(aaData[i][6].replace('$', '').replace(',', ''));
                        }
                        var pageValpagoTotal = 0;
                        /*calculate totals for this page*/
                        for (var i = iStart; i < iEnd; i++) {
                            pageValpagoTotal += parseInt(aaData[aiDisplay[i]][6].replace(',', ''));
                        }
                        /*modify the footer row*/
                        var nCells = nRow.getElementsByTagName('th');
                        nCells[1].innerHTML = pageValpagoTotal;

                    }

                });

                $('.currency').formatCurrency();
            }
            //Seleccionar
            $("#tbl-actas2 tbody").click(function (event) {
                $(oActasTable.fnSettings().aoData).each(function () {
                    $(this.nTr).removeClass('row_selected');
                    //alert("Selecciona");
                });
                $(event.target.parentNode).addClass('row_selected');
            });
            function refresh() {
                if (oActasTable != null)
                    oActasTable.fnDraw();
            }

        });

        
    </script>
    <div id="dialog-confirm" title="Anular el Acta?" >
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Desea Anular el Acta?</p>
    </div>
    <h2>
        Gestión del Contrato</h2>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Registrar</a></li>
            <li><a href="#tabs-2">Historial</a></li>
        </ul>
        <div id="tabs-1">
            <input id="hdID" type="hidden" />
            
            <h2>Filtro</h2>
            <fieldset id="regFiltro">
            <legend>Registro de Datos</legend>
            <div id="lbMsgResult" >
            </div>
                <div id="form">
                <p>
                    <label>
                        Tipo <span class="small"></span>
                    </label>
                    <asp:DropDownList ID="cboTipo" ClientIDMode="Static" runat="server">
                    </asp:DropDownList>
                    </p>
                    <p>
                    <input id="txtTip" type="text" />
                    <label>
                        N° Documento <span class="small"></span>
                    </label>

                    <asp:TextBox ID="txtNroDoc" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </p>
                    <p>
                    
                    <label>
                        Fecha Documento <span class="small"></span>
                    </label>
                    <input id="txtFecha" type="text" class="required" />
                    </p>
                    <p>
                    
                    <label>
                        N° Visitas <span class="small"></span>
                    </label>
                    <asp:TextBox ID="txtNVisitas" ClientIDMode="Static" runat="server" TextMode="Number"></asp:TextBox>
                    </p>
                    <p>
                    
                    <label>
                        Valor Autorizado <span class="small"></span>
                    </label>
                    <asp:TextBox ID="txtValAut" CssClass="currency" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </p>
                    <p>
                    
                    <label>
                        % Avance Fisico <span class="small">Acumulado</span>
                    </label>
                    <asp:TextBox ID="txtAvance" runat="server" class="required" ClientIDMode="Static"></asp:TextBox>
                    </p>
                    <p>
                    
                    <label>
                        Observación <span class="small"></span>
                    </label>
                    <asp:TextBox ID="txtObs" ClientIDMode="Static" runat="server" class="required" TextMode="MultiLine"></asp:TextBox>
                    </p>
                    <p>
                    
                    <br />
                    <label>
                        Documento <span class="small"></span>
                    </label>
                    <%--<input id="flDocumento" type="file" />--%>
                </div>
                <input id="btnGuardar" type="button" value="Guardar" class="button_example" />
            </fieldset>

            
            
        </div>
        <div id="tabs-2">
            <table id="tbl-actas2" class="mGrid">
                <thead>
                    <tr>
                        <th>
                            ID
                        </th>
                        <th>
                            Documento
                        </th>
                        <th>
                            N° Documento
                        </th>
                        <th>
                            Fecha
                        </th>
                        <th>
                            N° Visitas
                        </th>
                        <th>
                            % Ejecución
                        </th>
                        <th>
                            Valor Autorizado a Pagar
                        </th>
                        <th>
                            Editar
                        </th>
                        <th>
                            Anular
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                    <tr>
                        <th style="text-align: right" colspan="6">
                            Total Autorizado:
                        </th>
                        <th class=".currency">
                        </th>
                        <th>
                        </th>
                        <th>
                        </th>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

</asp:Content>
