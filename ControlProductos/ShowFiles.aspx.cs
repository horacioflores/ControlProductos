using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.Util;
using System.Xml.Serialization;
using System.Xml;
using DevExpress.Web;
using DevExpress.Utils.StructuredStorage.Reader;
using System.Text;

namespace ControlProductos
{
    public partial class ShowFiles : BasePage
    {
        #region "EncodeJsString"
        public string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\'':
                        sb.Append("\\\'");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string Archivo = Request.QueryString["codeFile"];
                //string codigoArchivo = Request.QueryString["codeFile"];
                //string tipoArchivo = Request.QueryString["typeFile"];
                //string nombreArchivo = Request.QueryString["nameFile"];

                string sPath = Server.MapPath("~/Upload/");
                string nombreCompleto = sPath + Archivo;
                string sFile = nombreCompleto;
                //string sFile = sPath + codigoArchivo + "." + tipoArchivo;
                //string nombreCompleto = nombreArchivo + "." + tipoArchivo;

                if (!File.Exists(sFile))
                    return;

                //Esto funciona igual a lo que esta abajo.
                //var fileInfo = new System.IO.FileInfo(sFile);
                //Response.ContentType = "application/octet-stream";
                //Response.AddHeader("Content-Disposition", String.Format("attachment;filename=\"{0}\"", Clave));
                //Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                //Response.WriteFile(sFile);
                //Response.End();

                FileStream MyFileStream = new FileStream(sFile, FileMode.Open);
                long FileSize = MyFileStream.Length;
                byte[] Buffer = new byte[(int)FileSize];
                MyFileStream.Read(Buffer, 0, (int)FileSize);
                MyFileStream.Close();

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", nombreCompleto));
                Response.BinaryWrite(Buffer);
                Response.End();
            }
            catch (Exception E1)
            {
                //Muestra mensaje
                // throw new Exception(E1.Message);

                //No muestra mensaje.
                System.Web.UI.Page pPage = new System.Web.UI.Page();
                string pMessage = EncodeJsString(E1.Message);
                pPage.ClientScript.RegisterStartupScript(pPage.GetType(), "Alert", String.Format("alert('{0}');", pMessage), true);

            }



        }
    }
}