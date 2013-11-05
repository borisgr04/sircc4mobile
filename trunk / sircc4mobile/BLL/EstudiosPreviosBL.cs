using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Transactions;
using ByA;
using System.Data;
using BLL.Vistas;
using BLL.EstPrev;
using AutoMapper;
using GDocWord;


namespace BLL
{
    public class EstudiosPreviosBL : absBLL
    {
        public string tipo { get; set; }
        public ESTPREV ep { get; set; }
        public ESTPREV proy { get; set; }

        #region Abrir
        public ByARpt Abrir()
        {
            byaRpt = new ByARpt();
            using (ctx = new Entities())
            {
                decimal id = ep.ID;

                /*var q = (from t in ctx.ESTPREV
                     where t.ID == id
                     select ep).FirstOrDefault();*/

                var q = ctx.ESTPREV.Where(t => t.ID == id).FirstOrDefault<ESTPREV>();

                if (q != null)
                {
                    ep = q;
                    byaRpt.Error = false;
                    byaRpt.Mensaje = "Se encontró";
                    byaRpt.Filas = 1;
                }
                else
                {
                    ep = null;
                    byaRpt.Error = true;
                    byaRpt.Mensaje = "No se encontró";
                    byaRpt.Filas = 0;
                }

            }
            return byaRpt;
        }

        public vESTPREV GetPK0()
        {
            vESTPREV Reg = new vESTPREV();
            Mapper.CreateMap<ESTPREV, vESTPREV>();
            
            using (ctx = new Entities())
            {
                decimal id = ep.ID;

                ep = ctx.ESTPREV.Where(t => t.ID == id).FirstOrDefault<ESTPREV>();
                if (ep!=null)
                {
                    Mapper.Map(ep, Reg);
                    if (ep.STIP_CON_EP != null)
                    {
                        Reg.TIP_CON_EP = ep.SUBTIPOS.COD_TIP;
                    }
                }
            }
            return Reg;
         
        }
        public vESTPREV GetPK(string tipo)
        {
            mESTPREV m = new mESTPREV();
            m.ep=ep;
            return m.GetPK(tipo);

        }
        #endregion

        #region Consultas

