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
    public partial class MENU_PERFIL
    {
        public string MENUID { get; set; }
        public string PERFIL { get; set; }
        public string MODULO { get; set; }
        public string USAP { get; set; }
        public string USBD { get; set; }
        public Nullable<System.DateTime> FREG { get; set; }
        public Nullable<System.DateTime> FNOV { get; set; }
    
        public virtual MENU2 MENU2 { get; set; }
    }
    
}