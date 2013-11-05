using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;
using BLL.Vistas;
using System.Web.Security;



namespace BLL.Menu
{
    public class gesMenuAdapter
    {
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }
        public List<dataTree> getOpciones(string modulo)
        {
            List<dataTree> lt;
            using (ctx = new Entities())
            {

                lt = ctx.MENU2.Where(t => t.MODULO == modulo  ).Select(t => new dataTree
                {
                    id = t.MENUID,
                    text = t.TITULO,
                    value = new valueTree { icono=t.ICONO, descripcion= t.DESCRIPCION, target= t.TARGET, url= t.URL },
                    parentid = t.MENUID == t.PADREID ? "-1" : t.PADREID,
                    roles= t.ROLES
                }
                ).ToList();
                lt=lt.Where(t=> (t.parentid == "-1") || (Roles.IsUserInRole(t.roles))).ToList();
            return lt;
            }

        }

    }
    public class valueTree { 

        public string target {get;set;}
        public string url { get; set; }
        public string icono { get; set; }
        public string descripcion { get; set; }
        

    }
    public class dataTree
    {
        public string id { get; set; }
        public string parentid { get; set; }
        public string text { get; set; }
        public valueTree value { get; set; }
        public string roles { get; set; }
    }
}
