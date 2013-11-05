using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;
using BLL.Vistas;
//using System.Linq.Dynamic;


namespace BLL
{
    public class conEstPrev
    {
        
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }

        //public List<vESTPREV> Consulta(vESTPREV filtro)
        //{
        //    //IList<ESTPREV> result;
        //    using (ctx = new Entities())
        //    {
        //           // result =(from t in ctx.ESTPREV
        //           // where ("t.ID == filtro.ID")
        //                   //select (new vESTPREV
        //                   //{
        //                   //    ID = t.ID,
        //                   //     NECE_EP = t.NECE_EP,
        //                   //      OBJE_EP = t.OBJE_EP
        //                   //})).ToList<vESTPREV>();

        //           /* var query = ctx.ESTPREV
        //                       .Where("ID == " + filtro.ID).ToList<ESTPREV>();*/
        //         //.Where("ID == " + filtro.ID)
        //        string QueryWhere="";
        //        if (filtro.ID>0) {
        //            QueryWhere = " ID == " + filtro.ID;
        //        }
        //      //if ( !String.IsNullOrEmpty(filtro.COD_PRO)){
        //      //   QueryWhere= !string.IsNullOrEmpty(QueryWhere)?" and "+" COD_PRO=="+filtro.COD_PRO:" COD_PRO=="+filtro.COD_PRO;
        //      //}

        //      if (!filtro.STIP_CON_EP.Equals("0"))
        //      {
        //          QueryWhere = !string.IsNullOrEmpty(QueryWhere) ? QueryWhere + " AND " + " STIP_CON_EP ==" + filtro.STIP_CON_EP : " STIP_CON_EP == " + filtro.STIP_CON_EP;
        //      }
                   
        //      var query = ctx.ESTPREV
        //      .Where(QueryWhere)
        //      .Select(t=> new vESTPREV{
        //                              ID= t.ID,
        //                              NECE_EP = t.NECE_EP,
        //                              OBJE_EP = t.OBJE_EP,
        //                               NOM_TIP_CON_EP=t.SUBTIPOS.TIPOSCONT.NOM_TIP
                                     
                                       
        //        })
        //        .ToList<vESTPREV>();
        //        return query;     
        //    }
        //}




       public IList<vESTPREVCON> Consulta2(vESTPREVCON filtro)
        {
            using (ctx = new Entities())
            {
                //Filtro Basico, X VIGENCIA Y POR INTERVENTOR
                IQueryable<vESTPREVCON> custQuery = (from est in ctx.ESTPREV
                                                       //where ic.IDE_INT == cFil.Ide_Interventor
                                                     where est.VIG_EP == filtro.VIG_EP
                                                     orderby est.ID
                                                      select (new vESTPREVCON
                                                       {
                                                          ID = est.ID,
                                                          VIG_EP = est.VIG_EP,
                                                          FEC_ELA_EP = est.FEC_ELA_EP,
                                                          VAL_ENT_EP=est.VAL_ENT_EP,
                                                          STIP_CON_EP=est.STIP_CON_EP,
                                                           //modalidad
                                                          EST_EP=est.EST_EP,
                                                          DEP_NEC_EP= est.DEP_NEC_EP,
                                                          IDE_DIL_EP = est.IDE_DIL_EP,
                                                          OBJE_EP = est.OBJE_EP,
                                                          NOM_PLAZ1= est.TIPO_PLAZOS.NOM_PLA,
                                                          NOM_PLAZ2 = est.TIPO_PLAZOS1.NOM_PLA,
                                                       })).AsQueryable<vESTPREVCON>();
                //FILTRO ADICIONALES
               if( (!string.IsNullOrEmpty(filtro.TIPO)) && (!filtro.TIPO.Equals("CN"))){
                   custQuery = custQuery.Where(x => x.EST_EP == filtro.TIPO);
               }

               if (filtro.ID> 0)
               {
                   custQuery = custQuery.Where(x => x.ID == filtro.ID);
               }

                if (!string.IsNullOrEmpty(filtro.STIP_CON_EP))
                {
                    custQuery = custQuery.Where(c => c.STIP_CON_EP.Equals(filtro.STIP_CON_EP));
                }
                if (!string.IsNullOrEmpty(filtro.DEP_NEC_EP))
                {
                    custQuery = custQuery.Where(c => c.DEP_NEC_EP.Equals(filtro.DEP_NEC_EP));
                }
                if (!string.IsNullOrEmpty(filtro.IDE_DIL_EP))
                {
                    custQuery = custQuery.Where(c => c.IDE_DIL_EP == filtro.IDE_DIL_EP);
                }
                if (!string.IsNullOrEmpty(filtro.VIG_EP))
                {
                    custQuery = custQuery.Where(c => c.VIG_EP.Equals(filtro.VIG_EP));
                }
                if ((filtro.VAL_ENT_EP_INI>=0) && (filtro.VAL_ENT_EP_FIN>0))
                {
                    custQuery = custQuery.Where(c => c.VAL_ENT_EP >= filtro.VAL_ENT_EP_INI && c.VAL_ENT_EP <= filtro.VAL_ENT_EP_FIN);
                }

                //if (((filtro.FEC_ELA_EP_INI == 0))&&((filtro.FEC_ELA_EP_FIN)))
                //{
                //    custQuery = custQuery.Where(c => c.FEC_ELA_EP >= filtro.FEC_ELA_EP_INI && c.FEC_ELA_EP <= filtro.FEC_ELA_EP_FIN);
                //}
                if (!string.IsNullOrEmpty(filtro.EST_EP))
                {
                    custQuery = custQuery.Where(c => c.EST_EP.Equals(filtro.EST_EP));
                }
                if (!string.IsNullOrEmpty(filtro.OBJE_EP))
                {
                    custQuery = custQuery.Where(c => c.OBJE_EP.Contains(filtro.OBJE_EP));
                }
                //if (!string.IsNullOrEmpty(cFil.Dep_Del))
                //{
                //    custQuery = custQuery.Where(c => c.Dep_Del.Contains(cFil.Dep_Del));
                //}
                //if (!string.IsNullOrEmpty(cFil.Objeto))
                //{
                //    custQuery = custQuery.Where(c => c.Objeto.Contains(cFil.Objeto));
                //}
                //if (!string.IsNullOrEmpty(cFil.Ide_Contratista))
                //{
                //    custQuery = custQuery.Where(c => c.Ide_Contratista.Equals(cFil.Ide_Contratista));
                //}
                //if (!string.IsNullOrEmpty(cFil.Estado))
                //{
                //    custQuery = custQuery.Where(c => c.Estado.Equals(cFil.Estado));
                //}
                return custQuery.ToList();

            }



        }



    }
}
