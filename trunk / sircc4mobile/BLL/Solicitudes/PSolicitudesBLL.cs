using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Solicitudes.Vistas;
using ByA;
using BLL.Solicitudes.Gestion;
using Entidades;
using AutoMapper;

namespace BLL.Solicitudes
{
    public class PSolicitudesBLL : absBLL
    {
        public vPSolicitudes ep { get; set; }

        #region PSolicitudes
        public ByARpt Insert(vPSolicitudes Reg)
        {
            PSOLICITUDES RegD = new PSOLICITUDES();
            mPSOLICITUDES manager = new mPSOLICITUDES();
            Mapper.CreateMap<vPSolicitudes, PSOLICITUDES>();
            Mapper.Map(Reg, RegD);
            manager.reg = RegD;
            return EnviaDatos.EnviarInsert(manager);
        }
        public ByARpt Update(vPSolicitudes Reg)
        {
            PSOLICITUDES RegD = new PSOLICITUDES();
            mPSOLICITUDES manager = new mPSOLICITUDES();
            Mapper.CreateMap<vPSolicitudes, PSOLICITUDES>();
            Mapper.Map(Reg, RegD);
            manager.reg = RegD;
            return EnviaDatos.EnviarUpdate(manager);
        }
        //public ByARpt Delete(EP_ESPTEC reg)
        //{
        //    mEP_ESPTEC manager = new mEP_ESPTEC();
        //    manager.et = reg;
        //    return EnviaDatos.EnviarDelete(manager);
        //}
        #endregion

        public vPSolicitudes GetPK(string Cod_Sol)
        {
            mPSOLICITUDES manager = new mPSOLICITUDES();
            manager.reg = new PSOLICITUDES { COD_SOL = Cod_Sol };
            return manager.GetPK();
        }

        public List<vPSolicitudes> getSolicitudes(string Dep_PSol)
        {
            mPSOLICITUDES manager = new mPSOLICITUDES();
            
            return manager.GetSolicitudes(Dep_PSol);
        }
    }
}
