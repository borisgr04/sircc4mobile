using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using ByA;

namespace Sircc4
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
                        
            Numalet let = new Numalet();
            let.SeparadorDecimalSalida = "pesos y";
            let.ConvertirDecimales = true;
            //redondeando en cuatro decimales
            let.Decimales = 2;
            let.MascaraSalidaDecimal = "centavos";
            Response.Write(let.ToCustomCardinal("3543875321,2510"));
            Response.Write(let.ToCustomCardinal("21,50"));
        }
    }
}
