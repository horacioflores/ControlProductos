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
    public partial class CatCounts : BasePage
    {
        private void ApplyLayout()
        {
            xgrdConteo.BeginUpdate();
            try
            {
                xgrdConteo.ClearSort();
            }
            finally
            {
                xgrdConteo.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BConteo = new ConteoDa();
            var oListPosicion = BConteo.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdConteo.DataSource = oListPosicion;
            xgrdConteo.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdConteo.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdConteo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdConteo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var ConteoID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BConteo = new ConteoDa();
                var res = BConteo.DelConteo(LoginInfo.CurrentUsuario.UsuarioId, ConteoID);
                if (res == 1)
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdConteo.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdConteo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ConteoID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BConteo = new ConteoDa();
                var res = BConteo.UpdConteo(LoginInfo.CurrentUsuario.UsuarioId, ConteoID, Codigo, Nombre);
                if (res == 1)
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdConteo.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdConteo.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdConteo_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BConteo = new ConteoDa();
                var res = BConteo.InsConteo(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdConteo.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdConteo.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdConteo_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdConteo.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var ConteoID = 0;

            if (!e.IsNewRow)
                ConteoID = (int)e.Keys[0];
            try
            {
                var BConteo = new ConteoDa();
                var res = BConteo.ValConteo(ConteoID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Machine with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdConteo_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("ConteoID");

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
                var BConteo = new ConteoDa();
                var res = BConteo.DelConteoSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdConteo.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BConteo = new ConteoDa();
                var res = BConteo.DelConteoAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdConteo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdConteo.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}