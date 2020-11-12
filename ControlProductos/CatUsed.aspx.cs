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
    public partial class CatUsed : BasePage
    {
        private void ApplyLayout()
        {
            xgrdUtilizado.BeginUpdate();
            try
            {
                xgrdUtilizado.ClearSort();
            }
            finally
            {
                xgrdUtilizado.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BUtilizado = new UtilizadoDa();
            var oListPosicion = BUtilizado.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdUtilizado.DataSource = oListPosicion;
            xgrdUtilizado.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdUtilizado.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdUtilizado_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdUtilizado_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var UtilizadoID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.DelUtilizado(LoginInfo.CurrentUsuario.UsuarioId, UtilizadoID);
                if (res == 1)
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUtilizado.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdUtilizado_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int UtilizadoID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.UpdUtilizado(LoginInfo.CurrentUsuario.UsuarioId, UtilizadoID, Codigo, Nombre);
                if (res == 1)
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUtilizado.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdUtilizado.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdUtilizado_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.InsUtilizado(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUtilizado.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdUtilizado.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdUtilizado_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdUtilizado.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var UtilizadoID = 0;

            if (!e.IsNewRow)
                UtilizadoID = (int)e.Keys[0];
            try
            {
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.ValUtilizado(UtilizadoID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Machine with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdUtilizado_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("UtilizadoID");

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
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.DelUtilizadoSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUtilizado.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BUtilizado = new UtilizadoDa();
                var res = BUtilizado.DelUtilizadoAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUtilizado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUtilizado.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}