using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;

namespace BLL
{
    public class EnviaDatos
    {
        /// <summary>
        /// 1. esValidoInsert()
        /// 2. AntesInsert(); //Especificar El objeto a Enviar
        /// 3. SaveChange(); no se deberia implementar
        //  4. DespuesInsert(); // Proceso Despues de implementar
        /// </summary>
        /// <returns></returns>
        public static ByARpt EnviarInsert(absBLL x)
        {
            x.byaRpt = new ByARpt();
            using (x.ctx = new Entities())
            {
                if (!x.esValidoInsert())
                {
                    return x.byaRpt;
                }
                try
                {
                    x.AntesInsert();
                    x.SaveChange();
                    x.DespuesInsert();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);
                }
                catch (Exception ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);

                }
                return x.byaRpt;

            }
        }

        public static ByARpt EnviarUpdate(absBLL x)
        {
            x.byaRpt = new ByARpt();
            using (x.ctx = new Entities())
            {
                if (!x.esValidoUpdate())
                {
                    return x.byaRpt;
                }
                try
                {
                    x.AntesUpdate();
                    x.SaveChange();
                    x.DespuesUpdate();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);
                }
                catch (Exception ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);

                }
                return x.byaRpt;

            }
        }

        public static ByARpt EnviarDelete(absBLL x)
        {
            x.byaRpt = new ByARpt();
            using (x.ctx = new Entities())
            {
                if (!x.esValidoDelete())
                {
                    return x.byaRpt;
                }
                try
                {
                    x.AntesDelete();
                    x.SaveChange();
                    x.DespuesDelete();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);
                }
                catch (Exception ex)
                {
                    ByAExcep.AdminException(x.byaRpt, ex);

                }
                return x.byaRpt;

            }
        }

    }
}
