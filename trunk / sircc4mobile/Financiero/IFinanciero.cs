using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Financiero
{
    public interface IFinanciero
    {
        SF_CDP GetCDP(string nro_cdp, string vigencia);
        SF_CDP GetRubros(string nro_cdp, string vigencia);
            
    }
}
