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
    public partial class CatMachines : BasePage
    {
        private void ApplyLayout()
        {
            xgrdMaquina.BeginUpdate();
            try
            {
                xgrdMaquina.ClearSort();
            }
            finally
            {
                xgrdMaquina.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BMaquina = new MaquinaDa();
            var oListPosicion = BMaquina.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdMaquina.DataSource = oListPosicion;
            xgrdMaquina.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdMaquina.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdMaquina_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdMaquina_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var MaquinaID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BMaquina = new MaquinaDa();
                var res = BMaquina.DelMaquina(LoginInfo.CurrentUsuario.UsuarioId, MaquinaID);
                if (res == 1)
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMaquina.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdMaquina_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int MaquinaID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BMaquina = new MaquinaDa();
                var res = BMaquina.UpdMaquina(LoginInfo.CurrentUsuario.UsuarioId, MaquinaID, Codigo, Nombre);
                if (res == 1)
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMaquina.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdMaquina.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMaquina_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BMaquina = new MaquinaDa();
                var res = BMaquina.InsMaquina(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMaquina.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdMaquina.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMaquina_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMaquina.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var MaquinaID = 0;

            if (!e.IsNewRow)
                MaquinaID = (int)e.Keys[0];
            try
            {
                var BMaquina = new MaquinaDa();
                var res = BMaquina.ValMaquina(MaquinaID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Machine with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdMaquina_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("MaquinaID");

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
                var BMaquina = new MaquinaDa();
                var res = BMaquina.DelMaquinaSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMaquina.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BMaquina = new MaquinaDa();
                var res = BMaquina.DelMaquinaAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMaquina.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMaquina.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}