using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using GDocWord;
using Entidades;
using BLL.Vistas;
using BLL.rESTPREV;
using ByA;
using AutoMapper;


namespace BLL
{

    public class GenEstudioPrevio
    {
    public Entities ctx { get; set; }
    public ByARpt byaRpt { get; set; }

	public byte[] Doc_Doc { get; set; }
	public byte[] Doc_PDF { get; set; }

    public ESTPREV ep { get; set; }

    DataTable dtDatos = new DataTable();
    DataTable dtPlantilla = new DataTable();
    List<string> ListaNomTablas = new List<string>();
    List<DataTable> ListaTablas = new List<DataTable>();
    List<string> ListaGrupoNomTabla = new List<string>();
    List<DataTable> ListaGrupoTabla = new List<DataTable>();
    rFmtoTablas FormatoTablas = new rFmtoTablas();

    

    public byte[] imprimir(decimal id)
    {
        
        ep = new ESTPREV();
        rEstPrev mep = new rEstPrev();
        ep.ID = id;
        
        PPLANTILLAS p=null;
        List<PPLANTILLAS_CAMPOS> lpc=null;
        //List<EP_OBLIGACIONESC> lObC = null;
        
        List<rEP_OBLIGACIONES> lObC = null;
        List<rEP_OBLIGACIONES> lObE = null;
        List<rEP_ESPTEC> lrET = null;
        
        List<rEstPrev> lep = new List<rEstPrev>();
        List<rEP_CAP_JUR> lCapJur = null;
        
        string vCDP = "";
        using (ctx = new Entities())
        {
            //Abrir Estudio Previo    
            var q = ctx.ESTPREV.Where(t => t.ID == id).FirstOrDefault<ESTPREV>();
            if (q != null)
            {
                ep = q;
                //Buscar Plantilla de Estudio Previo, revisar id
                p = ctx.PPLANTILLAS.Find(37);
                
                

                
                FormatoTablas.lft = p.PPLANTILLAS_FORMATO_TABLAS.Select(t => new rPPLANTILLAS_FORMATO_TABLAS { 
                                                                         DES_CAM=t.DES_CAM,
                                                                         ANCHO=t.ANCHO,
                                                                         TIP_DAT= t.TIP_DAT,
                                                                         NOM_CAM= t.NOM_CAM,
                                                                          NTABLA =t.NTABLA
                                                                        }).ToList();
                
                //Cargar Lista de Campos
                lpc = ctx.PPLANTILLAS_CAMPOS.Where(t => t.IDE_PLA == 37 && t.EST_CAM=="AC").ToList();

                //OBLIGACIONES_C
                lObC = ep.EP_OBLIGACIONESC.Select(t => new rEP_OBLIGACIONES { DES_OBLIG = t.DES_OBLIG }).ToList();
                if (lObC.Count > 0)
                {
                    ListaTablas.Add(ByAUtil.convertToDataTable(lObC));
                    ListaNomTablas.Add("EP_OBLIGACIONESC");
                }

                lrET = ep.EP_ESPTEC.Select(t => new rEP_ESPTEC { CANT_ITEM = t.CANT_ITEM, DESC_ITEM = t.DESC_ITEM, UNI_ITEM = t.UNI_ITEM }).ToList();
                if (lrET.Count > 0)
                {
                    ListaTablas.Add(ByAUtil.convertToDataTable(lrET));
                    ListaNomTablas.Add("EP_ESPTEC");
                }
                
                lObE = ep.EP_OBLIGACIONESE.Select(t => new rEP_OBLIGACIONES { DES_OBLIG = t.DES_OBLIG }).ToList();
                if (lObE.Count > 0)
                {
                    ListaTablas.Add(ByAUtil.convertToDataTable(lObE));
                    ListaNomTablas.Add("EP_OBLIGACIONESE");
                }

                lCapJur = ep.EP_CAP_JUR.Select(t => new rEP_CAP_JUR { DES_CAPJ=t.DES_CAPJ  }).ToList();
                if (lCapJur.Count > 0)
                {
                    ListaTablas.Add(ByAUtil.convertToDataTable(lCapJur));
                    ListaNomTablas.Add("EP_CAPJUR");
                }
                                
                vCDP = buildCDP();

                
                Mapper.CreateMap<ESTPREV, rEstPrev>();
                Mapper.Map(this.ep, mep);
                mep.CDP = vCDP;
                mep.NOM_DIL_EP = buildNomTer(ep.TERCEROS4);
                mep.NOM_RES_EP = buildNomTer(ep.TERCEROS6);
                mep.NOM_APTE_EP = buildNomTer(ep.TERCEROS1);
                mep.NOM_DEP_NEC_EP = ep.DEPENDENCIA.NOM_DEP;
                mep.NOM_TIP_EP = ep.SUBTIPOS.TIPOSCONT.NOM_TIP + " " + ep.SUBTIPOS.NOM_STIP;

                mep.PLAZO_EP=        buildPlazo();

                try
                {
                    mep.NOM_CAR_RES_EP = ep.EP_CARGO3.DES_CARGO;
                }
                catch {
                    mep.NOM_CAR_RES_EP = "";
                }
                try
                {
                    mep.NOM_CAR_SUP_EP = ep.EP_CARGO2.DES_CARGO;
                }
                catch
                {
                    mep.NOM_CAR_SUP_EP = "";
                }
                try
                {
                    mep.NOM_DEP_SUP_EP = ep.DEPENDENCIA2.NOM_DEP;
                }
                catch
                {
                    mep.NOM_DEP_SUP_EP = "";
                }
                try
                {
                    mep.NOM_CAR_APTE_EP = ep.EP_CARGO.DES_CARGO;
                }
                catch
                {
                    mep.NOM_CAR_APTE_EP = "";
                }

                mep.POLIZAS = buildPoliza();
                
                lep.Add(mep);

                

                
                
            }
            else {
                return null;
            }

            DataTable datos = ByAUtil.convertToDataTable(lep);
            DataTable dtConf = ByAUtil.convertToDataTable(lpc);
            string msg = this.GenDocumento(p.PLANTILLA, datos, dtConf);
            //return this.Doc_Doc;
            return this.Doc_PDF;
            
        }

        
    }

    
    private string buildPlazo()
    {
        string plazo="";
        if (ep.TIPO_PLAZOS != null) {
            plazo = ep.PLAZ1_EP + " " + ep.TIPO_PLAZOS.NOM_PLA;
            if (ep.TIPO_PLAZOS1 != null)
            {
                plazo +=" " +ep.PLAZ2_EP + " " + ep.TIPO_PLAZOS1.NOM_PLA;;
            }
        }

        return plazo; 
    }