        public IList<DEPENDENCIA> GetDependencia()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.DEPENDENCIA.Where(t=>t.ESTADO=="AC");
                return lst.ToList();
            }
        }

        public IList<TIPOSCONT> GetTipos()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.TIPOSCONT.Where(t=>t.EST_TIP=="AC");
                return lst.ToList();
            }
        }

        public IList<vTIPOSCONT> GetvTIPOSCONT()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.TIPOSCONT.Where(t => t.EST_TIP == "AC").Select(t=> new vTIPOSCONT{ COD_TIP=t.COD_TIP, NOM_TIP= t.NOM_TIP, EST_TIP= t.EST_TIP});
                return lst.ToList();
            }
        }

        public IList<SUBTIPOS> GetSubTipos(string Cod_Tip)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.SUBTIPOS.Where(st => st.COD_TIP == Cod_Tip && st.ESTADO=="AC");
                return lst.ToList();
            }
        }

        public IList<vEP_ESTADOS> GetvEP_ESTADOS()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.EP_ESTADOS.Select(t => new vEP_ESTADOS { COD_EST=t.COD_EST, NOM_EST= t.NOM_EST });
                return lst.ToList();
            }
        }
        public IList<vSUBTIPOS> GetvSUBTIPOS(string Cod_Tip)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.SUBTIPOS.Where(st => st.COD_TIP == Cod_Tip && st.ESTADO == "AC").Select(t=> new vSUBTIPOS{ COD_STIP= t.COD_STIP, NOM_STIP= t.NOM_STIP, NOMC_STIP= t.TIPOSCONT.NOM_TIP+" "+ t.NOM_STIP, COD_TIP= t.COD_STIP});
                return lst.ToList();
            }
        }


        public IList<TIPO_PLAZOS> GetPlazos()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.TIPO_PLAZOS;
                return lst.ToList();
            }
        }

        public IList<TIPO_PLAZOS> GetPlazos(string Cod_Plazo)
        {
            using (ctx = new Entities())
            {
                TIPO_PLAZOS tpInicial = (ctx.TIPO_PLAZOS.Where(tp => tp.COD_TPLA == Cod_Plazo)).FirstOrDefault();
                decimal? OrdSel = tpInicial.ORD_TPLA;
                var lst = ctx.TIPO_PLAZOS.Where(tp => tp.ORD_TPLA < OrdSel);
                return lst.ToList();
            }
        }


        public IList<vTIPO_PLAZOS> GetvTIPO_PLAZOS()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.TIPO_PLAZOS.Select(t=> new vTIPO_PLAZOS{ COD_TPLA= t.COD_TPLA, NOM_PLA= t.NOM_PLA });
                return lst.ToList();
            }
        }

        public IList<vTIPOSPROC> GetvModalidad()
        {
            using (ctx = new Entities())
            {
                
                var lst = ctx.TIPOSPROC.Where(t=>t.COD_TPROC!="TP00" && t.ESTA_TPROC=="AC").Select(t => new vTIPOSPROC { COD_TPROC=t.COD_TPROC, ABRE_TPROC= t.ABRE_TPROC, NOM_TPROC= t.NOM_TPROC }).OrderBy(t=>t.NOM_TPROC);
                return lst.ToList();
            }
        }

        public IList<vTIPO_PLAZOS> GetvTIPO_PLAZOS(string Cod_Plazo)
        {
            using (ctx = new Entities())
            {
                TIPO_PLAZOS tpInicial = (ctx.TIPO_PLAZOS.Where(tp => tp.COD_TPLA == Cod_Plazo)).FirstOrDefault();
                decimal? OrdSel = tpInicial.ORD_TPLA;
                var lst = ctx.TIPO_PLAZOS.Where(tp => tp.ORD_TPLA < OrdSel).Select(t=> new vTIPO_PLAZOS{ COD_TPLA= t.COD_TPLA, NOM_PLA= t.NOM_PLA });
                return lst.ToList();
            }
        }

        
        public IList<EP_CARGO> GetCargos(string Vig)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.EP_CARGO.Where(t => t.EST_CARGO == "AC" && t.VIG_CARGO == Vig);
                return lst.ToList();
            }
        }

        public IList<vEP_CARGO> GetvEP_CARGO(string Vig)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.EP_CARGO.Where(t => t.EST_CARGO == "AC" && t.VIG_CARGO == Vig).Select(t => new vEP_CARGO { COD_CARGO = t.COD_CARGO, DES_CARGO = t.DES_CARGO });
                return lst.ToList();
            }
        }

        public IList<vProyectos> GetProyectos(string filtro)
        {
            using (ctx = new Entities())
            {
                //var lst = ctx.PROYECTOS.Where(t => t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro));
                var lst = (from t in ctx.PROYECTOS
                           where (t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro) )
                           select (new vProyectos { Nro_Proyecto = t.PROYECTO, Nombre_Proyecto = t.NOMBRE_PROYECTO}));
                return lst.ToList();
            }
        }


        public IList<vEP_POLIZAS> GetEP_POLIZAS(int iID_EP)
        {
            using (ctx = new Entities())
            {
                //var lst = ctx.PROYECTOS.Where(t => t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro));
                var lst = (from t in ctx.EP_POLIZAS
                           where (t.ID_EP == iID_EP)
                           select (new vEP_POLIZAS
                           {
                               ID = t.ID,
                               COD_POL = t.COD_POL,
                               ID_EP = t.ID_EP,
                               POR_SMMLV = t.POR_SMMLV,
                               CAL_APARTIRDE = t.CAL_APARTIRDE,
                               VIGENCIA = t.VIGENCIA,
                               APARTIRDE = t.APARTIRDE,
                               TIPO = t.TIPO,
                               GRUPO = t.GRUPO,
                               NOM_POL = t.POLIZAS.NOM_POL.ToUpper(),
                               NOM_CALPOL = t.CALCULOPOL.DESCRIPCION,
                               NOM_CALVIGPOL = t.CAL_VIG_POL.DESCRIPCION
                           }));
                return lst.ToList();
            }
        }
        public IList<vPOLIZAS> GetPOLIZAS()
        {
            using (ctx = new Entities())
            {
                var lst =  (from  t in ctx.POLIZAS 
                            where t.EST_POL=="AC"
                            select (new vPOLIZAS { COD_POL = t.COD_POL, NOM_POL = t.NOM_POL.ToUpper() }));
                return lst.ToList();
            }
        }

        public IList<vCALCULOPOL> GetCALCULOPOL()
        {
            using (ctx = new Entities())
            {
                var lst = (from t in ctx.CALCULOPOL
                           select (new vCALCULOPOL { COD_CAL = t.COD_CAL, DESCRIPCION = t.DESCRIPCION }));
                return lst.ToList();
            }
        }

        public IList<vCAL_VIG_POL> GetCAL_VIG_POL()
        {
            
            using (ctx = new Entities())
            {
                var lst = (from t in ctx.CAL_VIG_POL
                           select (new vCAL_VIG_POL { COD_CAL = t.COD_CAL, DESCRIPCION = t.DESCRIPCION }));
                return lst.ToList();
            }
        }


        public IList<vEP_ESPTEC> GetEP_ESPTEC(int iID_EP)
        {
            using (ctx = new Entities())
            {
                //var lst = ctx.PROYECTOS.Where(t => t.PROYECTO.Contains(filtro) || t.NOMBRE_PROYECTO.Contains(filtro));
                var lst = (from t in ctx.EP_ESPTEC
                           where (t.ID_EP==iID_EP)
                           select (new vEP_ESPTEC {
                               ID_EP=t.ID_EP,
                               DESC_ITEM = t.DESC_ITEM,
                               CANT_ITEM = t.CANT_ITEM,
                               UNI_ITEM = t.UNI_ITEM,
                               VAL_UNI_ITEM=t.VAL_UNI_ITEM,
                               PORC_IVA= t.PORC_IVA,
                               ID= t.ID,
                               GRUPO= t.GRUPO

                           }));
                return lst.ToList();
            }
        }

        public IList<vTerceros> GetTerceros(string filtro)
        {
            using (ctx = new Entities())
            {
                var lst = (from t in ctx.TERCEROS
                           where t.ESTADO=="AC" && t.IDE_TER.Contains(filtro) || (t.APE1_TER.Trim() + " " + t.APE2_TER.Trim() + " " + t.NOM1_TER.Trim() + " " + t.NOM2_TER.Trim()).ToUpper().Contains(filtro.ToUpper())
                            
                           select (new vTerceros { 
                               IDE_TER = t.IDE_TER,
                               APE1_TER = t.APE1_TER, 
                               APE2_TER = t.APE2_TER, 
                               NOM1_TER=  t.NOM1_TER,
                               NOM2_TER= t.NOM2_TER }));
                return lst.ToList();
            }
        }

        public IList<vTerceros> GetTerceros()
        {
            using (ctx = new Entities())
            { 
                var lst = (from t in ctx.TERCEROS
                           where t.ESTADO == "AC"
                           select (new vTerceros { 
                               IDE_TER = t.IDE_TER, 
                               APE1_TER = t.APE1_TER,
                               APE2_TER = t.APE2_TER, 
                               NOM1_TER = t.NOM1_TER, 
                               NOM2_TER = t.NOM2_TER }));
                return lst.ToList();
            }
        }

        public IList<vEP_Proyectos> GetEP_Proyectos(decimal id)
        {
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_PROYECTOS
                          where t.ID_EP == id
                          orderby t.COD_PRO
                          select (new vEP_Proyectos { COD_PRO = t.COD_PRO, NOMBRE_PROYECTO = t.PROYECTOS.NOMBRE_PROYECTO, ID_EP = t.ID_EP });
                return lst.ToList();
            }
        }

        public IList<vDEPENDENCIA> GetvDEPENDENCIA(string ide_ter)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.HDEP_ABOGADOS.
                    Where(t => t.IDE_TER == ide_ter).
                    Select(t => t.DEPENDENCIA).
                    Where(t => t.ESTADO == "AC" && t.COD_DEP!="00").
                    Select(t => new vDEPENDENCIA { COD_DEP = t.COD_DEP, NOM_DEP = t.NOM_DEP }).OrderBy(t=>t.NOM_DEP);
                
                return lst.ToList();
            }
        }

        public IList<vDEPENDENCIA> GetvDEPENDENCIAD()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.DEPENDENCIA.Where(t => t.ESTADO == "AC" && t.COD_DEP != "00" && t.DEP_DEL=="S").
                    Select(t => new vDEPENDENCIA { COD_DEP = t.COD_DEP, NOM_DEP = t.NOM_DEP }).OrderBy(t => t.NOM_DEP);

                return lst.ToList();
            }
        }

        public IList<vDEPENDENCIA> GetvDEPENDENCIA()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.DEPENDENCIA.Where(t => t.ESTADO == "AC" && t.COD_DEP != "00").
                    Select(t => new vDEPENDENCIA { COD_DEP = t.COD_DEP, NOM_DEP = t.NOM_DEP }).OrderBy(t => t.NOM_DEP);

                return lst.ToList();
            }
        }


        public IList<vVIGENCIAS> GetvVIGENCIAS()
        {
            using (ctx = new Entities())
            {
                var lst = ctx.VIGENCIAS.Select(t => new vVIGENCIAS { YEAR_VIG = t.YEAR_VIG }).OrderByDescending(t=>t.YEAR_VIG);
                return lst.ToList();
            }
        }
