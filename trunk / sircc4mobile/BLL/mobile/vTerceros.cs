using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.mobile
{
    public class vTerceros
    {

        public string IDE_TER { get; set; }
        public string APE1_TER { get; set; }
        public string APE2_TER { get; set; }
        public string NOM1_TER { get; set; }
        public string NOM2_TER { get; set; }
        public string ORD_GAS { get; set; }
        public string CAR_FUN { get; set; }
        public string APNOMBRE
        {

            get
            {
                return (APE1_TER.Trim() + " " + (APE2_TER == null ? "" : APE2_TER.Trim()) + " " + (NOM1_TER == null ? "" : NOM1_TER.Trim()) + " " + (NOM2_TER == null ? "" : NOM2_TER.Trim())).Trim();
            }
        }

        public string NOMBRE
        {

            get
            {
                return (NOM1_TER == null ? "" : NOM1_TER.Trim()) + " " + (NOM2_TER == null ? "" : NOM2_TER.Trim()) + " " + APE1_TER.Trim() + " " + (APE2_TER == null ? "" : APE2_TER.Trim()).Trim();
            }
        }
        public string NOMBREC{set;get;}
        public int CANT_PROC { get; set; }
    }

    public class vTerceros2
    {

        public string IDE_TER { get; set; }
        public string NOMBREC { set; get; }
        public int CANT_PROC { get; set; }
    }
}
