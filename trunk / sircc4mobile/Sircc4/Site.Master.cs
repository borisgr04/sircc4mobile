using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Web.Security;
namespace Sircc4
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        Entities ctx;
        protected void Page_Load(object sender, EventArgs e)
        {
         // LMenu.Text = CargarMenuA("CONT");
        }

        private string AddMenuHijos(MENU2 item)
        {
            string hmenu = "";
            foreach (MENU2 itemHijo in item.MENU211)
            {
                if (itemHijo.MENUID != itemHijo.PADREID)
                {
                    //if (Roles.IsUserInRole("admin", itemHijo.ROLES))
                    //{
                        hmenu += "<li><a href=\"" + ResolveClientUrl("~/" + itemHijo.URL) + "\" title=\"" + itemHijo.DESCRIPCION + "\"  runat=\"server\">" + itemHijo.TITULO + "</a></li>";
                    //}
                }

            }
            return hmenu;
        }

        public string CargarMenuA(string modulo)
        {
            string StrMenu;

            using (ctx = new Entities())
            {
                List<MENU2> itemPadres = FindPadres(modulo);
                StrMenu = "<div id=\"accordion\">";
                foreach (MENU2 item in itemPadres)
                {
                    if (item.MENUID == item.PADREID)
                    {
                        StrMenu += "<h2><a id='' href=\"" + ResolveClientUrl("~/" + item.URL) + "\" title=\"" + item.DESCRIPCION + "\">"+ item.TITULO + "</a></h2>";
                        //    <img src=\"" + ResolveClientUrl("~/" + item.ICONO) + "\"/>  " 
                        StrMenu += "<div>";
                        StrMenu += AddMenuHijos2(item);
                        StrMenu += "</div>";
                    }
                }
            }
            StrMenu += "</div>";
            return StrMenu;
        }

        private string AddMenuHijos2(MENU2 item)
        {
            string hmenu = "";
            foreach (MENU2 itemHijo in item.MENU211)
            {
                if (itemHijo.MENUID != itemHijo.PADREID)
                {
                    //if (Roles.IsUserInRole("admin", itemHijo.ROLES))
                    //{
                    //hmenu += "<li><a href=\"" + ResolveClientUrl("~/" + itemHijo.URL) + "\" title=\"" + itemHijo.DESCRIPCION + "\"  runat=\"server\">" + itemHijo.TITULO + "</a></li>";
                    hmenu += "<p><a href=\"" + ResolveClientUrl("~/" + itemHijo.URL) + "\" title=\"" + itemHijo.DESCRIPCION + "\"  runat=\"server\">" + itemHijo.TITULO + "</a></p>";
                    //}
                }

            }
            return hmenu;
        }

        public List<MENU2> FindPadres(string modulo)
        {
            var q = from c in ctx.MENU2
                    where c.PADREID == c.MENUID && c.MODULO.Equals(modulo)
                    select c;
            return q.ToList();

        }



    }
}
