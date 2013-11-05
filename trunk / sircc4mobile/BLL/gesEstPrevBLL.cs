using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;
using BLL.Vistas;

namespace BLL
{
    public class gesEstPrevBLL
    {
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }

        public List<vESTPREVCON> getEstPrevPl(string vig)
        {
            return getEstPrev(vig, "S");
        }

        public List<vESTPREVCON> getEstPrev(string vig)
        {   
                return getEstPrev(vig,"N");
         
        }
        private List<vESTPREVCON> getEstPrev(string vig, string es_pla)
        {
            List<vESTPREVCON> lt;
            using (ctx = new Entities())
            {
                lt = ctx.ESTPREV.Where(est => est.VIG_EP == vig && est.ES_PLAN_EP == es_pla).
                                                     Select(est => new vESTPREVCON
                                                     {
                                                         ID = est.ID,
                                                         VIG_EP = est.VIG_EP,
                                                         OBJE_EP = est.OBJE_EP,
                                                         NOM_TIP_CON_EP = est.SUBTIPOS.TIPOSCONT.NOM_TIP,
                                                         NOM_STIP_CON_EP = est.SUBTIPOS.NOM_STIP,
                                                         DEP_NEC_EP = est.DEP_NEC_EP,
                                                         MODALIDAD = est.TIPOSPROC.NOM_TPROC
                                                     }
                                                         ).ToList();
                return lt;
            }
        }
        
        public class vESTPREVCON
        {
            public decimal ID { get; set; }
            public string VIG_EP { get; set; }
            public string OBJE_EP { get; set; }
            public string DEP_NEC_EP { get; set; }
            public string NOM_TIP_CON_EP { get; set; }
            public string NOM_STIP_CON_EP { get; set; }
            public string MODALIDAD { get; set; }
            public string CLASE
            {

                get
                {
                    return NOM_TIP_CON_EP + " " + NOM_STIP_CON_EP;
                }
            }


        }
    }
}
