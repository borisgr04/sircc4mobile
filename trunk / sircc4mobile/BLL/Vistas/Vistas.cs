using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Vistas
{



    public class vEP_CAP_JUR
    {
        public decimal ID { get; set; }
        public Nullable<decimal> ID_EP { get; set; }
        public Nullable<decimal> ID_CAPJ { get; set; }
        public string DES_CAPJ { get; set; }

    }
    public class vEP_DT_CAP_JUR
    {
        public string EST_CAPJ { get; set; }
        public string DES_CAPJ { get; set; }
        public decimal ID { get; set; }

    }
  
    public class vCAL_VIG_POL
    {
        
        public string COD_CAL { get; set; }
        public string DESCRIPCION { get; set; }

    }
    public class vCALCULOPOL
    {
        public string COD_CAL { get; set; }
        public string DESCRIPCION { get; set; }

    }
    public class vPOLIZAS
    {
        public int COD_POL { get; set; }
        public string NOM_POL { get; set; }
        public string EST_POL { get; set; }
        public string DESCRIPCION { get; set; }
    }
    
    
   

    public class vEstados
    {
        //public string codigo { get; set; }
        public string nombre { get; set; }
    }

}
