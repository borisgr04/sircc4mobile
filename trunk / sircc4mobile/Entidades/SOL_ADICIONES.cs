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
    public partial class SOL_ADICIONES
    {
        public SOL_ADICIONES()
        {
            this.MIN_MOD_CONTRATO = new HashSet<MIN_MOD_CONTRATO>();
            this.MOD_CDP_CONTRATOS = new HashSet<MOD_CDP_CONTRATOS>();
            this.MOD_COBLIGACIONES = new HashSet<MOD_COBLIGACIONES>();
        }
    
        public string NUM_SOL_ADI { get; set; }
        public Nullable<System.DateTime> FEC_SUS_ADI { get; set; }
        public string COD_CON { get; set; }
        public string TIP_ADI { get; set; }
        public string OBSER { get; set; }
        public string ID_ABOG_ENC { get; set; }
        public Nullable<System.DateTime> FECHA_ASIGNADO { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public string USAP { get; set; }
        public string USBD { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public Nullable<System.DateTime> FEC_RECIBIDO { get; set; }
    
        public virtual HREVSOLADI HREVSOLADI { get; set; }
        public virtual ICollection<MIN_MOD_CONTRATO> MIN_MOD_CONTRATO { get; set; }
        public virtual ICollection<MOD_CDP_CONTRATOS> MOD_CDP_CONTRATOS { get; set; }
        public virtual ICollection<MOD_COBLIGACIONES> MOD_COBLIGACIONES { get; set; }
        public virtual MOD_CPROYECTOS MOD_CPROYECTOS { get; set; }
    }
    
}
