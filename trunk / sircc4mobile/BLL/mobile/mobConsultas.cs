using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;


namespace BLL.mobile
{
    public class mobConsultas
    {
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }

        public List<vTerceros> getEncargados(string Dep_Del, short Vigencia) {
            List<vTerceros> lt;
            using (ctx = new Entities()) {
                lt = ctx.HDEP_ABOGADOS.Where(t => t.COD_DEP == Dep_Del && t.ESTADO == "AC" && t.ASIG_PROC == "SI" ).Select(
                    t => new vTerceros {
                        APE1_TER=t.TERCEROS.APE1_TER,
                        APE2_TER=t.TERCEROS.APE2_TER,
                        NOM1_TER= t.TERCEROS.NOM1_TER,
                        NOM2_TER = t.TERCEROS.NOM2_TER,
                        IDE_TER=t.IDE_TER,
                        CANT_PROC =t.TERCEROS.PCONTRATOS.Where(y=>y.VIG_CON==Vigencia).Count()
                    }
                    ).OrderByDescending(t=>t.CANT_PROC).ToList();
                return lt; 
            }
        }

        public List<vPCONTRATOS> getProcesos(string Ide_Fun, short Vigencia)
        {
            List<vPCONTRATOS> lt;
            using (ctx = new Entities())
            {

                lt = ctx.PCONTRATOS.Where(t => t.USUENCARGADO == Ide_Fun && t.VIG_CON == Vigencia).Select(
                    t => new vPCONTRATOS
                    {
                       OBJ_CON= t.OBJ_CON,
                       PRO_SEL_NRO= t.PRO_SEL_NRO,
                       MODALIDAD= t.TIPOSPROC.NOM_TPROC,
                       DEP_NEC= t.DEPENDENCIA1.NOM_DEP,
                       DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                       ENCARGADO = (t.TERCEROS.NOM1_TER + " " + t.TERCEROS.NOM2_TER + t.TERCEROS.APE1_TER + " " + t.TERCEROS.APE2_TER).Trim(),
                       ESTADO=t.PESTADOS.NOM_EST
                    }
                    ).OrderBy(t=>t.PRO_SEL_NRO).ToList();
                return lt;
            }
        }

        public List<vPCRONOGRAMAS> getCronograma(string NroPro)
        {
            List<vPCRONOGRAMAS> lt;
            using (ctx = new Entities())
            {
                
                lt = ctx.PCRONOGRAMAS.Where(t => t.NUM_PROC==NroPro).Select(
                    t => new vPCRONOGRAMAS
                    {
                      ID=t.ID,
                      NOM_ACT= t.NOM_ACT,
                      FECHAF = t.FECHAF,
                      FECHAI=t.FECHAI,
                      EST_ACT=t.EST_ACT,
                      NOM_EST_ACT =t.PESTADOSACT.NOM_EST,
                      OBS_SEG = t.OBS_SEG == null ? " Sin Comentarios" : t.OBS_SEG
                    }
                    ).ToList();
                return lt;
            }
        }

        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {

            List<vPESTADOS> lt;
            using (ctx = new Entities())
            {
                lt=ctx.PCONTRATOS.Where(t => t.DEP_PCON == DepDel && t.VIG_CON==Vigencia).
                    GroupBy(t => t.PESTADOS.NOM_EST).
                    Select(t => new vPESTADOS { CANT = t.Count(),  NOM_EST = t.Key }).OrderBy(t=> t.NOM_EST).ToList();
               return lt;
            }
        }

        public List<vPCONTRATOS> getProcesosxEst(string Dep_Del, string Estado, short Vigencia)
        {
            List<vPCONTRATOS> lt;
            using (ctx = new Entities())
            {

                lt = ctx.PCONTRATOS.Where(t => t.DEP_PCON == Dep_Del && t.PESTADOS.NOM_EST == Estado && t.VIG_CON == Vigencia).Select(
                    t => new vPCONTRATOS
                    {
                        OBJ_CON = t.OBJ_CON,
                        PRO_SEL_NRO = t.PRO_SEL_NRO,
                        MODALIDAD = t.TIPOSPROC.NOM_TPROC,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO = (t.TERCEROS.NOM1_TER + " " + t.TERCEROS.NOM2_TER +" "+ t.TERCEROS.APE1_TER + " " + t.TERCEROS.APE2_TER).Trim(),
                        ESTADO = t.PESTADOS.NOM_EST
                    }
                    ).OrderBy(t=>t.PRO_SEL_NRO).ToList();
                return lt;
            }
        }

        public List<vPCONTRATOS> getProcesosDD(string DepDel)
        {
            List<vPCONTRATOS> lt;
            using (ctx = new Entities())
            {

                lt = ctx.PCONTRATOS.Where(t => t.DEP_PCON == DepDel && t.PESTADOS.FINAL == "NO").Select(
                    t => new vPCONTRATOS
                    {
                        OBJ_CON = t.OBJ_CON,
                        PRO_SEL_NRO = t.PRO_SEL_NRO,
                        MODALIDAD = t.TIPOSPROC.NOM_TPROC,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO = (t.TERCEROS.NOM1_TER + " " + t.TERCEROS.NOM2_TER + t.TERCEROS.APE1_TER + " " + t.TERCEROS.APE2_TER).Trim(),
                        ESTADO = t.PESTADOS.NOM_EST
                    }
                    ).OrderBy(t=>t.PRO_SEL_NRO)
                    .ToList();
                return lt;
            }
        }

