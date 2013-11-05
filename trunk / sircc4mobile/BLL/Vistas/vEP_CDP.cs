using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vEP_CDP
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public string NRO_CDP { get; set; }
        public System.DateTime FEC_CDP { get; set; }
        public Nullable<decimal> VAL_CDP { get; set; }
        public string VIG_FUT { get; set; }
        public Nullable<decimal> GRUPO { get; set; }

    }
}