    public string  NumToLet(decimal num) {
            Numalet let = new Numalet();
            let.SeparadorDecimalSalida = "pesos y";
            let.ConvertirDecimales = true;
            //redondeando en cuatro decimales
            let.Decimales = 2;
            let.MascaraSalidaDecimal = "centavos";
            return let.ToCustomCardinal(num);
    }

    public string buildPoliza()
    {
        string temp = "";

        //Armar Cadena de Poliza
        List<stringD> lp = ep.EP_POLIZAS.Select(t => new stringD
        {
          Desc=t.POLIZAS.NOM_POL.ToUpper() + ": el valor de la garantía deberá ser por un monto equivalente al " +
                 t.POR_SMMLV + " " + t.TIPO + " del " + t.CALCULOPOL.DESCRIPCION + " '  y su vigencia será de " + t.VIGENCIA + "  días a partir de "
                 + t.CAL_VIG_POL.DESCRIPCION
        }).ToList();
        
      
        foreach (stringD p in lp) {
            temp += p.Desc;
        }
        return temp;
        
    }
    class stringD {
        public string Desc { get; set; }
    }
    private string buildNomTer(TERCEROS t)
    {
        if (t != null)
        {
            return t.NOM1_TER + " " + t.NOM2_TER + " " + t.APE1_TER + " " + t.APE2_TER;
        }
        return "";
    }

