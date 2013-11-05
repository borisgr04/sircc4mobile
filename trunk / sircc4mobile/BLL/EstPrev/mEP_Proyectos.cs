using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;
using System.Data;


namespace BLL.EstPrev
{
    
    public class mEP_Proyectos : absBLL
    {
        public EP_PROYECTOS pry { get; set; }

        #region Insert

        protected internal override bool esValidoInsert()
        {
            return true;
        }
        protected internal override void AntesInsert()
        {
            pry.FEC_REG = DateTime.Now;
            ctx.EP_PROYECTOS.Add(pry);
        }
        //protected override void DespuesInsert();
        #endregion

        #region Delete

        //public ByARpt EnviarDelete()
        //{
        //    byaRpt = new ByARpt();
        //    using (ctx = new Entities())
        //    {
        //        if (!esValidoDelete())
        //        {
        //            return byaRpt;
        //        }
        //        try
        //        {
        //            AntesDelete();
        //            SaveChange();
        //            DespuesDelete();
        //        }
        //        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //        {
        //            ByAExcep.AdminException(byaRpt, ex);
        //        }
        //        catch (Exception ex)
        //        {
        //            ByAExcep.AdminException(byaRpt, ex);

        //        }
        //        return byaRpt;

        //    }
        //}

        protected internal override bool esValidoDelete()
        {
            return true;
        }

        protected internal override void AntesDelete()
        {
            
            //EP_PROYECTOS found = ctx.EP_PROYECTOS.Find(pry.ID_EP,pry.PROYECTOS);
            //ctx. EP_PROYECTOS.Remove(found);

            //Student studentToDelete = new Student() { StudentID = id };
            ctx.Entry(pry).State = EntityState.Deleted;
            
            
        }
        //protected override void DespuesInsert();
        #endregion

    }
}
