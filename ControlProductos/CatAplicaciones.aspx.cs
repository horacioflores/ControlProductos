using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using DevExpress.Web;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Web.UI.WebControls;

namespace ControlProductos
{
    public partial class CatAplicaciones : BasePage
    {
        private void ApplyLayout()
        {
            xgrdAplicacion.BeginUpdate();
            try
            {
                xgrdAplicacion.ClearSort();
            }
            finally
            {
                xgrdAplicacion.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BAplicacion = new AplicacionDa();
            var oListAplicacion = BAplicacion.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdAplicacion.DataSource = oListAplicacion;
            xgrdAplicacion.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdAplicacion.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();

                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdAplicacion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdAplicacion_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var AplicacionId = int.Parse(e.Keys[0].ToString());

            try
            {
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.DelAplicacion(LoginInfo.CurrentUsuario.UsuarioId, AplicacionId);
                if (res == 1)
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdAplicacion.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdAplicacion_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var AplicacionId = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;
            var URL = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtURLEdit")).Text;

            try
            {
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.UpdAplicacion(LoginInfo.CurrentUsuario.UsuarioId, AplicacionId, Codigo, Descripcion, URL);
                if (res == 1)
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdAplicacion.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdAplicacion.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdAplicacion_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;
            var URL = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtURLEdit")).Text;

            try
            {
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.InsAplicacion(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Descripcion, URL);
                if (res == 1)
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdAplicacion.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdAplicacion.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdAplicacion_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdAplicacion.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text.Trim();

            var AplicacionId = 0;

            if (!e.IsNewRow)
                AplicacionId = (int)e.Keys[0];
            try
            {
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.ValAplicacion(AplicacionId, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "A Plant with the same key or description already exists!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdAplicacion_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("AppId");

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
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.DelAplicacionSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdAplicacion.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BAplicacion = new AplicacionDa();
                var res = BAplicacion.DelAplicacionAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdAplicacion.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdAplicacion.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}