using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vEP_HESTADOEP
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public Nullable<System.DateTime> FEC_EP { get; set; }
        public Nullable<System.DateTime> FSIS_EP { get; set; }
        public string USAP_EP { get; set; }
        public string OBS_EP { get; set; }
        public string TIP_EP { get; set; }
        public string EST_EP { get; set; }
    }
}
