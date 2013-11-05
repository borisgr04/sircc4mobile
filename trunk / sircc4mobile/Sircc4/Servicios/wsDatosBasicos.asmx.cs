using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BLL.Vistas;
using BLL;
using Sircc4.Clases;

namespace Sircc4.Servicios
{
    /// <summary>
    /// Summary description for wsDatosBasicos
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsDatosBasicos : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public IList<vSUBTIPOS> GetvSUBTIPOS(string cod_tip)
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vSUBTIPOS> lEp = ep.GetvSUBTIPOS(cod_tip);
            return lEp;

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public IList<vDEPENDENCIA> GetvDEPENDENCIAD()
        {
            string ide_ter = Usuario.UserName;
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vDEPENDENCIA> lEp = ep.GetvDEPENDENCIAD();
            return lEp;

        }

    }
}
