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
    public partial class CatEmpleados : BasePage
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

        public List<Entity.Perfil_Apps> PerfilApps
        {
            get
            {
                return (List<Entity.Perfil_Apps>)Session["CurrentPerfilApps"];
            }
            set
            {
                Session["CurrentPerfilApps"] = value;
            }
        }

        public string sPath
        {
            get { return (string)Session["sPath"]; }
            set { Session["sPath"] = value; }
        }

        private void ApplyLayout()
        {
            xgrdEmpleado.BeginUpdate();
            try
            {
                xgrdEmpleado.ClearSort();
            }
            finally
            {
                xgrdEmpleado.EndUpdate();
            }
        }

        public void fillGrid()
        {

            ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
            ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
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
            var BEmployee = new EmpleadosDa();
            var oListUser = BEmployee.GetCatalog(Posicion, Proveedor, xtxtCodigo.Text.Trim(), xtxtNombreCompleto.Text.Trim(), chkActive.Checked);
            xgrdEmpleado.DataSource = oListUser;
            xgrdEmpleado.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdEmpleado.JSProperties["cpAlertMessage"] = string.Empty;
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

        protected void xgrdEmpleado_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int IdEmpleado = int.Parse(e.Keys[0].ToString());
            string CodigoPosicion = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPosicionEdit")).Value.ToString();
            string Codigo = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string EMail = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtEmailEdit")).Text;
            string Credencial = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtCredencialEdit")).Text;
            string CodigoDepto = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbDeptoEdit")).Value.ToString();
            string CodigoPlanta = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPlantaEdit")).Value.ToString();
            string CodigoProveedor = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbProveedorEdit")).Value.ToString();
            string NSS = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtNSSEdit")).Text; 
            try
            {
                var BEmployee = new EmpleadosDa();

                var res = BEmployee.UpdEmpleados(LoginInfo.CurrentUsuario.UsuarioId, IdEmpleado, Codigo, Nombre, EMail, Credencial, NSS, CodigoPlanta, CodigoDepto, CodigoPosicion, CodigoProveedor);
                if (res == 1)
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Update";
                }
                else
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdEmpleado.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdEmpleado_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            string Nombre = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtNombreEdit")).Text;
            string EMail = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtEmailEdit")).Text;
            string Credencial = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtCredencialEdit")).Text;
            string CodigoDepto = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbDeptoEdit")).Value.ToString();
            string CodigoPlanta = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPlantaEdit")).Value.ToString();
            string CodigoPosicion = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPosicionEdit")).Value.ToString();
            string CodigoProveedor = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbProveedorEdit")).Value.ToString();
            string NSS = ((ASPxTextBox)xgrdEmpleado.FindEditFormTemplateControl("xtxtNSSEdit")).Text;

            try
            {
                var BUser = new EmpleadosDa();

                var res = BUser.InsEmpleados(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Nombre, EMail, Credencial, NSS, CodigoPlanta, CodigoDepto, CodigoPosicion, CodigoProveedor);
                if (res == 1)
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdEmpleado.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdEmpleado_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var EmpleadoId = int.Parse(e.Keys[0].ToString());

            try
            {
                var BUser = new EmpleadosDa();
                var res = BUser.DelEmpleados(LoginInfo.CurrentUsuario.UsuarioId, EmpleadoId);
                if (res == 1)
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdEmpleado_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdEmpleado_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
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

            var BPosicion = new PosicionDa();
            var cmbPosicionEdit = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPosicionEdit"));
            cmbPosicionEdit.TextField = "Descripcion";
            cmbPosicionEdit.ValueField = "Codigo";
            cmbPosicionEdit.DataSource = BPosicion.GetCombo(LoginInfo.CurrentPerfil.EsAdministrador);
            cmbPosicionEdit.DataBind();

            var BPlanta = new PlantaDa();
            var cmbPlantaEdit = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbPlantaEdit"));
            cmbPlantaEdit.TextField = "Descripcion";
            cmbPlantaEdit.ValueField = "Codigo";
            cmbPlantaEdit.DataSource = BPlanta.GetCatalog("","", true);
            cmbPlantaEdit.DataBind();

            var BDepartamentoo = new DepartamentoDa();
            var Codigo = string.Empty;
            var Descripcion = string.Empty;
            var Activo = true;
            var cmbDeptoEdit = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbDeptoEdit"));
            cmbDeptoEdit.TextField = "Descripcion";
            cmbDeptoEdit.ValueField = "Codigo";
            cmbDeptoEdit.DataSource = BDepartamentoo.GetCatalog(Codigo, Descripcion, Activo);
            cmbDeptoEdit.DataBind();

            var BProveedor = new ProveedorDa();
            var cmbProveedorEdit = ((ASPxComboBox)xgrdEmpleado.FindEditFormTemplateControl("cmbProveedorEdit"));
            cmbProveedorEdit.TextField = "Nombre";
            cmbProveedorEdit.ValueField = "Codigo";
            cmbProveedorEdit.DataSource = BProveedor.GetCatalog("", "", true);
            cmbProveedorEdit.DataBind();
        }

        protected void cmbPosicionEdit_DataBound(object sender, EventArgs e)
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

            ASPxComboBox cmbPosicionEdit = ((ASPxComboBox)sender);
            string Codigo = Posicion;
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "01")
            {
                Codigo = ((HtmlInputHidden)xgrdEmpleado.FindEditFormTemplateControl("hdnCodigoPosicion")).Value;
                if ((Codigo == string.Empty) || (Codigo == "0") || (Codigo == "NA"))
                    Codigo = "NA";
            }
            ListEditItem oItem = cmbPosicionEdit.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbPosicionEdit.SelectedIndex = 0;
            cmbPosicionEdit.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
        }

        protected void cmbPlantaEdit_DataBound(object sender, EventArgs e)
        {
            ASPxComboBox cmbPlantaEdit = ((ASPxComboBox)sender);
            string Codigo = "NA";
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "01")
            {
                Codigo = ((HtmlInputHidden)xgrdEmpleado.FindEditFormTemplateControl("hdnCodigoPlanta")).Value;
                if ((Codigo == string.Empty) || (Codigo == "0") || (Codigo == "NA"))
                    Codigo = "NA";
            }
            ListEditItem oItem = cmbPlantaEdit.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbPlantaEdit.SelectedIndex = 0;
            cmbPlantaEdit.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
        }

        protected void cmbDeptoEdit_DataBound(object sender, EventArgs e)
        {
            ASPxComboBox cmbDeptoEdit = ((ASPxComboBox)sender);
            string Codigo = "NA";
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "01")
            {
                Codigo = ((HtmlInputHidden)xgrdEmpleado.FindEditFormTemplateControl("hdnCodigoDepto")).Value;
                if ((Codigo == string.Empty) || (Codigo == "0") || (Codigo == "NA"))
                    Codigo = "NA";
            }
            ListEditItem oItem = cmbDeptoEdit.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbDeptoEdit.SelectedIndex = 0;
            cmbDeptoEdit.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
        }

        protected void cmbProveedorEdit_DataBound(object sender, EventArgs e)
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

            ASPxComboBox cmbProveedorEdit = ((ASPxComboBox)sender);
            string Codigo = LoginInfo.CurrentUsuario.CodigoProveedor;
            if (LoginInfo.CurrentUsuario.CodigoPosicion == "01")
            {
                Codigo = ((HtmlInputHidden)xgrdEmpleado.FindEditFormTemplateControl("hdnCodigoProveedor")).Value;
                if ((Codigo == string.Empty) || (Codigo == "0") || (Codigo == "NA"))
                    Codigo = "NA";
            }
            ListEditItem oItem = cmbProveedorEdit.Items.FindByValue(Codigo);
            if (oItem != null)
                oItem.Selected = true;
            else
                cmbProveedorEdit.SelectedIndex = 0;
            cmbProveedorEdit.Enabled = (LoginInfo.CurrentUsuario.CodigoPosicion == "01");
        }

        protected void xgrdEmpleado_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("EmpleadoId");

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
                var BEmpleados = new EmpleadosDa();
                var res = BEmpleados.DelEmpleadosSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
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

                var BEmpleados = new EmpleadosDa();
                var res = BEmpleados.DelEmpleadosAll(LoginInfo.CurrentUsuario.UsuarioId, Posicion, Proveedor, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void CallbackPanelDowload_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
            ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
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
            DataSet dts = new DataSet();
            var BEmployee = new EmpleadosDa();
            dts = BEmployee.GetCatalogEmployees(Posicion, Proveedor, xtxtCodigo.Text.Trim(), xtxtNombreCompleto.Text.Trim(), chkActive.Checked);

            DataTable dt = new DataTable();
            dt = dts.Tables[0];

            if (dt.Rows.Count > 0)
            {
                const double dFactor = 4;
                using (Workbook _workbook = new Workbook()) // Se crea workbook  using DevExpress.Spreadsheet y agregando la refrencia DevExpress.Docs;
                {
                    //Declaramos el numero de Hojas que tendra el documento 
                    _workbook.Worksheets.Insert(1);

                    ////**********************************************PAGINA 2****************************************************
                    //Worksheet _wss = _workbook.Worksheets[1];

                    //_wss.Cells[0].Value = "ACTIVO";
                    //_wss.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    //_wss.Cells[0].Font.Color = System.Drawing.Color.White;

                    ////Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    //_wss.Columns[0].Width = 150 * dFactor;

                    //_wss.Cells[0, 0].Protection.Locked = true;
                    //_wss.Cells[1, 0].Protection.Locked = true;
                    //_wss.Cells[2, 0].Protection.Locked = true;

                    //_wss.Cells[1, 0].Value = "0";
                    //_wss.Cells[2, 0].Value = "1";

                    //**********************************************PAGINA 1****************************************************   
                    Worksheet _ws = _workbook.Worksheets[0];

                    //    EmpleadoId = (int)dr["EmpleadoId"],
                    //    Codigo = (int)dr["Codigo"],
                    //    NombreCompleto = (string)dr["NombreCompleto"],
                    //    Credencial = (string)dr["Credencial"],
                    //    CodigoPlanta = (int)dr["CodigoPlanta"],
                    //    Planta = (string)dr["Planta"],
                    //    CodigoPosicion = (int)dr["CodigoPosicion"],
                    //    Posicion = (string)dr["Posicion"],
                    //    CodigoDepto = (int)dr["CodigoDepto"],                       
                    //    Departamento = (string)dr["Departamento"],
                    //    Activo = (bool)dr["Activo"],

                    _ws.Cells[0].Value = "EmpleadoId";
                    _ws.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[0].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[1].Value = "Codigo";
                    _ws.Cells[1].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[1].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[2].Value = "NombreCompleto";
                    _ws.Cells[2].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[2].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[3].Value = "Credencial";
                    _ws.Cells[3].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[3].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[4].Value = "CodigoPlanta";
                    _ws.Cells[4].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[4].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[5].Value = "Planta";
                    _ws.Cells[5].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[5].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[6].Value = "CodigoDepto";
                    _ws.Cells[6].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[6].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[7].Value = "Departamento";
                    _ws.Cells[7].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[7].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[8].Value = "CodigoPosicion";
                    _ws.Cells[8].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[8].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[9].Value = "Posicion";
                    _ws.Cells[9].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[9].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[10].Value = "Activo";
                    _ws.Cells[10].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[10].Font.Color = System.Drawing.Color.White;


                    //Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    _ws.Columns[0].Width = 100 * dFactor;
                    _ws.Columns[1].Width = 200 * dFactor;
                    _ws.Columns[2].Width = 400 * dFactor;
                    _ws.Columns[3].Width = 200 * dFactor;
                    _ws.Columns[4].Width = 200 * dFactor;
                    _ws.Columns[5].Width = 200 * dFactor;
                    _ws.Columns[6].Width = 400 * dFactor;
                    _ws.Columns[7].Width = 200 * dFactor;
                    _ws.Columns[8].Width = 400 * dFactor;
                    _ws.Columns[9].Width = 100 * dFactor;
                    _ws.Columns[10].Width = 100 * dFactor;

                    // El formato que se asigna es #,##0 , si pones mal esto al abrir el archivo con excel marca que esta dañado....
                    // El formato que se asigna a moneda es "$#,##0.00"..
                    //_ws.Columns[3].NumberFormat = "$#,##0.00";
                    //_ws.Columns[4].NumberFormat = "d/m/yy";                        


                    int idxROW = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ////Bloqueamos las celdas EmpleadoId y Codigo
                        //_ws.Cells[idxROW, 0].Protection.Locked = true;
                        //_ws.Cells[idxROW, 0].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        //_ws.Columns[0]
                        //    .Visible = false;

                        _ws.Cells[idxROW, 1].Protection.Locked = false;
                        _ws.Cells[idxROW, 2].Protection.Locked = false;
                        _ws.Cells[idxROW, 3].Protection.Locked = false;
                        _ws.Cells[idxROW, 4].Protection.Locked = false;
                        _ws.Cells[idxROW, 5].Protection.Locked = false;
                        _ws.Cells[idxROW, 6].Protection.Locked = false;
                        _ws.Cells[idxROW, 7].Protection.Locked = false;
                        _ws.Cells[idxROW, 8].Protection.Locked = false;
                        _ws.Cells[idxROW, 9].Protection.Locked = false;
                        _ws.Cells[idxROW, 10].Protection.Locked = false;

                        // Si es null no llenar celdas
                        if (dr[1] != DBNull.Value)
                        {
                            _ws.Cells[idxROW, 0].Value = Convert.ToString(dr[0]);
                            _ws.Cells[idxROW, 1].Value = Convert.ToString(dr[1]);
                            _ws.Cells[idxROW, 2].Value = Convert.ToString(dr[2]);
                            _ws.Cells[idxROW, 3].Value = Convert.ToString(dr[3]);
                            _ws.Cells[idxROW, 4].Value = Convert.ToString(dr[4]);
                            _ws.Cells[idxROW, 5].Value = Convert.ToString(dr[5]);
                            _ws.Cells[idxROW, 6].Value = Convert.ToString(dr[6]);
                            _ws.Cells[idxROW, 7].Value = Convert.ToString(dr[7]);
                            _ws.Cells[idxROW, 8].Value = Convert.ToString(dr[8]);
                            _ws.Cells[idxROW, 9].Value = Convert.ToString(dr[9]);
                            if (Convert.ToString(dr[10]) == "True")
                            {
                                _ws.Cells[idxROW, 10].Value = 1;
                            }
                            else
                            {
                                _ws.Cells[idxROW, 10].Value = 0;
                            }
                        }

                        idxROW = idxROW + 1;
                    }

                    // Con esto se le pone la proteccion al excel
                    //_wss.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    _ws.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    string filename = "Empleados";
                    // Esta es una rutina que tengo para que se mande al response 
                    //SendWorkBookToResponse(_workbook, filename);                

                    using (MemoryStream st = new MemoryStream())
                    {
                        HttpResponse Response = HttpContext.Current.Response;

                        _workbook.SaveDocument(st, DocumentFormat.OpenXml);
                        Response.Clear();
                        Response.ContentType = "application/force-download";
                        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xlsx", filename));
                        Response.BinaryWrite(st.ToArray());
                        Response.End();
                    }
                }
            }
            else
            {
                throw new Exception("Nothing Information!");
            }
        }

        #region "SendWorkBookToResponse"
        public void SendWorkBookToResponse(Workbook pWorkbook, string pFileName)
        {
            using (MemoryStream st = new MemoryStream())
            {
                HttpResponse Response = HttpContext.Current.Response;

                pWorkbook.SaveDocument(st, DocumentFormat.OpenXml);
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}_{1:hhmmss}.xlsx", pFileName, DateTime.Now));
                Response.BinaryWrite(st.ToArray());
                Response.End();
            }
        }
        #endregion

        #region "ExportGridToExcel"
        public static void ExportGridToExcel(ASPxGridView pGrid, string pFileName)
        {
            ASPxGridViewExporter ASPxGridViewExporter1 = new ASPxGridViewExporter()
            {
                GridViewID = pGrid.ID,
                FileName = pFileName,
            };

            XlsExportOptions ExportOptions = new DevExpress.XtraPrinting.XlsExportOptions()
            {
                ExportHyperlinks = false,
                TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value
            };

            ASPxGridViewExporter1.WriteXlsToResponse(true, ExportOptions);

        }
        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ASPxComboBox cmbPosicion = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbPosicion"));
            ASPxComboBox cmbProveedor = ((ASPxComboBox)ASPxNavBar2.Groups[0].FindControl("cmbProveedor"));
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
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
            DataSet dts = new DataSet();
            var BEmployee = new EmpleadosDa();
            dts = BEmployee.GetCatalogEmployees(Posicion, Proveedor, xtxtCodigo.Text.Trim(), xtxtNombreCompleto.Text.Trim(), chkActive.Checked);

            DataTable dt = new DataTable();
            dt = dts.Tables[0];

            if (dt.Rows.Count > 0)
            {
                const double dFactor = 4;
                using (Workbook _workbook = new Workbook()) // Se crea workbook  using DevExpress.Spreadsheet y agregando la refrencia DevExpress.Docs;
                {
                    //Declaramos el numero de Hojas que tendra el documento 
                    _workbook.Worksheets.Insert(1);  //0 equivale a uno, 1 equivale a 2 etc.

                    ////**********************************************PAGINA 2****************************************************
                    //Worksheet _wss = _workbook.Worksheets[1];

                    //_wss.Cells[0].Value = "ACTIVO";
                    //_wss.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    //_wss.Cells[0].Font.Color = System.Drawing.Color.White;

                    ////Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    //_wss.Columns[0].Width = 150 * dFactor;

                    //_wss.Cells[0, 0].Protection.Locked = true;
                    //_wss.Cells[1, 0].Protection.Locked = true;
                    //_wss.Cells[2, 0].Protection.Locked = true;

                    //_wss.Cells[1, 0].Value = "0";
                    //_wss.Cells[2, 0].Value = "1";

                    //**********************************************PAGINA 1****************************************************   
                    Worksheet _ws = _workbook.Worksheets[0];
                    
                    //    Codigo = (int)dr["Codigo"],
                    //    NombreCompleto = (string)dr["NombreCompleto"],
                    //    Credencial = (string)dr["Credencial"],
                    //    CodigoPlanta = (int)dr["CodigoPlanta"],
                    //    Planta = (string)dr["Planta"],
                    //    CodigoPosicion = (int)dr["CodigoPosicion"],
                    //    Posicion = (string)dr["Posicion"],
                    //    CodigoDepto = (int)dr["CodigoDepto"],                       
                    //    Departamento = (string)dr["Departamento"],
                    //    Activo = (bool)dr["Activo"],

                    //_ws.Cells[0].Value = "EmpleadoId";
                    //_ws.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    //_ws.Cells[0].Font.Color = System.Drawing.Color.White;

                    _ws.Cells[0].Value = "Codigo";
                    _ws.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[0].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[1].Value = "NombreCompleto";
                    _ws.Cells[1].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[1].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[2].Value = "EMail";
                    _ws.Cells[2].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[2].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[3].Value = "Credencial";
                    _ws.Cells[3].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[3].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[4].Value = "NSS";
                    _ws.Cells[4].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[4].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[5].Value = "CodigoPlanta";
                    _ws.Cells[5].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[5].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[6].Value = "Planta";
                    _ws.Cells[6].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[6].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[7].Value = "CodigoDepto";
                    _ws.Cells[7].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[7].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[8].Value = "Departamento";
                    _ws.Cells[8].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[8].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[9].Value = "CodigoPosicion";
                    _ws.Cells[9].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[9].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[10].Value = "Posicion";
                    _ws.Cells[10].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[10].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[11].Value = "CodigoProveedor";
                    _ws.Cells[11].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[11].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[12].Value = "Proveedor";
                    _ws.Cells[12].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[12].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[13].Value = "Activo";
                    _ws.Cells[13].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[13].Font.Color = System.Drawing.Color.White;


                    //Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    //_ws.Columns[0].Width = 100 * dFactor;
                    _ws.Columns[0].Width = 200 * dFactor;
                    _ws.Columns[1].Width = 400 * dFactor;
                    _ws.Columns[2].Width = 400 * dFactor;
                    _ws.Columns[3].Width = 100 * dFactor;
                    _ws.Columns[4].Width = 100 * dFactor;
                    _ws.Columns[5].Width = 200 * dFactor;
                    _ws.Columns[6].Width = 400 * dFactor;
                    _ws.Columns[7].Width = 200 * dFactor;
                    _ws.Columns[8].Width = 400 * dFactor;
                    _ws.Columns[9].Width = 200 * dFactor;
                    _ws.Columns[10].Width = 400 * dFactor;
                    _ws.Columns[11].Width = 200 * dFactor;
                    _ws.Columns[12].Width = 400 * dFactor;
                    _ws.Columns[13].Width = 100 * dFactor;

                    // El formato que se asigna es #,##0 , si pones mal esto al abrir el archivo con excel marca que esta dañado....
                    // El formato que se asigna a moneda es "$#,##0.00"..
                    //_ws.Columns[3].NumberFormat = "$#,##0.00";
                    //_ws.Columns[4].NumberFormat = "d/m/yy";                        


                    int idxROW = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ////Bloqueamos las celdas EmpleadoId y Codigo
                        //_ws.Cells[idxROW, 0].Protection.Locked = true;
                        //_ws.Cells[idxROW, 0].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        //_ws.Columns[0].Visible = false;

                        _ws.Cells[idxROW, 0].Protection.Locked = false;
                        _ws.Cells[idxROW, 1].Protection.Locked = false;
                        _ws.Cells[idxROW, 2].Protection.Locked = false;
                        _ws.Cells[idxROW, 3].Protection.Locked = false;
                        _ws.Cells[idxROW, 4].Protection.Locked = false;
                        _ws.Cells[idxROW, 5].Protection.Locked = false;
                        _ws.Cells[idxROW, 6].Protection.Locked = false;
                        _ws.Cells[idxROW, 7].Protection.Locked = false;
                        _ws.Cells[idxROW, 8].Protection.Locked = false;
                        _ws.Cells[idxROW, 9].Protection.Locked = false;
                        _ws.Cells[idxROW, 10].Protection.Locked = false;
                        _ws.Cells[idxROW, 11].Protection.Locked = false;
                        _ws.Cells[idxROW, 12].Protection.Locked = false;
                        _ws.Cells[idxROW, 13].Protection.Locked = false;

                        // Si es null no llenar celdas
                        if (dr[1] != DBNull.Value)
                        {
                            _ws.Cells[idxROW, 0].Value = Convert.ToString(dr[1]);
                            _ws.Cells[idxROW, 1].Value = Convert.ToString(dr[2]);
                            _ws.Cells[idxROW, 2].Value = Convert.ToString(dr[3]);
                            _ws.Cells[idxROW, 3].Value = Convert.ToString(dr[4]);
                            _ws.Cells[idxROW, 4].Value = Convert.ToString(dr[5]);
                            _ws.Cells[idxROW, 5].Value = Convert.ToString(dr[6]);
                            _ws.Cells[idxROW, 6].Value = Convert.ToString(dr[7]);
                            _ws.Cells[idxROW, 7].Value = Convert.ToString(dr[8]);
                            _ws.Cells[idxROW, 8].Value = Convert.ToString(dr[9]);
                            _ws.Cells[idxROW, 9].Value = Convert.ToString(dr[10]);
                            _ws.Cells[idxROW, 10].Value = Convert.ToString(dr[11]);
                            _ws.Cells[idxROW, 11].Value = Convert.ToString(dr[12]);
                            _ws.Cells[idxROW, 12].Value = Convert.ToString(dr[13]);
                            if (Convert.ToString(dr[14]) == "True")
                            {
                                _ws.Cells[idxROW, 13].Value = 1;
                            }
                            else
                            {
                                _ws.Cells[idxROW, 13].Value = 0;
                            }                           
                        }
                        idxROW = idxROW + 1;
                    }

                    // Con esto se le pone la proteccion al excel
                    //_wss.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    //_ws.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    string filename = "Empleados";
                    // Esta es una rutina que tengo para que se mande al response 
                    //SendWorkBookToResponse(_workbook, filename);                

                    using (MemoryStream st = new MemoryStream())
                    {
                        HttpResponse Response = HttpContext.Current.Response;

                        _workbook.SaveDocument(st, DocumentFormat.OpenXml);
                        Response.Clear();
                        Response.ContentType = "application/force-download";
                        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xlsx", filename));
                        Response.BinaryWrite(st.ToArray());
                        Response.End();
                    }
                }
            }
            else
            {
                throw new Exception("Nothing Information!");
            }
        }

        #region UploadFilesExcel
        protected void uplReceipt_FileUploadCompleteExcel(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = SavePostedReceiptFileExcel(e.UploadedFile);
        }

        string SavePostedReceiptFileExcel(UploadedFile uploadedFile)
        {
            //---------Guardamos el archivo en la tabla temporal si es un archivo excel lo leemos pero no lo guardamos--------
            //----------------------------------------------------------------------------------------------------------------
            if (!uploadedFile.IsValid)
                return string.Empty;

            //------Renombramos el archivo antes de guardarlo----------------------------------------------------
            //---------------------------------------------------------------------------------------------------
            string NewFileName = "";
            string FileName = uploadedFile.FileName;
            string[] wordsDos = FileName.Split('.');
            string Typefile = "";

            int rowe = 0;
            foreach (string word in wordsDos)
            {
                if (rowe == 1)
                {
                    Typefile = word;
                }
                rowe = rowe + 1;
            }

            //Renombramos el archivo y lo guardamos en el caso del excel solo lo leemos.
            string NoArchivo = DateTime.Now.ToString("dd/MM/yy");
            NoArchivo = NoArchivo.Replace('/', '_');
            string NameFile = uploadedFile.FileName;

            sPath = "~/Upload/";
            NewFileName = NoArchivo + '.' + Typefile;
            string NewPath = Path.Combine(Server.MapPath(sPath), NewFileName);
            uploadedFile.SaveAs(NewPath);

            if (Typefile == "xlsx")
            {
                Worksheet _ws;
                //Debemos de leer el archivo excel y almacenar su informacion en un dataset.
                using (Workbook _workbook = new Workbook())
                {
                    //Abrir archivo de excel
                    _workbook.LoadDocument(NewPath);

                    //Los datos deben estar en la primer hoja de excel
                    _ws = _workbook.Worksheets[0];

                    DataTable dtSheet = new DataTable("Empleados");
                    //DataColumn workCol = dtSheet.Columns.Add("EmpleadoId", typeof(Int32));
                    //workCol.AllowDBNull = false;
                    //workCol.Unique = true;
                    
                    //    Codigo = (int)dr["Codigo"],
                    //    NombreCompleto = (string)dr["NombreCompleto"],
                    //    Credencial = (string)dr["Credencial"],
                    //    CodigoPlanta = (int)dr["CodigoPlanta"],
                    //    Planta = (string)dr["Planta"],
                    //    CodigoPosicion = (int)dr["CodigoPosicion"],
                    //    Posicion = (string)dr["Posicion"],
                    //    CodigoDepto = (int)dr["CodigoDepto"],                       
                    //    Departamento = (string)dr["Departamento"],
                    //    Activo = (bool)dr["Activo"],

                    dtSheet.Columns.Add("Codigo", typeof(String));
                    dtSheet.Columns.Add("NombreCompleto", typeof(String));
                    dtSheet.Columns.Add("Credencial", typeof(String));
                    dtSheet.Columns.Add("CodigoPlanta", typeof(String));
                    dtSheet.Columns.Add("Planta", typeof(String));
                    dtSheet.Columns.Add("CodigoPosicion", typeof(String));
                    dtSheet.Columns.Add("Posicion", typeof(String));
                    dtSheet.Columns.Add("CodigoDepto", typeof(String));
                    dtSheet.Columns.Add("Departamento", typeof(String));                    
                    dtSheet.Columns.Add("NSS", typeof(String));
                    dtSheet.Columns.Add("Activo", typeof(String));
                                                          

                    // Obtener el valor de la celda con 		
                    for (int idxRow = 1; idxRow < _ws.Rows.LastUsedIndex; idxRow++)
                    {
                        if (_ws.Cells[idxRow, 0].Value.TextValue == null) {
                            break;
                        }
                        string sCodigo = _ws.Cells[idxRow, 0].Value.TextValue.ToString();
                        string sNombreCompleto = _ws.Cells[idxRow, 1].Value.TextValue.ToString();
                        string sCredencial = _ws.Cells[idxRow, 2].Value.ToString();
                        string sCodigoPlanta = _ws.Cells[idxRow, 3].Value.ToString();
                        string sPlanta = _ws.Cells[idxRow, 4].Value.ToString();
                        string sCodigoPosicion = _ws.Cells[idxRow, 5].Value.ToString();
                        string sPosicion = _ws.Cells[idxRow, 6].Value.ToString();
                        string sCodigoDepto = _ws.Cells[idxRow, 7].Value.ToString();
                        string sDepartamento = _ws.Cells[idxRow, 8].Value.ToString();
                        string sNSS = _ws.Cells[idxRow, 9].Value.ToString();
                        string sActivo = _ws.Cells[idxRow, 10].Value.ToString();

                        if (sCodigo != "" && sNombreCompleto != "")
                        {
                            dtSheet.Rows.Add(sCodigo, sNombreCompleto, sCredencial, sCodigoPlanta, sPlanta, sCodigoPosicion, sPosicion, sCodigoDepto, sDepartamento, sNSS, sActivo);
                        }
                    }

                    //Guardamos la lista de materiales en una tabla temporal para despues usarlo al guardar el archivo.                    
                    DataSet dtLista = new DataSet("ListaEmpleados");
                    dtLista.Tables.Add(dtSheet.Copy());
                    string xmlListaEmpleados = dtLista.GetXml();

                    try
                    {
                        var BEmpleados = new EmpleadosDa();
                        var res = BEmpleados.UpdEmpleadosXlS(xmlListaEmpleados, LoginInfo.CurrentUsuario.UsuarioId);
                        if (res == 1)
                        {
                            xgrdEmpleado.JSProperties["cpAlertMessage"] = "Update";
                        }
                        else
                        {
                            xgrdEmpleado.JSProperties["cpAlertMessage"] = "Error";
                        }
                    }
                    catch (Exception ex)
                    {
                        xgrdEmpleado.JSProperties["cpAlertMessage"] = ex.Message;
                    }

                }
            }
            return "";
        }
        #endregion
    }
}