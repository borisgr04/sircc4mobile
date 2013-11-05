using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;

namespace BLL.mobile
{
    public class mobGeneral
    {
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }

        public short getVigenciasUlt()
        {
            short vig;
            using (ctx = new Entities())
            {
                vig = ctx.VIGENCIAS.OrderByDescending(t => t.YEAR_VIG).
                    Select(t => t.YEAR_VIG).FirstOrDefault();
                return vig;
            }
        }
        public List<short> getVigencias()
        {
            List<short> lt;
            using (ctx = new Entities())
            {
                lt = ctx.VIGENCIAS.OrderByDescending(t=> t.YEAR_VIG).
                    Select(t => t.YEAR_VIG) .ToList();
                return lt;
            }
        }
    }
}
