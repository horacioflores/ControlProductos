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
    public partial class CatMonedas : BasePage
    {
        private void ApplyLayout()
        {
            xgrdMoneda.BeginUpdate();
            try
            {
                xgrdMoneda.ClearSort();
            }
            finally
            {
                xgrdMoneda.EndUpdate();
            }
        }
        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BMoneda = new MonedaDa();
            var oListMoneda = BMoneda.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdMoneda.DataSource = oListMoneda;
            xgrdMoneda.DataBind();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdMoneda.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }
        protected void xgrdMoneda_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }
        protected void xgrdMoneda_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var IdMoneda = int.Parse(e.Keys[0].ToString());

            try
            {
                var BMoneda = new MonedaDa();
                var res = BMoneda.DelMoneda(LoginInfo.CurrentUsuario.UsuarioId, IdMoneda);
                if (res == 1)
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMoneda.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }
        protected void xgrdMoneda_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var IdMoneda = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Nombre = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            var Simbolo = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtSimboloEdit")).Text;
            var precs = Convert.ToDecimal(((ASPxSpinEdit)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtPresicion")).Text);
            var sep_millar = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtSepMillar")).Text;
            var sep_decimal = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtSepDecimal")).Text;

            try
            {
                var BMoneda = new MonedaDa();
                var res = BMoneda.UpdMoneda(LoginInfo.CurrentUsuario.UsuarioId, IdMoneda, Codigo, Nombre, Simbolo, precs, sep_millar, sep_decimal);
                if (res == 1)
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMoneda.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdMoneda.CancelEdit();
            e.Cancel = true;
        }
        protected void xgrdMoneda_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Nombre = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            var Simbolo = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtSimboloEdit")).Text;
            var precs = Convert.ToDecimal(((ASPxSpinEdit)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtPresicion")).Text);
            var sep_millar = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtSepMillar")).Text;
            var sep_decimal = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("ASPxtxtSepDecimal")).Text;

            try
            {
                var BMoneda = new MonedaDa();
                var res = BMoneda.InsMoneda(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, Simbolo, precs, sep_millar, sep_decimal);
                if (res == 1)
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMoneda.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdMoneda.CancelEdit();
            e.Cancel = true;
        }
        protected void xgrdMoneda_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdMoneda.FindEditFormTemplateControl("xtxtNombreEdit")).Text.Trim();

            var IdMoneda = 0;

            if (!e.IsNewRow)
                IdMoneda = (int)e.Keys[0];
            try
            {
                var BMoneda = new MonedaDa();
                var res = BMoneda.ValMoneda(IdMoneda, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "A Plant with the same key or description already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }
        protected void xgrdMoneda_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("MonedaID");

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
                var BMoneda = new MonedaDa();
                var res = BMoneda.DelMonedaSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMoneda.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }
        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BMoneda = new MonedaDa();
                var res = BMoneda.DelMonedaAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMoneda.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMoneda.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}