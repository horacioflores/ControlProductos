using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlProductos.dataAccess;
using ControlProductos.Entity;

namespace ControlProductos
{
    public partial class CatGlider : BasePage
    {
        private void ApplyLayout()
        {
            xgrdPlaneador.BeginUpdate();
            try
            {
                xgrdPlaneador.ClearSort();
            }
            finally
            {
                xgrdPlaneador.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BPlaneador = new PlaneadorDa();
            var oListPlaneador = BPlaneador.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdPlaneador.DataSource = oListPlaneador;
            xgrdPlaneador.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdPlaneador.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdPlaneador_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdPlaneador_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var PlaneadorID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.DelPlaneador(LoginInfo.CurrentUsuario.UsuarioId, PlaneadorID);
                if (res == 1)
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlaneador.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdPlaneador_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int PlaneadorID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.UpdPlaneador(LoginInfo.CurrentUsuario.UsuarioId, PlaneadorID, Codigo, Nombre);
                if (res == 1)
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlaneador.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdPlaneador.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlaneador_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.InsPlaneador(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlaneador.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdPlaneador.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlaneador_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlaneador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var PlaneadorID = 0;

            if (!e.IsNewRow)
                PlaneadorID = (int)e.Keys[0];
            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.ValPlaneador(PlaneadorID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Glider with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdPlaneador_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PlaneadorID");

                e.Cell.Text = string.Format("<input type='checkbox' class='chk' id='chk{0}'>", id);
            }
        }

        protected void CallbackPanelDisable_Callback(object sender, CallbackEventArgsBase e)
        {
            var Valores = e.Parameter;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //Enviamos a la base de datos los Valores y desabilitamos con un update masivo.
            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.DelPlaneadorSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlaneador.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BPlaneador = new PlaneadorDa();
                var res = BPlaneador.DelPlaneadorAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlaneador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlaneador.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}