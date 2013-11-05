using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;
using AutoMapper;

namespace BLL.EstPrev
{
    public class mESTPREVplantilla : absBLL
    {
        public ESTPREV ep { get; set; }

        #region Insert
        

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {

            
            ESTPREV found = new ESTPREV();
            CopiasEstPrev(found);
            //ObligacionesC 
            ObligC(found);
            //Obligaciones Entidad
            ObligE(found);
            //Capacidad Juridica
            CapJuridica(found);
            //Polizas
            Polizas(found);
            //Regiosnes//Municipios
            RegionesMunicipios(found);
            //Especificaciones Tecnicas
            EspTecnicas(found);
            ctx.Entry(ep).State = EntityState.Detached;
            ctx.ESTPREV.Add(found);
            //ctx.Entry(found).State = EntityState.Added;
        }

        private void CopiasEstPrev(ESTPREV found)
        {
            decimal ultId = 0;
            decimal ultNro = 0;
            ep = ctx.ESTPREV.Find(ep.ID);
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


            found.NECE_EP = ep.NECE_EP;
            found.OBJE_EP = ep.OBJE_EP;
            found.DESC_EP = ep.DESC_EP;
            found.PLAZ1_EP = ep.PLAZ1_EP;
            found.TPLA1_EP = ep.TPLA1_EP;
            found.PLAZ2_EP = ep.PLAZ2_EP;
            found.TPLA2_EP = ep.TPLA2_EP;
            found.LUGE_EP = ep.LUGE_EP;
            found.PLIQ_EP = ep.PLIQ_EP;
            found.FJUR_EP = ep.FJUR_EP;
            found.VAL_ENT_EP = ep.VAL_ENT_EP;
            found.VAL_OTR_EP = ep.VAL_OTR_EP;
            found.JFAC_SEL_EP = ep.JFAC_SEL_EP;
            found.CAP_FIN_EP = ep.CAP_FIN_EP;
            found.CON_EXP_EP = ep.CON_EXP_EP;
            found.CAP_RES_EP = ep.CAP_RES_EP;
            found.FAC_ESC_EP = ep.FAC_ESC_EP;
            found.ANA_EXI_EP = ep.ANA_EXI_EP;
            found.IDE_DIL_EP = ep.IDE_DIL_EP;
            found.CAR_DIL_EP = ep.CAR_DIL_EP;
            found.DEP_NEC_EP = ep.DEP_NEC_EP;
            found.STIP_CON_EP = ep.STIP_CON_EP;
            found.FEC_ELA_EP = ep.FEC_ELA_EP;
            found.FEC_MOD_EP = ep.FEC_MOD_EP;
            found.USAP_MOD_EP = ep.USAP_MOD_EP;
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

            found.ID = ultId + 1;//Consecutivo unico
            found.NRO_EP = ultNro + 1;//Consecutivvo por año.
            found.EST_EP = "EL"; //Por defecto en elaboración
            found.EST_FLU_EP = "NE"; // Por defecto el estado del flujo del proceso esta en no enviado.
            found.CODIGO_EP = found.VIG_EP + "-" +  found.NRO_EP.ToString().PadLeft(5, '0'); //Codigo Clave
            found.FEC_ELAS_EP = DateTime.Now;//fecha de elaboración real
            found.FEC_REG_EP = DateTime.Now;
            found.ES_PLAN_EP = "";

            byaRpt.id = found.ID.ToString();

            foreach (EP_PROYECTOS f in ep.EP_PROYECTOS)
            {
                EP_PROYECTOS d = new EP_PROYECTOS();
                d.COD_PRO = f.COD_PRO;
                d.FEC_REG = found.FEC_REG_EP;
                d.USAP_REG = found.USAP_REG_EP;
                found.EP_PROYECTOS.Add(d);
            }
        }

        private void ObligC(ESTPREV found)
        {
            decimal ultOblC = 0;
            try
            {
                ultOblC = ctx.EP_OBLIGACIONESC.Max(t => t.ID) + 1;
            }
            catch
            {
            }

            foreach (EP_OBLIGACIONESC f in ep.EP_OBLIGACIONESC)
            {
                EP_OBLIGACIONESC d = new EP_OBLIGACIONESC();
                d.DES_OBLIG = f.DES_OBLIG;
                d.GRUPO = f.GRUPO;
                //d.FEC_REG = found.FEC_REG_EP;
                //d.USAP_REG = found.USAP_REG_EP;
                d.ID = ultOblC;
                ultOblC++;
                found.EP_OBLIGACIONESC.Add(d);
            }
        }

