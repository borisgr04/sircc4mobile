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
    public partial class EP_OBLIGACIONESE
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public string DES_OBLIG { get; set; }
        public Nullable<decimal> GRUPO { get; set; }
    
        public virtual ESTPREV ESTPREV { get; set; }
    }
    
}
