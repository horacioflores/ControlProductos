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
using System.Xml.Serialization;
using System.Xml;

namespace ControlProductos
{
    public partial class CatProveedor : BasePage
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
        private void ApplyLayout()
        {
            xgrdProveedor.BeginUpdate();
            try
            {
                xgrdProveedor.ClearSort();
            }
            finally
            {
                xgrdProveedor.EndUpdate();
            }
        }

        //public List<Entity.ProveedorFabricante> ProveedorFabricante
        //{
        //    get
        //    {
        //        return (List<Entity.ProveedorFabricante>)Session["CurrentProveedorFabricante"];
        //    }
        //    set
        //    {
        //        Session["CurrentProveedorFabricante"] = value;
        //    }
        //}

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar2.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxTextBox xtxtDescripcion = ASPxNavBar2.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BProveedor = new ProveedorDa();
            var oListProveedor = BProveedor.GetCatalog(xtxtCodigo.Text.Trim(), xtxtDescripcion.Text.Trim(), chkActive.Checked);
            xgrdProveedor.DataSource = oListProveedor;
            xgrdProveedor.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdProveedor.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                AllowBound = false;
                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdProveedor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdProveedor_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var ProveedorId = int.Parse(e.Keys[0].ToString());

            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.DelProveedor(LoginInfo.CurrentUsuario.UsuarioId, ProveedorId);
                if (res == 1)
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdProveedor.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdProveedor_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var ProveedorId = int.Parse(e.Keys[0].ToString());
            string sKey = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            string sNombre = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtNombreEdit")).Text.Trim();
            string sAbbrev = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtAbbrevEdit")).Text.Trim();
            string sDireccion1 = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtDireccion1Edit")).Text.Trim();
            string sDireccion2 = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtDireccion2Edit")).Text.Trim();
            string sCiudad = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCiudadEdit")).Text.Trim();
            string sEstado = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtEstadoEdit")).Text.Trim();
            string sCP = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCPEdit")).Text.Trim();
            string sPais = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtPaisEdit")).Text.Trim();
            string sTel = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtTelefonoEdit")).Text.Trim();
            string sCorreo = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCorreoEdit")).Text.Trim();

            //string sListProductosFabs = string.Empty;
            //if (ProveedorFabricante.Count > 0)
            //{
            //    sListProductosFabs = "</ArrayOfProveedores_Fabricantes>";
            //    var oSerializer = new XmlSerializer(typeof(List<Entity.ProveedorFabricante>));
            //    var sWriter = new StringWriter();
            //    var writer = XmlWriter.Create(sWriter);
            //    oSerializer.Serialize(writer, ProveedorFabricante);
            //    sListProductosFabs = sWriter.ToString();
            //    int iIdx = sListProductosFabs.IndexOf("<Proveedores_Fabricantes>");
            //    sListProductosFabs = "<ArrayOfProveedores_Fabricantes>" + sListProductosFabs.Substring(iIdx);
            //}

            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.UpdProveedor(LoginInfo.CurrentUsuario.UsuarioId, ProveedorId, sKey, sNombre, sAbbrev, sDireccion1, sDireccion2, sCiudad, sEstado, sCP, sPais, sTel, sCorreo);
                if (res == 1)
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Update";
                else
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdProveedor.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdProveedor.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdProveedor_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string sKey = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            string sNombre = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtNombreEdit")).Text.Trim();
            string sAbbrev = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtAbbrevEdit")).Text.Trim();
            string sDireccion1 = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtDireccion1Edit")).Text.Trim();
            string sDireccion2 = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtDireccion2Edit")).Text.Trim();
            string sCiudad = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCiudadEdit")).Text.Trim();
            string sEstado = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtEstadoEdit")).Text.Trim();
            string sCP = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCPEdit")).Text.Trim();
            string sPais = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtPaisEdit")).Text.Trim();
            string sTel = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtTelefonoEdit")).Text.Trim();
            string sCorreo = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtCorreoEdit")).Text.Trim();
            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.InsProveedor(LoginInfo.CurrentUsuario.UsuarioId, sKey, sNombre, sAbbrev, sDireccion1, sDireccion2, sCiudad, sEstado, sCP, sPais, sTel, sCorreo);
                if (res == 1)
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Insert";
                else
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdProveedor.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdProveedor.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdProveedor_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtNombreEdit")).Text.Trim();

            var ProveedorId = 0;

            if (!e.IsNewRow)
            {
                ProveedorId = (int)e.Keys[0];
            }
            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.ValProveedor(ProveedorId, Codigo, Descripcion);
                if (res == 1)
                    e.RowError = "Ya existe un Proveedoro con la misma clave o descripción!";
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdProveedor_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("ProveedorId");

