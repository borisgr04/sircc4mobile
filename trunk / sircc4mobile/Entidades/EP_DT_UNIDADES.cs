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
    public partial class EP_DT_UNIDADES
    {
        public EP_DT_UNIDADES()
        {
            this.EP_ESPTEC = new HashSet<EP_ESPTEC>();
        }
    
        public string COD_UNI { get; set; }
        public string NOM_UNI { get; set; }
    
        public virtual ICollection<EP_ESPTEC> EP_ESPTEC { get; set; }
    }
    
}
