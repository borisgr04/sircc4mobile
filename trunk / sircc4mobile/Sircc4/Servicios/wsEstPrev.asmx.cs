using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL;
using ByA;
using Entidades;
using System.Web.Script.Services;
using BLL.Vistas;

namespace Sircc4.Servicios
{
    /// <summary>
    /// Summary description for wsEstPrev
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsEstPrev : System.Web.Services.WebService
    {
        //Servicio de creación de estudios previos
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetEstudiosPreviosPl(string vig)
        {
            gesEstPrevBLL mg = new gesEstPrevBLL();
            return ByAUtil.convertListToXML(mg.getEstPrevPl(vig));
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetEstudiosPrevios(string vig)
        {
            gesEstPrevBLL mg = new gesEstPrevBLL();
            return ByAUtil.convertListToXML(mg.getEstPrev(vig));
        }

        [WebMethod(EnableSession = true)]
        public ByARpt NuevodePlantilla(decimal id_plantilla)
        {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            epBLL.ep = new ESTPREV();
            epBLL.ep.ID = id_plantilla;
            return epBLL.NuevodePlantilla();
        }
        //De los estudios previos
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public vESTPREV GetEstPrev(string id_ep, string tipo)
        {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            ESTPREV ep = new ESTPREV();
            ep.ID = Convert.ToInt32(id_ep);
            epBLL.ep = ep;

            vESTPREV Reg = epBLL.GetPK(tipo);
            return Reg;
        }
    }
}
