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
    public partial class PROYECTOS
    {
        public PROYECTOS()
        {
            this.GPPROYECTOS = new HashSet<GPPROYECTOS>();
            this.MOD_CPROYECTOS = new HashSet<MOD_CPROYECTOS>();
            this.EP_PROYECTOS = new HashSet<EP_PROYECTOS>();
        }
    
        public string VIGENCIA { get; set; }
        public string PROYECTO { get; set; }
        public string NOMBRE_PROYECTO { get; set; }
        public Nullable<System.DateTime> FECHA_RAD { get; set; }
        public string COMITE { get; set; }
        public Nullable<decimal> VALOR { get; set; }
        public string ESTADO { get; set; }
    
        public virtual ICollection<GPPROYECTOS> GPPROYECTOS { get; set; }
        public virtual ICollection<MOD_CPROYECTOS> MOD_CPROYECTOS { get; set; }
        public virtual PROYECTOS PROYECTOS1 { get; set; }
        public virtual PROYECTOS PROYECTOS2 { get; set; }
        public virtual ICollection<EP_PROYECTOS> EP_PROYECTOS { get; set; }
    }
    
}
