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
    public partial class CatMttoAlmn : BasePage
    {
        private void ApplyLayout()
        {
            xgrdMttoAlmn.BeginUpdate();
            try
            {
                xgrdMttoAlmn.ClearSort();
            }
            finally
            {
                xgrdMttoAlmn.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxComboBox cmbtipoBusc = ASPxNavBar2.Groups[0].FindControl("cmbtipoBusc") as ASPxComboBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var Bmtto = new MttoAlmnDa();
            var oListTipo = Bmtto.GetCatalog(xtxtCodigo.Text.Trim(), cmbtipoBusc.SelectedItem.Value.ToString().Trim(), chkActive.Checked);
            xgrdMttoAlmn.DataSource = oListTipo;
            xgrdMttoAlmn.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdMttoAlmn.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;

                List<Tipo> lTipo = new List<Tipo>();
                Tipo item0 = new Tipo();
                item0.value = "";
                item0.text = "All";
                Tipo item = new Tipo();
                item.value = "M";
                item.text = "Mantenimiento";
                Tipo item2 = new Tipo();
                item2.value = "A";
                item2.text = "Almacén";
                lTipo.Add(item0);
                lTipo.Add(item);
                lTipo.Add(item2);

                ASPxComboBox cmbtipoBusc = ASPxNavBar2.Groups[0].FindControl("cmbtipoBusc") as ASPxComboBox;
                cmbtipoBusc.TextField = "text";
                cmbtipoBusc.ValueField = "value";
                cmbtipoBusc.DataSource = lTipo;
                cmbtipoBusc.DataBind();

                ListEditItem oItem = cmbtipoBusc.Items.FindByValue("");
                if (oItem != null)
                    oItem.Selected = true;
                else
                    cmbtipoBusc.SelectedIndex = 0;

            }
            fillGrid();
        }

        protected void xgrdMttoAlmn_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdMttoAlmn_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var MttoAlmnID = int.Parse(e.Keys[0].ToString());

            try
            {
                var BMtto = new MttoAlmnDa();
                var res = BMtto.DelMttoAlmn(LoginInfo.CurrentUsuario.UsuarioId, MttoAlmnID);
                if (res == 1)
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMttoAlmn.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdMttoAlmn_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int MttoAlmnID = int.Parse(e.Keys[0].ToString());
            string Codigo = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtCodigoMtto")).Text;
            string especificacion = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtEspecificacion")).Text;
            string notas = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtNotas")).Text;
            string clasificacion = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtCasificacion")).Text;
            string responsable = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtResponsable")).Text;
            string tipo = ((ASPxComboBox)xgrdMttoAlmn.FindEditFormTemplateControl("cmbtipo")).SelectedItem.Value.ToString();
            
            try
            {
                var BMtto = new MttoAlmnDa();
                var res = BMtto.UpdMttoAlmn(LoginInfo.CurrentUsuario.UsuarioId, MttoAlmnID, Codigo, especificacion, notas, clasificacion, responsable,tipo);
                if (res == 1)
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMttoAlmn.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdMttoAlmn.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMttoAlmn_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtCodigoMtto")).Text;
            string especificacion = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtEspecificacion")).Text;
            string notas = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtNotas")).Text;
            string clasificacion = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtCasificacion")).Text;
            string responsable = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtResponsable")).Text;
            string tipo = ((ASPxComboBox)xgrdMttoAlmn.FindEditFormTemplateControl("cmbtipo")).SelectedItem.Value.ToString();

            try
            {
                var BMtto = new MttoAlmnDa();
                var res = BMtto.InsMttoAlmn(LoginInfo.CurrentUsuario.UsuarioId, Codigo, especificacion, notas, clasificacion, responsable, tipo);
                if (res == 1)
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMttoAlmn.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdMttoAlmn.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdMttoAlmn_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdMttoAlmn.FindEditFormTemplateControl("txtCodigoMtto")).Text;
            string tipo = ((ASPxComboBox)xgrdMttoAlmn.FindEditFormTemplateControl("cmbtipo")).SelectedItem.Value.ToString();

            var MttoAlmnID = 0;

            if (!e.IsNewRow)
                MttoAlmnID = (int)e.Keys[0];
            try
            {
                var BMtto = new MttoAlmnDa();
                var res = BMtto.ValMttoAlmn(MttoAlmnID, Codigo, tipo);
                if (res == 1)
                {
                    string opc = "";
                    if(tipo== "M")
                    {
                        opc = "Maintenance";
                    }
                    else
                    {
                        opc = "Warehouse";
                    }
                    e.RowError = "A "+ opc + " with the same key or name already exists!";
                }
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdMttoAlmn_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("MttoAlmnID");

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
                var Bmtto = new MttoAlmnDa();
                var res = Bmtto.DelMttoAlmnSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores);
                if (res >= 1)
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMttoAlmn.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var Bmtto = new MttoAlmnDa();
                var res = Bmtto.DelMttoAlmnAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdMttoAlmn.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdMttoAlmn.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void xgrdMttoAlmn_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            List<Tipo> lTipo = new List<Tipo>();
            Tipo item = new Tipo();
            item.value = "M";
            item.text = "Mantenimiento";
            Tipo item2 = new Tipo();
            item2.value = "A";
            item2.text = "Almacén";
            lTipo.Add(item);
            lTipo.Add(item2);
        
            ASPxComboBox cmbtipo = (ASPxComboBox)xgrdMttoAlmn.FindEditFormTemplateControl("cmbtipo");
            cmbtipo.TextField = "text";
            cmbtipo.ValueField = "value";
            cmbtipo.DataSource = lTipo;
            cmbtipo.DataBind();
        }

        protected void cmbtipo_DataBound(object sender, EventArgs e)
        {
            string Codigo = ((HtmlInputHidden)xgrdMttoAlmn.FindEditFormTemplateControl("hdnTipo")).Value;
            var cmbtipo = ((ASPxComboBox)xgrdMttoAlmn.FindEditFormTemplateControl("cmbtipo"));
            ListEditItem oItem = cmbtipo.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbtipo.SelectedIndex = 0;
        }
    }

    public class Tipo
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}