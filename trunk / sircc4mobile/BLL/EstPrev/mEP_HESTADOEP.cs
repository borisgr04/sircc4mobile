using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entidades;

namespace BLL.EstPrev
{
    class mEP_HESTADOEP : absBLL
    {
        public EP_HESTADOEP reg { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            
            return true;
        }
        protected internal override void AntesInsert()
        {
            //reg.FEC_REG = DateTime.Now;
            decimal ultId;
            try
            {
                ultId = ctx.EP_HESTADOEP.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            
            
            reg.FSIS_EP = DateTime.Now;
            reg.EST_EP = "AC";
            reg.ID = ultId + 1;

            //CAMBIAR DE ESTADO AL ESTUDIO PREVIO
            ESTPREV ep = ctx.ESTPREV.Where(t => t.ID == reg.ID_EP).FirstOrDefault();
            if (reg.TIP_EP == "RV") {
                ep.ID_REV = reg.ID;
            }
            if (reg.TIP_EP == "AP")
            {
                ep.ID_APR = reg.ID;
            }
            ep.EST_EP = reg.TIP_EP;

            if (reg.TIP_EP == "DA")
            {
                ep.ID_REV = null;
                ep.ID_APR = null;
                ep.EST_EP = "EL";
            }
            
            

            byaRpt.id = ultId.ToString();
            ctx.Entry(reg).State = EntityState.Added; //Adicionar Registro
            
        }
        //protected override void DespuesInsert();
        #endregion
    }
}

