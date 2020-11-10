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
    public partial class CatPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ASPxNavBar2.JSProperties["cpAlertMessage"] = string.Empty;
        }

        protected void CallbackChangePassword_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            string sEncryptionKey = "r0b1nr0y";
            ASPxTextBox xtxtOldPassword = ASPxNavBar2.Groups[0].FindControl("xtxtOldPassword") as ASPxTextBox;
            ASPxTextBox xtxtNewPassword = ASPxNavBar2.Groups[0].FindControl("xtxtNewPassword") as ASPxTextBox;
            ASPxTextBox xtxtConfirm = ASPxNavBar2.Groups[0].FindControl("xtxtConfirm") as ASPxTextBox;            
            try
            {
                UsuarioDa BUser = new UsuarioDa();
                int res = BUser.ChangePassword(this.LoginInfo.CurrentUsuario.UsuarioId, xtxtOldPassword.Text, xtxtNewPassword.Text);
                if (res >= 1)
                    ASPxNavBar2.JSProperties["cpAlertMessage"] = "Update";                    
                else
                    ASPxNavBar2.JSProperties["cpAlertMessage"] = "Error";
            }
            catch (Exception ex)
            {
                ASPxNavBar2.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }
    }
}