using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entidades;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ByA;
using BLL.Vistas;
using AutoMapper;
using System.Web.Security;
using Sircc4.Clases;


namespace Sircc4.EstPrev
{
    public partial class wfRgEstPrev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*foreach(string s in Request.Form){
                //this.LbMsg.Text += s;
            }*/
            
            
            
        }

        
    #region Proyecto
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public static string GetProyectos(string filtro)
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            IList<vProyectos> lt = ep.GetProyectos(filtro);
            return ByAUtil.convertListToXML(lt);
        }
        //EnableSession = true)
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public static string GetEP_Proyectos(decimal id)
        {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            return ByAUtil.convertListToXML(epBLL.GetEP_Proyectos(id));
        }

        //GetEP_Proyectos
    [WebMethod(EnableSession = true)]
    [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
        public static ByARpt GuardarNuevoProyecto(string Cod, int Id_EP)
        {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            EP_PROYECTOS pry = new EP_PROYECTOS();
            pry.COD_PRO = Cod;
            pry.ID_EP = Id_EP;
            pry.USAP_REG=Clases.Usuario.UserName;
            ByARpt rpt = epBLL.Insert(pry);
            return rpt;
        }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteProyecto(string Cod, int Id_EP)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_PROYECTOS pry = new EP_PROYECTOS();
        pry.COD_PRO = Cod;
        pry.ID_EP = Id_EP;
        ByARpt rpt = epBLL.Delete(pry);
        return rpt;
    }
    #endregion

    #region FormaPago

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertFP(vEP_FORMA_PAGO Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_FORMA_PAGO aeReg = new EP_FORMA_PAGO();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_FORMA_PAGO, EP_FORMA_PAGO>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdateFP(vEP_FORMA_PAGO Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_FORMA_PAGO aeReg = new EP_FORMA_PAGO();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_FORMA_PAGO, EP_FORMA_PAGO>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt DeleteFP(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_FORMA_PAGO aeReg = new EP_FORMA_PAGO();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_FORMA_PAGO(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(ep.GetEP_FORMA_PAGO(id));
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vTIPO_PAGO> GetTIPO_PAGO()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ep.GetTIPO_PAGO(); 
    }
    #endregion

    #region EspTec


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_ESPTEC(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_ESPTEC> lEp = ep.GetEP_ESPTEC(id);
        return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
    public static ByARpt GuardarEP(vEP_ESPTEC Reg)
    {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            EP_ESPTEC aeReg = new EP_ESPTEC();
            //Mapear Objeto DTO a Ado Entity FrameWork
            Mapper.CreateMap<vEP_ESPTEC, EP_ESPTEC>();
            Mapper.Map(Reg, aeReg);
            //Envia el registro a la base de datos
            ByARpt rpt = epBLL.Insert(aeReg);
            return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteEP(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_ESPTEC aeReg = new EP_ESPTEC();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt GuardarModEP(vEP_ESPTEC Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_ESPTEC aeReg = new EP_ESPTEC();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_ESPTEC, EP_ESPTEC>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt GuardarModEPList(List<ListEspTec> Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        ByARpt rpt = new ByARpt();
        //rpt.Filas = Reg.index;
        foreach (ListEspTec i in Reg)
        {
            EP_ESPTEC aeReg = new EP_ESPTEC();
            //Mapear Objeto DTO a Ado Entity FrameWork
            Mapper.CreateMap<vEP_ESPTEC, EP_ESPTEC>();
            //Mapper.CreateMap<Data, EP_ESPTEC>();
            Mapper.Map(i.data, aeReg);
            //Envia el registro a la base de datos
            rpt = epBLL.Update(aeReg);
        }
        
        return rpt;
    }
    
    public class ListEspTec
    {
        public int index { get; set; }
        public vEP_ESPTEC data { get; set; }
    }
    #endregion

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetTercerosPk(string ide_ter)
    {
        GCConsContratosBL Gestcont = new GCConsContratosBL();
        vTerceros t = Gestcont.GetTercerosPk(ide_ter);
        return t == null ? "0" : t.NOMBRE;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetTipos()
    {
        TIPOSCONT t = new TIPOSCONT();

        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        List<cTip_Con> lt = ep.GetTipos().Select(i => new cTip_Con { COD_TIP = i.COD_TIP, NOM_TIP = i.NOM_TIP }).ToList();
        return ByAUtil.convertListToXML(lt);
    }

        
    
    #region EP_ObligacionesC

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_OBLIGACIONESC(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_OBLIGACIONESC> lEp= ep.GetEP_OBLIGACIONESC(id);
        return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertObligacionesC(vEP_OBLIGACIONESC   Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESC aeReg = new EP_OBLIGACIONESC();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_OBLIGACIONESC, EP_OBLIGACIONESC>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdateObligacionesC(vEP_OBLIGACIONESC Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESC aeReg = new EP_OBLIGACIONESC();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_OBLIGACIONESC, EP_OBLIGACIONESC>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteObligacionesC(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESC aeReg = new EP_OBLIGACIONESC();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
    #endregion

    #region EP_ObligacionesE

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_OBLIGACIONESE(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_OBLIGACIONESE> lEp = ep.GetEP_OBLIGACIONESE(id);
        return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertObligacionesE(vEP_OBLIGACIONESE Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESE aeReg = new EP_OBLIGACIONESE();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_OBLIGACIONESE, EP_OBLIGACIONESE>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdateObligacionesE(vEP_OBLIGACIONESE Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESE aeReg = new EP_OBLIGACIONESE();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_OBLIGACIONESE, EP_OBLIGACIONESE>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteObligacionesE(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_OBLIGACIONESE aeReg = new EP_OBLIGACIONESE();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
    #endregion
    
    #region POLIZAS
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_POLIZAS(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_POLIZAS> lEp = ep.GetEP_POLIZAS(id);
        return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vPOLIZAS> GetPOLIZAS()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vPOLIZAS> lEp = ep.GetPOLIZAS();
        return lEp;
        //return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vCAL_VIG_POL> GetCAL_VIG_POL()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vCAL_VIG_POL> lEp = ep.GetCAL_VIG_POL();
        return lEp;
        //return ByAUtil.convertListToXML(lEp);
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vCALCULOPOL> GetCALCULOPOL()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vCALCULOPOL> lEp = ep.GetCALCULOPOL();
        return lEp;
        //return ByAUtil.convertListToXML(lEp);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod( ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertPolizas(vEP_POLIZAS Reg)
    {
            EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
            EP_POLIZAS aeReg = new EP_POLIZAS();
            //Mapear Objeto DTO a Ado Entity FrameWork
            Mapper.CreateMap<vEP_POLIZAS, EP_POLIZAS>();
            Mapper.Map(Reg, aeReg);
            //Envia el registro a la base de datos
            ByARpt rpt = epBLL.Insert(aeReg);
            return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdatePolizas(vEP_POLIZAS Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_POLIZAS aeReg = new EP_POLIZAS();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_POLIZAS, EP_ESPTEC>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        
        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deletePolizas(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_POLIZAS aeReg = new EP_POLIZAS();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
    #endregion
    
    #region EP_RUBROS_CDP
        
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_RUBROS_CDP(int id_ep_cdp)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(ep.GetEP_RUBROS_CDP(id_ep_cdp));
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertRub(vEP_RUBROS_CDP Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_RUBROS_CDP aeReg = new EP_RUBROS_CDP();
        //Mapear Objeto DTO a Ado Entity FrameWork

        Mapper.CreateMap<vEP_RUBROS_CDP, EP_RUBROS_CDP>();
        Mapper.Map(Reg, aeReg);
        aeReg.USAP_REG = Usuario.UserName;
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdateRub(vEP_RUBROS_CDP Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_RUBROS_CDP aeReg = new EP_RUBROS_CDP();
        //Mapear Objeto DTO a Ado Entity FrameWork

        Mapper.CreateMap<vEP_RUBROS_CDP, EP_RUBROS_CDP>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        aeReg.USAP_MOD = Usuario.UserName;

        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteRub(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_RUBROS_CDP aeReg = new EP_RUBROS_CDP();
        
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
        
    #endregion
    
    #region CDP
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_CDP(int id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(ep.GetEP_CDP(id));
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertCDP(vEP_CDP Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CDP aeReg = new EP_CDP();
        //Mapear Objeto DTO a Ado Entity FrameWork
        
        Mapper.CreateMap<vEP_CDP, EP_CDP>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt UpdateCDP(vEP_CDP Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CDP aeReg = new EP_CDP();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_CDP, EP_CDP>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos

        ByARpt rpt = epBLL.Update(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteCDP(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CDP aeReg = new EP_CDP();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
    #endregion
    
    #region CAP_JUR
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_DT_CAP_JUR(decimal id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_DT_CAP_JUR> lt = ep.GetEP_DT_CAP_JUR(id);
        return ByAUtil.convertListToXML(lt);
    }
    //EnableSession = true)
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_CAP_JUR(decimal id)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(epBLL.GetEP_CAP_JUR(id));
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertCapJur(vEP_CAP_JUR Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CAP_JUR aeReg = new EP_CAP_JUR();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_CAP_JUR, EP_CAP_JUR>();
        Mapper.Map(Reg, aeReg);
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteCapJur(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CAP_JUR aeReg = new EP_CAP_JUR();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }
    
    #endregion

    #region MUNICIPIOS_REGIONES
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetEP_MUN(decimal id)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(ep.GetEP_MUN(id));
    }
    //EnableSession = true)
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetMun(decimal id)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(epBLL.GetMUN(id));
    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertMun(vEP_CONMUN Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CONMUN aeReg = new EP_CONMUN();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_CONMUN, EP_CONMUN>();
        Mapper.Map(Reg, aeReg);
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt deleteMun(decimal ID)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_CONMUN aeReg = new EP_CONMUN();
        aeReg.ID = ID;
        ByARpt rpt = epBLL.Delete(aeReg);
        return rpt;
    }

    #endregion
    #region ESTPREV

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static vESTPREV GetEstPrev(string id_ep, string tipo)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        ESTPREV ep = new ESTPREV();
        ep.ID = Convert.ToInt32(id_ep);
        epBLL.ep = ep;

        vESTPREV Reg = epBLL.GetPK(tipo);
        return Reg;
    }
        /*
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static vESTPREV GetEstPrev(string id_ep)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        ESTPREV ep = new ESTPREV();
        ep.ID = Convert.ToInt32(id_ep);
        epBLL.ep = ep;
        vESTPREV Reg = epBLL.GetPK();
        return Reg;
    }
        */
    #endregion

    #region COMBOS
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vEP_CARGO> GetvEP_CARGO()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_CARGO> lEp = ep.GetvEP_CARGO("2013");
        return lEp;
        
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vDEPENDENCIA> GetvDEPENDENCIA()
    {
        string ide_ter = Usuario.UserName;
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vDEPENDENCIA> lEp = ep.GetvDEPENDENCIA(ide_ter);
        return lEp;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vDEPENDENCIA> GetvDEPENDENCIAD()
    {
        string ide_ter = Usuario.UserName;
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vDEPENDENCIA> lEp = ep.GetvDEPENDENCIAD();
        return lEp;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vDEPENDENCIA> GetvDEPENDENCIAT()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vDEPENDENCIA> lEp = ep.GetvDEPENDENCIA();
        return lEp;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vTIPOSCONT> GetvTIPOSCONT()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vTIPOSCONT> lEp = ep.GetvTIPOSCONT();
        return lEp;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vSUBTIPOS> GetvSUBTIPOS(string cod_tip)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vSUBTIPOS> lEp = ep.GetvSUBTIPOS(cod_tip);
        return lEp;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vTIPO_PLAZOS> GetvTIPO_PLAZOS2(string cod_tpla)
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vTIPO_PLAZOS> lEp = ep.GetvTIPO_PLAZOS(cod_tpla);
        return lEp;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vTIPOSPROC> GetvModalidad()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vTIPOSPROC> lEp = ep.GetvModalidad();
        return lEp;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vEP_ESTADOS> GetvEP_ESTADOS()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vEP_ESTADOS> lEp = ep.GetvEP_ESTADOS();
        return lEp;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vVIGENCIAS> GetvVIGENCIAS()
    {
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vVIGENCIAS> lEp = ep.GetvVIGENCIAS();
        return lEp;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static IList<vTIPO_PLAZOS> GetvTIPO_PLAZOS()
    {
        
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        IList<vTIPO_PLAZOS> lEp = ep.GetvTIPO_PLAZOS();
        return lEp;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
    public static string GetTerceros()
    {
        
        EstudiosPreviosBL ep = new EstudiosPreviosBL();
        return ByAUtil.convertListToXML(ep.GetTerceros()); ;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt insertESTPREV(vESTPREV Reg)
    {
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        ESTPREV aeReg = new ESTPREV();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vESTPREV, ESTPREV>();
        Mapper.Map(Reg, aeReg);
        epBLL.ep=aeReg;
        epBLL.ep.USAP_REG_EP = Usuario.UserName;
        //epBLL.ep.USAP_ELA_EP = Usuario.UserName; OJO CON EL USUARIO
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert();
        return rpt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt updateESTPREV(vESTPREV Reg)
    {
        
        ByARpt ByARpt= new ByARpt(); 
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        ESTPREV ep = new ESTPREV();
        ep.ID = Reg.ID;
        epBLL.ep = ep;
        if (epBLL.Abrir() == 0)
        {
            ByARpt = new ByARpt { 
                Error=true,
                 Mensaje= "No se encontró registro",
                 Filas=0,
                  id=null
            }; 
        }
        else{
            try
            {
                //Mapear Objeto DTO a Ado Entity FrameWork
                Mapper.CreateMap<vESTPREV, ESTPREV>();
                Mapper.Map(Reg, ep); //Solo deben ir los datos que viene para la modificación
                epBLL.ep = ep;
                epBLL.ep.USAP_MOD_EP = Clases.Usuario.UserName;
                ByARpt = epBLL.Update();
            }
            catch (Exception ex)
            {
                ByARpt = new ByARpt
                {
                    Error = true,
                    Mensaje = "Captura:" + ex.Message,
                    Filas = 0,
                    id = null
                }; 
            }
            
        }
        return ByARpt;
    }
    #endregion
    #region FormaPago

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static ByARpt InsertHEstadoEP(vEP_HESTADOEP Reg)
    {
        Reg.USAP_EP = Usuario.UserName;
        EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
        EP_HESTADOEP aeReg = new EP_HESTADOEP();
        //Mapear Objeto DTO a Ado Entity FrameWork
        Mapper.CreateMap<vEP_HESTADOEP, EP_HESTADOEP>();
        Mapper.Map(Reg, aeReg);
        //Envia el registro a la base de datos
        ByARpt rpt = epBLL.Insert(aeReg);
        return rpt;
    }
    //[WebMethod(EnableSession = true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static ByARpt UpdateFP(vEP_FORMA_PAGO Reg)
    //{
    //    EstudiosPreviosBL epBLL = new EstudiosPreviosBL();
    //    EP_FORMA_PAGO aeReg = new EP_FORMA_PAGO();
    //    //Mapear Objeto DTO a Ado Entity FrameWork
    //    Mapper.CreateMap<vEP_FORMA_PAGO, EP_FORMA_PAGO>();
    //    Mapper.Map(Reg, aeReg);
    //    //Envia el registro a la base de datos
    //    ByARpt rpt = epBLL.Update(aeReg);
    //    return rpt;
    //}
    //
    #endregion
    }
    public class cTip_Con
    {
        public string COD_TIP{get; set;}
        public string NOM_TIP{get; set;}
    }
}