    private string buildCDP()
    {
        string vCDP = "";
        foreach (EP_CDP cdp in ep.EP_CDP)
        {
            string vRubros = "";
            foreach (EP_RUBROS_CDP rubro in cdp.EP_RUBROS_CDP)
            {
                string cadRub = rubro.COD_RUB + " denominado: " + rubro.RUBROS.DES_RUB;
                vRubros = String.IsNullOrEmpty(vRubros) ? cadRub : "," + cadRub;
            }
            string cadCDP = "Certificado de Disponilidad Presupuestal N° " + cdp.NRO_CDP + " de fecha " + cdp.FEC_CDP + " con un Valor " + cdp.VAL_CDP + " y Rubro(s) Presupuestal(es) " + vRubros;
            vCDP = String.IsNullOrEmpty(vCDP) ? cadCDP : "," + cadCDP;
        }
        return vCDP;
    }

	public string GenDocumento(byte[] DocPlantilla, DataTable datos, DataTable dtPlantillasCampos)
	{
        string Msg = "";
		//Generar el Documento Word
		if (dtPlantillasCampos.Rows.Count > 0) {
			dtDatos = datos;
			
			byte[] Documento = null;
			byte[] DocumentoPDF = null;
			GDWord oWord = new GDWord();

			if ((DocPlantilla != null)) {
				//If dtPlantilla.Rows(0)("EDITABLE").ToString = "1" Then
				//    oWord.DocProtegido = True
				//    'oWord.ClavePlantilla = Publico.Clave_Minuta
				//Else
				//    oWord.DocProtegido = False
				//End If
				oWord.IdPlantilla = "37";
				// ide de plantillas
				oWord.ListaNomTablas = ListaNomTablas;
				oWord.ListaTablas = ListaTablas;
                oWord.pfm = this.FormatoTablas;
				Documento = oWord.GenerarDocumento(DocPlantilla, dtPlantillasCampos, dtDatos);
				DocumentoPDF = oWord.Documento_Pdf;
				if (!oWord.lErrorG) {
					if ((Documento != null)) {
						Doc_Doc = oWord.Documento_Word;
						Doc_PDF = oWord.Documento_Pdf;
						Msg = "Se Generó el Documento N°";
					}
				}
			} else {
				Msg = "La plantilla no esta definida. Por favor verifique";
			}
		}
		return Msg;
	}
}
    class rEP_OBLIGACIONES {
        public string DES_OBLIG { get; set; }
    }

    class rEP_CAP_JUR {
        public string DES_CAPJ { get; set; }
    }

    public partial class rEP_ESPTEC
    {
        public string DESC_ITEM { get; set; }
        public Nullable<decimal> CANT_ITEM { get; set; }
        public string UNI_ITEM { get; set; }
    }

    public class rPPLANTILLAS_FORMATO_TABLAS
    {
        public string NTABLA { get; set; }
        public string NOM_CAM { get; set; }
        public string DES_CAM { get; set; }
        public string TIP_DAT { get; set; }
        public Nullable<decimal> ANCHO { get; set; }
    }

    public class rFmtoTablas : GDocWord.IFmtoColumn
    {
        public List<rPPLANTILLAS_FORMATO_TABLAS> lft;
        int IFmtoColumn.getAncho(string NomTabla, string NomCam)
        {
           return (int)lft.Where(t => t.NTABLA == NomTabla && t.NOM_CAM == NomCam).Select(t=> t.ANCHO).FirstOrDefault();
        }

        string IFmtoColumn.getTipoDato(string NomTabla, string NomCam)
        {
            return lft.Where(t => t.NTABLA == NomTabla && t.NOM_CAM == NomCam).Select(t => t.TIP_DAT).FirstOrDefault();
        }


        public int tieneConf(string NomTabla, string NomCam)
        {
            return (int)lft.Where(t => t.NTABLA == NomTabla && t.NOM_CAM == NomCam).Count();
        }


        public string getDescripcion(string NomTabla, string NomCam)
        {
            return lft.Where(t => t.NTABLA == NomTabla && t.NOM_CAM == NomCam).Select(t => t.DES_CAM).FirstOrDefault();
        }
    }


}
