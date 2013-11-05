using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{
    public class vEP_POLIZAS
    {
        public decimal ID { get; set; }
        public Nullable<int> COD_POL { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public Nullable<decimal> POR_SMMLV { get; set; }
        public string CAL_APARTIRDE { get; set; }
        public Nullable<decimal> VIGENCIA { get; set; }
        public string APARTIRDE { get; set; }
        public string TIPO { get; set; }
        public Nullable<int> GRUPO { get; set; }
        public string NOM_POL{ get; set; }
        public string NOM_CALPOL{ get; set; }
        public string NOM_CALVIGPOL{ get; set; }
        public string DESCRIPCION { 
            get { 
                string  desc=NOM_POL+": el valor de la garantía deberá ser por un monto equivalente al "+
                     POR_SMMLV +" "+ TIPO+" del " + NOM_CALPOL + " '  y su vigencia será de " + VIGENCIA + "  días a partir de "
                     + NOM_CALVIGPOL;
                return desc;
                } 
            }

    }
    
}
