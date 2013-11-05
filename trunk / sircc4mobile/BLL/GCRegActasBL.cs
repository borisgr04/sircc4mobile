using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using Entidades;
using ByA;

namespace BLL
{
    public class GCRegActasBL
    {
        Entities ctx;
        ByARpt byaRpt = new ByARpt();

        public IList<ESTADOS> GetRutaActas(string cod_con)
        {
            IList<ESTADOS> lstE=null;
            using (ctx = new Entities())
            {
                CONTRATOS Cont = ctx.CONTRATOS.Where(c => c.COD_CON.Equals(cod_con)).FirstOrDefault();
                if (Cont!=null) { 
                ESTADOS Est = ctx.ESTADOS.Where(t => t.COD_EST.Equals(Cont.EST_CON)).Single();
                lstE= Est.ESTADOS1.ToList();// El Estado 1, sera el siguiente
                }
                return lstE;
            }
        }

        public IList<vEstContratos> GetActas(string cod_con)
        {
            using (ctx = new Entities())
            {
                var lst = from ec in ctx.ESTCONTRATOS
                          where ec.COD_CON == cod_con && ec.ESTADO=="AC"
                          orderby ec.ID, ec.FEC_ENT
                          select (new vEstContratos {
                              ID = ec.ID,
                              COD_EST= ec.EST_FIN,
                              NRO_DOC = ec.NRO_DOC,
                              NOM_EST=ec.ESTADOS.NOM_EST,  
                              FEC_ENT=ec.FEC_ENT,
                              NVISITAS= ec.NVISITAS,
                              POR_EJE_FIS= ec.POR_EJE_FIS,
                              VAL_PAGO = ec.VAL_PAGO,
                          });

                             
                return lst.ToList();
            }
        }

        public ByARpt Insert(ESTCONTRATOS ec)
        {
            using (ctx = new Entities())
            {
                CONTRATOS oContrato = ctx.CONTRATOS.Where(t => t.COD_CON == ec.COD_CON).FirstOrDefault();
                ec.EST_INI = oContrato.EST_CON;
                ec.DOC_ACT = "";
                ec.FEC_REG = DateTime.Now;
                ec.ESTADO = "AC";
                ec.NRO_DOC = 1;
                ec.USUARIO = "boris";
                //VALIDAR 
                // LA FECHA DEBE SER MAYOR O IGUAL A LA FECHA DE: RP,  FEC_APR_POL o MAYOR A ULTIMA ACTTA
                if (ValidarFechaAndUltEst(ec, oContrato) && ValidarValor(ec, oContrato))
                {
                    return byaRpt;
                }
                try
                {
                    oContrato.EST_CON = ec.EST_FIN;//Se Actualiza el Contrato.
                    ctx.ESTCONTRATOS.Add(ec);
                    ctx.SaveChanges();
                    byaRpt.Mensaje = "Se Agregó el Registro";
                    byaRpt.Error = false;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    AdminException(byaRpt, ex);
                }
                catch (Exception ex)
                {
                    AdminException(byaRpt, ex);

                }
                return byaRpt;
            }
        }

