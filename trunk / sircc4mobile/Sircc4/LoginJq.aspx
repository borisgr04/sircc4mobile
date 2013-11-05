<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginJq.aspx.cs" Inherits="Sircc4.LoginJq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIRCC 4.0- Inicio de Sesión </title>
    
    <link rel="stylesheet" href="jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="jqscripts/jquery-1.10.1.min.js"></script>
    
    <script type="text/javascript" src="jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="jqwidgets/jqxwindow.js"></script>
    <script type="text/javascript" src="jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="jqwidgets/jqxexpander.js"></script>
    <script type="text/javascript" src="jqwidgets/jqxpasswordinput.js"></script>
    <script type="text/javascript" src="jqwidgets/jqxinput.js"></script>
    <script src="jqscripts/jquery_ext.js" type="text/javascript"></script>
    <script src="jqscripts/byaSite.js" type="text/javascript"></script>
    <link href="Styles/EstiloFormulario.css" rel="stylesheet" type="text/css" />
    <style>
      .centrado  
      {
          position : absolute;
          height: 400px;
          width: 440px;
          left : 50%;
          top : 50%;
          margin-top: -200px;
          margin-left: -220px;
      }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            theme = 'bootstrap';
            
            $(document).find('head').append('<link rel="stylesheet" href="/jqwidgets/styles/jqx.bootstrap.css" media="screen" />');
            //$(document).find('head').append('<link rel="stylesheet" href="/jqwidgets/styles/jqx.web.css" media="screen" />');

            //$('#divlogin').jqxWindow({ theme: theme, width: '450px', height: '400px', isModal: false, showCloseButton: false, keyboardCloseKey: 0 });

            $("#divlogin").jqxExpander({ width: '450px', height: '400px', toggleMode: 'dblclick', theme: theme });

            $("#txtUserName").jqxInput({ placeHolder: 'Digite su identificación', theme: theme, width: '200px', height: '25px' });

            $("#txtPassword").jqxPasswordInput({ placeHolder: 'Digite su Contraseña', theme: theme, width: '200px', height: '25px' });

            $("#BtnIniciar").jqxButton({ theme: theme });
            
            $('#txtUserName').focus();
        });
        function Validar() {
            var isValido = false;
            var UserId = $("#txtUserName").val();
            var Password = $("#txtPassword").val();
            
            if (UserId != "" && Password != "") {
                jsonData = "{'user':'" + UserId + "','pwd':'" + Password + "'}";
                urlToHandler = 'LoginJq.aspx/Validar';
                $.ajax({
                    type: "POST",
                    url: urlToHandler,
                    data: jsonData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        if (result.d != 0) {
                            byaSite.setUsuario(UserId);
                            isValido = true;
                            //alert("Es Válido");
                        }
                        else {
                            $("#txtUserName").val('');
                            $("#txtPassword").val('');
                            alert("Usuario o Contraseña Errada.");
                        }
                    },
                    error: function (jqXHR, status, error) {
                        alert(jqXHR.responseText);
                    }
                });
            } else {
                alert("Por favor digite usuario y contraseña.");
            }
            return isValido;
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <div class="centrado" >
    
        <div style="overflow: hidden;" id="divlogin">
            <div>
                SIRCC 4.0- Inicio de Sesión
            </div>
            <div style="font-family: Verdana; font-size: 13px;">
                <div class="formLogin">
                    <div>
                          <img src="Styles/imagesLogin/Nombre.png" alt="Usuario" 
                                style="height: 160px; width: 400px; margin: 0px;"/>
                    </div>
                    <div class="filaform">
                        <label for="txtUserName">
                            Usuario</label>
                        <input id="txtUserName" runat="server" runat="server" />
                        
                    </div>
                    <div class="filaform">
                        <label for="txtPassword">
                            Clave</label>
                        <input id="txtPassword" runat="server" type="password" runat="server" />
                    </div>
                    <div class="filaformBtn" style="text-align:center">
                    &nbsp
                    </div>
                    <div class="filaformBtn" style="text-align:center">
                        <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar Sesión"  
                        onclick="BtnIniciar_Click" OnClientClick="javascript:return Validar()" />
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>
    </form>
</body>
</html>
