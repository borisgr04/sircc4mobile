//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Entidades
{
    public partial class MOD_CDP_CONTRATOS
    {
        public string NUM_SOL_ADI { get; set; }
        public string NRO_CDP { get; set; }
        public string COD_CON { get; set; }
        public System.DateTime FEC_CDP { get; set; }
        public Nullable<decimal> VAL_CDP { get; set; }
        public Nullable<System.DateTime> FEC_REG { get; set; }
        public string USBD { get; set; }
        public string USAP { get; set; }
    
        public virtual SOL_ADICIONES SOL_ADICIONES { get; set; }
    }
    
}
