var theme;
$(document).ready(function () {
    //$.data(document.body, 'theme', 'bootstrap');
                $.data(document.body, 'theme', 'metro');
                theme = getDemoTheme();
                var url = "jqwidgets/styles/jqx." + theme + '.css';
                $(document).find('head').append('<link rel="stylesheet" href="' + url + '" media="screen" />');

    var rss = (function ($) {
        var ServicioMenu = "/Servicios/wsMenu.asmx/GetMenu";
        var createWidgets = function () {
            var theme = getDemoTheme();

            $('#mainSplitter').jqxSplitter({ theme: theme, width: 950, height: 600, panels: [{ size: 220, min: 100 }, { min: 700, size: 700}] });
            $("#feedExpander").jqxExpander({ toggleMode: 'click', showArrow: true, width: "100%", height: "200px", theme: theme, initContent: function () { } });
            $("#feedListExpander").jqxExpander({ toggleMode: 'none', showArrow: false, width: "100%", height: 800, theme: theme, initContent: function () { } });
        };

        var loadFeed = function (feed, callback) {
            $.get(feed, function (data) {
                config.feedContainer.html(data);
            });
        };

        var displayFeedHeader = function (header) {
            $("#feedListExpander").jqxExpander('setHeaderContent', header);
        };

        function crearNaviagation() {

            var idNav = "Nav";

            function addEventListeners(i, modulo) {
                var tree = $('#jqxMTree' + i);
                tree.jqxTree({ height: '300', width: '97%', theme: theme, source: cargarTree(modulo) });
                tree.on('select', function (event) {
                    var item = tree.jqxTree('getItem', event.args.element);
                    if (item.value != '') {
                        if (item.value.target == 'div') {
                            displayFeedHeader(item.value.descripcion);
                            loadFeed(item.value.url);
                        }
                        if (item.value.target == '_blank') {
                            byaPage.pantallacompleta(item.value.url);
                        }
                    }
                });
                tree.jqxTree('selectItem', tree.find('li:first')[0]);
                var selectedItem = tree.jqxTree('selectedItem');
                if (selectedItem != null) {
                    tree.jqxTree('expandItem', selectedItem.element);
                }
            }

            function crearItem(i, titulo, urlimg) {
                var str = "<div>";
                var imagen = (urlimg == "") ? "<img alt='*' src='" + urlimg + "' />" : "";
                str += "<div style='margin-top: 2px;'><div style='float: left;'>" + imagen + "</div>";
                str += "<div style='margin-left: 4px; float: left;'>" + titulo + "</div>";
                str += "</div></div>"
                str += "<div>";
                str += '<div id="jqxMTree' + i + '" ></div>';
                str += "</div>";
                return str;
            }

            function cargarTree(modulo) {
                var param = "{modulo:'" + modulo + "',usuario:'" + byaSite.getUsuario() + "'}";
                var data = byaPage.postSource(ServicioMenu, param);
                var source =
                        {
                            datatype: "json",
                            datafields: [
                                { name: 'id' },
                                { name: 'parentid' },
                                { name: 'text' },
                                { name: 'value' }
                                ],
                            id: 'id',
                            localdata: data
                        };
                var dataAdapter = new $.jqx.dataAdapter(source);
                dataAdapter.dataBind();
                var records = dataAdapter.getRecordsHierarchy('id', 'parentid', 'items', [{ name: 'text', map: 'label'}]);
                return records;
            }
            //CrearPlantilla HTML
            var str = "<div id=" + idNav + ">";
            str += crearItem(1, "Estudios Previos");
            str += crearItem(2, "Solicitudes");
            str += crearItem(3, "Procesos");
            str += crearItem(4, "Contratos");
            str += "</div>";
            $("#zonaMenu").append(str);
            //Aplica Jqxwidget
            $("#Nav").jqxNavigationBar({ height: '100%', width: 200, expandMode: 'toggle', theme: theme });
            addEventListeners(1, "ESPR4");
            addEventListeners(2, "SOLI4");
            addEventListeners(3, "PREC");
            addEventListeners(4, "CONT");
            $('#Nav').jqxNavigationBar('expandAt', 0);
        }


        var dataDir = 'data';

        var config = {
            feeds: { 'CNN.com': 'cnn', 'Geek.com': 'geek', 'ScienceDaily': 'sciencedaily' },
            format: 'txt',
            dataDir: dataDir,
            feedTree: $('#jqxTree'),
            feedContainer: $('#feedContainer'),
            feedItemContent: $('#feedItemContent'),
            feedItemHeader: $('#feedItemHeader'),
            feedUpperPanel: $('#feedUpperPanel'),
            feedHeader: $('#feedHeader'),
            feedContentArea: $('#feedContentArea'),
            selectedIndex: -1,
            currentFeed: '',
            currentFeedContent: {}
        };

        return {

            init: function () {
                Html = '<div><div id="zonaMenu"></div></div><div><div id="contentSplitter"><div  id="feedUpperPanel"><div class="jqx-hideborder" id="feedListExpander"><div id="feedHeader"></div><div class="jqx-hideborder jqx-hidescrollbars"><div class="jqx-hideborder" id="feedContainer"></div></div></div></div></div></div>';
                $("#mainSplitter").append(Html);
                createWidgets();
                crearNaviagation();
            }
        }

    } (jQuery));

    rss.init();

});