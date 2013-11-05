using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.mobile
{
   [Serializable]
   public class vPSolicitudesM
    {
        public decimal NUM_SOL { get; set; }
        public string COD_SOL { get; set; }
        public string DEP_SOL { get; set; }
        public string DEP_PSOL { get; set; }
        public short VIG_SOL { get; set; }
        public string TIP_CON { get; set; }
        public string STIP_CON { get; set; }
        public string COD_TPRO { get; set; }
        public string OBJ_SOL { get; set; }
        public string ID_ABOG_ENC { get; set; }
        public Nullable<System.DateTime> FECHA_ASIGNADO { get; set; }
        public Nullable<System.DateTime> FECHA_REGISTRO { get; set; }
        public string USAP { get; set; }
        public string USBD { get; set; }
        public Nullable<System.DateTime> FEC_MOD { get; set; }
        public Nullable<decimal> NUM_PLA { get; set; }
        public Nullable<System.DateTime> FEC_RECIBIDO { get; set; }
        public Nullable<decimal> VAL_CON { get; set; }
        public string COD_EP { get; set; }
        public string OBS_SOL { get; set; }
        public string DEP_NEC { get; set; }
        public string DEP_DEL { get; set; }
        public string ENCARGADO_NOM { get; set; }
        public string ENCARGADO_TEL { get; set; }
        public string ENCARGADO_EMA { get; set; }

        public Nullable<System.DateTime> FEC_REVISION { get; set; }
       private string concepto;
        public string CONCEPTO {
            get{
                    string r=null;
                switch (concepto){
                case "A": r = "Aceptado"; break;
                case "R": r = "Rechazado"; break;
                case "P": r = "Pendiente"; break;
                }
                return r;
            }
            set{
                concepto=value;
            }
        }


        public string OBS_RECIBIDO {get;set;}



    }
}
