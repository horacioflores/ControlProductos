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
    public partial class CatBrands : BasePage
    {
        private void ApplyLayout()
        {
            xgrdMarca.BeginUpdate();
            try
            {
                xgrdMarca.ClearSort();
            }
            finally
            {
                xgrdMarca.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BMarca = new MarcaDa();
            var oListPosicion = BMarca.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdMarca.DataSource = oListPosicion;
            xgrdMarca.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdMarca.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdMarca_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdMarca_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var MarcaID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BMarca = new MarcaDa();
                var res = BMarca.DelMarca(LoginInfo.CurrentUsuario.UsuarioId, MarcaID);
                if (res == 1)
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMarca.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdMarca_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int MarcaID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            try
            {
                var BMarca = new MarcaDa();
                var res = BMarca.UpdMarca(LoginInfo.CurrentUsuario.UsuarioId, MarcaID, Codigo, Nombre);
                if (res == 1)
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMarca.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdMarca.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMarca_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            try
            {
                var BMarca = new MarcaDa();
                var res = BMarca.InsMarca(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre);
                if (res == 1)
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMarca.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdMarca.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMarca_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtCodigoEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdMarca.FindEditFormTemplateControl("xtxtNombreEdit")).Text;

            var MarcaID = 0;

            if (!e.IsNewRow)
                MarcaID = (int)e.Keys[0];
            try
            {
                var BMarca = new MarcaDa();
                var res = BMarca.ValMarca(MarcaID, Codigo, Nombre);
                if (res == 1)
                    e.RowError = "A Machine with the same key or name already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdMarca_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("MarcaID");

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
                var BMarca = new MarcaDa();
                var res = BMarca.DelMarcaSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMarca.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BMarca = new MarcaDa();
                var res = BMarca.DelMarcaAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMarca.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMarca.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}