                e.Cell.Text = string.Format("<input type='checkbox' class='chk' id='chk{0}'>", id);
            }
        }

        protected void xgrdProveedor_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            //dataAccess.Fabricantes BFabricante = new dataAccess.Fabricantes();
            //ASPxComboBox cmbfabricanteEdit = ((ASPxComboBox)xgrdProveedor.FindEditFormTemplateControl("cmbfabricanteEdit"));
            //cmbfabricanteEdit.TextField = "Nombre";
            //cmbfabricanteEdit.ValueField = "Codigo";
            //cmbfabricanteEdit.DataSource = BFabricante.GetCatalog("", "", true);
            //cmbfabricanteEdit.DataBind();
        }

        protected void xgrdProveedor_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //ProveedorFabricante = new List<Entity.ProveedorFabricante>();
            //ProveedorFabricante.Clear();
            AllowBound = true;
        }

        protected void xgrdProveedor_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            AllowBound = true;
        }

        //protected void cmbfabricanteEdit_DataBound(object sender, EventArgs e)
        //{
        //    if (AllowBound == true)
        //    {
        //        ASPxTextBox xtxtProveedorId = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtProveedorId"));
        //        string sProveedorId = xtxtProveedorId.Text;
        //        int ProveedorId = 0;

        //        if (ProveedorFabricante == null)
        //        {
        //            ProveedorFabricante = new List<Entity.ProveedorFabricante>();
        //        }
        //        if (sProveedorId != string.Empty)
        //        {
        //            ProveedorId = int.Parse(sProveedorId);
        //            var BID = new ProveedorDa();
        //            ProveedorFabricante = BID.GetFabricantes(ProveedorId);
        //            var xgrdFabricantes = ((ASPxGridView)xgrdProveedor.FindEditFormTemplateControl("xgrdFabricantes"));
        //            xgrdFabricantes.DataSource = ProveedorFabricante;
        //            xgrdFabricantes.DataBind();
        //            AllowBound = false;
        //        }
        //    }
        //}

        protected void CallbackPanelDisable_Callback(object sender, CallbackEventArgsBase e)
        {
            var Valores = e.Parameter;
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //Enviamos a la base de datos los Valores y desabilitamos con un update masivo.
            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.DelProveedorSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdProveedor.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar2.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BProveedor = new ProveedorDa();
                var res = BProveedor.DelProveedorAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Delete";
                else
                    xgrdProveedor.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                xgrdProveedor.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        //protected void xgrdFabricantes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    var xgrdFabricantes = ((ASPxGridView)xgrdProveedor.FindEditFormTemplateControl("xgrdFabricantes"));
        //    xgrdFabricantes.JSProperties["cpAlertMessage"] = string.Empty;

        //    var pars = e.Parameters;

        //    ASPxTextBox xtxtProveedorId = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtProveedorId"));
        //    ASPxTextBox xtxtKeyEdit = ((ASPxTextBox)xgrdProveedor.FindEditFormTemplateControl("xtxtKeyEdit"));
        //    int ProveedorId = int.Parse(xtxtProveedorId.Text);
        //    string ProveedorCodigo = xtxtKeyEdit.Text;

        //    ASPxComboBox cmbfabricanteEdit = ((ASPxComboBox)xgrdProveedor.FindEditFormTemplateControl("cmbfabricanteEdit"));
        //    string IdFabricanteSelected = "";
        //    string FabricanteSelected = "";
        //    if (cmbfabricanteEdit.SelectedIndex != -1)
        //    {
        //        IdFabricanteSelected = cmbfabricanteEdit.SelectedItem.Value.ToString();
        //        FabricanteSelected = cmbfabricanteEdit.SelectedItem.Text.ToString();
        //    }


        //    if (pars == "Save" && IdFabricanteSelected != "")
        //    {
        //        bool bFound = false;

        //        Entity.ProveedorFabricante oProvsFabricante = new Entity.ProveedorFabricante();
        //        foreach (Entity.ProveedorFabricante item in ProveedorFabricante)
        //        {
        //            if (item.CodigoFabricante == IdFabricanteSelected)
        //            {
        //                bFound = true;
        //                break;
        //            }
        //        }
        //        if (bFound == true)
        //        {
        //            xgrdFabricantes.JSProperties["cpAlertMessage"] = "Exist";
        //        }
        //        else
        //        {
        //            oProvsFabricante.ProvFabricantesId = 0;
        //            oProvsFabricante.ProveedorId = ProveedorId;
        //            oProvsFabricante.CodigoProveedor = ProveedorCodigo;
        //            oProvsFabricante.FabricanteId = 0;
        //            oProvsFabricante.CodigoFabricante = IdFabricanteSelected;
        //            oProvsFabricante.Proveedor = "";
        //            oProvsFabricante.Fabricante = FabricanteSelected;
        //            ProveedorFabricante.Add(oProvsFabricante);
        //        }
        //    }
        //    else if (pars == "Save" && IdFabricanteSelected == "")
        //    {
        //        xgrdFabricantes.JSProperties["cpAlertMessage"] = "SelectOne";
        //    }

        //    if (pars == "Delete")
        //    {
        //        xgrdFabricantes.DataSource = null;
        //        ProveedorFabricante.Clear();
        //    }
        //    else
        //    {
        //        if (pars != "Save")
        //        {
        //            var Valores = pars;
        //            string[] data = Valores.Split(',');

        //            foreach (string d in data)
        //            {
        //                string valor = d.Replace("chk", "");
        //                string codigoFabricante = valor;

        //                ProveedorFabricante = ProveedorFabricante.FindAll(p => p.CodigoFabricante != codigoFabricante);
        //            }
        //        }

        //        xgrdFabricantes.DataSource = ProveedorFabricante;
        //    }


        //    xgrdFabricantes.DataBind();

        //}

        protected void xgrdFabricantes_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("CodigoFabricante");

                e.Cell.Text = string.Format("<input type='checkbox' class='chkFab' id='chk{0}'>", id);
            }
        }
    }
}