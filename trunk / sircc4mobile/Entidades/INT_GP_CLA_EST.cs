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
    public partial class INT_GP_CLA_EST
    {
        public INT_GP_CLA_EST()
        {
            this.INT_GP_RUT_EST = new HashSet<INT_GP_RUT_EST>();
        }
    
        public string COD_CLA { get; set; }
        public string NOM_CLA { get; set; }
        public string USAP { get; set; }
        public string USBD { get; set; }
        public Nullable<System.DateTime> FEC_REG { get; set; }
        public string USAPM { get; set; }
        public string USBDM { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public string EST_EST { get; set; }
    
        public virtual ICollection<INT_GP_RUT_EST> INT_GP_RUT_EST { get; set; }
    }
    
}
