using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL.mobile;
using System.Web.Security;
using System.Web.Script.Services;


namespace mSircc4
{
    /// <summary>
    /// Summary description for wsSircc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsSircc : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Iniciar(string usu, string pass)
        {
            if (Membership.ValidateUser(usu, pass))
            {
                string vigencia;
                mobGeneral mg = new mobGeneral();
                vigencia=mg.getVigencias().FirstOrDefault().ToString();
                return vigencia;
            }
            else
            {
                return "0";
            }

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<short> Vigencias()
        {
            mobGeneral mg = new mobGeneral();
            return mg.getVigencias();
        }
    }
}
