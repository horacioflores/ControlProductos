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
    public partial class CatUsuarios : BasePage
    {
        private void ApplyLayout()
        {
            xgrdUser.BeginUpdate();
            try
            {
                xgrdUser.ClearSort();
            }
            finally
            {
                xgrdUser.EndUpdate();
            }
        }
        public void fillGrid()
        {
            ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
            ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
            ASPxTextBox xtxtUsuario = ASPxNavBar2.Groups[0].FindControl("xtxtUsuario") as ASPxTextBox;
            ASPxTextBox xtxtNombreCompleto = ASPxNavBar2.Groups[0].FindControl("xtxtNombreCompleto") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            string Posicion = "";
            string Proveedor = "";
            if (cmbPosicion.SelectedIndex != -1)
            {
                Posicion = cmbPosicion.Value.ToString();
                if (cmbProveedor.SelectedIndex > 0)
                    Proveedor = cmbProveedor.Value.ToString();
            }
            var BUser = new UsuarioDa();
            var oListUser = BUser.GetCatalog(Posicion, Proveedor, xtxtUsuario.Text.Trim(), xtxtNombreCompleto.Text.Trim(), chkActive.Checked);
            xgrdUser.DataSource = oListUser;
            xgrdUser.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdUser.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;

                var BPosicion = new PosicionDa();
                var cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
                cmbPosicion.TextField = "Descripcion";
                cmbPosicion.ValueField = "Codigo";
                cmbPosicion.DataSource = BPosicion.GetCombo(LoginInfo.CurrentPerfil.EsAdministrador);
                cmbPosicion.DataBind();
                ListEditItem oItem = cmbPosicion.Items.FindByValue(LoginInfo.CurrentUsuario.CodigoPosicion);
                if (oItem != null)
                    oItem.Selected = true;
                else
                    cmbPosicion.SelectedIndex = 0;
                cmbPosicion.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");

                var BProveedor = new ProveedorDa();
                List<Entity.Proveedor> ProveedorList = new List<Entity.Proveedor>();
                Entity.Proveedor ProvTodos = new Entity.Proveedor { Codigo = "", Nombre = "Todos" };
                ProveedorList.Add(ProvTodos);
                ProveedorList.AddRange(BProveedor.GetCombo());
                var cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
                cmbProveedor.TextField = "Nombre";
                cmbProveedor.ValueField = "Codigo";
                cmbProveedor.DataSource = ProveedorList;
                cmbProveedor.DataBind();
                if (LoginInfo.CurrentUsuario.CodigoPosicion == "01" && LoginInfo.CurrentUsuario.CodigoProveedor == "NA")
                    oItem = cmbProveedor.Items.FindByValue("");
                else
                    oItem = cmbProveedor.Items.FindByValue(LoginInfo.CurrentUsuario.CodigoProveedor);
                if (oItem != null)
                    oItem.Selected = true;
                cmbProveedor.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
            }
            fillGrid();
        }

        protected void xgrdUser_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var IdUser = int.Parse(e.Keys[0].ToString());
            var Usuario = ((ASPxTextBox)xgrdUser.FindEditFormTemplateControl("xtxtUserName")).Text;
            var EmpleadoId = int.Parse(((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbEmpleadoEdit")).Value.ToString());
            var CodigoPerfil = ((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbCodigoPerfilEdit")).Value.ToString();

            try
            {
                var BUser = new UsuarioDa();

                var res = BUser.UpdUsuario(LoginInfo.CurrentUsuario.UsuarioId, IdUser, Usuario, EmpleadoId, CodigoPerfil);
                if (res == 1)
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Update";
                }
                else
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdUser.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdUser.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdUser_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string sEncryptionKey = "r0b1nr0y";
            var Usuario = ((ASPxTextBox)xgrdUser.FindEditFormTemplateControl("xtxtUserName")).Text;
            var Contrasena = ((ASPxTextBox)xgrdUser.FindEditFormTemplateControl("xtxtPassword")).Text;
            var EmpleadoId = int.Parse(((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbEmpleadoEdit")).Value.ToString());
            var CodigoPerfil = ((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbCodigoPerfilEdit")).Value.ToString();

            try
            {
                var BUser = new UsuarioDa();
                var res = BUser.InsUsuario(LoginInfo.CurrentUsuario.UsuarioId, Usuario, Contrasena, EmpleadoId, CodigoPerfil);
                if (res == 1)
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Insert";
                }
                else
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdUser.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdUser.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdUser_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var IdUser = int.Parse(e.Keys[0].ToString());

            try
            {
                var BUser = new UsuarioDa();
                var res = BUser.DelUsuario(LoginInfo.CurrentUsuario.UsuarioId, IdUser);
                if (res == 1)
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdUser.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdUser_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdUser_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
            ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            string Posicion = "";
            string Proveedor = "";
            if (cmbPosicion.SelectedIndex != -1)
            {
                Posicion = cmbPosicion.Value.ToString();
                if (cmbProveedor.SelectedIndex > 0)
                    Proveedor = cmbProveedor.Value.ToString();
            }

            var BPerfil = new PerfilDa();
            var cmbCodigoPerfilEdit = ((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbCodigoPerfilEdit"));
            cmbCodigoPerfilEdit.TextField = "Descripcion";
            cmbCodigoPerfilEdit.ValueField = "Codigo";
            cmbCodigoPerfilEdit.DataSource = BPerfil.GetCombo();
            cmbCodigoPerfilEdit.DataBind();

            var BEmpleados = new EmpleadosDa();
            var cmbEmpleadoEdit = ((ASPxComboBox)xgrdUser.FindEditFormTemplateControl("cmbEmpleadoEdit"));
            cmbEmpleadoEdit.TextField = "NombreCompleto";
            cmbEmpleadoEdit.ValueField = "EmpleadoId";
            cmbEmpleadoEdit.DataSource = BEmpleados.GetCatalog(Posicion, Proveedor, "", "", true);
            cmbEmpleadoEdit.DataBind();
        }

        protected void cmbCodigoPerfilEdit_DataBound(object sender, EventArgs e)
        {
            ASPxComboBox cmbPerfilEdit = ((ASPxComboBox)sender);
            string Codigo = "NA";
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "01")
            {
                Codigo = ((HtmlInputHidden)xgrdUser.FindEditFormTemplateControl("hdnCodigoPerfil")).Value;
                if ((Codigo == string.Empty) || (Codigo == "0") || (Codigo == "NA"))
                    Codigo = "NA";
            }
            else
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "02")
                Codigo = "Contratist";
            else
                Codigo = "Visitante";
            ListEditItem oItem = cmbPerfilEdit.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbPerfilEdit.SelectedIndex = 0;
            cmbPerfilEdit.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
        }

        protected void cmbEmpleadoEdit_DataBound(object sender, EventArgs e)
        {
            var cmbEmpleadoEdit = ((ASPxComboBox)sender);
            var EmpleadoId = ((HtmlInputHidden)xgrdUser.FindEditFormTemplateControl("hdnEmpleadoId")).Value;
            if ((EmpleadoId == string.Empty) || (EmpleadoId == "0"))
                cmbEmpleadoEdit.SelectedIndex = -1;
            else
                cmbEmpleadoEdit.Items.FindByValue(EmpleadoId).Selected = true;
            var bIsNew = (((ASPxTextBox)xgrdUser.FindEditFormTemplateControl("xtxtUserName")).Text == string.Empty);
            if (!bIsNew)
            {
                //((HtmlTableRow)xgrdUser.FindEditFormTemplateControl("trlblPassword")).Visible = false;
                //((HtmlTableRow)xgrdUser.FindEditFormTemplateControl("trtxtPassword")).Visible = false;
                //((HtmlTableRow)xgrdUser.FindEditFormTemplateControl("trlblConfirm")).Visible = false;
                //((HtmlTableRow)xgrdUser.FindEditFormTemplateControl("trtxtConfirm")).Visible = false;
                ((HtmlGenericControl)xgrdUser.FindEditFormTemplateControl("divPassword")).Visible = false;
                ((HtmlGenericControl)xgrdUser.FindEditFormTemplateControl("divConfirm")).Visible = false;
            }
        }

        protected void xgrdUser_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("UsuarioId");

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
                var BUsuario = new UsuarioDa();
                var res = BUsuario.DelUsuarioSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdUser.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            //desabilitamos o habilitamos con un update masivo.
            try
            {
                ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
                ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
                ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;
                string Posicion = "";
                string Proveedor = "";
                if (cmbPosicion.SelectedIndex != -1)
                {
                    Posicion = cmbPosicion.Value.ToString();
                    if (cmbProveedor.SelectedIndex > 0)
                        Proveedor = cmbProveedor.Value.ToString();
                }

                var BUsuario = new UsuarioDa();
                var res = BUsuario.DelUsuarioAll(LoginInfo.CurrentUsuario.UsuarioId, Posicion, Proveedor, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdUser.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdUser.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}