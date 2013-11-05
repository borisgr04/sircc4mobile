<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfRgEstPrev.aspx.cs" Inherits="Sircc4.EstPrev.wfRgEstPrev"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIRCC - ESTUDIOS PREVIOS</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/EstiloFormulario.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="../jqscripts/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxwindow.js"></script>
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
    <script type="text/javascript" src="../jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxtabs.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxinput.js"></script>
    <script type="text/javascript" src="../jqscripts/gettheme.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxnumberinput.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxcombobox.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxValidator.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.grouping.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxgrid.export.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxDateTimeInput.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxcalendar.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxtooltip.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxDropDownButton.js"></script>
    <script type="text/javascript" src="../jqwidgets/jqxTree.js"></script>
    <script src="../jqscripts/bya_Page.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jqscripts/gettheme.js"></script>
    <script src="../jqwidgets/globalization/globalize.js" type="text/javascript"></script>
    <script src="../jqwidgets/globalization/globalize.culture.es-CO.js" type="text/javascript"></script>
    <script src="../jqscripts/jquery_ext.js" type="text/javascript"></script>
    <script src="../jqscripts/byaSite.js" type="text/javascript"></script>
    <style>
        textarea
        {
            padding: 10px;
            width: 97%;
            min-height: 300px;
        }
        .sectionButtonsWrapper
        {
            float: right;
            margin-top: 30px;
            margin-right: 10px;
            width: 115px;
        }
        .backButton
        {
            float: left;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    SIRCC - Diligenciamiento de Estudios Previos
                </h1>
            </div>
            <div class="loginDisplay">
                <div>
                    Bienvenido <span id="HeadLoginName">Usuario</span>! <span class="bold">[ <a href="/">
                        Log Out</a> ]</span>
                </div>
            </div>
            <div class="clear hideSkiplink">
                <%--<asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home" />
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="About" />
                    </Items>
                </asp:Menu>--%>
            </div>
        </div>
        <div id="main" style="padding: 30px">
            <form id="FrmEstPrev" runat="server">
            <asp:HiddenField ID="HdUser" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="HdVig" ClientIDMode="Static" runat="server" />
            <hr />
            <div id="divToolElab">
            </div>
            <div id="divToolRevApr">
            </div>
            <hr />
            <div id="LbMsg">
            </div>
            <div id='jqxTabs'>
                <ul>
                    <li>1. Datos Iniciales</li>
                    <li title="Descripción de la Necesidad">2. Necesidad </li>
                    <li title="Objeto a Contratar  y Descripción del Objeto a Contratar">3. Objeto </li>
                    <li title="Proyecto y Plan de Compras">4. Proy. /P.Compras</li>
                    <li title="Especificaciones Técnicas, Caracteristicas del Bien, Obra y/o Servicio">5.
                        Especificaciones </li>
                    <li title="Plazo de Ejecución y Lugar de Ejecución">6. Plazo y Lugar </li>
                    <li>7. Oblig. Ctatista </li>
                    <li>8. Oblig. Entidad </li>
                    <li>9. Fund. Juridicos</li>
                    <li>10. Presupuesto </li>
                    <li>11. Forma de Pago</li>
                    <li>12. Mod. de Selección</li>
                    <li>13. Capacidad Juridica</li>
                    <li title="Experiencia y Capacidad Residual">14. Exp. y C. Residual</li>
                    <li>15. Polizas </li>
                    <li>16. Municipio </li>
                </ul>
                <div class="section" id="content1">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Estado:
                            </label>
                            <div id='CboEstEP' title="Estado del Estudio Previo.">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Vigencia:
                            </label>
                            <div id='CboVig' title="Vigencia del Estudio Previo.">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Fecha de elaboración :</label>
                            <div id='TxtFecElab'>
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Funcionario que diligencia :</label>
                            <div id='TxtIdeFun' style="float: left">
                            </div>
                            <input id="btnBuscarC" type="button" value="" class="button_buscar" title="Abrir Cuadro de Busqueda" />
                            <input id="TxtNomFun" type="text" />
                        </div>
                        <div class="filaform">
                            <label>
                                Cargo :</label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboCarDilJq" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Dependencia solicitante:</label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboDepSol" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Responsable
                            </label>
                            <div id='TxtIdeRes' style="float: left">
                            </div>
                            <input id="btnBuscarR" type="button" value="" class="button_buscar" title="Abrir Cuadro de Busqueda" />
                            <input id="TxtNomRes" type="text" />
                        </div>
                        <div class="filaform">
                            <label>
                                Cargo del Responsable
                            </label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboCarRes" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Apoyo Técnico
                            </label>
                            <div id='TxtIdeApoTec' style="float: left">
                            </div>
                            <input id="btnBuscarApoTec" type="button" value="" class="button_buscar" title="Abrir Cuadro de Busqueda" />
                            <input id="TxtNomApoTec" type="text" />
                        </div>
                        <div class="filaform">
                            <label>
                                Cargo
                            </label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboCarApoTec" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Dependencia Supervisión
                            </label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboDepSup" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Cargo del Funcionario Supervisor</label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboCarSup" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Dependencia Delegada:</label>
                            <div style="font-size: 12px; font-family: Verdana;" id="CboDepDel" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Identificación del Contrato:</label>
                            <div style="font-size: 12px; font-family: Verdana; float: left" id="CboTip" class="cbo">
                            </div>
                            <div style="font-size: 12px; font-family: Verdana; padding-left: 10px;" class="cbo"
                                id="CboSubTip">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Valor a Contratar:
                            </label>
                            <div id='txtValTot' title="Especifique El Valor Total a Contratar.">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Aportes Entidad:
                            </label>
                            <div id='txtValProp' title="Valor aportado por al entidad, equivalente el presupuesto oficial.">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Aportes Otros:
                            </label>
                            <div id='txtValOtros' title="Valor aportado por otra entidad">
                            </div>
                        </div>
                        <hr />
                        <div class="filaform">
                            <label>
                                Cantidad de Grupos:
                            </label>
                            <div id='TxtGrupos' title="Especifique la cantidad de grupo del contrato, si no es por grupo deje el valor en 0.">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Nro Empleos Directos:
                            </label>
                            <div id='TxtNEmpleos' title="Especifique la cantidad de empleos directos a generar en el Contrato.">
                            </div>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton1" />
                    </div>
                </div>
                <div class="section" id="content2">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Descripción de la necesidad</label>
                            <asp:TextBox ID="TxtDesNec" ClientIDMode="Static" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton2" />
                    </div>
                </div>
                <div class="section" id="content3">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Descripción del objeto a contratar:</label>
                            <asp:TextBox ID="TxtDesObj" ClientIDMode="Static" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="filaform">
                            <label>
                                Objeto a Contratar</label>
                            <asp:TextBox ID="TxtObjCon" runat="server" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton3" />
                    </div>
                </div>
                <div class="section" id="content4">
                    Cargando Proyecto....
                </div>
                <div class="section" id="content5">
                    Cargando Especificaciones Técnicas....
                </div>
                <div class="section" id="content6">
                    <div class="formC">
                        <div class="filaform" style="margin-top: 10px;">
                            <label>
                                Plazo de ejecución del contrato:</label>
                            <div style='margin-top: 3px; float: left' id='TxtPlazo1'>
                            </div>
                            <div style="font-size: 12px; font-family: Verdana; padding-left: 10px;" class="cbo"
                                id="CboTPlazo1">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                            </label>
                            <div style='margin-top: 3px; float: left' id='TxtPlazo2'>
                            </div>
                            <div style="font-size: 12px; font-family: Verdana; padding-left: 10px;" class="cbo"
                                id="CboTPlazo2">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Lugar de ejecución del contrato:
                            </label>
                            <asp:TextBox ID="TxtLugar" ClientIDMode="Static" runat="server" Width="97%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton5" />
                    </div>
                </div>
                <div class="section" id="content7">
                    Cargando Oblig....
                </div>
                <div class="section" id="content8">
                    Cargando Oblig....
                </div>
                <div class="section" id="content9">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Plazo de liquidación del contrato
                            </label>
                            <div id='TxtPlazoLiq' title="Especifique la cantidad de empleos directos a generar en el Contrato.">
                            </div>
                            Mes(es)
                        </div>
                        <div class="filaform">
                            <label>
                                Fundamentos Jurídicos de la modalidad de selección</label>
                            <asp:TextBox ID="TxtFundJur" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton6" />
                    </div>
                </div>
                <div class="section" id="content10">
                    <%--<div class="formC">
                        <div class="filaform">
                            <label>
                                Presupuesto oficial
                            </label>
                            TABLA DEL PRESUPUESTO
                        </div>
                        <div class="filaform">
                            <label>
                                Certificado de disponibilidad presupuestal
                            </label>
                            <asp:TextBox ID="TxtNroCDP" runat="server"></asp:TextBox>
                            <asp:TextBox ID="TxtFecCdp" runat="server"></asp:TextBox>
                            TABLA DE CDP Y RUBROS
                        </div>
                    </div>--%>
                    <div class="sectionButtonsWrapper">
                        <%--<input type="button" value="Siguiente" class="nextButton" id="nextButton7" />--%>
                    </div>
                </div>
                <div class="section" id="content11">
                </div>
                <div class="section" id="content12">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Modalidad de Selección
                            </label>
                            <div id="CboMod" class="cbo">
                            </div>
                        </div>
                        <div class="filaform">
                            <label>
                                Justificación de los factores de selección
                            </label>
                            <asp:TextBox ID="TxtJFacSel" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="filaform">
                            <label>
                                Capacidad Financiera
                            </label>
                            <asp:TextBox ID="TxtCapFin" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton11" />
                    </div>
                </div>
                <div class="section" id="content13">
                    Cargando... Capacidad Juridica
                </div>
                <div class="section" id="content14">
                    <div class="formC">
                        <div class="filaform">
                            <label>
                                Condiciones de experiencia
                            </label>
                            <asp:TextBox ID="TxtCodExp" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="filaform">
                            <label>
                                Capacidad residual de contratación
                            </label>
                            <asp:TextBox ID="TxtCapRes" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="filaform">
                            <label>
                                Factores de escogencia y calificación
                            </label>
                            <asp:TextBox ID="TxtFacEsc" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="sectionButtonsWrapper">
                        <input type="button" value="Siguiente" class="nextButton" id="nextButton12" />
                    </div>
                </div>
                <div class="section" id="content15">
                    Cargando... Polizas
                </div>
                <div class="section" id="content16">
                    Cargando... Region
                </div>
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
            <!--VENTANA APROBACIÓN  -->
            <div id="winRAI">
                <div id="winHRAI">
                    <span>Estudios Previos </span>
                </div>
                <div style="overflow: hidden;" id="Div3">
                    <div id="winConRAI" class="information">
                        Hacer click para seleccionar el funcionario</div>
                    <div>
                        <div class="form">
                            <div class="filaform">
                                <label for="txtFec">
                                    Fecha
                                </label>
                                <div id="txtFec" style="margin-top: 3px;" />
                            </div>
                            <div class="filaform">
                                <label for="txtObs">
                                    Observación</label>
                                <textarea id="txtObs" class="textareapq" name="txtObs"></textarea>
                            </div>
                            <div class="filaformBtn">
                                <input type="button" value="Guardar" id="BtnRAIGuardar" />
                                <input type="button" value="Cancelar" id="BtnRAICancelar" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </form>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
        PIE DE PAGINA
    </div>
    <script src="js/wizard.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LoadJqWidget() {
            $.data(document.body, 'theme', 'bootstrap');
            //$.data(document.body, 'theme', 'metro');
            theme = getDemoTheme();
            var url = "../jqwidgets/styles/jqx." + theme + '.css';
            $(document).find('head').append('<link rel="stylesheet" href="' + url + '" media="screen" />');

            wizard.config.theme = theme;
            wizard.init();
            wizard.Deshabilitar(); //Deshabilita los controles y tabs
            _createToolBar();

        }

        //Agregar factorias de jquery al update panel.
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(LoadJqWidget);


        //Factoria de jquery(pagina lista)
        $(document).ready(function () {
            LoadJqWidget();
        });

        function pagActual() {
            this.tipo = $.getUrlVar('tip');
            this.EP = $.getUrlVar('nep');
        }
        var pag = new pagActual();

        
        
        
    </script>
    <script src="js/ModalTer.js" type="text/javascript"></script>
    <script src="js/admHEstadosEP.js" type="text/javascript"></script>
</body>
</html>
