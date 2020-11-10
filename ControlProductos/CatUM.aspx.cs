using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using System.Web.UI.HtmlControls;
using System.Data;
using DevExpress.Spreadsheet;
using System.IO;
using DevExpress.XtraPrinting;

namespace ControlProductos
{
    public partial class CatUM : BasePage
    {
        private void ApplyLayout()
        {
            xgrdUmedida.BeginUpdate();
            try
            {
                xgrdUmedida.ClearSort();
            }
            finally
            {
                xgrdUmedida.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BUnidad = new UMedidaDa();
            var oListUnidad = BUnidad.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdUmedida.DataSource = oListUnidad;
            xgrdUmedida.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdUmedida.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdUmedida_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdUmedida_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var IdUm = int.Parse(e.Keys[0].ToString());

            try
            {
                var BUnidad = new UMedidaDa();
                var res = BUnidad.DelUMedida(LoginInfo.CurrentUsuario.UsuarioId, IdUm);
                if (res == 1)
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUmedida.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }
        protected void xgrdUmedida_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var IdUm = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Nombre = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BUnidad = new UMedidaDa();
                var res = BUnidad.UpdUMedida(LoginInfo.CurrentUsuario.UsuarioId, IdUm, Codigo, Nombre);
                if (res == 1)
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUmedida.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdUmedida.CancelEdit();
            e.Cancel = true;
        }
        protected void xgrdUmedida_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Nombre = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BUnidad = new UMedidaDa();
                var res = BUnidad.InsUMedida(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUmedida.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdUmedida.CancelEdit();
            e.Cancel = true;
        }
        protected void xgrdUmedida_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdUmedida.FindEditFormTemplateControl("xtxtNombreEdit")).Text.Trim();

            var IdUm = 0;

            if (!e.IsNewRow)
                IdUm = (int)e.Keys[0];
            try
            {
                var BUnidad = new UMedidaDa();
                var res = BUnidad.ValUMedida(IdUm, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "A Plant with the same key or description already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }
        protected void xgrdUmedida_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("UMedidaID");

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
                var BUnidad = new UMedidaDa();
                var res = BUnidad.DelUMedidaSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUmedida.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BUnidad = new UMedidaDa();
                var res = BUnidad.DelUMedidaAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdUmedida.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdUmedida.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}