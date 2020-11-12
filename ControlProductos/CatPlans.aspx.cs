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
    public partial class CatPlans : BasePage
    {
        private void ApplyLayout()
        {
            xgrdPlan.BeginUpdate();
            try
            {
                xgrdPlan.ClearSort();
            }
            finally
            {
                xgrdPlan.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BPlan = new PlanDa();
            var oListPosicion = BPlan.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdPlan.DataSource = oListPosicion;
            xgrdPlan.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdPlan.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdPlan_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdPlan_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var PlanID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BPlan = new PlanDa();
                var res = BPlan.DelPlan(LoginInfo.CurrentUsuario.UsuarioId, PlanID);
                if (res == 1)
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlan.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdPlan_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int PlanID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BPlan = new PlanDa();
                var res = BPlan.UpdPlan(LoginInfo.CurrentUsuario.UsuarioId, PlanID, Codigo, Nombre);
                if (res == 1)
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlan.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdPlan.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlan_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BPlan = new PlanDa();
                var res = BPlan.InsPlan(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlan.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdPlan.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlan_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdPlan.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var PlanID = 0;

            if (!e.IsNewRow)
                PlanID = (int)e.Keys[0];
            try
            {
                var BPlan = new PlanDa();
                var res = BPlan.ValPlan(PlanID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Machine with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdPlan_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PlanID");

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
                var BPlan = new PlanDa();
                var res = BPlan.DelPlanSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlan.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BPlan = new PlanDa();
                var res = BPlan.DelPlanAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlan.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlan.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}