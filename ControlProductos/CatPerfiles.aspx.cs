using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using System.IO;
using System.Web.Util;
using System.Xml.Serialization;
using System.Xml;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using ControlProductos.dataAccess;
using ControlProductos.Entity;
using ControlProductos.models;

namespace ControlProductos
{
    public partial class CatPerfiles : BasePage
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

        public bool getValueBool(object obj)
        {
            if (obj == null)
                return false;
            else
                return (bool)obj;
        }

        private void ApplyLayout()
        {
            xgrdPerfiles.BeginUpdate();
            try
            {
                xgrdPerfiles.ClearSort();
            }
            finally
            {
                xgrdPerfiles.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtCodigo = ASPxNavBar1.Groups[0].FindControl("xtxtCodigo") as ASPxTextBox;
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            var BPerfile = new PerfilDa();
            var oListPerfile = BPerfile.GetCatalog(xtxtCodigo.Text.Trim(), chkActive.Checked);
            xgrdPerfiles.DataSource = oListPerfile;
            xgrdPerfiles.DataBind();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdPerfiles.JSProperties["cpAlertMessage"] = string.Empty;

            if (!IsPostBack)
            {
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                AllowBound = false;

                foreach (NavBarGroup group in ASPxNavBar1.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdPerfiles_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var PerfilId = int.Parse(e.Keys[0].ToString());
            var Codigo = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtKeyEdit")).Text;
            var Descripcion = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text;
            bool EsAdministrador = ((ASPxCheckBox)xgrdPerfiles.FindEditFormTemplateControl("chkEsAdministradorEdit")).Checked;
            bool RealizaEncuestas = ((ASPxCheckBox)xgrdPerfiles.FindEditFormTemplateControl("chkRealizaEncuestasEdit")).Checked;

            string sListPerfilApps = string.Empty;
            if (PerfilApps.Count > 0)
            {
                sListPerfilApps = "</ArrayOfPerfil_Apps>";
                var oSerializer = new XmlSerializer(typeof(List<Entity.Perfil_Apps>));
                var sWriter = new StringWriter();
                var writer = XmlWriter.Create(sWriter);
                oSerializer.Serialize(writer, PerfilApps);
                sListPerfilApps = sWriter.ToString();
                int iIdx = sListPerfilApps.IndexOf("<Perfil_Apps>");
                sListPerfilApps = "<ArrayOfPerfil_Apps>" + sListPerfilApps.Substring(iIdx);
            }

            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.UpdPerfil(LoginInfo.CurrentUsuario.UsuarioId, PerfilId, Codigo, Descripcion, EsAdministrador, RealizaEncuestas, PerfilApps);
                if (res >= 1)
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Update";
                }
                else
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
            }
            xgrdPerfiles.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPerfiles_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string Codigo = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            string Description = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text.Trim();
            bool EsAdministrador = ((ASPxCheckBox)xgrdPerfiles.FindEditFormTemplateControl("chkEsAdministradorEdit")).Checked;
            bool RealizaEncuestas = ((ASPxCheckBox)xgrdPerfiles.FindEditFormTemplateControl("chkRealizaEncuestasEdit")).Checked;

            string sListPerfilApps = string.Empty;
            if (PerfilApps.Count > 0)
            {
                sListPerfilApps = "</ArrayOfPerfil_Apps>";
                var oSerializer = new XmlSerializer(typeof(List<Entity.Perfil_Apps>));
                var sWriter = new StringWriter();
                var writer = XmlWriter.Create(sWriter);
                oSerializer.Serialize(writer, PerfilApps);
                sListPerfilApps = sWriter.ToString();
                int iIdx = sListPerfilApps.IndexOf("<Perfil_Apps>");
                sListPerfilApps = "<ArrayOfPerfil_Apps>" + sListPerfilApps.Substring(iIdx);
            }

            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.InsPerfil(LoginInfo.CurrentUsuario.UsuarioId, Codigo, Description, EsAdministrador, RealizaEncuestas, PerfilApps);
                if (res >= 1)
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Insert";
                }
                else
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
            }

