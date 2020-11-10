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
    public partial class CatBuyers : BasePage
    {
        private void ApplyLayout()
        {
            xgrdComprador.BeginUpdate();
            try
            {
                xgrdComprador.ClearSort();
            }
            finally
            {
                xgrdComprador.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BComprador = new CompradorDa();
            var oListPosicion = BComprador.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdComprador.DataSource = oListPosicion;
            xgrdComprador.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdComprador.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdComprador_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdComprador_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var CompradorID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BComprador = new CompradorDa();
                var res = BComprador.DelComprador(LoginInfo.CurrentUsuario.UsuarioId, CompradorID);
                if (res == 1)
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdComprador.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdComprador_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int CompradorID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string Telefono = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtTelefonoEdit")).Text;
            string Email = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtEmailEdit")).Text;

            try
            {
                var BComprador = new CompradorDa();
                var res = BComprador.UpdComprador(LoginInfo.CurrentUsuario.UsuarioId, CompradorID, Codigo, Nombre, Telefono, Email);
                if (res == 1)
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdComprador.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdComprador.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdComprador_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string Telefono = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtTelefonoEdit")).Text;
            string Email = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtEmailEdit")).Text;

            try
            {
                var BComprador = new CompradorDa();
                var res = BComprador.InsComprador(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, Telefono, Email);
                if (res == 1)
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdComprador.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdComprador.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdComprador_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdComprador.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var CompradorId = 0;

            if (!e.IsNewRow)
                CompradorId = (int)e.Keys[0];
            try
            {
                var BComprador = new CompradorDa();
                var res = BComprador.ValComprador(CompradorId, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Buyer with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdComprador_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("CompradorID");

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
                var BComprador = new CompradorDa();
                var res = BComprador.DelCompradorSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdComprador.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BComprador = new CompradorDa();
                var res = BComprador.DelCompradorAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdComprador.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdComprador.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}