#endregion

        #region EP_ESPTEC
        public ByARpt Insert(EP_ESPTEC et)
        {
            mEP_ESPTEC manager = new mEP_ESPTEC();
            manager.et = et;
            return EnviaDatos.EnviarInsert(manager);
        }
        public ByARpt Update(EP_ESPTEC et)
        {
            mEP_ESPTEC manager = new mEP_ESPTEC();
            manager.et = et;
            return EnviaDatos.EnviarUpdate(manager);
        }
        #endregion
        
        #region EP_PORYECTOS
        public ByARpt Insert(EP_PROYECTOS pry)
        {
            mEP_Proyectos manager = new mEP_Proyectos();
            manager.pry = pry;
            return EnviaDatos.EnviarInsert(manager);
        }

        public ByARpt Delete(EP_PROYECTOS pry)
        {
            mEP_Proyectos manager = new mEP_Proyectos();
            manager.pry = pry;
            return EnviaDatos.EnviarDelete(manager);
        }
        #endregion
        
        #region EP_ESTPRE
        public ByARpt Insert()
        {
            mESTPREV manager = new mESTPREV();
            manager.ep = this.ep;
            return EnviaDatos.EnviarInsert(manager);
        }
        public ByARpt Update()
        {
            mESTPREV manager = new mESTPREV();
            manager.ep = this.ep;
            return EnviaDatos.EnviarUpdate(manager);
        }
        public ByARpt Delete(EP_ESPTEC reg)
        {
            mEP_ESPTEC manager = new mEP_ESPTEC();
            manager.et = reg;
            return EnviaDatos.EnviarDelete(manager);
        }
        #endregion

        #region EP_POLIZAS
        public ByARpt Insert(EP_POLIZAS reg)
        {
            mEP_POLIZAS manager = new mEP_POLIZAS();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }
        public ByARpt Update(EP_POLIZAS reg)
        {
            mEP_POLIZAS manager = new mEP_POLIZAS();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_POLIZAS reg)
        {
            mEP_POLIZAS manager = new mEP_POLIZAS();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }
        #endregion

        #region ObligacionesC

        public IList<vEP_OBLIGACIONESC> GetEP_OBLIGACIONESC(decimal id)
        {
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_OBLIGACIONESC
                          where t.ID_EP == id
                          select (new vEP_OBLIGACIONESC { ID = t.ID, GRUPO = t.GRUPO, DES_OBLIG = t.DES_OBLIG, ID_EP = t.ID_EP });
                return lst.ToList();
            }
        }

        public ByARpt Insert(EP_OBLIGACIONESC reg)
        {
            mEP_OBLIGACIONESC manager = new mEP_OBLIGACIONESC();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        public ByARpt Update(EP_OBLIGACIONESC reg)
        {
            mEP_OBLIGACIONESC manager = new mEP_OBLIGACIONESC();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_OBLIGACIONESC reg)
        {
            mEP_OBLIGACIONESC manager = new mEP_OBLIGACIONESC();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }

        #endregion

        #region ObligacionesE

        public IList<vEP_OBLIGACIONESE> GetEP_OBLIGACIONESE(decimal id)
        {
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_OBLIGACIONESE
                          where t.ID_EP == id
                          select (new vEP_OBLIGACIONESE { ID = t.ID, GRUPO = t.GRUPO, DES_OBLIG = t.DES_OBLIG, ID_EP = t.ID_EP });
                return lst.ToList();
            }
        }

        public ByARpt Insert(EP_OBLIGACIONESE reg)
        {
            mEP_OBLIGACIONESE manager = new mEP_OBLIGACIONESE();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        public ByARpt Update(EP_OBLIGACIONESE reg)
        {
            mEP_OBLIGACIONESE manager = new mEP_OBLIGACIONESE();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_OBLIGACIONESE reg)
        {
            mEP_OBLIGACIONESE manager = new mEP_OBLIGACIONESE();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }

        #endregion

        #region CapJur

        public IList<vEP_DT_CAP_JUR> GetEP_DT_CAP_JUR(decimal id)
        {
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_DT_CAP_JUR
                          where t.EP_CAP_JUR.Where(t2=>t2.ID_EP==id && t2.ID_CAPJ==t.ID).Count()==0
                          select (new vEP_DT_CAP_JUR { EST_CAPJ = t.EST_CAPJ, DES_CAPJ = t.DES_CAPJ, ID = t.ID });
                return lst.ToList();
            }
        }

        public IList<vEP_CAP_JUR> GetEP_CAP_JUR(decimal id)
        {
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_CAP_JUR
                          where t.ID_EP == id
                          select (new vEP_CAP_JUR { ID = t.ID, DES_CAPJ = t.DES_CAPJ, ID_EP = t.ID_EP, ID_CAPJ=t.ID_CAPJ });
                return lst.ToList();
            }
        }


        public ByARpt Insert(EP_CAP_JUR reg)
        {
            mEP_CAP_JUR manager = new mEP_CAP_JUR();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        public ByARpt Update(EP_CAP_JUR reg)
        {
            mEP_CAP_JUR manager = new mEP_CAP_JUR();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_CAP_JUR reg)
        {
            mEP_CAP_JUR manager = new mEP_CAP_JUR();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }
        #endregion

        #region MunRegion
        public IList<vEP_CONMUN> GetEP_MUN(decimal id)
        {
            
            using (ctx = new Entities())
            {
                var lst = from t in ctx.EP_CONMUN
                          where t.ID_EP == id
                          select (new vEP_CONMUN { ID = t.ID, NOM_MUN = t.MUNICIPIOS.NOM_MUN, ID_EP = t.ID_EP, COD_MUN = t.COD_MUN });
                return lst.ToList();
            }
        }

        public IList<vMUNICIPIOS> GetMUN(decimal id)
        {
            using (ctx = new Entities())
            {

                var lst = from t in ctx.MUNICIPIOS
                          where t.EP_CONMUN.Where(t2 => t2.ID_EP == id && t2.COD_MUN == t.COD_MUN).Count() == 0
                          select (new vMUNICIPIOS { NOM_MUN = t.NOM_MUN, COD_MUN = t.COD_MUN });
                return lst.ToList();
            }
        }

         public ByARpt Insert(EP_CONMUN reg)
        {
            mEP_CONMUN manager = new mEP_CONMUN();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        public ByARpt Delete(EP_CONMUN reg)
        {
            mEP_CONMUN manager = new mEP_CONMUN();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }
    
        #endregion
        
        #region CDP
        public IList<vEP_CDP> GetEP_CDP(int id)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.EP_CDP.Where(t=>t.ID_EP==id).Select(t => new vEP_CDP {  FEC_CDP=t.FEC_CDP, GRUPO= t.GRUPO, ID= t.ID, ID_EP= t.ID_EP, NRO_CDP= t.NRO_CDP, VAL_CDP= t.VAL_CDP, VIG_FUT= t.VIG_FUT  }).OrderByDescending(t => t.NRO_CDP);
                return lst.ToList();
            }
        }

        public ByARpt Update(EP_CDP reg)
        {
            mEP_CDP manager =new mEP_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_CDP reg)
        {
            mEP_CDP manager = new mEP_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager); 
        }

        public ByARpt Insert(EP_CDP reg)
        {
            //mEP_CDP_I manager = new mEP_CDP_I();
            mEP_CDP manager = new mEP_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        

        #endregion

        #region EP_RUBROS_CDP

        public IList<vEP_RUBROS_CDP> GetEP_RUBROS_CDP(decimal id_ep_cdp)
        {
            using (ctx = new Entities())
            {
                var lst = ctx.EP_RUBROS_CDP.Where(t=>t.ID_EP_CDP==id_ep_cdp).Select(t => new vEP_RUBROS_CDP
                { 
                     COD_RUB=t.COD_RUB,
                     GRUPO= t.GRUPO,
                     NOM_RUB=t.RUBROS.DES_RUB,
                     VALOR= t.VALOR,
                     NRO_CDP = t.NRO_CDP,
                     ID_EP= t.ID_EP,
                     ID_EP_CDP= t.ID_EP_CDP,
                     ID=t.ID,
                     VIG_CDP= t.VIG_CDP
               }).OrderByDescending(t => t.NRO_CDP);
                return lst.ToList();
            }
        }

        public ByARpt Update(EP_RUBROS_CDP reg)
        {
            mEP_RUBROS_CDP manager = new mEP_RUBROS_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarUpdate(manager);
        }

        public ByARpt Delete(EP_RUBROS_CDP reg)
        {
            mEP_RUBROS_CDP manager = new mEP_RUBROS_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarDelete(manager);
        }

        public ByARpt Insert(EP_RUBROS_CDP reg)
        {
            mEP_RUBROS_CDP manager = new mEP_RUBROS_CDP();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }

        #endregion

       //Crear EP Apartir de Plantilla.
        public ByARpt NuevodePlantilla(){
            return this.InsertDePlantilla();
        }
        public ByARpt InsertDePlantilla()
        {
            mESTPREVplantilla manager = new mESTPREVplantilla();
            manager.ep = this.ep;
            return EnviaDatos.EnviarInsert(manager);
        }

        #region FORMADEPAGO
        public IList<vEP_FORMA_PAGO> GetEP_FORMA_PAGO(int iID_EP)
        {
            
            using (ctx = new Entities())
            {
                
                var lst = ctx.EP_FORMA_PAGO.Where(t=>t.ID_EP == iID_EP)
                           .Select(t=> new vEP_FORMA_PAGO
                           {
                               ID_EP = t.ID_EP,
                               ID = t.ID,
                               CON_FPAG=t.CON_FPAG,
                               ORD_FPAG=t.ORD_FPAG,
                               PGEN_FPAG=t.PGEN_FPAG,
                               POR_FPAG=t.POR_FPAG,
                               TIP_FPAG= t.TIP_FPAG,
                               VAL_FPAG=t.VAL_FPAG,
                               NOM_TIP_FPAG = t.TIPO_PAGO.DES_PAGO
                           }
                           ).OrderBy(t=>t.ORD_FPAG);
                return lst.ToList();
            }
        }

        public IList<vTIPO_PAGO> GetTIPO_PAGO()
        {
            using (ctx = new Entities())
            {
                
                var lst = (from t in ctx.TIPO_PAGO
                           select (new vTIPO_PAGO {  ID_PAGO= t.ID_PAGO, DES_PAGO=t.DES_PAGO }));
                return lst.ToList();
            }
        }

        public ByARpt Insert(EP_FORMA_PAGO fp)
        {
            mEP_FORMA_PAGO manager = new mEP_FORMA_PAGO();
            manager.fp = fp;
            return EnviaDatos.EnviarInsert(manager);
        }
        public ByARpt Update(EP_FORMA_PAGO fp)
        {
            mEP_FORMA_PAGO manager = new mEP_FORMA_PAGO();
            manager.fp = fp;
            return EnviaDatos.EnviarUpdate(manager);
        }
        
        public ByARpt Delete(EP_FORMA_PAGO fp)
        {
            mEP_FORMA_PAGO manager = new mEP_FORMA_PAGO();
            manager.fp = fp;
            return EnviaDatos.EnviarDelete(manager);
        }

        public ByARpt Insert(EP_HESTADOEP reg)
        {
            mEP_HESTADOEP manager = new mEP_HESTADOEP();
            manager.reg = reg;
            return EnviaDatos.EnviarInsert(manager);
        }
        #endregion

    }   
   
}