        public List<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            List<vDEPENDENCIAS> lt;
            using (ctx = new Entities())
            {
                lt = ctx.DEPENDENCIA.Where(t => t.ESTADO == "AC" && t.COD_DEP!="00").
                    Select(t => 
                        new vDEPENDENCIAS 
                        { NOM_DEP=t.NOM_DEP,
                            COD_DEP=t.COD_DEP, 
                            CANT_PROC =t.PCONTRATOS1.Where(pc=> pc.VIG_CON==Vigencia && pc.DEP_PCON==DepDel).Count() //Dependencia Solicitante
                        }).OrderByDescending(t=>t.CANT_PROC).ToList();
            }
            return lt;
        }

        public List<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
        {

            List<vTIPOSPROC> lt;
            using (ctx = new Entities())
            {
                lt = ctx.TIPOSPROC.Where(t=> t.ESTA_TPROC == "AC" && t.COD_TPROC!="TP00"  ).
                    Select(t =>
                        new vTIPOSPROC
                        {
                            NOM_TPROC = t.NOM_TPROC,
                            COD_TPROC = t.COD_TPROC,
                            CANT_PROC = t.PCONTRATOS.Where(pc => pc.VIG_CON == Vigencia && pc.DEP_PCON == DepDel ).Count() //Dependencia Solicitante
                        }).OrderByDescending(t => t.CANT_PROC).ToList();
            }
            return lt;
        }

        public List<vPCONTRATOS> getProcesosDN(string DepDel, string DepNec, short Vigencia)
        {
            List<vPCONTRATOS> lt;
            using (ctx = new Entities())
            {

                lt = ctx.PCONTRATOS.Where(t => t.DEP_PCON == DepDel && t.DEP_CON == DepNec && t.VIG_CON == Vigencia).Select(
                    t => new vPCONTRATOS
                    {
                        OBJ_CON = t.OBJ_CON,
                        PRO_SEL_NRO = t.PRO_SEL_NRO,
                        MODALIDAD = t.TIPOSPROC.NOM_TPROC,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO = (t.TERCEROS.NOM1_TER + " " + t.TERCEROS.NOM2_TER +" "+ t.TERCEROS.APE1_TER + " " + t.TERCEROS.APE2_TER).Trim(),
                        ESTADO = t.PESTADOS.NOM_EST
                    }
                    ).OrderBy(t=>t.PRO_SEL_NRO).ToList();
                return lt;
            }
        }

        public IList<vPCONTRATOS> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
        {
            List<vPCONTRATOS> lt;
            using (ctx = new Entities())
            {

                lt = ctx.PCONTRATOS.Where(t => t.DEP_PCON == DepDel && t.COD_TPRO == Modalidad && t.VIG_CON == Vigencia).Select(
                    t => new vPCONTRATOS
                    {
                        OBJ_CON = t.OBJ_CON,
                        PRO_SEL_NRO = t.PRO_SEL_NRO,
                        MODALIDAD = t.TIPOSPROC.NOM_TPROC,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO = (t.TERCEROS.NOM1_TER + " " + t.TERCEROS.NOM2_TER + " " + t.TERCEROS.APE1_TER + " " + t.TERCEROS.APE2_TER).Trim(),
                        ESTADO = t.PESTADOS.NOM_EST
                    }
                    ).OrderBy(t => t.PRO_SEL_NRO).ToList();
                return lt;
            }
        }

        public List<vTerceros> getEncargadosP(string DepDel, short Vigencia, int Pagina)
        {
            List<vTerceros> lt=null;;
            using (ctx = new Entities())
            {
                var TamanoPagina = 5;
                var Total = ctx.HDEP_ABOGADOS.Where(t => t.COD_DEP == DepDel && t.ESTADO == "AC" && t.ASIG_PROC == "SI").Count();
                var Residuo = Total % TamanoPagina;
                var NElementos = TamanoPagina - Residuo;
                if (NElementos > 0)
                {
                    lt = ctx.HDEP_ABOGADOS.Where(t => t.COD_DEP == DepDel && t.ESTADO == "AC" && t.ASIG_PROC == "SI").Select(
                        t => new vTerceros
                        {
                            APE1_TER = t.TERCEROS.APE1_TER,
                            APE2_TER = t.TERCEROS.APE2_TER,
                            NOM1_TER = t.TERCEROS.NOM1_TER,
                            NOM2_TER = t.TERCEROS.NOM2_TER,
                            IDE_TER = t.IDE_TER,
                            CANT_PROC = t.TERCEROS.PCONTRATOS.Where(y => y.VIG_CON == Vigencia).Count()
                        }
                        ).OrderByDescending(t => t.CANT_PROC).Skip(5 * (Pagina - 1) + 1).Take(NElementos).ToList();
                    
                }
                return lt;
                
            }
        }
    }
}
