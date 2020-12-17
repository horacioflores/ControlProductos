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
    public partial class CatTypeArticle : BasePage
    {
        private void ApplyLayout()
        {
            xgrdTipoArticulo.BeginUpdate();
            try
            {
                xgrdTipoArticulo.ClearSort();
            }
            finally
            {
                xgrdTipoArticulo.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BTipo = new TipoArticuloDa();
            var oListTipo = BTipo.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdTipoArticulo.DataSource = oListTipo;
            xgrdTipoArticulo.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdTipoArticulo.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdTipoArticulo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdTipoArticulo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var tipoArticuloID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BTipo = new TipoArticuloDa();
                var res = BTipo.DelTipoArticulo(LoginInfo.CurrentUsuario.UsuarioId, tipoArticuloID);
                if (res == 1)
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdTipoArticulo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int tipoArticuloID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtCodigoArticulo")).Text;
            string tipo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtTipoArticulo")).Text;
            string M = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtM")).Text;
            string N = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtN")).Text;
            string comentarios = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtComentarios")).Text;
            try
            {
                var BTipo = new TipoArticuloDa();
                var res = BTipo.UpdTipoArticulo(LoginInfo.CurrentUsuario.UsuarioId, tipoArticuloID, Codigo, tipo,M,N,comentarios);
                if (res == 1)
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdTipoArticulo.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdTipoArticulo_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtCodigoArticulo")).Text;
            string tipo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtTipoArticulo")).Text;
            string M = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtM")).Text;
            string N = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtN")).Text;
            string comentarios = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtComentarios")).Text;

            try
            {
                var BTipo = new TipoArticuloDa();
                var res = BTipo.InsTipoArticulo(LoginInfo.CurrentUsuario.UsuarioId, Codigo, tipo, M, N, comentarios);
                if (res == 1)
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdTipoArticulo.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdTipoArticulo_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtCodigoArticulo")).Text;
            string tipo = ((ASPxTextBox)xgrdTipoArticulo.FindEditFormTemplateControl("txtTipoArticulo")).Text;

            var tipoArticuloID = 0;

            if (!e.IsNewRow)
                tipoArticuloID = (int)e.Keys[0];
            try
            {
                var BTipo = new TipoArticuloDa();
                var res = BTipo.ValTipoArticulo(tipoArticuloID, Codigo, tipo);
                if (res == 1)
                    e.RowError = "A Type with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdTipoArticulo_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("tipoArticuloID");

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
                var BTipo = new TipoArticuloDa();
                var res = BTipo.DelTipoArticuloSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BTipo = new TipoArticuloDa();
                var res = BTipo.DelTipoArticuloAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}