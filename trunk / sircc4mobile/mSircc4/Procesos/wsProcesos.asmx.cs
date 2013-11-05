using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BLL.mobile;
using System.Web.Security;

namespace mSircc4.Procesos
{
    /// <summary>
    /// Summary description for wsProcesos
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class wsProcesos : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargados(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getEncargados(DepDel, Vigencia);
        }

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargados2(string DepDel, short Vigencia, int Pagina)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getEncargadosP(DepDel, Vigencia, Pagina);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getxEstados(DepDel, Vigencia);
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPCONTRATOS> getProcesos(string IdeFun, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesos(IdeFun, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCRONOGRAMAS> getCronograma(string NroPro)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getCronograma(NroPro);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxEst(string DepDel, string Estado, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosxEst(DepDel, Estado, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosDD(string DepDel)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosDD(DepDel);
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getDependencias(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxDN(string DepDel,string DepNec, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosDN(DepDel, DepNec, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getModalidad(DepDel, Vigencia);
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
        {
            mobConsultas mc = new mobConsultas();
            return mc.getProcesosxMod(DepDel, Modalidad, Vigencia);
        }
    }
}
