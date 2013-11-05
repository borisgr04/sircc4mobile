using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;
using Financiero;

namespace BLL.EstPrev
{
    
    public class mEP_CDP_I : absBLL
    {
        public EP_CDP reg { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            IFinanciero f = FinancieroFactory.Create("SIIAF");
            SF_CDP cdp= f.GetCDP(reg.NRO_CDP,  reg.FEC_CDP.Year.ToString());
            
            reg.NRO_CDP= cdp.nro_cdp;
            reg.FEC_CDP = cdp.fec_cdp;
            reg.VAL_CDP= cdp.val_cdp;
            var i=0;
            foreach ( SF_RubrosCDP r in cdp.Rubros){
                //Sinno existe en RUBROS agregarlo tambien en rubros
                bool existe=(ctx.RUBROS.Where(t => t.COD_RUB == r.cod_rub && t.VIGENCIA == (short)reg.FEC_CDP.Year).Count()>0);
                if (!existe) {
                    RUBROS rub = new RUBROS();
                    rub.COD_RUB = r.cod_rub;
                    rub.COD_UNIDAD = r.cod_unidad_rub;
                    rub.DES_RUB = r.nom_rub;
                    rub.COD_RECURSO = r.cod_recurso_rub;
                    rub.VIGENCIA = (short)reg.FEC_CDP.Year;
                    ctx.Entry(rub).State = EntityState.Added; //Adicionar Registro
                }
                reg.EP_RUBROS_CDP.Add( new EP_RUBROS_CDP{
                    COD_RUB = r.cod_rub,
                    GRUPO= (int)reg.GRUPO,
                    ID_EP= reg.ID_EP,
                    VALOR= r.val_rub,
                    NRO_CDP = reg.NRO_CDP,
                    ID = i,
                    VIG_CDP= (short)reg.FEC_CDP.Year}
                    );
            }
            


            
            return true;
        }
        protected internal override void AntesInsert()
        {
            //reg.FEC_REG = DateTime.Now;
            decimal ultId;
            try
            {
                ultId = ctx.EP_CDP.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            reg.ID = ultId + 1;
            byaRpt.id = ultId.ToString();
            ctx.Entry(reg).State = EntityState.Added; //Adicionar Registro
            
        }
        //protected override void DespuesInsert();
        #endregion

        #region Update

        protected internal override bool esValidoUpdate()
        {
            return true;
        }
        protected internal override void AntesUpdate()
        {
            //reg.FEC_MOD = DateTime.Now;

            var found = ctx.EP_CDP.Find(reg.ID);
            if (found != null)
            {
                var entry = ctx.Entry(found);
                entry.OriginalValues.SetValues(found);
                entry.CurrentValues.SetValues(reg);
            }
            else
            {
                throw new Exception("No se encontro el registró");
            }


        }
        //protected override void DespuesUpdate();
        #endregion

        #region Delete

        protected internal override bool esValidoDelete()
        {
            return true;
        }

        protected internal override void AntesDelete()
        {

            ctx.Entry(reg).State = EntityState.Deleted;
        }
        //protected override void DespuesInsert();
        #endregion

    }
}
