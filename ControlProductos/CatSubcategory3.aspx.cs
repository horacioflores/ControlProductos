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

namespace ControlProductos
{
    public partial class CatSubcategory3 : BasePage
    {
        private void ApplyLayout()
        {
            xgrdSubcategoria3.BeginUpdate();
            try
            {
                xgrdSubcategoria3.ClearSort();
            }
            finally
            {
                xgrdSubcategoria3.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BSubCategoria3 = new SubCategoria3Da();
            var oListSubCategoria3 = BSubCategoria3.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdSubcategoria3.DataSource = oListSubCategoria3;
            xgrdSubcategoria3.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdSubcategoria3.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdSubcategoria3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdSubcategoria3_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var Subcategoria3ID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.DelSubCategoria3(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria3ID);
                if (res == 1)
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria3.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdSubcategoria3_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int Subcategoria3ID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();
            string cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria1Edit")).Value.ToString();
            string cmbSubcategoria2 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria2Edit")).Value.ToString();
            try
            {
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.UpdSubCategoria3(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria3ID, Codigo, Nombre, CodigoMaquina, cmbSubcategoria1, cmbSubcategoria2);
                if (res == 1)
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria3.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdSubcategoria3.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria3_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();
            string cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria1Edit")).Value.ToString();
            string cmbSubcategoria2 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria2Edit")).Value.ToString();

            try
            {
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.InsSubCategoria3(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, CodigoMaquina, cmbSubcategoria1, cmbSubcategoria2);
                if (res == 1)
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria3.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdSubcategoria3.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria3_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria3.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var Subcategoria3ID = 0;

            if (!e.IsNewRow)
                Subcategoria3ID = (int)e.Keys[0];
            try
            {
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.ValSubCategoria3(Subcategoria3ID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Subcategory3 with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdSubcategoria3_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("Subcategoria3ID");

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
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.DelSubCategoria3Selected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria3.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BSubCategoria3 = new SubCategoria3Da();
                var res = BSubCategoria3.DelSubCategoria3All(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria3.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria3.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void xgrdSubcategoria3_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            var BMaquina = new MaquinaDa();
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbMaquinaEdit"));
            cmbMaquina.TextField = "Nombre";
            cmbMaquina.ValueField = "Codigo";
            cmbMaquina.DataSource = BMaquina.GetCombo();
            cmbMaquina.DataBind();

            var BSubcategoria1 = new SubCategoria1Da();
            var cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria1Edit"));
            cmbSubcategoria1.TextField = "Nombre";
            cmbSubcategoria1.ValueField = "Codigo";
            cmbSubcategoria1.DataSource = BSubcategoria1.GetCombo();
            cmbSubcategoria1.DataBind();

            var BSubcategoria2 = new SubCategoria2Da();
            var cmbSubcategoria2 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria2Edit"));
            cmbSubcategoria2.TextField = "Nombre";
            cmbSubcategoria2.ValueField = "Codigo";
            cmbSubcategoria2.DataSource = BSubcategoria2.GetCombo();
            cmbSubcategoria2.DataBind();
        }

        protected void cmbMaquinaEdit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria3.FindEditFormTemplateControl("hdnCodigoMaquina")).Value;
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbMaquinaEdit"));
            ListEditItem oItem = cmbMaquina.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbMaquina.SelectedIndex = 0;
        }

        protected void cmbSubcategoria1Edit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria3.FindEditFormTemplateControl("hdnCodigoSubcategoria1")).Value;
            var cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria1Edit"));
            ListEditItem oItem = cmbSubcategoria1.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbSubcategoria1.SelectedIndex = 0;
        }

        protected void cmbSubcategoria2Edit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria3.FindEditFormTemplateControl("hdnCodigoSubcategoria2")).Value;
            var cmbSubcategoria2 = ((ASPxComboBox)xgrdSubcategoria3.FindEditFormTemplateControl("cmbSubcategoria2Edit"));
            ListEditItem oItem = cmbSubcategoria2.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbSubcategoria2.SelectedIndex = 0;
        }
    }
}