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
    public partial class CatSubcategory2 : BasePage
    {
        private void ApplyLayout()
        {
            xgrdSubcategoria2.BeginUpdate();
            try
            {
                xgrdSubcategoria2.ClearSort();
            }
            finally
            {
                xgrdSubcategoria2.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BSubCategoria2 = new SubCategoria2Da();
            var oListSubCategoria2 = BSubCategoria2.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdSubcategoria2.DataSource = oListSubCategoria2;
            xgrdSubcategoria2.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdSubcategoria2.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdSubcategoria2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdSubcategoria2_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var Subcategoria2ID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.DelSubCategoria2(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria2ID);
                if (res == 1)
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria2.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdSubcategoria2_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int Subcategoria2ID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();
            string cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbSubcategoria1Edit")).Value.ToString();
            try
            {
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.UpdSubCategoria2(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria2ID, Codigo, Nombre, CodigoMaquina, cmbSubcategoria1);
                if (res == 1)
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria2.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdSubcategoria2.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria2_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();
            string cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbSubcategoria1Edit")).Value.ToString();

            try
            {
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.InsSubCategoria2(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, CodigoMaquina, cmbSubcategoria1);
                if (res == 1)
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria2.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdSubcategoria2.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria2_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria2.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var Subcategoria2ID = 0;

            if (!e.IsNewRow)
                Subcategoria2ID = (int)e.Keys[0];
            try
            {
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.ValSubCategoria2(Subcategoria2ID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Subcategory2 with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdSubcategoria2_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("Subcategoria2ID");

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
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.DelSubCategoria2Selected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria2.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BSubCategoria2 = new SubCategoria2Da();
                var res = BSubCategoria2.DelSubCategoria2All(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria2.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void xgrdSubcategoria2_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            var BMaquina = new MaquinaDa();
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbMaquinaEdit"));
            cmbMaquina.TextField = "Nombre";
            cmbMaquina.ValueField = "Codigo";
            cmbMaquina.DataSource = BMaquina.GetCombo();
            cmbMaquina.DataBind();

            var BSubcategoria1 = new SubCategoria1Da();
            var cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbSubcategoria1Edit"));
            cmbSubcategoria1.TextField = "Nombre";
            cmbSubcategoria1.ValueField = "Codigo";
            cmbSubcategoria1.DataSource = BSubcategoria1.GetCombo();
            cmbSubcategoria1.DataBind();
        }

        protected void cmbMaquinaEdit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria2.FindEditFormTemplateControl("hdnCodigoMaquina")).Value;
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbMaquinaEdit"));
            ListEditItem oItem = cmbMaquina.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbMaquina.SelectedIndex = 0;
        }

        protected void cmbSubcategoria1Edit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria2.FindEditFormTemplateControl("hdnCodigoSubcategoria1")).Value;
            var cmbSubcategoria1 = ((ASPxComboBox)xgrdSubcategoria2.FindEditFormTemplateControl("cmbSubcategoria1Edit"));
            ListEditItem oItem = cmbSubcategoria1.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbSubcategoria1.SelectedIndex = 0;
        }
    }
}