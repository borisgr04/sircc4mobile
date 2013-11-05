using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BLL.mobile;


namespace mSircc4.Solicitudes
{
    /// <summary>
    /// Summary description for wsSolicitudes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsSolicitudes : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getxEstados(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPSolicitudesM> getSolicitudesxEst(string DepDel, string Estado, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getSolicitudesxEst(DepDel, Estado, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getDependencias(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPSolicitudesM> getProcesosxDN(string DepDel, string DepNec, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getProcesosDN(DepDel, DepNec, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getModalidad(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTerceros> getEncargados(string DepDel, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getEncargados(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPSolicitudesM> getProcesos(string IdeFun, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getProcesos(IdeFun, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPSolicitudesM> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
        {
            mobSolicitudes mc = new mobSolicitudes();
            return mc.getProcesosxMod(DepDel, Modalidad, Vigencia);
        }

    }
}
