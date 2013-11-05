using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Security;

namespace Sircc4
{
    public partial class LoginJq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Validar(string user, string pwd)
        {
            if (Membership.ValidateUser(user, pwd))
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        protected void BtnIniciar_Click(object sender, EventArgs e)
        {
            
            string user = this.txtUserName.Value;
            string pwd = this.txtPassword.Value;
            if (Membership.ValidateUser(user, pwd))
            {
                SetCookieUser(user,"2013");
                FormsAuthentication.RedirectFromLoginPage(user, false);
            }
                                                        
        }
        private void SetCookieUser(string usuario, string vig )
        {
            DateTime now = DateTime.Now;

            HttpCookie myCookie;
            
            myCookie = new HttpCookie("sircc_user");
            myCookie.Value = usuario;
            myCookie.Expires = now.AddHours(8);
            HttpContext.Current.Response.Cookies.Add(myCookie);
            
            myCookie = new HttpCookie("sircc_vig");
            myCookie.Value = vig;
            myCookie.Expires = now.AddHours(8);
            HttpContext.Current.Response.Cookies.Add(myCookie);
                
        }


    }
}