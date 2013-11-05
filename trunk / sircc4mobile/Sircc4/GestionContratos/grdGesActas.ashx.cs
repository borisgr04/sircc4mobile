using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using BLL;
namespace Sircc4.GestionContratos
{
    /// <summary>
    /// Summary description for grdGesActas
    /// </summary>
    public class grdGesActas : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();//Retorno

            int iDisplayLength = Convert.ToInt32(HttpContext.Current.Request["iDisplayLength"]);
            string codcon = HttpContext.Current.Request["codcon"];
            int iDisplayStart = Convert.ToInt32(HttpContext.Current.Request["iDisplayStart"]);
            int iEcho = Convert.ToInt32(HttpContext.Current.Request["sEcho"]);
            int iSortingCols = Convert.ToInt32(HttpContext.Current.Request["iSortingCols"]);
            int iSortCol = Convert.ToInt32(HttpContext.Current.Request["iSortCol_0"]);
            int iColumns = Convert.ToInt32(HttpContext.Current.Request["iColumns"]);
            string searchString = HttpContext.Current.Request["sSearch"];
            string sSortDir = HttpContext.Current.Request["sSortDir_0"];
            GCRegActasBL gcRa = new GCRegActasBL();
            IEnumerable<vEstContratos> lst = gcRa.GetActas(codcon);
            //Datos Totales de la Consulta
            var list = new FormatedList();
            list.sEcho = iEcho;
            list.iTotalRecords = lst.Count();
            if(iDisplayLength>-1)
                lst = lst.Skip(iDisplayStart).Take(iDisplayLength);
            if (!String.IsNullOrEmpty(searchString))
            {
                //Se puede escoger las colomnas
                lst=lst.Where(p => p.NOM_EST.Contains(searchString));
            }

            // iSortCol_0  //    0=cust Ac     1=cust name    2=rep
            // iSortDir_0  // asc or desc...
            if (iSortingCols == 1)
            {
                if (sSortDir == "asc" && iSortCol == 0) { lst = lst.OrderBy(p => p.ID); }
                if (sSortDir == "desc" && iSortCol == 0) { lst = lst.OrderByDescending(p => p.ID); }

                if (sSortDir == "asc" && iSortCol == 1) { lst = lst.OrderBy(p => p.NRO_DOC); }
                if (sSortDir == "desc" && iSortCol == 1) { lst = lst.OrderByDescending(p => p.NRO_DOC); }

                if (sSortDir == "asc" && iSortCol == 2) { lst = lst.OrderBy(p => p.sFEC_ENT); }
                if (sSortDir == "desc" && iSortCol == 2) { lst = lst.OrderByDescending(p => p.sFEC_ENT); }
            }

            //string cod_con)
            List<List<string>> lightList = new List<List<string>>();
            int index=0;
            foreach (vEstContratos obj in lst)
            {
                var item = new List<string>();
                item.Add(obj.ID.ToString());
                item.Add(obj.NOM_EST.ToString());
                item.Add(obj.NRO_DOC.ToString());
                item.Add(obj.sFEC_ENT.ToString());
                item.Add(obj.NVISITAS.ToString());
                item.Add(obj.POR_EJE_FIS.ToString());
                item.Add(obj.VAL_PAGO.ToString());
                var Editar = "<input id='Editar' class='button_editar' type='button' />";
                item.Add(Editar);
                var Anular = (index == lst.Count() - 1) ? "<input id='Anular' class='button_anular' type='button' />" : "";
                index = index + 1;
                item.Add(Anular);
                
                lightList.Add(item);
                
            }
            list.iTotalDisplayRecords = lst.Count();
            list.aaData = lightList;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            //context.Response.ContentType = "text/plain";
            context.Response.ContentType = ("text/html");
            context.Response.BufferOutput = true;
            //ser.Serialize(
            context.Response.Write(json);
            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
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
   
}