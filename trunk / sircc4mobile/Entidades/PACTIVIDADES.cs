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
    public partial class PACTIVIDADES
    {
        public string COD_ACT { get; set; }
        public string NOM_ACT { get; set; }
        public string COD_TPRO { get; set; }
        public string VIGENCIA { get; set; }
        public Nullable<decimal> ORDEN { get; set; }
        public string OCUPADO { get; set; }
        public string EST_PROC { get; set; }
        public string ESTADO { get; set; }
        public string OBLIGATORIO { get; set; }
        public string DIA_NOHABIL { get; set; }
        public string NOTIFICAR { get; set; }
        public string MFECINI { get; set; }
        public string MHORINI { get; set; }
        public string MFECFIN { get; set; }
        public string MHORFIN { get; set; }
        public string UBICACION { get; set; }
    
        public virtual TIPOSPROC TIPOSPROC { get; set; }
        public virtual PESTADOS PESTADOS { get; set; }
    }
    
}