        public ByARpt Anular(int Ide_Acta)
        {
            using (ctx = new Entities())
            {
                try
                {
                    ESTCONTRATOS ec = ctx.ESTCONTRATOS.Where(t => t.ID == Ide_Acta).FirstOrDefault();
                    if (ec != null) {
                        CONTRATOS oContrato = ctx.CONTRATOS.Where(t => t.COD_CON == ec.COD_CON).FirstOrDefault();
                        oContrato.EST_CON = ec.EST_INI;//Se Devuelve al Ultimo
                        ec.ESTADO = "IN"; //    INACTIVA EL ACTA
                        ctx.SaveChanges();
                        byaRpt.Mensaje = "Se Anuló el Registro!!!";
                        byaRpt.Error = false;
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    AdminException(byaRpt, ex);
                }
                catch (Exception ex)
                {
                    AdminException(byaRpt, ex);

                }
                return byaRpt;
            }
        }

        private bool ValidarValor(ESTCONTRATOS ec, CONTRATOS oContrato)
        {
            //Validar el Valor del Contrato con la Sumatoria de todos los pagos.
            decimal totVvalor = ctx.ESTCONTRATOS.Where(t => t.COD_CON == ec.COD_CON & t.ESTADO == "AC").Sum(t => t.VAL_PAGO).Value;
            if (ec.VAL_PAGO > (oContrato.VAL_APO_GOB + oContrato.VAL_ADI - totVvalor))
            {
                byaRpt.Mensaje = String.Format("El Valor a Autorizar: {0} Supera el Saldo del Contrato {1}.", ec.VAL_PAGO, (oContrato.VAL_APO_GOB + oContrato.VAL_ADI) - totVvalor);
                byaRpt.Error = true;
            }
            return byaRpt;
        }

        private bool ValidarFechaAndUltEst(ESTCONTRATOS ec, CONTRATOS oContrato)
        {
            if (oContrato != null)
            {
                if (oContrato.FEC_APR_POL > ec.FEC_ENT)
                {  //El Acta debe ser mayor o igual a la fecha de aprobación de la poliza
                    byaRpt.Mensaje = String.Format("Error Fecha de Acta: {0} debe ser mayor a la Fecha Aprobación de la Poliza{1}.", ec.FEC_ENT.ToShortDateString(), oContrato.FEC_APR_POL.ToString());
                    byaRpt.Error = true;
                }
            }
            RP_CONTRATOS rp = ctx.RP_CONTRATOS.Where(t => t.COD_CON == ec.COD_CON & t.DOC_SOP == ec.COD_CON).OrderByDescending(t => t.FEC_RP).FirstOrDefault();
            if (rp != null)
            {
                if (rp.FEC_RP > ec.FEC_ENT)
                {
                    //El Acta debe ser mayor o igual a la fecha del ultimo RP asociado al contratp
                    byaRpt.Mensaje = String.Format("Error Fecha de Acta: {0} debe ser mayor a la Fecha del Registro Presupuestal {1}.", ec.FEC_ENT.ToShortDateString(), rp.FEC_RP.ToShortDateString());
                    byaRpt.Error = true;
                }
            }
            ESTCONTRATOS actaUlt = ctx.ESTCONTRATOS.Where( t => t.COD_CON==ec.COD_CON & t.ESTADO == "AC").OrderByDescending(t => t.FEC_ENT).FirstOrDefault();
            if (actaUlt != null)
            {
                if (actaUlt.FEC_ENT > ec.FEC_ENT)
                {
                    //El Acta debe ser mayor o igual a la fecha del ultimo RP asociado al contratp
                    byaRpt.Mensaje = String.Format("Error Fecha de Acta: {0} debe ser mayor del Ultimo Acta {1}.", ec.FEC_ENT.ToShortDateString(), actaUlt.FEC_ENT.ToShortDateString());
                    byaRpt.Error = true;
                }
                else if (actaUlt.EST_FIN!=ec.EST_INI)
                {
                    byaRpt.Mensaje = String.Format("El Ultimo acta: {0} no coincide con el Acta Anterior {1}.", actaUlt.EST_FIN, ec.EST_INI);
                    byaRpt.Error = true;
                
                }
            }
            return byaRpt;
        }

        private static void AdminException(ByARpt byaRpt, Exception ex)
        {
            if (ex.InnerException.InnerException != null)
            {
                byaRpt.Mensaje = ex.InnerException.InnerException.Message; ;
                byaRpt.Error = false;
            }
            else
            {
                byaRpt.Mensaje = ex.Message;
                byaRpt.Error = false;
            }
        }

        private static void AdminException(ByARpt byaRpt, System.Data.Entity.Validation.DbEntityValidationException ex)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                foreach (var valErr in eve.ValidationErrors)
                {
                    byaRpt.Mensaje += valErr.PropertyName + ":" + valErr.ErrorMessage + "<br/>";
                }
            }
            byaRpt.Error = true;
        }
        
        public ByARpt Update(ESTCONTRATOS ec)
        {
            ByARpt byaRpt = new ByARpt();
            using (ctx = new Entities()) {
                try
                {
                    var ecN=ctx.ESTCONTRATOS.Find(ec.ID);
                    if(ecN!=null){ //Si el Objeto existe
                        ecN.FEC_ENT=ec.FEC_ENT;//Ojo Verificar
                        ecN.OBS_EST=ec.OBS_EST;
                        ecN.VAL_PAGO=ec.VAL_PAGO;
                        ecN.NVISITAS = ec.NVISITAS;
                        ecN.POR_EJE_FIS = ec.POR_EJE_FIS;
                        ctx.Entry(ecN).State = EntityState.Modified;
                        ctx.SaveChanges();
                        
                        byaRpt.Mensaje="Se Actualizó el Registro";
                        byaRpt.Error = false;
                    }
                    else{
                        byaRpt.Mensaje="Se Intentó Actualizar un registro que no se encontró en la base de datos";
                        byaRpt.Error = false;
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    AdminException(byaRpt, ex);
                }
                catch (Exception ex)
                {
                    AdminException(byaRpt, ex);    
                }
                return byaRpt; 
                }
        } 
    }

    [Serializable]
    public class vEstContratos {
        public int ID { get; set; }
        public string COD_EST { get; set; }
        public int? NRO_DOC {get; set;}
        public string NOM_EST { get; set; }
        public DateTime FEC_ENT { get; set; }
        public string sFEC_ENT { 
            get{ 
                return FEC_ENT.ToShortDateString();
            }
        }
        public int? NVISITAS {get; set;}
        public decimal? POR_EJE_FIS {get; set;}
        public decimal? VAL_PAGO {get; set;}
    }
   
}
