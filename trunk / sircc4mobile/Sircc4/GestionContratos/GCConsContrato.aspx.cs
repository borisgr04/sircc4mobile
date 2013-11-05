using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using Sircc4.Clases;
using BLL.Vistas;

namespace Sircc4.GestionContratos
{
    public partial class GCConsContrato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user=Usuario.UserName;
            HdUser.Value = user;
            if(user!="admin"){
                this.TxtIdeSup.Text =user ;
                this.TxtSupervisor.Text = GCConsContrato.GetTercerosPk(this.TxtIdeSup.Text);
            }
            
        }

        //Web Metodo (Estatico para llamar 
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetTercerosPk(string ide_ter)
        {
            GCConsContratosBL Gestcont = new GCConsContratosBL();
            vTerceros t = Gestcont.GetTercerosPk(ide_ter);
            return t == null ? "0" : t.NOMBRE;
            
        }

        protected void BtnCons_Click(object sender, EventArgs e)
        {
            GCConsContratosBL Gestcont = new GCConsContratosBL();
            this.MultiView1.ActiveViewIndex = 1;
            this.ListView1.DataBind();          
        }

        protected void LnBtFilt_Click(object sender, EventArgs e)
        {
            this.MultiView1.ActiveViewIndex = 0;
        }

        protected void ObjDtConsCont_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            BLL.Filtros.vContratosIntFiltro vc = new BLL.Filtros.vContratosIntFiltro();
            if (ChkTipo.Checked)
            {
                vc.Cod_Tip = this.cboTipo.SelectedValue;
            }
            if (ChkSTipo.Checked)
            {
                vc.Cod_STip = this.CboSubTipo.SelectedValue;
            }

            if (ChkVigencia.Checked) {
                vc.Vigencia = Convert.ToInt16(this.CmbVig.SelectedValue);    
            }
            if (ChkEstado.Checked)
            {
                vc.Estado = CmbEst.SelectedValue;
            }
            vc.FilxFS = ChkFecha.Checked;
            if (vc.FilxFS)
            {
                vc.FS_Inicial = Convert.ToDateTime(this.TxtFecIni.Text);
                vc.FS_Final = Convert.ToDateTime(this.TxtFecFin.Text);
            }
            if (this.ChkNContrato.Checked)
            {
                vc.Numero = TxtNroCto.Text;
            }
            if (ChkContratista.Checked)
            {
                vc.Ide_Contratista = this.TxtIdeCon.Text;
            }
            if (this.ChkSup.Checked)
            {
                vc.Ide_Interventor = this.TxtIdeSup.Text;
            }
            if (this.ChkDepNec.Checked)
            {
                vc.Dep_Nec = CmbDep.SelectedValue;
            }
            if (this.ChkObj.Checked)
            {
                vc.Objeto = this.TxtObjCont.Text;
            }
            e.InputParameters["cFil"] = vc;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            EstudiosPreviosBL ep = new EstudiosPreviosBL();
            string ent = HdTipo.Value;
            if (ent.Equals("Terceros"))
            {
                grdBusqueda.DataSourceID = odsTerceros.ID;
            }
            grdBusqueda.DataBind();
            modalPopup1.Show();
        }

        protected void grdBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["OnClick"] = "return GetSelectedRowC(this);";
            }
        }

      

        protected void grdBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

       
        
    }
}