﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Financiero
{
    public partial class SIF_Entities : DbContext
    {
        public SIF_Entities()
            : base("name=SIF_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<DCOMPROMISO> DCOMPROMISO { get; set; }
        public DbSet<DEGRESO> DEGRESO { get; set; }
        public DbSet<DRESERVA> DRESERVA { get; set; }
        public DbSet<MCOMPROMISO> MCOMPROMISO { get; set; }
        public DbSet<MEGRESO> MEGRESO { get; set; }
        public DbSet<MORDEN> MORDEN { get; set; }
        public DbSet<MORDEN_EGRESO> MORDEN_EGRESO { get; set; }
        public DbSet<MRESERVA> MRESERVA { get; set; }
        public DbSet<PPTO_GASTOS_V1> PPTO_GASTOS_V1 { get; set; }
    }
}
