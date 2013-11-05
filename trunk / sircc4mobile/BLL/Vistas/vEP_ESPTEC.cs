using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vEP_ESPTEC
    {
        public decimal ID { get; set; }
        public string DESC_ITEM { get; set; }
        public Nullable<decimal> CANT_ITEM { get; set; }
        public string UNI_ITEM { get; set; }
        public Nullable<decimal> VAL_UNI_ITEM { get; set; }
        public Nullable<decimal> PORC_IVA { get; set; }
        public Nullable<decimal> VAL_PAR
        {
            get
            {
                return CANT_ITEM * VAL_UNI_ITEM;
            }
        }

        public string USAP_REG { get; set; }
        public Nullable<System.DateTime> FEC_REG { get; set; }
        public string USAP_MOD { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public Nullable<short> GRUPO { get; set; }

    }
}
