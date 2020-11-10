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
    public partial class CatPhoto : BasePage
    {
        string UploadDirectory = "~/Assets/images/users/";

        public string SelectedPhoto
        {
            get { return (string)Session["SelectedPhoto"]; }
            set { Session["SelectedPhoto"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.imgProfile.ImageUrl = LoginInfo.CurrentUsuario.ImagenUrl;
            //this.imbPhotoBig.ImageUrl = LoginInfo.CurrentUsuario.ImagenUrl;
            ASPxImage imgPreview = ASPxNavBar2.Groups[0].FindControl("imgPreview") as ASPxImage;

            imgPreview.ImageUrl = UploadDirectory + LoginInfo.CurrentUsuario.ImagenUrl;
        }

        protected void CIuplGraphicsFile_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.IsValid) {
                e.CallbackData = SavePostedGraphicsFile(e.UploadedFile);

                UsuarioDa BUser = new UsuarioDa();
                int res = BUser.ChangePhoto(this.LoginInfo.CurrentUsuario.UsuarioId, this.SelectedPhoto); //this.SelectedPhoto
                if (res >= 1)
                {
                    LoginInfo.CurrentUsuario.ImagenUrl =  this.SelectedPhoto;
                    e.IsValid = true;
                }
                else {
                    e.IsValid = false;
                }
            }          
            else
            {
                e.IsValid = false;
                e.ErrorText = "Unexpected error";
            }
        }

        string SavePostedGraphicsFile(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return string.Empty;

            string sPath = Path.Combine(Server.MapPath(UploadDirectory), uploadedFile.FileName);

            if (ExistsFile(sPath))
                File.Delete(sPath);

            uploadedFile.SaveAs(sPath);
            //SelectedPhoto = UploadDirectory + uploadedFile.FileName;
            SelectedPhoto = uploadedFile.FileName;

            return uploadedFile.FileName;
        }

        private bool ExistsFile(string sFile)
        {
            bool bExists = false;

            bExists = System.IO.File.Exists(sFile);

            return bExists;
        }

        protected void cbRecovery_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            SelectedPhoto = e.Parameter;
        }
    }
}