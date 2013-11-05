using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Financiero
{
    public static class FinancieroFactory
    {
        public static IFinanciero Create(string sistema)
        {
            IFinanciero f = null;
            if(sistema=="SIIAF"){
                f= new  FinacieroSIIAF();
            }
            return f;
        }
    }
}
