using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using BLL;
using ByA;
using BLL.Vistas;
using Entidades;

namespace Sircc4.EPConsultas
{
    public partial class conEP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Se crea metodo web que devolvera los datos en xml
        //EnableSession = true)
       [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public static string GetConsulta(vESTPREVCON filtro)
        {
            conEstPrev con = new conEstPrev();
            return  ByAUtil.convertListToXML(con.Consulta2(filtro)); //Convierte resultado a XML
        }
        

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vDEPENDENCIA> GetvDEPENDENCIAT()
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vDEPENDENCIA> lEp = ep.GetvDEPENDENCIA();
            return lEp;

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vVIGENCIAS> GetvVIGENCIAS()
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vVIGENCIAS> lEp = ep.GetvVIGENCIAS();
            return lEp;
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vTIPOSPROC> GetvModalidad()
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vTIPOSPROC> lEp = ep.GetvModalidad();
            return lEp;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vEP_ESTADOS> GetvEP_ESTADOS()
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vEP_ESTADOS> lEp = ep.GetvEP_ESTADOS();
            return lEp;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vTIPOSCONT> GetvTIPOSCONT()
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vTIPOSCONT> lEp = ep.GetvTIPOSCONT();
            return lEp;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static IList<vSUBTIPOS> GetvSUBTIPOS(string cod_tip)
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vSUBTIPOS> lEp = ep.GetvSUBTIPOS(cod_tip);
            return lEp;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public static string GetTerceros()
        {

            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            return ByAUtil.convertListToXML(ep.GetTerceros()); ;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetTercerosPk(string ide_ter)
        {
            GCConsContratosBL Gestcont = new GCConsContratosBL();
            vTerceros t = Gestcont.GetTercerosPk(ide_ter);
            return t == null ? "0" : t.NOMBRE;

        }

      
    }
}