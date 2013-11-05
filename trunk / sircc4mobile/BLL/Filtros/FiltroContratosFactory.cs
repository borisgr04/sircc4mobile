using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Filtros
{
    public enum TipoFiltrosContratos
    {
        Interventor,
        DependenciaN,
    }
    class FiltroContratosFactory
    {
        public static FiltrosContratos CreateFiltroContratos(TipoFiltrosContratos Type)
        {
            FiltrosContratos fc = null;
            switch (Type) {
                case TipoFiltrosContratos.Interventor:
                    fc = new FiltrosContratosI();
                    break;
                case TipoFiltrosContratos.DependenciaN: 
                    fc = new FiltrosContratosN();
                    break;
            }
            return fc;
        }

    }
}
