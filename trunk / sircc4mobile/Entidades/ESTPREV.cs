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
    public partial class ESTPREV
    {
        public ESTPREV()
        {
            this.EP_CAP_JUR = new HashSet<EP_CAP_JUR>();
            this.EP_CDP = new HashSet<EP_CDP>();
            this.EP_CONMUN = new HashSet<EP_CONMUN>();
            this.EP_ESPTEC = new HashSet<EP_ESPTEC>();
            this.EP_OBLIGACIONESC = new HashSet<EP_OBLIGACIONESC>();
            this.EP_OBLIGACIONESE = new HashSet<EP_OBLIGACIONESE>();
            this.EP_POLIZAS = new HashSet<EP_POLIZAS>();
            this.EP_PROYECTOS = new HashSet<EP_PROYECTOS>();
            this.EP_FORMA_PAGO = new HashSet<EP_FORMA_PAGO>();
            this.EP_HESTADOEP = new HashSet<EP_HESTADOEP>();
        }
    
        public decimal ID { get; set; }
        public string NECE_EP { get; set; }
        public string OBJE_EP { get; set; }
        public string DESC_EP { get; set; }
        public Nullable<decimal> PLAZ1_EP { get; set; }
        public string TPLA1_EP { get; set; }
        public Nullable<decimal> PLAZ2_EP { get; set; }
        public string TPLA2_EP { get; set; }
        public string LUGE_EP { get; set; }
        public Nullable<decimal> PLIQ_EP { get; set; }
        public string FJUR_EP { get; set; }
        public Nullable<decimal> VAL_ENT_EP { get; set; }
        public Nullable<decimal> VAL_OTR_EP { get; set; }
        public string JFAC_SEL_EP { get; set; }
        public string CAP_FIN_EP { get; set; }
        public string CON_EXP_EP { get; set; }
        public string CAP_RES_EP { get; set; }
        public string FAC_ESC_EP { get; set; }
        public string ANA_EXI_EP { get; set; }
        public string IDE_DIL_EP { get; set; }
        public string CAR_DIL_EP { get; set; }
        public string DEP_NEC_EP { get; set; }
        public string STIP_CON_EP { get; set; }
        public Nullable<System.DateTime> FEC_ELA_EP { get; set; }
        public Nullable<System.DateTime> FEC_REG_EP { get; set; }
        public Nullable<System.DateTime> FEC_MOD_EP { get; set; }
        public string USAP_REG_EP { get; set; }
        public string USAP_MOD_EP { get; set; }
        public Nullable<System.DateTime> FEC_REV_EP { get; set; }
        public string USAP_REV_EP { get; set; }
        public string USAP_ELA_EP { get; set; }
        public Nullable<System.DateTime> FEC_ELAS_EP { get; set; }
        public string USAP_APR_EP { get; set; }
        public Nullable<System.DateTime> FEC_APR_EP { get; set; }
        public string USAP_ANU_EP { get; set; }
        public Nullable<System.DateTime> FEC_ANU_EP { get; set; }
        public string USAP_DAN_EP { get; set; }
        public Nullable<System.DateTime> FEC_DAN_EP { get; set; }
        public string DEP_SUP_EP { get; set; }
        public string CAR_SUP_EP { get; set; }
        public string VIG_EP { get; set; }
        public string IDE_APTE_EP { get; set; }
        public string CAR_APTE_EP { get; set; }
        public string CODIGO_EP { get; set; }
        public Nullable<decimal> GRUPOS_EP { get; set; }
        public Nullable<decimal> NUM_EMP_EP { get; set; }
        public string IDE_RES_EP { get; set; }
        public string CAR_RES_EP { get; set; }
        public string MOD_SEL_EP { get; set; }
        public Nullable<decimal> NRO_EP { get; set; }
        public string EST_EP { get; set; }
        public string EST_FLU_EP { get; set; }
        public string ES_PLAN_EP { get; set; }
        public string DEP_DEL_EP { get; set; }
        public string VAR_PPT_EP { get; set; }
        public string ENPLANC_EP { get; set; }
        public string NOM_PLA_EP { get; set; }
        public Nullable<decimal> ID_REV { get; set; }
        public Nullable<decimal> ID_APR { get; set; }
    
        public virtual DEPENDENCIA DEPENDENCIA { get; set; }
        public virtual DEPENDENCIA DEPENDENCIA1 { get; set; }
        public virtual DEPENDENCIA DEPENDENCIA2 { get; set; }
        public virtual ICollection<EP_CAP_JUR> EP_CAP_JUR { get; set; }
        public virtual EP_CARGO EP_CARGO { get; set; }
        public virtual EP_CARGO EP_CARGO1 { get; set; }
        public virtual EP_CARGO EP_CARGO2 { get; set; }
        public virtual EP_CARGO EP_CARGO3 { get; set; }
        public virtual ICollection<EP_CDP> EP_CDP { get; set; }
        public virtual ICollection<EP_CONMUN> EP_CONMUN { get; set; }
        public virtual ICollection<EP_ESPTEC> EP_ESPTEC { get; set; }
        public virtual EP_ESTADOS EP_ESTADOS { get; set; }
        public virtual EP_ESTFLU EP_ESTFLU { get; set; }
        public virtual ICollection<EP_OBLIGACIONESC> EP_OBLIGACIONESC { get; set; }
        public virtual ICollection<EP_OBLIGACIONESE> EP_OBLIGACIONESE { get; set; }
        public virtual ICollection<EP_POLIZAS> EP_POLIZAS { get; set; }
        public virtual ICollection<EP_PROYECTOS> EP_PROYECTOS { get; set; }
        public virtual TERCEROS TERCEROS { get; set; }
        public virtual TERCEROS TERCEROS1 { get; set; }
        public virtual TERCEROS TERCEROS2 { get; set; }
        public virtual TERCEROS TERCEROS3 { get; set; }
        public virtual TERCEROS TERCEROS4 { get; set; }
        public virtual TERCEROS TERCEROS5 { get; set; }
        public virtual SUBTIPOS SUBTIPOS { get; set; }
        public virtual TIPO_PLAZOS TIPO_PLAZOS { get; set; }
        public virtual TIPO_PLAZOS TIPO_PLAZOS1 { get; set; }
        public virtual TERCEROS TERCEROS6 { get; set; }
        public virtual TIPOSPROC TIPOSPROC { get; set; }
        public virtual ICollection<EP_FORMA_PAGO> EP_FORMA_PAGO { get; set; }
        public virtual ICollection<EP_HESTADOEP> EP_HESTADOEP { get; set; }
        public virtual EP_HESTADOEP EP_HESTADOEP_1 { get; set; }
        public virtual EP_HESTADOEP EP_HESTADOEP1 { get; set; }
    }
    
}