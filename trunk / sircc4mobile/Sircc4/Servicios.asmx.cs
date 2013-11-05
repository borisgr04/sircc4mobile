using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BLL;
using BLL.Vistas;

namespace Sircc4
{
    /// <summary>
    /// Summary description for Servicios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicios : System.Web.Services.WebService
    {

        
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetTercerosPk(string ide_ter)
        {
            GCConsContratosBL Gestcont = new GCConsContratosBL();
            vTerceros t = Gestcont.GetTercerosPk(ide_ter);
            return t == null ? "0" : t.NOMBRE;
        }

    }
}
