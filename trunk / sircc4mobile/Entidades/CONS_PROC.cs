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
    public partial class CONS_PROC
    {
        public short VIGENCIA { get; set; }
        public string DEP_DEL { get; set; }
        public string TIP_PROC { get; set; }
        public Nullable<decimal> INICIAL { get; set; }
        public Nullable<decimal> SIGUIENTE { get; set; }
    
        public virtual DEPENDENCIA DEPENDENCIA { get; set; }
        public virtual VIGENCIAS VIGENCIAS { get; set; }
        public virtual TIPOSPROC TIPOSPROC { get; set; }
    }
    
}
