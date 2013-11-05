using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;

namespace BLL.Filtros
{
    class FiltrosContratosI : FiltrosContratos
    {
        Entities ctx;
    
        IList<vContratosInt> FiltrosContratos.GetContratos(vContratosIntFiltro cFil)
        {
            using (ctx = new Entities())
            {
                //Filtro Basico, X VIGENCIA Y POR INTERVENTOR
                IQueryable<vContratosInt> custQuery = (from ic in ctx.INTERVENTORES_CONTRATO
                                                       join c in ctx.CONTRATOS on ic.COD_CON equals c.COD_CON
                                                       //where ic.IDE_INT == cFil.Ide_Interventor
                                                       orderby c.COD_CON
                                                       select (new vContratosInt
                                                       {
                                                           Numero = c.COD_CON,
                                                           Tipo = c.TIPOSCONT.NOM_TIP + " " + c.SUBTIPOS.NOM_STIP,
                                                           Objeto = c.OBJ_CON,
                                                           Fecha_Suscripcion = c.FEC_SUS_CON,
                                                           Valor_Contrato = c.VAL_CON,
                                                           DependenciaNec = c.DEPENDENCIA.NOM_DEP,
                                                           DependenciaDel = c.DEPENDENCIA1.NOM_DEP,
                                                           Contratista = c.TERCEROS.APE1_TER.Trim() + " " + c.TERCEROS.APE2_TER.Trim() + " " + c.TERCEROS.NOM1_TER.Trim() + " " + c.TERCEROS.NOM2_TER.Trim(),
                                                           Ide_Contratista = c.IDE_CON,
                                                           Ide_Interventor = ic.IDE_INT,
                                                           Nom_Interventor = ic.TERCEROS.APE1_TER.Trim() + " " + ic.TERCEROS.APE2_TER.Trim() + " " + ic.TERCEROS.NOM1_TER.Trim() + " " + ic.TERCEROS.NOM2_TER.Trim(),
                                                           Dep_Nec = c.DEP_CON,
                                                           Dep_Del = c.DEP_PCON,
                                                           Vigencia = c.VIG_CON,
                                                           Estado = c.ESTADOS.ESTADO,
                                                           Cod_STip = c.STIP_CON,
                                                           Cod_Tip = c.TIP_CON,
                                                       })).AsQueryable<vContratosInt>();
                //FILTRO ADICIONALES
                if (cFil.Vigencia>0)
                {
                    custQuery = custQuery.Where(c => c.Vigencia == cFil.Vigencia);
                }
                
                if (!string.IsNullOrEmpty(cFil.Ide_Interventor))
                {
                    custQuery = custQuery.Where(c => c.Ide_Interventor == cFil.Ide_Interventor);
                }
                if (!string.IsNullOrEmpty(cFil.Numero))
                {
                    custQuery = custQuery.Where(c => c.Numero.Equals(cFil.Numero));
                }
                if (!string.IsNullOrEmpty(cFil.Cod_Tip))
                {
                    custQuery = custQuery.Where(c => c.Cod_Tip.Equals(cFil.Cod_Tip));
                }
                if (!string.IsNullOrEmpty(cFil.Cod_STip))
                {
                    custQuery = custQuery.Where(c => c.Cod_STip.Equals(cFil.Cod_STip));
                }
                if (cFil.FilxFS)
                {
                    custQuery = custQuery.Where(c => c.Fecha_Suscripcion >= cFil.FS_Inicial && c.Fecha_Suscripcion <= cFil.FS_Final);
                }
                if (!string.IsNullOrEmpty(cFil.Dep_Nec))
                {
                    custQuery = custQuery.Where(c => c.Dep_Nec.Contains(cFil.Dep_Nec));
                }
                if (!string.IsNullOrEmpty(cFil.Dep_Del))
                {
                    custQuery = custQuery.Where(c => c.Dep_Del.Contains(cFil.Dep_Del));
                }
                if (!string.IsNullOrEmpty(cFil.Objeto))
                {
                    custQuery = custQuery.Where(c => c.Objeto.Contains(cFil.Objeto));
                }
                if (!string.IsNullOrEmpty(cFil.Ide_Contratista))
                {
                    custQuery = custQuery.Where(c => c.Ide_Contratista.Equals(cFil.Ide_Contratista));
                }
                if (!string.IsNullOrEmpty(cFil.Estado))
                {
                    custQuery = custQuery.Where(c => c.Estado.Equals(cFil.Estado));
                }
                return custQuery.ToList();

            }

            

        }
    }
}
