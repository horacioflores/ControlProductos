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
    public partial class CatSubcategory1 : BasePage
    {
        private void ApplyLayout()
        {
            xgrdSubcategoria1.BeginUpdate();
            try
            {
                xgrdSubcategoria1.ClearSort();
            }
            finally
            {
                xgrdSubcategoria1.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BSubCategoria1 = new SubCategoria1Da();
            var oListSubCategoria1 = BSubCategoria1.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdSubcategoria1.DataSource = oListSubCategoria1;
            xgrdSubcategoria1.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdSubcategoria1.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdSubcategoria1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdSubcategoria1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var Subcategoria1ID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.DelSubCategoria1(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria1ID);
                if (res == 1)
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria1.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdSubcategoria1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int Subcategoria1ID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria1.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();
            try
            {
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.UpdSubCategoria1(LoginInfo.CurrentUsuario.UsuarioId, Subcategoria1ID, Codigo, Nombre, CodigoMaquina);
                if (res == 1)
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria1.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdSubcategoria1.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string CodigoMaquina = ((ASPxComboBox)xgrdSubcategoria1.FindEditFormTemplateControl("cmbMaquinaEdit")).Value.ToString();

            try
            {
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.InsSubCategoria1(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, CodigoMaquina);
                if (res == 1)
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria1.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdSubcategoria1.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSubcategoria1_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdSubcategoria1.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var Subcategoria1ID = 0;

            if (!e.IsNewRow)
                Subcategoria1ID = (int)e.Keys[0];
            try
            {
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.ValSubCategoria1(Subcategoria1ID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Subcategory1 with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdSubcategoria1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("Subcategoria1ID");

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
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.DelSubCategoria1Selected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria1.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BSubCategoria1 = new SubCategoria1Da();
                var res = BSubCategoria1.DelSubCategoria1All(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSubcategoria1.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSubcategoria1.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void xgrdSubcategoria1_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            var BMaquina= new MaquinaDa();
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria1.FindEditFormTemplateControl("cmbMaquinaEdit"));
            cmbMaquina.TextField = "Nombre";
            cmbMaquina.ValueField = "Codigo";
            cmbMaquina.DataSource = BMaquina.GetCombo();
            cmbMaquina.DataBind();
        }

        protected void cmbMaquinaEdit_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdSubcategoria1.FindEditFormTemplateControl("hdnCodigoMaquina")).Value;
            var cmbMaquina = ((ASPxComboBox)xgrdSubcategoria1.FindEditFormTemplateControl("cmbMaquinaEdit"));
            ListEditItem oItem = cmbMaquina.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbMaquina.SelectedIndex = 0;
        }
    }
}