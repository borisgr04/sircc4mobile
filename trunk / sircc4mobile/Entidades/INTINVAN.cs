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
    public partial class INTINVAN
    {
        public INTINVAN()
        {
            this.INTANT_CONT = new HashSet<INTANT_CONT>();
        }
    
        public string COD_INVAN { get; set; }
        public string NOM_INVAN { get; set; }
        public string EST_INVAN { get; set; }
    
        public virtual ICollection<INTANT_CONT> INTANT_CONT { get; set; }
    }
    
}
