        var Servicio = "wsContratos.asmx";

        function CargarClase() {
            //data: "{cod_con:'" + Cod_Con + "'}",
            $.ajax({
                type: "POST",
                url: Servicio + "/getClase",
                data: "{DepDel:'" + byaSite.getDepDel() + "',Vigencia:" + byaSite.getVigencia() + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#lstClase").empty();
                    $.each(response.d, function (index, item) {
                        crearItem("lstClase", item);
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
                var str = "<a href='" + "pagPClase.htm?IdeFun=" + item.COD_TIP + "&NomFun=" + item.NOM_TIP + " ' data-ajax='false'>" + item.NOM_TIP + "</a>";
                str += '<span class="ui-li-count">' + item.CANT_CONT + '</span>';
                listItem.innerHTML = str;
                parent.appendChild(listItem);
                $(parent).listview('refresh');
            }
        }

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
                var str = "<a href='pagPEstados.htm?Estado=" + item.NOM_EST + "' data-ajax='false'>" + item.NOM_EST + "</a>";
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

        $('#pgClase').live('pageshow', function (event, ui) {
            
            $("#PiePaginaClase").html(byaSite.PiePagina());
            $("#headerClase").html(byaSite.NomApp());
            CargarClase();
        });
        $('#pgEstado').live('pageshow', function (event, ui) {
            $("#PiePaginaEstados").html(byaSite.PiePagina());
            $("#headerEst").html(byaSite.NomApp());
            CargarEstados();
        });
        $('#pagProcesos').live('pageshow', function (event, ui) {
            $("#PiePaginaProcesos").html(byaSite.PiePagina());
            $("#headerProc").html(byaSite.NomApp());
            CargarProcesos();
        });
        $('#pagDependencias').live('pageshow', function (event, ui) {
            $("#PiePaginaDependencias").html(byaSite.PiePagina());
            $("#headerDep").html(byaSite.NomApp());
            CargarDependencias();
        });

        $('[data-role=page]').live('pagecreate', function (event, ui) {
            $("#PiePagina").html(byaSite.PiePagina());
            $("#HeaderPpal").html(byaSite.NomApp());
            byaSite.CargarVigencias("#cboVig");
            $.mobile.pageLoadErrorMessage = 'Esta pagina no esxiste, el mensaje de error ha sido cambiado';
        });
    