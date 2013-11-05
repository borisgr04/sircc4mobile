using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using ByA;
using BLL.Solicitudes.Vistas;

namespace BLL.mobile
{
    public class mobSolicitudes
    {
        public Entities ctx { get; set; }
        public ByARpt byaRpt { get; set; }

        public List<vPESTADOS> getxEstados(string DepDel, short Vigencia)
        {

            List<vPESTADOS> lt = new List<vPESTADOS>();
            vPESTADOS e;
            using (ctx = new Entities())
            {

                var porAsignar = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC == null).Count();
                e = new vPESTADOS
                {
                    COD_EST = "PA",
                    NOM_EST = "Por Asignar",
                    CANT = porAsignar
                };
                lt.Add(e);

                var porRecibir = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "N").Count();
                e = new vPESTADOS
                {
                    COD_EST = "PR",
                    NOM_EST = "Por Recibir",
                    CANT = porRecibir
                };
                lt.Add(e);


                var Pendiente = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "S" && t.HREVISADO1.CONCEPTO_REVISADO == "P").Count();
                e = new vPESTADOS
                {
                    COD_EST = "PE",
                    NOM_EST = "Pendientes",
                    CANT = Pendiente
                };
                lt.Add(e);

                var Aceptadas = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null  && t.HREVISADO1.CONCEPTO_REVISADO == "A").Count();
                e = new vPESTADOS
                {
                    COD_EST = "AC",
                    NOM_EST = "Aceptadas",
                    CANT = Aceptadas
                };
                lt.Add(e);

                var Rechazadas = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null  && t.HREVISADO1.CONCEPTO_REVISADO == "R").Count();
                e = new vPESTADOS
                {
                    COD_EST = "RE",
                    NOM_EST = "Rechazadas",
                    CANT = Rechazadas
                };
                lt.Add(e);


                return lt;
            }
        }
        public IList<vPSolicitudesM> getSolicitudesxEst(string DepDel, string Estado, short Vigencia)
        {
            
            IList<vPSolicitudesM> lt = null;
            
            using (ctx = new Entities())
            {
                if (Estado == "Por Asignar")
                {
                    lt = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC == null)
                    .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
                }
                if (Estado == "Por Recibir")
                {

                    lt = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "N")
                    .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
                }
                if (Estado == "Pendientes")
                {

                    lt= ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "S" && t.HREVISADO1.CONCEPTO_REVISADO == "P")
                    .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
                 
                }
                if (Estado == "Aceptadas")
                {
                    lt = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "S" && t.HREVISADO1.CONCEPTO_REVISADO == "A")
                    .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
                }
                if (Estado == "Rechazadas")
                {
                    lt = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.VIG_SOL == Vigencia && t.ID_ABOG_ENC != null && t.HREVISADO1.RECIBIDO_ABOG == "S" && t.HREVISADO1.CONCEPTO_REVISADO == "R")
                        .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
                }
                
                return lt;
            }
        }
    public List<vDEPENDENCIAS> getDependencias(string DepDel, short Vigencia)
        {
            List<vDEPENDENCIAS> lt;
            using (ctx = new Entities())
            {
                lt = ctx.DEPENDENCIA.Where(t => t.ESTADO == "AC" && t.COD_DEP != "00").
                    Select(t =>
                        new vDEPENDENCIAS
                        {
                            NOM_DEP = t.NOM_DEP,
                            COD_DEP = t.COD_DEP,
                            CANT_PROC = t.PSOLICITUDES1.Where(pc => pc.VIG_SOL == Vigencia && pc.DEP_PSOL == DepDel).Count() //Dependencia Solicitante
                        }).OrderByDescending(t => t.CANT_PROC).Where(t=>t.CANT_PROC>0).ToList();
            }
            return lt;
        }
    
    public List<vTIPOSPROC> getModalidad(string DepDel, short Vigencia)
    {

        List<vTIPOSPROC> lt;
        using (ctx = new Entities())
        {
            lt = ctx.TIPOSPROC.Where(t => t.ESTA_TPROC == "AC" && t.COD_TPROC != "TP00").
                Select(t =>
                    new vTIPOSPROC
                    {
                        NOM_TPROC = t.NOM_TPROC,
                        COD_TPROC = t.COD_TPROC,
                        CANT_PROC = t.PSOLICITUDES.Where(pc => pc.VIG_SOL == Vigencia && pc.DEP_PSOL == DepDel).Count() //Dependencia Solicitante
                    }).OrderByDescending(t => t.CANT_PROC).Where(t=>t.CANT_PROC>0).ToList();
        }
        return lt;
    }

    public List<vTerceros> getEncargados(string Dep_Del, short Vigencia)
    {
        List<vTerceros> lt;
        using (ctx = new Entities())
        {
            lt = ctx.HDEP_ABOGADOS.Where(t => t.COD_DEP == Dep_Del && t.ESTADO == "AC" && t.ASIG_PROC == "SI").Select(
                t => new vTerceros
                {
                    APE1_TER = t.TERCEROS.APE1_TER,
                    APE2_TER = t.TERCEROS.APE2_TER,
                    NOM1_TER = t.TERCEROS.NOM1_TER,
                    NOM2_TER = t.TERCEROS.NOM2_TER,
                    IDE_TER = t.IDE_TER,
                    CANT_PROC = t.TERCEROS.HREVISADO.Where(y => y.PSOLICITUDES.VIG_SOL == Vigencia && y.PSOLICITUDES.DEP_PSOL==Dep_Del).Count()
                }
                ).OrderByDescending(t => t.CANT_PROC).Where(t=>t.CANT_PROC>0).ToList();
            return lt;
        }
    }

    public IList<vPSolicitudesM> getProcesos(string Ide_Fun, short Vigencia)
    {
        List<vPSolicitudesM> lt;
        using (ctx = new Entities())
        {
            lt = ctx.PSOLICITUDES.Where(t => t.HREVISADO1.NIT_ABOG_RECIBE == Ide_Fun && t.VIG_SOL == Vigencia)
                    .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
            return lt;
        }
    }



    public IList<vPSolicitudesM> getProcesosxMod(string DepDel, string Modalidad, short Vigencia)
    {
        List<vPSolicitudesM> lt;
        using (ctx = new Entities())
        {
            lt = ctx.PSOLICITUDES.Where(t => t.COD_TPRO == Modalidad && t.VIG_SOL == Vigencia)
                .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO

                    }).ToList();
            return lt;
        }
    }
    
    public IList<vPSolicitudesM> getProcesosDN(string DepDel, string DepNec, short Vigencia)
    {
        List<vPSolicitudesM> lt;
        using (ctx = new Entities())
        {
            lt = ctx.PSOLICITUDES.Where(t => t.DEP_PSOL == DepDel && t.DEP_SOL == DepNec && t.VIG_SOL == Vigencia)
                .Select(
                    t => new vPSolicitudesM
                    {
                        COD_SOL = t.COD_SOL,
                        OBJ_SOL = t.OBJ_SOL,
                        VAL_CON = t.VAL_CON,
                        OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                        DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                        DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                        ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                        ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                        ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,

                        FECHA_REGISTRO = t.FECHA_REGISTRO,
                        FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                        FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                        CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                        OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                        FEC_REVISION = t.HREVISADO1.FECHA_REVISADO
                    }).ToList();
            return lt;
        }
    }

    private IList<vPSolicitudesM> MapearPSolTovPSol(IList<PSOLICITUDES> lsSol)
    {
        IList<vPSolicitudesM> r = new List<vPSolicitudesM>();
        if (lsSol.Count > 0)
        {
            r= lsSol.Select(
                        t => new vPSolicitudesM
                        {
                            COD_SOL = t.COD_SOL,
                            OBJ_SOL = t.OBJ_SOL,
                            VAL_CON = t.VAL_CON,
                            OBS_SOL = t.HREVISADO1.OBS_REVISADO,
                            DEP_NEC = t.DEPENDENCIA1.NOM_DEP,
                            DEP_DEL = t.DEPENDENCIA.NOM_DEP,
                            ENCARGADO_NOM = (t.HREVISADO1.TERCEROS.APE1_TER + " " + t.HREVISADO1.TERCEROS.APE2_TER + " " + t.HREVISADO1.TERCEROS.NOM1_TER + " " + t.HREVISADO1.TERCEROS.NOM2_TER).Trim(),
                            ENCARGADO_EMA = t.HREVISADO1.TERCEROS.EMA_TER,
                            ENCARGADO_TEL = t.HREVISADO1.TERCEROS.TEL_TER,
                            FECHA_REGISTRO = t.FECHA_REGISTRO,
                            FECHA_ASIGNADO = t.HREVISADO1.FEC_ASIGNADO,
                            FEC_RECIBIDO = t.HREVISADO1.FEC_REC_ABOG,
                            CONCEPTO = t.HREVISADO1.CONCEPTO_REVISADO,
                            OBS_RECIBIDO = t.HREVISADO1.OBS_RECIBIDO_ABOG,
                            FEC_REVISION = t.HREVISADO1.FECHA_REVISADO
                        }).ToList();
        }
        return r;
    }

   
    }
}
