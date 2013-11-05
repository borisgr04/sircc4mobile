using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using BLL.Filtros;
using BLL.Vistas;

namespace BLL
{
   public  class GCConsContratosBL
    {
        Entities ctx;
        FiltrosContratos fc;

        public IList<DEPENDENCIA> GetDependencia()
        {
            using (ctx = new Entities())
            {
                
                var lst = ctx.DEPENDENCIA.Where(c=>c.COD_DEP != "00").OrderBy(t=> t.COD_DEP).OrderBy(t=>t.NOM_DEP);
                return lst.ToList(); 
            }
        }

        public IList<DEPENDENCIA> GetDependenciaN(string ide_ter)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.HDEP_ABOGADOS.Where(t => t.IDE_TER == ide_ter).Select(t => t.DEPENDENCIA);
                return lst.ToList();
            }
        }
        public IList<vEstados> GetEstados()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.ESTADOS.Select( t => new vEstados{ nombre= t.ESTADO.ToUpper()}).Distinct().OrderBy(c=>c.nombre);
                
                return lst.ToList();
            }
        }

        public IList<VIGENCIAS> GetVigencias()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.VIGENCIAS.OrderByDescending(t => t.YEAR_VIG );
                return lst.ToList();
            }
        }

        public IList<TIPOSCONT> GetTipos()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.TIPOSCONT;
                return lst.ToList();
            }
        }

        public IList<SUBTIPOS> GetSubTipos(string Cod_Tip)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.SUBTIPOS.Where(st => st.COD_TIP == Cod_Tip);
                return lst.ToList();
            }
        }

        public IList<vContratosInt> GetContratos(vContratosIntFiltro cFil) {
            //Crear Una Tabla que Tenga Configurado el Formulario y el Tipo de Filtro
            fc = FiltroContratosFactory.CreateFiltroContratos(TipoFiltrosContratos.Interventor);
            return fc.GetContratos(cFil);
        }

        public IList<vProyectos> GetProyectos(string filtro)
        {
            using (ctx = new Entities())
            {
                //var lst = ctx.PROYECTOS.Where(t => t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro));
                var lst = (from t in ctx.PROYECTOS
                           where (t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro))
                           select (new vProyectos { Nro_Proyecto = t.PROYECTO, Nombre_Proyecto = t.NOMBRE_PROYECTO }));
                return lst.ToList();
            }
        }

        public IList<vTerceros> GetTerceros(string filtro)
        {
            using (ctx = new Entities())
            {
                var lst = (from t in ctx.TERCEROS
                           where t.IDE_TER.Contains(filtro) || (t.APE1_TER.Trim() + " " + t.APE2_TER.Trim() + " " + t.NOM1_TER.Trim() + " " + t.NOM2_TER.Trim()).ToUpper().Contains(filtro.ToUpper())
                           select (new vTerceros { 
                               IDE_TER = t.IDE_TER, 
                               APE1_TER = t.APE1_TER,
                               APE2_TER = t.APE2_TER, 
                               NOM1_TER = t.NOM1_TER, 
                               NOM2_TER = t.NOM2_TER }));
                
                return lst.ToList();
            }
        }
        public vTerceros GetTercerosPk(string ide_ter)
        {
            using (ctx = new Entities())
            {
                vTerceros ter = (from t in ctx.TERCEROS
                                 where t.IDE_TER.Equals(ide_ter)
                                 select (new vTerceros
                                 {
                                     IDE_TER = t.IDE_TER,
                                     APE1_TER = t.APE1_TER,
                                     APE2_TER = t.APE2_TER,
                                     NOM1_TER = t.NOM1_TER,
                                     NOM2_TER = t.NOM2_TER
                                 })).FirstOrDefault();
                return ter;
            }
        }
        
    }
   
}
