using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;

namespace BLL.EstPrev
{
    
    public class mEP_POLIZAS : absBLL
    {
        public EP_POLIZAS reg { get; set; }

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
                ultId = ctx.EP_POLIZAS.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            reg.ID = ultId + 1;
            byaRpt.id = ultId.ToString();
            ctx.EP_POLIZAS.Add(reg);
            
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

            var found = ctx.EP_POLIZAS.Find(reg.ID);
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

            ctx.Entry<EP_POLIZAS>(reg).State = EntityState.Deleted;
        }
        //protected override void DespuesInsert();
        #endregion

    }
}