            xgrdPerfiles.CancelEdit();
            e.Cancel = true;
        }

        protected void xgrdPerfiles_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            var PerfilId = int.Parse(e.Keys[0].ToString());
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.DelPerfil(LoginInfo.CurrentUsuario.UsuarioId, PerfilId, chkActive.Checked);
                if (res == 1)
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
            }
            e.Cancel = true;
        }

        protected void xgrdPerfiles_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var pars = e.Parameters;
            if (pars == "Search")
            {
                fillGrid();
            }
        }

        protected void xgrdPerfiles_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            var Codigo = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtKeyEdit")).Text.Trim();
            var Descripcion = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtDescriptionEdit")).Text.Trim();

            var id = 0;

            if (!e.IsNewRow)
            {
                id = (int)e.Keys[0];
            }
            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.ValPerfil(id, Codigo, Descripcion);
                if (res == 1)
                {
                    e.RowError = "A profile with the same key or description already exists!";
                }
            }
            catch (Exception ex)
            {
                e.RowError = ex.Message;
            }
        }

        protected void xgrdPerfiles_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("PerfilId");

                e.Cell.Text = string.Format("<input type='checkbox' class='chk' id='chk{0}'>", id);
            }
        }

        protected void CallbackPanelDisable_Callback(object sender, CallbackEventArgsBase e) {
            var Valores = e.Parameter;
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //Enviamos a la base de datos los Valores y desabilitamos con un update masivo.
            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.DelPerfilSelected(LoginInfo.CurrentUsuario.UsuarioId, Valores, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
            }

        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;

            //desabilitamos o habilitamos con un update masivo.
            try
            {
                var BPerfile = new PerfilDa();
                var res = BPerfile.DelPerfilAll(LoginInfo.CurrentUsuario.UsuarioId, chkActive.Checked);
                if (res >= 1)
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Delete";
                }
                else
                {
                    xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                }
            }
            catch (Exception ex)
            {
                xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
            }
        }

        protected void CallbackPanelDowload_Callback(object sender, CallbackEventArgsBase e)
        {
            var BPerfile = new PerfilDa();
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            DataSet dts = new DataSet();
            dts = BPerfile.GetCatalogPerfiles("", chkActive.Checked);

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

                    _ws.Cells[0].Value = "PerfilId";
                    _ws.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[0].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[1].Value = "Codigo";
                    _ws.Cells[1].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[1].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[2].Value = "Descripcion";
                    _ws.Cells[2].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[2].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[3].Value = "Activo";
                    _ws.Cells[3].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[3].Font.Color = System.Drawing.Color.White;


                    //Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    _ws.Columns[0].Width = 100 * dFactor;
                    _ws.Columns[1].Width = 200 * dFactor;
                    _ws.Columns[2].Width = 400 * dFactor;
                    _ws.Columns[3].Width = 100 * dFactor;

                    // El formato que se asigna es #,##0 , si pones mal esto al abrir el archivo con excel marca que esta dañado....
                    // El formato que se asigna a moneda es "$#,##0.00"..
                    //_ws.Columns[3].NumberFormat = "$#,##0.00";
                    //_ws.Columns[4].NumberFormat = "d/m/yy";                        


                    int idxROW = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Bloqueamos las celdas PerfilId y Codigo
                        _ws.Cells[idxROW, 0].Protection.Locked = true;
                        _ws.Cells[idxROW, 0].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        _ws.Columns[0].Visible = false;

                        _ws.Cells[idxROW, 1].Protection.Locked = true;
                        _ws.Cells[idxROW, 1].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        _ws.Cells[idxROW, 2].Protection.Locked = true;
                        _ws.Cells[idxROW, 3].Protection.Locked = false;

                        // Si es null no llenar celdas
                        if (dr[1] != DBNull.Value)
                        {
                            _ws.Cells[idxROW, 0].Value = Convert.ToString(dr[0]);
                            _ws.Cells[idxROW, 1].Value = Convert.ToString(dr[1]);
                            _ws.Cells[idxROW, 2].Value = Convert.ToString(dr[2]);
                            _ws.Cells[idxROW, 3].Value = Convert.ToString(dr[3]); 
                            //_ws.Cells[idxROW, 3].Value = _workbook.Worksheets[1].Range["A2"].Value;                                

                            //// depende del tipo de dato es como se llena (para que salga bien en el excel)
                            //if (col.IsNumerical)
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToDouble(dr[col.FieldName]);
                            //else if (col.IsPercent)
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToDouble(dr[col.FieldName]) / 100;
                            //else
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToString(dr[col.FieldName]);
                        }

                        idxROW = idxROW + 1;
                    }

                    // Con esto se le pone la proteccion al excel
                    //_wss.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    _ws.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    string filename = "Profiles";
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
            var BPerfile = new PerfilDa();
            ASPxCheckBox chkActive = ASPxNavBar1.Groups[0].FindControl("chkActive") as ASPxCheckBox;
            DataSet dts = new DataSet();
            dts = BPerfile.GetCatalogPerfiles("", chkActive.Checked);

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

                    _ws.Cells[0].Value = "PerfilId";
                    _ws.Cells[0].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[0].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[1].Value = "Codigo";
                    _ws.Cells[1].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[1].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[2].Value = "Descripcion";
                    _ws.Cells[2].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[2].Font.Color = System.Drawing.Color.White;
                    _ws.Cells[3].Value = "Activo";
                    _ws.Cells[3].FillColor = System.Drawing.Color.FromArgb(0x33, 0x99, 0x66);
                    _ws.Cells[3].Font.Color = System.Drawing.Color.White;


                    //Asi se asigna el width a la columna, las medidas no son en pixeles, pero multiplicando por 4 mas o menos da
                    _ws.Columns[0].Width = 100 * dFactor;
                    _ws.Columns[1].Width = 200 * dFactor;
                    _ws.Columns[2].Width = 400 * dFactor;
                    _ws.Columns[3].Width = 100 * dFactor;

                    // El formato que se asigna es #,##0 , si pones mal esto al abrir el archivo con excel marca que esta dañado....
                    // El formato que se asigna a moneda es "$#,##0.00"..
                    //_ws.Columns[3].NumberFormat = "$#,##0.00";
                    //_ws.Columns[4].NumberFormat = "d/m/yy";                        


                    int idxROW = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //Bloqueamos las celdas PerfilId y Codigo
                        _ws.Cells[idxROW, 0].Protection.Locked = true;
                        _ws.Cells[idxROW, 0].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        _ws.Columns[0].Visible = false;

                        _ws.Cells[idxROW, 1].Protection.Locked = true;
                        _ws.Cells[idxROW, 1].FillColor = System.Drawing.Color.FromArgb(0xCC, 0xFF, 0xFF);
                        _ws.Cells[idxROW, 2].Protection.Locked = true;
                        _ws.Cells[idxROW, 3].Protection.Locked = false;

                        // Si es null no llenar celdas
                        if (dr[1] != DBNull.Value)
                        {
                            _ws.Cells[idxROW, 0].Value = Convert.ToString(dr[0]);
                            _ws.Cells[idxROW, 1].Value = Convert.ToString(dr[1]);
                            _ws.Cells[idxROW, 2].Value = Convert.ToString(dr[2]);
                            if (Convert.ToString(dr[3]) == "True") {
                                _ws.Cells[idxROW, 3].Value = 1;
                            }else{
                                _ws.Cells[idxROW, 3].Value = 0;
                            }
                            
                            //_ws.Cells[idxROW, 3].Value = _workbook.Worksheets[1].Range["A2"].Value;                                

                            //// depende del tipo de dato es como se llena (para que salga bien en el excel)
                            //if (col.IsNumerical)
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToDouble(dr[col.FieldName]);
                            //else if (col.IsPercent)
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToDouble(dr[col.FieldName]) / 100;
                            //else
                            //    _ws.Cells[idxROW, idxCOL].Value = Convert.ToString(dr[col.FieldName]);
                        }

                        idxROW = idxROW + 1;
                    }

                    // Con esto se le pone la proteccion al excel
                    //_wss.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    _ws.Protect("EPS$20150101", WorksheetProtectionPermissions.Default);
                    string filename = "Profiles";
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

                    DataTable dtSheet = new DataTable("Perfiles");
                    DataColumn workCol = dtSheet.Columns.Add("PerfilId", typeof(Int32));
                    workCol.AllowDBNull = false;
                    //workCol.Unique = true;

                    dtSheet.Columns.Add("CODIGO", typeof(String));
                    dtSheet.Columns.Add("DESCRIPCION", typeof(String));
                    dtSheet.Columns.Add("ESADMINISTRADOR", typeof(String));
                    dtSheet.Columns.Add("REALIZAENCUESTAS", typeof(String));
                    dtSheet.Columns.Add("ACTIVO", typeof(String));                                           

                    // Obtener el valor de la celda con 		
                    for (int idxRow = 1; idxRow <= _ws.Rows.LastUsedIndex; idxRow++)
                    {
                        string sPerfilId = _ws.Cells[idxRow, 0].Value.TextValue.ToString();
                        string sCodigo= _ws.Cells[idxRow, 1].Value.TextValue.ToString();
                        string sDescripcion = _ws.Cells[idxRow, 2].Value.TextValue.ToString();
                        string sEsAdministrador = _ws.Cells[idxRow, 3].Value.TextValue.ToString();
                        string sRealizaEncuestas = _ws.Cells[idxRow, 4].Value.TextValue.ToString();
                        string sActivo = _ws.Cells[idxRow, 5].Value.ToString();

                        if (sCodigo != "" && sDescripcion != "")
                        {
                            dtSheet.Rows.Add(sPerfilId, sCodigo, sDescripcion, sEsAdministrador, sRealizaEncuestas, sActivo);
                        }
                    }

                    //Guardamos la lista de materiales en una tabla temporal para despues usarlo al guardar el archivo.                    
                    //DataSet dtLista = new DataSet("ListaPerfiles");
                    //dtLista.Tables.Add(dtSheet.Copy());

                    List<Perfiles> lperf = new List<Perfiles>();
                    foreach (DataRow dr in dtSheet.Rows)
                    {
                        Perfiles perf = new Perfiles();
                        perf.PerfilId = Convert.ToInt32(dr["PerfilId"]);
                        perf.Codigo = dr["CODIGO"].ToString();
                        perf.DESCRIPCION = dr["DESCRIPCION"].ToString();
                        perf.EsAdministrador = Convert.ToBoolean(dr["ESADMINISTRADOR"].ToString());
                        perf.RealizaEncuestas = Convert.ToBoolean(dr["REALIZAENCUESTAS"].ToString());
                        perf.ACTIVO = Convert.ToInt32(dr["ACTIVO"]);
                        lperf.Add(perf);
                    }
                    //string xmlListaPerfiles = dtLista.GetXml();                   

                    try
                    {
                        var BPerfile = new PerfilDa();
                        var res = BPerfile.UpdPerfilXlS(lperf, LoginInfo.CurrentUsuario.UsuarioId);
                        if (res == 1)
                        {
                            xgrdPerfiles.JSProperties["cpAlertMessage"] = "Update";
                        }
                        else
                        {
                            xgrdPerfiles.JSProperties["cpAlertMessage"] = "Error";
                        }
                    }
                    catch (Exception ex)
                    {
                        xgrdPerfiles.JSProperties["cpAlertMessage"] = ex.Message;
                    }

                }
            }
            return "";
        }
        #endregion

        protected void xgrdPerfiles_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {            
            AplicacionDa BAplicacion = new AplicacionDa();
            ASPxComboBox cmbApplicationEdit = ((ASPxComboBox)xgrdPerfiles.FindEditFormTemplateControl("cmbApplicationEdit"));
            cmbApplicationEdit.TextField = "Codigo";
            cmbApplicationEdit.ValueField = "AppId";
            cmbApplicationEdit.DataSource = BAplicacion.GetCatalog("", "", true);
            cmbApplicationEdit.DataBind();           
        }

        protected void cmbApplicationEdit_DataBound(object sender, EventArgs e)
        {

            if (AllowBound == true)
            {
                ASPxTextBox xtxtPerfilEdit = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtPerfilEdit"));
                string sPerfilId = xtxtPerfilEdit.Text;
                int PerfilId = 0;


                if (PerfilApps == null)
                {
                    PerfilApps = new List<Entity.Perfil_Apps>();
                }
                if (sPerfilId != string.Empty)
                {
                    PerfilId = int.Parse(sPerfilId);
                    List<Entity.Perfil_Apps> ICD = new List<Entity.Perfil_Apps>();
                    var BID = new Perfil_AppsDa();
                    PerfilApps = BID.GetListPerfilApps(PerfilId, true);
                    var xgrdApps = ((ASPxGridView)xgrdPerfiles.FindEditFormTemplateControl("xgrdApps"));
                    xgrdApps.DataSource = PerfilApps;
                    xgrdApps.DataBind();
                    AllowBound = false;
                }
            }

        }

        protected void xgrdApps_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var xgrdApps = ((ASPxGridView)xgrdPerfiles.FindEditFormTemplateControl("xgrdApps"));
            xgrdApps.JSProperties["cpAlertMessage"] = string.Empty;

            var pars = e.Parameters;

            ASPxTextBox xtxtPerfilEdit = ((ASPxTextBox)xgrdPerfiles.FindEditFormTemplateControl("xtxtPerfilEdit"));
            int PerfilId = int.Parse(xtxtPerfilEdit.Text);

            ASPxComboBox cmbApplicationEdit = ((ASPxComboBox)xgrdPerfiles.FindEditFormTemplateControl("cmbApplicationEdit"));
            int IdAppSelected = 0;
            string AppSelected = "";
            if (cmbApplicationEdit.SelectedIndex != -1)
            {
                IdAppSelected = int.Parse(cmbApplicationEdit.SelectedItem.Value.ToString());
                AppSelected = cmbApplicationEdit.SelectedItem.Text.ToString();
            }           
            
            
            if (pars == "Save" && IdAppSelected != 0)
            {
                bool bFound = false;

                Entity.Perfil_Apps oPerfilApps = new Entity.Perfil_Apps();
                foreach (Entity.Perfil_Apps item in PerfilApps)
                {
                    if (item.AppId == IdAppSelected)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound == true)
                {
                    xgrdApps.JSProperties["cpAlertMessage"] = "Exist";
                }
                else
                {
                    oPerfilApps.PerfilId = PerfilId;
                    oPerfilApps.AppId = IdAppSelected;                    
                    oPerfilApps.Nombre = AppSelected;
                    oPerfilApps.Activo = true;
                    PerfilApps.Add(oPerfilApps);
                }
            }
            else if( pars == "Save" && IdAppSelected == 0)
            {
                xgrdApps.JSProperties["cpAlertMessage"] = "SelectOne";
            }

            if (pars == "Delete")
            {
                xgrdApps.DataSource = null;
                PerfilApps.Clear();
            }
            else {
                xgrdApps.DataSource = PerfilApps;
            }

            
            xgrdApps.DataBind();

        }

        protected void xgrdPerfiles_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            PerfilApps = new List<Entity.Perfil_Apps>();
            PerfilApps.Clear();
            AllowBound = true;
        }

        protected void xgrdPerfiles_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            AllowBound = true;
        }
    }
}