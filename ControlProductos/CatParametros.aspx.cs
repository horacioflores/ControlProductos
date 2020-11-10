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
    public partial class CatParametros : BasePage
    {
        public string ValidaParametroEncriptado(object oChk, object oVal)
        {
            bool bEncrypt = false;
            if (oChk != null)
                bEncrypt = (bool)oChk;
            string sVal = string.Empty;
            if (oChk != null)
                sVal = (string)oVal;

            if (bEncrypt)
                return utilities.Encryption.DecryptURL(sVal);
            else
                return sVal;
        }

        public bool ValidaBoleano(object valor)
        {
            bool resultado = false;
            if (valor != null)
                if (!bool.TryParse(valor.ToString(), out resultado))
                    resultado = false;
            return resultado;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            xgrdParametro.JSProperties["cpAlertMessage"] = string.Empty;

            fillGrid();
            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();

                foreach (NavBarGroup group in ASPxNavBar1.Groups)
                    group.Expanded = false;
            }
        }
        public void fillGrid()
        {
            ASPxTextBox xtxtDescripcion = ASPxNavBar1.Groups[0].FindControl("xtxtDescripcion") as ASPxTextBox;

            var BParametro = new ParametroDa();
            var oListParametro = BParametro.GetCatalog(xtxtDescripcion.Text.Trim());
            xgrdParametro.DataSource = oListParametro;
            xgrdParametro.DataBind();
        }

        protected void xgrdParametro_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var IdParametro = int.Parse(e.Keys[0].ToString());
            bool bEncrypted = ((ASPxCheckBox)xgrdParametro.FindEditFormTemplateControl("chkEncrypted")).Checked;
            var sValue = ((ASPxTextBox)xgrdParametro.FindEditFormTemplateControl("xtxtValue")).Text;
            if (bEncrypted)
                sValue = utilities.Encryption.EncryptURL(sValue);
            try
            {
                var BParametro = new ParametroDa();
                var res = BParametro.UpdParametro(LoginInfo.CurrentUsuario.UsuarioId, IdParametro, sValue);
                if (res == 1)
                {
                    xgrdParametro.JSProperties["cpAlertMessage"] = "Update";
                }
                else
                {
                    xgrdParametro.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdParametro.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdParametro.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdParametro_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdParametro_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            string sVal = xgrdParametro.GetRowValuesByKeyValue(e.EditingKeyValue, "Editable").ToString();
            bool bVal = bool.Parse(sVal);
            if (!bVal)
                e.Cancel = true;
        }

        private void ApplyLayout()
        {
            xgrdParametro.BeginUpdate();
            try
            {
                xgrdParametro.ClearSort();
                xgrdParametro.GroupBy(xgrdParametro.Columns["Padre"]);
            }
            finally
            {
                xgrdParametro.EndUpdate();
            }
            xgrdParametro.ExpandAll();
        }
    }
}