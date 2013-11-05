using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
   public class vEP_RUBROS_CDP
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public string COD_RUB { get; set; }
        public Nullable<decimal> VALOR { get; set; }
        public string NRO_CDP { get; set; }
        public Nullable<short> VIG_CDP { get; set; }
        public Nullable<int> GRUPO { get; set; }
        public Nullable<System.DateTime> FEC_REG { get; set; }
        public string USAP_REG { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public string USAP_MOD { get; set; }
        public Nullable<decimal> ID_EP_CDP { get; set; }

        public string NOM_RUB { get; set; }
    }
    
}
