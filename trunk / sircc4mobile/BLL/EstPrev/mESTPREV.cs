using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;
using BLL.Vistas;
using AutoMapper;

namespace BLL.EstPrev
{
    public class mESTPREV : absBLL
    {
        Dictionary<string, string> dEst{get; set;}
        
        public mESTPREV() {
            dEst = new Dictionary<string, string>();
            dEst.Add("EL","EL");
            dEst.Add("RV","EL");
            dEst.Add("AP","RV");
            dEst.Add("DA","AP");
            dEst.Add("CN","*");
        } 
        public ESTPREV ep { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {

            decimal ultId = 0;
            decimal ultNro = 0;
            try
            {
             
                ultId = ctx.ESTPREV.Max(t => t.ID);
                
            }
            catch { }
            
            try
            {
                ultNro = (decimal)ctx.ESTPREV.Where(t => t.VIG_EP == ep.VIG_EP).Max(t => t.NRO_EP);
            }
            catch { } 
            
            ep.ID = ultId + 1;//Consecutivo unico
            ep.NRO_EP = ultNro + 1;//Consecutivvo por año.
            ep.EST_EP = "EL"; //Por defecto en elaboración
            ep.EST_FLU_EP = "NE"; // Por defecto el estado del flujo del proceso esta en no enviado.
            ep.CODIGO_EP = ep.VIG_EP + "-" + ep.NRO_EP.ToString().PadLeft(5, '0'); //Codigo Clave
            ep.FEC_ELAS_EP = DateTime.Now;//fecha de elaboración real
            ep.FEC_REG_EP = DateTime.Now;
            byaRpt.id = ep.ID.ToString();
            ctx.ESTPREV.Add(ep);
        }
        //protected override void DespuesInsert();

        #endregion

        #region Update


        protected internal override void AntesUpdate()
        {
            ep.FEC_MOD_EP = DateTime.Now;
            var found= ctx.ESTPREV.Find(ep.ID);
            if (found!= null)
            {
                found.FEC_MOD_EP = DateTime.Now;
                found.NECE_EP = ep.NECE_EP;
                found.OBJE_EP=ep.OBJE_EP;
                found.DESC_EP =ep.DESC_EP ;
                found.PLAZ1_EP =ep.PLAZ1_EP ;
                found.TPLA1_EP =ep.TPLA1_EP ;
                found.PLAZ2_EP =ep.PLAZ2_EP;
                found.TPLA2_EP =ep.TPLA2_EP ;
                found.LUGE_EP =ep.LUGE_EP ;
                found.PLIQ_EP =ep.PLIQ_EP ;
                found.FJUR_EP =ep.FJUR_EP ;
                found.VAL_ENT_EP =ep.VAL_ENT_EP ;
                found.VAL_OTR_EP =ep.VAL_OTR_EP ;
                found.JFAC_SEL_EP =ep.JFAC_SEL_EP ;
                found.CAP_FIN_EP =ep.CAP_FIN_EP ;
                found.CON_EXP_EP =ep.CON_EXP_EP ;
                found.CAP_RES_EP =ep.CAP_RES_EP;
                found.FAC_ESC_EP =ep.FAC_ESC_EP ;
                found.ANA_EXI_EP =ep.ANA_EXI_EP ;
                found.IDE_DIL_EP =ep.IDE_DIL_EP; 
                found.CAR_DIL_EP =ep.CAR_DIL_EP ;
                found.DEP_NEC_EP =ep.DEP_NEC_EP;
                found.STIP_CON_EP =ep.STIP_CON_EP;
                found.FEC_ELA_EP =ep.FEC_ELA_EP;
                found.FEC_MOD_EP =ep.FEC_MOD_EP ;
                found.USAP_MOD_EP =ep.USAP_MOD_EP;
                found.DEP_SUP_EP = ep.DEP_SUP_EP;
                found.CAR_SUP_EP = ep.CAR_SUP_EP;
                found.VIG_EP = ep.VIG_EP;
                found.IDE_APTE_EP = ep.IDE_APTE_EP;
                found.CAR_APTE_EP = ep.CAR_APTE_EP;
                found.GRUPOS_EP = ep.GRUPOS_EP;
                found.NUM_EMP_EP = ep.NUM_EMP_EP;
                found.IDE_RES_EP = ep.IDE_RES_EP;
                found.CAR_RES_EP = ep.CAR_RES_EP;
                found.MOD_SEL_EP = ep.MOD_SEL_EP;
                found.DEP_DEL_EP = ep.DEP_DEL_EP;

                //al hacer el inseert la primera vez
                //found.FEC_REG_EP =
                //found.USAP_REG_EP =
                //AHORA QUE SE MODIFICA
                //AL REVISAR
                //found.FEC_REV_EP =
                //found.USAP_REV_EP =
        
                //AL APROBAR
                //found.USAP_APR_EP =
                //found.FEC_APR_EP =
                //ANULAR
                //found.USAP_ANU_EP =ep.USAP_ANU_EP ;
                //found.FEC_ANU_EP =ep.FEC_ANU_EP ;
                //DESANULAR
                //found.USAP_DAN_EP =ep.USAP_DAN_EP ;
                //found.FEC_DAN_EP =ep.FEC_DAN_EP ;

                
                //AL CREAR
                //found.CODIGO_EP =ep.CODIGO_EP;
                //found.NRO_EP =
                // AL CAMBIAR DE ESTADO
                //found.EST_EP =
                //AL ENVIAR O RECIBIR
                //found.EST_FLU_EP =

                ctx.Entry(found).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("No se encontro el registró");
            }

        }


        #endregion

        public vESTPREV GetPK(string tipo)
        {
            vESTPREV Reg = new vESTPREV();
            Mapper.CreateMap<ESTPREV, vESTPREV>();

            
            decimal id = ep.ID;
            ep = null;
            using (ctx = new Entities())
            {
                string est = dEst[tipo];
                if (est == "*")
                {
                    ep = ctx.ESTPREV.Where(t => t.ID == id ).FirstOrDefault<ESTPREV>();
                }
                else {
                    ep = ctx.ESTPREV.Where(t => t.ID == id && t.EST_EP.Contains(est)).FirstOrDefault<ESTPREV>();
                }
                if (ep != null)
                {
                    Mapper.Map(ep, Reg);
                    if (ep.STIP_CON_EP != null)
                    {
                        Reg.TIP_CON_EP = ep.SUBTIPOS.COD_TIP;
                    }
                }
                return Reg;
            }

            
            
        }
    }
}
