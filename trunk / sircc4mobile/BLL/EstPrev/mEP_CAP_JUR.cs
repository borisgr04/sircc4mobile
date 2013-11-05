using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;

namespace BLL.EstPrev
{

    public class mEP_CAP_JUR : absBLL
    {
        public EP_CAP_JUR reg { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {
            //et.FEC_REG = DateTime.Now;
            decimal ultId;
            try
            {
                ultId = ctx.EP_CAP_JUR.Max(t => t.ID);
            }
            catch
            {
                ultId = 0;
            }
            reg.ID = ultId + 1;
            byaRpt.id = ultId.ToString();
            ctx.EP_CAP_JUR.Add(reg);
            
        }
        //protected override void DespuesInsert();
        #endregion


        #region Delete

        protected internal override bool esValidoDelete()
        {
            return true;
        }

        protected internal override void AntesDelete()
        {

            ctx.Entry<EP_CAP_JUR>(reg).State = EntityState.Deleted;
        }
        //protected override void DespuesInsert();
        #endregion
    }
}
