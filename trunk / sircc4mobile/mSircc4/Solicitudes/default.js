
var Servicio = "wsSolicitudes.asmx";

        function CargarEstados() {
            //data: "{cod_con:'" + Cod_Con + "'}",
            $.ajax({
                type: "POST",
                url: Servicio + "/getxEstados",
                data: "{DepDel:'" + byaSite.getDepDel() + "', Vigencia:" + byaSite.getVigencia() + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lstporEstados").empty();
                    $.each(response.d, function (index, item) {

                        crearItem("lstporEstados", item);
                    });
                },
                error: function (jqXHR, status, error) {
                    //alert(error + "-" + jqXHR.responseText);
                    alert("Ajax " + error + " - " + jqXHR.responseText);
                    //alert(status);
                }
            });

            function crearItem(lst, item) {
                var parent = document.getElementById(lst);
                var listItem = document.createElement('li');
                listItem.setAttribute('id', 'listitem');
                var str = "<a href='pagSEstados.htm?Estado=" + item.NOM_EST + "' data-ajax='false'>" + item.NOM_EST + "</a>";
                str += '<span class="ui-li-count">' + item.CANT + '</span>';
                listItem.innerHTML = str;
                parent.appendChild(listItem);
                $(parent).listview('refresh');
            }
        }

        function CargarProcesos() {
            $.ajax({
                type: "POST",
                data: "{DepDel:'" + byaSite.getDepDel() + "', Vigencia:" + byaSite.getVigencia() + "}",
                url: Servicio + "/getModalidad",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lstProc").empty();
                    $.each(response.d, function (index, item) {

                        crearItem("lstProc", item);
                    });
                    $("#lstProc").listview('refresh');
                },
                error: function (jqXHR, status, error) {
                    alert("Ajax " + error + " - " + jqXHR.responseText);
                }
            });
            
            function crearItem(lst, item) {
                var parent = document.getElementById(lst);
                var listItem = document.createElement('li');
                str = "<a href='pagPMod.htm?CodMod=" + item.COD_TPROC + "&NomMod=" + item.NOM_TPROC + " ' data-ajax='false'><h3>" + item.NOM_TPROC + "</h3>";
                str += '<span class="ui-li-count">' + item.CANT_PROC + '</span>';
                listItem.setAttribute('id', 'listitem');
                listItem.innerHTML = str;
                parent.appendChild(listItem);

            }
            
        }

        function CargarDependencias() {
            $.ajax({
                type: "POST",
                url: Servicio + "/getDependencias",
                data: "{DepDel:'" + byaSite.getDepDel() + "', Vigencia:" + byaSite.getVigencia() + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lstDep").empty();
                    $.each(response.d, function (index, item) {

                        crearItem("lstDep", item);
                    });
                    $("#lstDep").listview('refresh');
                },
                error: function (jqXHR, status, error) {
                    alert("Ajax " + error + " - " + jqXHR.responseText);
                }
            });


            function crearItem(lst, item) {
                var parent = document.getElementById(lst);
                var listItem = document.createElement('li');
                listItem.setAttribute('id', 'listitem');
                var str = "<a href='pagPDep.htm?CodDep=" + item.COD_DEP + "&NomDep=" + item.NOM_DEP + " ' data-ajax='false'>" + item.NOM_DEP + "</a>";
                str += '<span class="ui-li-count">' + item.CANT_PROC + '</span>';
                listItem.innerHTML = str;
                parent.appendChild(listItem);
                $(parent).listview('refresh');
            }
        }

        function CargarFuncionarios() {
            //data: "{cod_con:'" + Cod_Con + "'}",
            $.ajax({
                type: "POST",
                url: Servicio + "/getEncargados",
                data: "{DepDel:'" + byaSite.getDepDel() + "',Vigencia:" + byaSite.getVigencia() + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lstProcEnc").empty();
                    $.each(response.d, function (index, item) {
                        crearItem("lstProcEnc", item);
                    });
                },
                error: function (jqXHR, status, error) {
                    //alert(error + "-" + jqXHR.responseText);
                    alert("Ajax " + error + " - " + jqXHR.responseText);
                    //alert(status);
                }
            });
            function crearItem(lst, item) {
                var parent = document.getElementById(lst);
                var listItem = document.createElement('li');
                listItem.setAttribute('id', 'listitem');
                var str = "<a href='" + "pagPEncargados.htm?IdeFun=" + item.IDE_TER + "&NomFun=" + item.NOMBRE + " ' data-ajax='false'>" + item.NOMBRE + "</a>";
                str += '<span class="ui-li-count">' + item.CANT_PROC + '</span>';
                listItem.innerHTML = str;
                parent.appendChild(listItem);
                $(parent).listview('refresh');
            }
        }

        $('#pgEncargado').live('pageshow', function (event, ui) {
            $("#PieEncargado").html(byaSite.PiePagina());
            $("#headerEnc").html(byaSite.NomApp());
            CargarFuncionarios();
        });

        $('#pgEstado').live('pageshow', function (event, ui) {
            $("#PieEstados").html(byaSite.PiePagina());
            $("#headerEst").html(byaSite.NomApp());
            CargarEstados();
        });
        $('#pgModalidad').live('pageshow', function (event, ui) {
            $("#PiePaginaProcesos").html(byaSite.PiePagina());
            $("#headerProc").html(byaSite.NomApp());
            CargarProcesos();
        });
        $('#pgDependencia').live('pageshow', function (event, ui) {
            $("#PiePaginaDependencias").html(byaSite.PiePagina());
            $("#headerDep").html(byaSite.NomApp());
            CargarDependencias();
        });

        $('[data-role=page]').live('pagecreate', function (event, ui) {
            $("#PiePagina").html(byaSite.PiePagina());
            $("#HeaderPpal").html(byaSite.NomApp());
            byaSite.CargarVigencias("#cboVig");
            $.mobile.pageLoadErrorMessage = 'Esta pagina no existe, el mensaje de error ha sido cambiado';
        });
    