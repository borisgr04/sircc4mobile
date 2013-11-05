using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vEP_FORMA_PAGO
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public Nullable<decimal> ORD_FPAG { get; set; }
        public string TIP_FPAG { get; set; }
        public Nullable<decimal> VAL_FPAG { get; set; }
        public Nullable<decimal> POR_FPAG { get; set; }
        public string CON_FPAG { get; set; }
        public string PGEN_FPAG { get; set; }

        public Nullable<System.DateTime> FEC_REG { get; set; }
        public string USAP_REG { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public string USAP_MOD { get; set; }

        public string NOM_TIP_FPAG { get; set; }


    }
}
