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
    public partial class CatMenu : BasePage
    {
        public bool AllowBound
        {
            get
            {
                return (bool)Session["AllowBound"];
            }
            set
            {
                Session["AllowBound"] = value;
            }
        }

        public int IdPerfile
        {
            get
            {
                if (Session["IdPerfileSA"] == null)
                    Session["IdPerfileSA"] = -1;
                return (int)Session["IdPerfileSA"];
            }
            set
            {
                Session["IdPerfileSA"] = value;
            }
        }

        private void ApplyLayout()
        {
            xgrdSA.BeginUpdate();
            try
            {
                xgrdSA.ClearSort();
            }
            finally
            {
                xgrdSA.EndUpdate();
            }
        }
        public void fillGrid()
        {
            ASPxComboBox cmbPerfile = ASPxNavBar2.Groups[0].FindControl("cmbPerfile") as ASPxComboBox;
            ASPxComboBox cmbApplication = ASPxNavBar2.Groups[0].FindControl("cmbApplication") as ASPxComboBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BSA = new MenuPerfilDa();
            if (cmbPerfile.SelectedIndex != -1)
                IdPerfile = int.Parse(cmbPerfile.SelectedItem.Value.ToString());
            else
            {
                IdPerfile = 1;
                cmbPerfile.SelectedIndex = 0;
            }

            //Este dato debe de salir del config
            int IdAplication = 1;
            if (cmbApplication.SelectedIndex != -1)
                IdAplication = int.Parse(cmbApplication.SelectedItem.Value.ToString());
            else
                cmbApplication.SelectedIndex = 0;

            var oListSA = BSA.GetMenuPerfil(IdPerfile, IdAplication, chkActive.Checked);
            xgrdSA.DataSource = oListSA;
            xgrdSA.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdSA.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                AllowBound = true;

                ASPxComboBox cmbPerfile = ASPxNavBar2.Groups[0].FindControl("cmbPerfile") as ASPxComboBox;
                ASPxComboBox cmbApplication = ASPxNavBar2.Groups[0].FindControl("cmbApplication") as ASPxComboBox;

                var BPerfile = new PerfilDa();
                cmbPerfile.TextField = "Codigo";
                cmbPerfile.ValueField = "PerfilId";
                cmbPerfile.DataSource = BPerfile.GetCombo();
                cmbPerfile.DataBind();

                var BApplication = new AplicacionDa();
                cmbApplication.TextField = "Codigo";
                cmbApplication.ValueField = "AppId";
                cmbApplication.DataSource = BApplication.GetCatalog("", "", true);
                cmbApplication.DataBind();

                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdSA_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdSA_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var PerfilId = int.Parse(e.Keys[0].ToString());
            var MenuId = int.Parse(e.Keys[1].ToString());

            try
            {
                var BSA = new MenuPerfilDa();
                var res = BSA.DelMenuPerfil(LoginInfo.CurrentUsuario.UsuarioId, PerfilId, MenuId);
                if (res == 1)
                    xgrdSA.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSA.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSA.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }
        protected void xgrdSA_RowInserting(object senedr, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //var IdApplication = int.Parse(((ASPxComboBox)xgrdSA.FindEditFormTemplateControl("cmbApplicationEdit")).SelectedItem.Value.ToString());
            //var IdParent = int.Parse(((ASPxComboBox)xgrdSA.FindEditFormTemplateControl("cmbParenEdit")).SelectedItem.Value.ToString());
            //var Description = ((ASPxTextBox)xgrdSA.FindEditFormTemplateControl("xTxtDescriptionEdit")).Text.Trim();
            //var URLImg = ((ASPxTextBox)xgrdSA.FindEditFormTemplateControl("xTxtDescriptionEdit")).Text.Trim();
            //var Key = ((ASPxTextBox)xgrdSA.FindEditFormTemplateControl("xTxtDescriptionEdit")).Text.Trim();
            //var URL = ((ASPxTextBox)xgrdSA.FindEditFormTemplateControl("xTxtURL")).Text.Trim();

            //try
            //{
            //    var BMenuPerfil = new Business.MenuPerfil();
            //    var res = BMenuPerfil.InsMenuPerfil(LoginInfo.CurrentUsuario.UsuarioId, IdApplication, IdParent, Description, URLImg, Key, URL);
            //    if (res == 1)
            //        xgrdSA.JSProperties["cpAlertMessage"] = "Insert";
            //    else
            //        xgrdSA.JSProperties["cpAlertMessage"] = "Error";
            //}
            //catch (Exception ex)
            //{
            //    xgrdSA.JSProperties["cpAlertMessage"] = ex.Message;
            //}
            //xgrdSA.CancelEdit();
            //e.Cancel = true;
        }

        protected void chkListArea_DataBound(object sender, EventArgs e)
        {
            if (AllowBound == true)
            {
                var MenuId = ((ASPxLabel)xgrdSA.FindEditFormTemplateControl("lblMenuId")).Text;
                var PerfilId = ((ASPxLabel)xgrdSA.FindEditFormTemplateControl("lblPerfilId")).Text;

                if (MenuId != string.Empty && PerfilId != string.Empty)
                {
                    var BAcces = new MenuPerfilDa();
                    var oListAccesosSelect = BAcces.GetAccesoMenuPerfil(int.Parse(PerfilId), int.Parse(MenuId));
                    SetcheckedAccesos(oListAccesosSelect);
                    AllowBound = false;
                }
            }
        }

        public void SetcheckedAccesos(List<MenuPerfil> oListAccesosSelect)
        {
            var oChkListArea = ((CheckBoxList)xgrdSA.FindEditFormTemplateControl("chkListArea"));
            foreach (MenuPerfil ItemAcceso in oListAccesosSelect)
            {
                oChkListArea.Items.FindByValue(ItemAcceso.AccionId.ToString()).Selected = true;
            }
        }

        protected void xgrdSA_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            var oChkListArea = ((CheckBoxList)xgrdSA.FindEditFormTemplateControl("chkListArea"));
            var BAcces = new MenuPerfilDa();
            oChkListArea.DataTextField = "Nombre";
            oChkListArea.DataValueField = "AccionId";
            oChkListArea.DataSource = BAcces.GetAccesoMenuPerfilAll();
            oChkListArea.DataBind();
        }

        protected void xgrdSA_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var MenuId = ((ASPxLabel)xgrdSA.FindEditFormTemplateControl("lblMenuId")).Text;
            var PerfilId = ((ASPxLabel)xgrdSA.FindEditFormTemplateControl("lblPerfilId")).Text;
            var oChkListArea = ((CheckBoxList)xgrdSA.FindEditFormTemplateControl("chkListArea"));

            int i;
            string Acciones;
            Acciones = string.Empty;
            for (i = 0; i <= (oChkListArea.Items.Count - 1); i++)
            {
                if (oChkListArea.Items[i].Selected == true)
                {
                    if (Acciones == string.Empty)
                        Acciones = Acciones + oChkListArea.Items[i].Value.ToString();
                    else
                        Acciones = Acciones + ", " + oChkListArea.Items[i].Value.ToString();
                }
            }

            try
            {
                var BMenuPerfil = new MenuPerfilDa();
                var res = BMenuPerfil.UpdMenuAccionPerfil(LoginInfo.CurrentUsuario.UsuarioId, int.Parse(PerfilId), int.Parse(MenuId), Acciones);
                if (res == 1)
                    xgrdSA.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdSA.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSA.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdSA.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdSA_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            AllowBound = true;
        }

        protected void xgrdSA_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PerfilId;MenuId");

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
                var BMenuPerfil = new MenuPerfilDa();
                var res = BMenuPerfil.DelMenuPerfilSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdSA.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSA.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSA.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            int PerfilId = 1;
            ASPxComboBox cmbPerfile = ASPxNavBar2.Groups[0].FindControl("cmbPerfile") as ASPxComboBox;
            if (cmbPerfile.SelectedIndex != -1)
            {
                PerfilId = int.Parse(cmbPerfile.SelectedItem.Value.ToString());
            }
            //Este dato debe de salir del config
            int AplicacionId = 1;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BMenuPerfil = new MenuPerfilDa();
                var res = BMenuPerfil.DelMenuPerfilAll(LoginInfo.CurrentUsuario.UsuarioId, AplicacionId, PerfilId, chkActive.Checked);
                if (res >= 1)
                    xgrdSA.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdSA.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdSA.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

    }
}