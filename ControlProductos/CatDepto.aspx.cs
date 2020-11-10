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
    public partial class CatDepto : BasePage
    {
        private void ApplyLayout()
        {
            xgrdDepartamento.BeginUpdate();
            try
            {
                xgrdDepartamento.ClearSort();
            }
            finally
            {
                xgrdDepartamento.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BDepartamento = new DepartamentoDa();
            var oListDepartamento = BDepartamento.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdDepartamento.DataSource = oListDepartamento;
            xgrdDepartamento.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdDepartamento.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdDepartamento_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdDepartamento_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var IdDepartamento = int.Parse(e.Keys[0].ToString());

            try
            {
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.DelDepartamento(LoginInfo.CurrentUsuario.UsuarioId, IdDepartamento);
                if (res == 1)
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdDepartamento.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdDepartamento_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var DeptoId = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;

            try
            {
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.UpdDepartamento(LoginInfo.CurrentUsuario.UsuarioId, DeptoId, Codigo, Descripcion);
                if (res == 1)
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdDepartamento.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdDepartamento.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdDepartamento_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;

            try
            {
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.InsDepartamento(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Descripcion);
                if (res == 1)
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdDepartamento.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdDepartamento.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdDepartamento_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdDepartamento.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text.Trim();

            var IdDepartamento = 0;

            if (!e.IsNewRow)
            {
                IdDepartamento = (int)e.Keys[0];
            }
            try
            {
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.ValDepartamento(IdDepartamento, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "Ya existe un Departamentoo con la misma clave o descripción!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdDepartamento_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("DeptoId");

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
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.DelDepartamentoSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdDepartamento.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BDepartamento = new DepartamentoDa();
                var res = BDepartamento.DelDepartamentoAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdDepartamento.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdDepartamento.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}