        private void ObligE(ESTPREV found)
        {
            decimal ultOblE = 0;
            try
            {
                ultOblE = ctx.EP_OBLIGACIONESE.Max(t => t.ID) + 1;
            }
            catch
            {
            }

            foreach (EP_OBLIGACIONESE f in ep.EP_OBLIGACIONESE)
            {
                EP_OBLIGACIONESE d = new EP_OBLIGACIONESE();
                d.DES_OBLIG = f.DES_OBLIG;
                d.GRUPO = f.GRUPO;
                //d.FEC_REG = found.FEC_REG_EP;
                //d.USAP_REG = found.USAP_REG_EP;
                d.ID = ultOblE;
                ultOblE++;
                found.EP_OBLIGACIONESE.Add(d);
            }
        }

        private void CapJuridica(ESTPREV found)
        {
            //Capacidad Juridica
            decimal ultCapJur = 0;
            try
            {
                ultCapJur = ctx.EP_CAP_JUR.Max(t => t.ID) + 1;
            }
            catch
            {
            }
            foreach (EP_CAP_JUR f in ep.EP_CAP_JUR)
            {
                EP_CAP_JUR d = new EP_CAP_JUR();
                d.DES_CAPJ = f.DES_CAPJ;
                d.ID_CAPJ = f.ID_CAPJ;
                //d.FEC_REG = found.FEC_REG_EP;
                //d.USAP_REG = found.USAP_REG_EP;
                d.ID = ultCapJur;
                ultCapJur++;
                found.EP_CAP_JUR.Add(d);
            }
        }

        private void Polizas(ESTPREV found)
        {
            decimal ultPol = 0;
            try
            {
                ultPol = ctx.EP_POLIZAS.Max(t => t.ID) + 1;
            }
            catch
            {
            }
            foreach (EP_POLIZAS f in ep.EP_POLIZAS)
            {
                EP_POLIZAS d = new EP_POLIZAS();
                d.APARTIRDE = f.APARTIRDE;
                d.CAL_APARTIRDE = f.CAL_APARTIRDE;
                d.CAL_VIG_POL = f.CAL_VIG_POL;
                d.CALCULOPOL = f.CALCULOPOL;
                d.COD_POL = f.COD_POL;
                d.GRUPO = f.GRUPO;
                d.POR_SMMLV = f.POR_SMMLV;
                d.TIPO = f.TIPO;
                d.VIGENCIA = f.VIGENCIA;
                d.ID = ultPol;
                //d.FEC_REG = found.FEC_REG_EP;
                //d.USAP_REG = found.USAP_REG_EP;
                ultPol++;
                found.EP_POLIZAS.Add(d);
            }
        }

        private void RegionesMunicipios(ESTPREV found)
        {
            decimal ultMun = 0;
            try
            {
                ultMun = ctx.EP_CONMUN.Max(t => t.ID) + 1;
            }
            catch
            {
            }
            foreach (EP_CONMUN f in ep.EP_CONMUN)
            {
                EP_CONMUN d = new EP_CONMUN();
                d.COD_MUN = f.COD_MUN;
                d.ID = ultMun;
                //d.FEC_REG = found.FEC_REG_EP;
                //d.USAP_REG = found.USAP_REG_EP;
                ultMun++;
                found.EP_CONMUN.Add(d);
            }
        }

        private void EspTecnicas(ESTPREV found)
        {
            decimal ultET = 0;
            try
            {
                ultET = ctx.EP_ESPTEC.Max(t => t.ID) + 1;
            }
            catch
            {
            }
            foreach (EP_ESPTEC f in ep.EP_ESPTEC)
            {
                EP_ESPTEC d = new EP_ESPTEC();
                d.CANT_ITEM = f.CANT_ITEM;
                d.DESC_ITEM = f.DESC_ITEM;
                d.GRUPO = f.GRUPO;
                d.PORC_IVA = d.PORC_IVA;
                d.UNI_ITEM = f.UNI_ITEM;
                d.USAP_REG = ep.USAP_REG_EP;
                d.VAL_UNI_ITEM = f.VAL_UNI_ITEM;
                d.ID = ultET;
                d.FEC_REG = found.FEC_REG_EP;
                d.USAP_REG = found.USAP_REG_EP;
                ultET++;
                found.EP_ESPTEC.Add(d);
            }
        }

        
        //protected override void DespuesInsert();

        #endregion

   
    }
}
