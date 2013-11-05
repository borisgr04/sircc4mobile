using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using Entidades;
using BLL;
using Sircc4.Util;
using System.Text;
using ByA;
using System.Web.Security;
using Newtonsoft.Json;

namespace Sircc4.GestionContratos
{
    public partial class GCRegActas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static IList<vEstContratos> getActas(string cod_con)
        {
            GCRegActasBL gcRa = new GCRegActasBL();
            IList<vEstContratos> lst = gcRa.GetActas(cod_con);
            return lst;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static ByARpt anularActas(int Ide_Acta)
        {
            GCRegActasBL gcRa = new GCRegActasBL();
            return gcRa.Anular(Ide_Acta); 
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static ByARpt guardarActa(RegActas regActas)
        {
            ESTCONTRATOS ec = new ESTCONTRATOS();
            ec.COD_CON = regActas.CodCon;
            ec.EST_FIN = regActas.EstFin;
            ec.FEC_ENT = Convert.ToDateTime(regActas.Fecha);
            ec.OBS_EST = regActas.Observacion;
            ec.VAL_PAGO = regActas.ValAut;
            ec.NVISITAS = regActas.NVisitas;
            ec.POR_EJE_FIS = regActas.Avance;
            ec.ID = regActas.Id;
            ec.USUARIO = "boris";
            GCRegActasBL gcRa = new GCRegActasBL();
            return regActas.Id == 0 ? gcRa.Insert(ec) : gcRa.Update(ec); 
        }
        [WebMethod]
        public static List<CCombo> GetRutaEst(string cod_con)
        {
            List<CCombo> Est=null;
            if (!String.IsNullOrEmpty(cod_con))
            {
                GCRegActasBL gcRa = new GCRegActasBL();
                Est = gcRa.GetRutaActas(cod_con).Select(t => new CCombo() { Codigo = t.COD_EST, Descripcion = t.NOM_EST }).ToList();
            }
            return Est;
        }


       
}
    public class RegActas {
        public int Id;
        public string CodCon;
        public string EstFin;
        public decimal Avance;
        public int NVisitas;
        public decimal ValAut;
        public string Fecha;
        public string Observacion;
        public string NroDoc;
    }

    public class FormatedList
    {
        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<List<string>> aaData { get; set; }
        //public string sColumns { get; set; }
    }
    
}