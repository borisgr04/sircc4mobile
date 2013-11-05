using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entidades;

namespace BLL.EstPrev
{
    
    public class mEP_FORMA_PAGO : absBLL
    {
        public EP_FORMA_PAGO fp { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {
            fp.FEC_REG = DateTime.Now;
            decimal ultId;
            try
            {
                ultId = ctx.EP_FORMA_PAGO.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            fp.ID = ultId + 1;
            byaRpt.id = ultId.ToString();
            ctx.EP_FORMA_PAGO.Add(fp);

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
            fp.FEC_MOD = DateTime.Now;

            var found = ctx.EP_FORMA_PAGO.Find(fp.ID);
            if (found != null)
            {
                var entry = ctx.Entry(found);
                entry.OriginalValues.SetValues(found);
                entry.CurrentValues.SetValues(fp);
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
            ctx.Entry(fp).State = EntityState.Deleted;
        }
        //protected override void DespuesInsert();
        #endregion

    }

}
