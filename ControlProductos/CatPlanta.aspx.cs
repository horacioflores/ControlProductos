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
    public partial class CatPlanta : BasePage
    {
        private void ApplyLayout()
        {
            xgrdPlanta.BeginUpdate();
            try
            {
                xgrdPlanta.ClearSort();
            }
            finally
            {
                xgrdPlanta.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BPlanta = new PlantaDa();
            var oListPlanta = BPlanta.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdPlanta.DataSource = oListPlanta;
            xgrdPlanta.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdPlanta.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdPlanta_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdPlanta_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var IdPlanta = int.Parse(e.Keys[0].ToString());

            try
            {
                var BPlanta = new PlantaDa();
                var res = BPlanta.DelPlanta(LoginInfo.CurrentUsuario.UsuarioId, IdPlanta);
                if (res == 1)
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlanta.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdPlanta_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var DeptoId = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;
            var Direccion = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtDireccionEdit")).Text;

            try
            {
                var BPlanta = new PlantaDa();
                var res = BPlanta.UpdPlanta(LoginInfo.CurrentUsuario.UsuarioId, DeptoId, Codigo, Descripcion, Direccion);
                if (res == 1)
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlanta.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdPlanta.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlanta_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;
            var Direccion = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtDireccionEdit")).Text;

            try
            {
                var BPlanta = new PlantaDa();
                var res = BPlanta.InsPlanta(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Descripcion, Direccion);
                if (res == 1)
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlanta.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdPlanta.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPlanta_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdPlanta.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text.Trim();

            var IdPlanta = 0;

            if (!e.IsNewRow)
                IdPlanta = (int)e.Keys[0];
            try
            {
                var BPlanta = new PlantaDa();
                var res = BPlanta.ValPlanta(IdPlanta, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "A Plant with the same key or description already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdPlanta_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PlantaId");

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
                var BPlanta = new PlantaDa();
                var res = BPlanta.DelPlantaSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlanta.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BPlanta = new PlantaDa();
                var res = BPlanta.DelPlantaAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdPlanta.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdPlanta.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

    }
}