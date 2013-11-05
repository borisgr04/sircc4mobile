using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vTIPOSCONT
    {
        public string COD_TIP { get; set; }
        public string NOM_TIP { get; set; }
        public string EST_TIP { get; set; }
        public string NOMC_TIP {
            get{
             return COD_TIP+"-"+NOM_TIP;
            }
            
        }
    }
}
