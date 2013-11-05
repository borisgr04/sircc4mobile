using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.Script.Services;
using BLL;
using ByA;
using BLL.Menu;

namespace Sircc4.Servicios
{
    /// <summary>
    /// Summary description for wsMenu
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsMenu : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<dataTree> GetMenu(string modulo, string usuario)
        {
            gesMenuAdapter mg = new gesMenuAdapter();
            return mg.getOpciones(modulo);
        }


    }
}
