<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="conEP.aspx.cs" Inherits="Sircc4.EPConsultas.conEP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #txtNomFunEP
        {
            width: 266px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/jqwidgets/styles/jqx.base.css")%>" type="text/css" />
    <script type="text/javascript" src="<%= ResolveUrl("~/jqscripts/gettheme.js")%>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/jqscripts/jquery-1.10.1.min.js")%>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/jqwidgets/jqxcore.js")%>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/jqwidgets/jqxnavigationbar.js")%>"></script>
    <script type="text/javascript" src="../jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.aggregates.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxinput.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxcalendar.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdatetimeinput.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxnumberinput.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxwindow.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxexpander.js"></script>
    <script src="js/ModalTer.js" type="text/javascript"></script>
    <script src="../jqwidgets/globalization/globalize.js" type="text/javascript"></script>
    <script src="../jqwidgets/globalization/globalize.culture.es-CO.js" type="text/javascript"></script>
    <script src="../jqscripts/gettheme.js" type="text/javascript"></script>
    <%--<link href="../jqwidgets/styles/jqx.bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <script src="../jqscripts/jquery_ext.js" type="text/javascript"></script>
    <script src="../jqscripts/bya_Page.js" type="text/javascript"></script>

    <script type="text/javascript">
        var theme;
        $(document).ready(function () {
            $.data(document.body, 'theme', 'bootstrap');
            theme = getDemoTheme();
            var url = "../jqwidgets/styles/jqx." + theme + '.css';
            alert(url);
            $(document).find('head').append('<link rel="stylesheet" href="' + url + '" media="screen" />');
            // Create jqxNavigationBar.
            //$("#jqxNavigationBar").jqxNavigationBar({ width: 200, expandMode: 'multiple', expandedIndexes: [2, 3], theme: theme });
        });
    </script>
    
    <script src="js/admConEP.js" type="text/javascript"></script>
    
    <div class="formConEP">
        <div id="msgResult">
        </div>
        <div class="filaform">
            <label>
                N° Estudio Previo
            </label>
            <input type="text" id="txtNroEP" />
        </div>
        <div class="filaform">
            <label>
                Objeto
            </label>
            <input type="text" id="txtObjEP" />
        </div>
        <div class="filaform">
            <label>
                Vigencia
            </label>
            <div id='CmbVigEP' >
            </div>
        </div>
        <div id="jqxExpander">
            <div>
                Busqueda Avanzada</div>
            <div>
                <div class="filaform">
                    <label>
                        Fecha entre
                    </label>
                    <div id='txtFecEla'>
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        Valor Desde
                    </label>
                    <div style='margin-top: 3px; float: left;' id='txtValIniEP'>
                    </div>
                    <span class="spanlabel">Hasta </span>
                    <div style='margin-top: 3px;' id='txtValFinEP'>
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        Tipo Cto
                    </label>
                    <div id="cmbTipConEP" class="cmbo">
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        SubTipo Cto
                    </label>
                    <div id="cmbSTipConEP" class="cmbo">
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        Modalidad
                    </label>
                    <div id="cmbModconEP" class="cmbo">
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        Estado
                    </label>
                    <div id="CmbEstEP" class="cmbo">
                    </div>
                </div>
                <div class="filaform">
                    <label>
                        Dependencia
                    </label>
                    <div id="cmbDepEP" class="cmbo">
                    </div>
                </div>
                <%--<div class="filaform">
            <label>
                Nro Proyecto
            </label>
            <input type="text" id="txtNumProEp" />
        </div>

        <div class="filaform">
            <label>
                Nro CDP
            </label>
            <input type="text" id="txtNroCDPEP" />
        </div>

        <div class="filaform">
            <label>
               Rubro
            </label>
            <input type="text" id="TtxtRubEP" />
        </div>--%>
                <div class="filaform">
                    <label>
                        Creado Por
                    </label>
                    <input type="text" id="txtIdFunEP" class="txtInp" />
                    <input id="btnBuscarFunEP" type="button" value="" class="button_buscar" title="Abrir Cuadro de Busqueda" />
                    <input type="text" id="txtNomFunEP" class="txtInp" />
                </div>
                <%-- <div class="filaform">
            <label>
             Necesidad
            </label>
            <input type="text" id="txtNeceEP" />
        </div>--%>
            </div>
        </div>
        <div class="filaformBtn" id="divBotones">
        </div>
    </div>
    <div id="jqxgridResult">
    </div>
    <div id="divBtnGridConEP">
    </div>
    <!--VENTANA CONSULTA DE TERCEROS  -->
    <div id="winTer">
        <div id="winHTer">
            <span>Listado de Terceros </span>
        </div>
        <div style="overflow: hidden;" id="wConTer">
            <div id="msgTer" class="information">
                Hacer click para seleccionar el funcionario</div>
            <div>
                <div id="jqxgridTer">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
