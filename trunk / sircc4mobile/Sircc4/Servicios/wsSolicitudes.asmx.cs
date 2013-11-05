using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Entidades;
using BLL.Solicitudes.Vistas;
using AutoMapper;
using ByA;
using BLL.Solicitudes;
using Sircc4.Clases;

namespace Sircc4.Servicios
{
    /// <summary>
    /// Summary description for wsSolicitudes
    /// </summary>
    [WebService(Namespace = "http://www.byasystems.com.co/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsSolicitudes : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ByARpt InsertPSolicitud(vPSolicitudes Reg)
        {
            PSolicitudesBLL epBLL = new PSolicitudesBLL();
            Reg.USAP = Usuario.UserName;
            ByARpt rpt = epBLL.Insert(Reg);
            return rpt;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ByARpt UpdatePSolicitud(vPSolicitudes Reg)
        {
            PSolicitudesBLL epBLL = new PSolicitudesBLL();
            //Reg.USAP = Usuario.UserName;
            ByARpt rpt = epBLL.Update(Reg);
            return rpt;
        }

        //Solicitudes
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public vPSolicitudes GetSolicitud(string codsol)
        {
            PSolicitudesBLL epBLL = new PSolicitudesBLL();
            return epBLL.GetPK(codsol);
        }

        //Solicitudes
        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Xml)]
        public string GetSolicitudes(string dep_psol)
        {
            PSolicitudesBLL epBLL = new PSolicitudesBLL();
            string username = Usuario.UserName;
            return ByAUtil.convertListToXML(epBLL.getSolicitudes(dep_psol));
        }
    }
}
