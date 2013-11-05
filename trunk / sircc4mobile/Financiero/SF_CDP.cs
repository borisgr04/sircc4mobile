using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Financiero
{
    public class SF_CDP
    {
        public string nro_cdp { get; set; }
        public DateTime fec_cdp { get; set; }
        public decimal val_cdp { get; set; }
        public string vig_cdp { get; set; }

        public List<SF_RubrosCDP> Rubros { get; set; }

        public SF_CDP()
        {
           Rubros= new List<SF_RubrosCDP>();
        }
    }
}
