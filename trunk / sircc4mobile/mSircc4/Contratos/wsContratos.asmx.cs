using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL.mobile;
using System.Web.Security;
using System.Web.Services;
using System.Web.Script.Services;

namespace mSircc4.Contratos
{
    /// <summary>
    /// Summary description for wsProcesos
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class wsContratos : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargados(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getEncargados(DepDel, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {
            mobContratos mc = new mobContratos();
            return mc.getxEstados(DepDel, Vigencia);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPCONTRATOS> getProcesos(string IdeFun, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesos(IdeFun, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCRONOGRAMAS> getCronograma(string NroPro)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getCronograma(NroPro);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxEst(string DepDel, string Estado, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosxEst(DepDel, Estado, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosDD(string DepDel)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosDD(DepDel);
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            mobContratos mc = new mobContratos();
            return mc.getDependencias(DepDel, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxDN(string DepDel, string DepNec, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosDN(DepDel, DepNec, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getModalidad(DepDel, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosxMod(DepDel, Modalidad, Vigencia);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTIPOSCONT> getClase(string DepDel, short Vigencia)
        {
            mobContratos mc = new mobContratos();
            return mc.getClase(DepDel, Vigencia);
        }
    }
}
