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
    public partial class CatPosiciones : BasePage
    {
        private void ApplyLayout()
        {
            xgrdPosicion.BeginUpdate();
            try
            {
                xgrdPosicion.ClearSort();
            }
            finally
            {
                xgrdPosicion.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BPosicion = new PosicionDa();
            var oListPosicion = BPosicion.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdPosicion.DataSource = oListPosicion;
            xgrdPosicion.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdPosicion.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdPosicion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdPosicion_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var PosicionId = int.Parse(e.Keys[0].ToString());

            try
            {
                var BPosicion = new PosicionDa();
                var res = BPosicion.DelPosicion(LoginInfo.CurrentUsuario.UsuarioId, PosicionId);
                if (res == 1)
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPosicion.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdPosicion_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int PosicionId = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Descripcion = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtDescripcionEdit")).Text;
            int Nivel = int.Parse(((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtNivelEdit")).Text);

            try
            {
                var BPosicion = new PosicionDa();
                var res = BPosicion.UpdPosicion(LoginInfo.CurrentUsuario.UsuarioId, PosicionId, Codigo, Descripcion, Nivel);
                if (res == 1)
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPosicion.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdPosicion.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPosicion_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Descripcion = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtDescripcionEdit")).Text;
            int Nivel = int.Parse(((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtNivelEdit")).Text);

            try
            {
                var BPosicion = new PosicionDa();
                var res = BPosicion.InsPosicion(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Descripcion, Nivel);
                if (res == 1)
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPosicion.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdPosicion.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPosicion_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtCodigoEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdPosicion.FindEditFormTemplateControl("xtxtDescripcionEdit")).Text.Trim();

            var PosicionId = 0;

            if (!e.IsNewRow)
                PosicionId = (int)e.Keys[0];
            try
            {
                var BPosicion = new PosicionDa();
                var res = BPosicion.ValPosicion(PosicionId, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "A Position with the same key or description already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdPosicion_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PosicionId");

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
                var BPosicion = new PosicionDa();
                var res = BPosicion.DelPosicionSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPosicion.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BPosicion = new PosicionDa();
                var res = BPosicion.DelPosicionAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPosicion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPosicion.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}