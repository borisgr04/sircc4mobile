using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;

namespace BLL.EstPrev
{
    
    public class mEP_ESPTEC : absBLL
    {
        public EP_ESPTEC et { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {
            et.FEC_REG = DateTime.Now;
            decimal ultId;
            try
            {
                ultId = ctx.EP_ESPTEC.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            et.ID = ultId + 1;
            byaRpt.id = ultId.ToString();
            ctx.EP_ESPTEC.Add(et);
            
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
            et.FEC_MOD = DateTime.Now;

            var found = ctx.EP_ESPTEC.Find(et.ID);
            if (found != null)
            {
                var entry = ctx.Entry(found);
                entry.OriginalValues.SetValues(found);
                entry.CurrentValues.SetValues(et);
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

            ctx.Entry<EP_ESPTEC>(et).State = EntityState.Deleted;
        }
        //protected override void DespuesInsert();
        #endregion

    }
}
