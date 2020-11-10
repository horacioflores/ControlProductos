using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DevExpress.Web;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using ControlProductos.models;

namespace ControlProductos
{
    public partial class login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void LoginImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string Perfil = "NA";

            var BUsuario = new UsuarioDa();
            int iIdUser = 0;
            iIdUser = BUsuario.ValidateUsuario(xtxtUsuario.Text.Trim(), xtxtPassword.Text.Trim());
            if (iIdUser > 0)
            {
                var iIdPerfil = BUsuario.GetPerfileByUsuario(iIdUser);
                var oLoginInfo = new loggedEmpleado();
                oLoginInfo.CurrentUsuario = BUsuario.GetUsuario(iIdUser);
                var BPerfile = new PerfilDa();
                oLoginInfo.CurrentPerfil = BPerfile.GetPerfil(iIdPerfil);
                Perfil = oLoginInfo.CurrentPerfil.Codigo;
                LoginInfo = oLoginInfo;
                Response.Redirect("wellcome.aspx");
            }
        }

        protected void cbRecovery_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            try
            {
                if (this.xtxtUserName.Text.Trim() == "")
                {
                    cbRecovery.JSProperties["cpAlertMessage"] = "User is required";
                    e.Result = "0";
                }
                else
                {
                    UsuarioDa BUser = new UsuarioDa();
                    List<Entity.Usuario> oListUser = new List<Entity.Usuario>();
                    Entity.Usuario oUser = new Entity.Usuario();
                    string sUserName = this.xtxtUserName.Text.Trim();
                    oListUser = BUser.GetUsuarioByUsername(sUserName);
                    if (oListUser.Count == 0)
                    {
                        cbRecovery.JSProperties["cpAlertMessage"] = "NoUser";
                        e.Result = "0";
                    }
                    else
                    {
                        //SMTP Conection Parameters
                        ParametroDa BParametero = new ParametroDa();
                        string pEnviaCorreos = BParametero.GetParametro("EnviaCorreos").Valor.ToUpper();
                        bool bEnviaCorreos = (pEnviaCorreos == "TRUE" || pEnviaCorreos == "VERDADERO" || pEnviaCorreos == "YES" || pEnviaCorreos == "SI" || pEnviaCorreos == "1");
                        if (bEnviaCorreos)
                        {
                            string pSMTPServer = BParametero.GetParametro("SMTPServer").Valor;
                            string pSMTPPort = BParametero.GetParametro("SMTPPort").Valor;
                            string pSMTPUser = BParametero.GetParametro("SMTPUser").Valor;
                            string pSMTPPswd = BParametero.GetParametro("SMTPPswd").Valor;
                            string pSMTPSSL = BParametero.GetParametro("SMTPSSL").Valor;
                            bool bSMTPSSL = (pSMTPSSL == "TRUE" || pSMTPSSL == "VERDADERO" || pSMTPSSL == "YES" || pSMTPSSL == "SI" || pSMTPSSL == "1");
                            string pSMTPFrom = BParametero.GetParametro("SMTPFrom").Valor;
                            string pSMTPBCC = BParametero.GetParametro("SMTPBCC").Valor;

                            oUser = oListUser.First<Entity.Usuario>();

                            XmlDocument doc = new XmlDocument();
                            doc.Load(Server.MapPath("Documents/Notifications.xml"));
                            string subject = doc.SelectNodes("/Notifications/RecoveryPassword/Subject")[0].InnerXml;
                            string body = doc.SelectNodes("/Notifications/RecoveryPassword/Body")[0].InnerXml;

                            string[] param = new string[3];

                            param[0] = oUser.NombreCompleto;
                            param[1] = oUser.Username;
                            param[2] = BUser.GetPassword(oUser.UsuarioId);

                            body = String.Format(body, param);

                            utilities.Utility.SendEmail(pSMTPServer, pSMTPPort, pSMTPUser, pSMTPPswd, bSMTPSSL, pSMTPFrom, "Sistema de Encuestas COVID", oUser.Email, "", "", subject, body, "");
                        }
                        cbRecovery.JSProperties["cpAlertMessage"] = "Success";
                        e.Result = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                //cbRecovery.JSProperties["cpAlertMessage"] = ex.Message;
                cbRecovery.JSProperties["cpAlertMessage"] = "Error";
            }
        }

        protected void CallbackPanelDisable_Callback(object sender, CallbackEventArgsBase e)
        {

        }

    }
}