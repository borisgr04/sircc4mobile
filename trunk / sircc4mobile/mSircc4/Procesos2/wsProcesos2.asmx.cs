using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using BLL.mobile;

namespace mSircc4.Procesos2
{
    /// <summary>
    /// Descripción breve de wsProcesos2
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsProcesos2 : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargadosxEstado(string DepDel, short Vigencia, string Estado)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getEncargadosxEstado(DepDel, Vigencia, Estado);
        }

        /// <summary>
        /// Listado de Procesos Filtrado por Estado, Encargado  
        /// </summary>
        /// <param name="IdeFun"></param>
        /// <param name="Vigencia"></param>
        /// <param name="Estado"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPCONTRATOS> getEstxEncxProc(string IdeFun, short Vigencia,string Estado)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getEstxEncxProc(IdeFun, Vigencia, Estado);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargados(string DepDel, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getEncargados(DepDel, Vigencia);
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vTerceros> getEncargados2(string DepDel, short Vigencia, int Pagina)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getEncargadosP(DepDel, Vigencia, Pagina);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getxEstados(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPESTADOS> getxEncEstados(string DepDel, short Vigencia, string IdeFunc)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getxEncEstados(DepDel, Vigencia, IdeFunc);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<vPCONTRATOS> getProcesos(string IdeFun, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getProcesos(IdeFun, Vigencia);
        }
        
        

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCRONOGRAMAS> getCronograma(string NroPro)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getCronograma(NroPro);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxEst(string DepDel, string Estado, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getProcesosxEst(DepDel, Estado, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosDD(string DepDel)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getProcesosDD(DepDel);
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getDependencias(DepDel, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxDN(string DepDel, string DepNec, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getProcesosDN(DepDel, DepNec, Vigencia);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getModalidad(DepDel, Vigencia);
        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<vPCONTRATOS> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
        {
            mobProcesos mc = new mobProcesos();
            return mc.getProcesosxMod(DepDel, Modalidad, Vigencia);
        }

    }
}
