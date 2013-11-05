using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Data;

namespace BLL.EstPrev
{
    public class mRevESTPREV : absBLL
    {
        public ESTPREV ep { get; set; }

    
        #region Update


        protected internal override void AntesUpdate()
        {
            var found= ctx.ESTPREV.Find(ep.ID);
            if (found!= null)
            {
                
                found.FEC_REV_EP = DateTime.Now;
                //found.USAP_REV_EP = "";
                
                //AL APROBAR
                //found.USAP_APR_EP =
                //found.FEC_APR_EP =
                //ANULAR
                //found.USAP_ANU_EP =ep.USAP_ANU_EP ;
                //found.FEC_ANU_EP =ep.FEC_ANU_EP ;
                //DESANULAR
                //found.USAP_DAN_EP =ep.USAP_DAN_EP ;
                //found.FEC_DAN_EP =ep.FEC_DAN_EP ;

                
                //AL CREAR
                //found.CODIGO_EP =ep.CODIGO_EP;
                //found.NRO_EP =
                // AL CAMBIAR DE ESTADO
                //found.EST_EP =
                //AL ENVIAR O RECIBIR
                //found.EST_FLU_EP =

                ctx.Entry(found).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("No se encontro el registró");
            }

        }


        #endregion
    }
}
