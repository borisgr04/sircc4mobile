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
    public partial class USUARIOS
    {
        public USUARIOS()
        {
            this.ANX_CONTRATOS = new HashSet<ANX_CONTRATOS>();
            this.AUDITORIA = new HashSet<AUDITORIA>();
        }
    
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string CED_USU { get; set; }
        public string APE_USU { get; set; }
        public string NOM_USU { get; set; }
        public string PER_USU { get; set; }
        public string ESTADO { get; set; }
    
        public virtual ICollection<ANX_CONTRATOS> ANX_CONTRATOS { get; set; }
        public virtual ICollection<AUDITORIA> AUDITORIA { get; set; }
    }
    
}
