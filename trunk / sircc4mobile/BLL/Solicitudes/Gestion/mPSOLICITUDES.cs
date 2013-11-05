using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;
using BLL.Solicitudes.Vistas;
using AutoMapper;

namespace BLL.Solicitudes.Gestion
{
    public class mPSOLICITUDES: absBLL
    {
        public PSOLICITUDES reg { get; set; }
        
        #region Insert

        protected internal override bool esValidoInsert()
        {
            
            return NoHaySolicitudAceptada();

            
        }
        /// <summary>
        /// Analiza si existe una solidtud asociada al estudio previo relacionado, que este en estado aceptado.
        /// Si esta en estado aceptada retorna 0
        /// Esto se comprueba solo si se relaciona un estudio previo en otro caso no se valida.
        /// </summary>
        /// <returns></returns>
        private bool NoHaySolicitudAceptada()
        {
            bool sw = true;
            if (reg.COD_EP != null)
                sw=ctx.PSOLICITUDES.Where(t => t.COD_EP == reg.COD_EP && t.HREVISADO1.CONCEPTO_REVISADO == "A").Count() == 0;
            return sw;
        }
        protected internal override void AntesInsert()
        {
            //Mapear Objeto DTO a Ado Entity FrameWork
            decimal ultId = 0;
            try
            {
                ultId = (decimal)ctx.PSOLICITUDES.Where(t => t.VIG_SOL == reg.VIG_SOL && t.DEP_PSOL == reg.DEP_PSOL).Max(t => t.NUM_SOL);
            }
            catch { }
            
            reg.NUM_SOL= ultId + 1;//Consecutivo unico
            string AbrDep = ctx.DEPENDENCIA.Where(t => t.COD_DEP == reg.DEP_PSOL).Select(t => t.DEP_ABR).Single();
            reg.COD_SOL = reg.VIG_SOL + "-" + AbrDep +"-"+ reg.NUM_SOL.ToString().PadLeft(4, '0');
            reg.FECHA_REGISTRO = DateTime.Now;
            byaRpt.id = reg.COD_SOL;
            ctx.Entry(reg).State = EntityState.Added;
        }
        //protected override void DespuesInsert();

        #endregion
        #region Update


        protected internal override void AntesUpdate()
        {
            reg.FEC_MOD = DateTime.Now;
            var found = ctx.PSOLICITUDES.Find(reg.COD_SOL);
            if (found != null)
            {
                found.OBJ_SOL = reg.OBJ_SOL;
                found.FEC_RECIBIDO = reg.FEC_RECIBIDO;
                found.DEP_SOL = reg.DEP_SOL;
                found.TIP_CON = reg.TIP_CON;
                found.COD_TPRO = reg.COD_TPRO;
                found.STIP_CON =reg.STIP_CON;
                found.VIG_SOL = reg.VIG_SOL;
                found.DEP_PSOL = reg.DEP_PSOL ;
                found.VAL_CON = reg.VAL_CON;
                found.ID_ABOG_ENC = reg.ID_ABOG_ENC;
                found.NUM_PLA = reg.NUM_PLA;
                //found.USAP = reg.USAP;
                //found.USBD = reg.USBD;
                //found.FECHA_ASIGNADO = reg.FECHA_ASIGNADO;
                
                ctx.Entry(found).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("No se encontro el registró");
            }

        }


        #endregion

        internal Vistas.vPSolicitudes GetPK()
        {
            vPSolicitudes Reg = new vPSolicitudes();
            Mapper.CreateMap<PSOLICITUDES, vPSolicitudes>();
            using (ctx = new Entities())
            {
                PSOLICITUDES ep = ctx.PSOLICITUDES.Where(t => t.COD_SOL == reg.COD_SOL).FirstOrDefault();
                
                if (ep != null)
                {
                    Mapper.Map(ep, Reg);
                }
                return Reg;
            }
        }

        internal List<vPSolicitudes> GetSolicitudes(string Dep_PSol)
        {
            using (ctx = new Entities())
            {



                List<vPSolicitudes> lst = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == Dep_PSol )
                .Select(
                t => new vPSolicitudes { 
                     COD_EP= t.COD_EP,
                     COD_SOL=t.COD_SOL,
                     COD_TPRO= t.COD_TPRO,
                     COD_TPRO_NOM = t.TIPOSPROC.NOM_TPROC,
                     ID_ABOG_ENC = t.ID_ABOG_ENC,
                     OBJ_SOL = t.OBJ_SOL,
                     DEP_PSOL= t.DEP_PSOL,
                     DEP_PSOL_NOM = t.DEPENDENCIA.NOM_DEP,
                     DEP_SOL = t.DEP_SOL,
                     DEP_SOL_NOM = t.DEPENDENCIA1.NOM_DEP,
                     TIP_CON = t.TIP_CON,
                     TIP_CON_NOM= t.TIPOSCONT.NOM_TIP,
                     STIP_CON = t.STIP_CON,
                     STIP_CON_NOM= t.SUBTIPOS.NOM_STIP,
                     NOM_ABOG_ENC = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER +" "+ t.HREVISADO1.TERCEROS.NOM1_TER+ " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim()
                }
                )
                .OrderByDescending(t=>t.COD_SOL)
                .ToList<vPSolicitudes>();
            return lst;
            }
        }
    }
}
