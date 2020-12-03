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
    public partial class ctrolProds : BasePage
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
            xgrdProds.BeginUpdate();
            try
            {
                xgrdProds.ClearSort();
            }
            finally
            {
                xgrdProds.EndUpdate();
            }
        }

        public void fillGrid()
        {
            ASPxTextBox xtxtFolio = ASPxNavBar2.Groups[0].FindControl("xtxtFolio") as ASPxTextBox;
            ASPxDateEdit xDateFechaFin = ASPxNavBar2.Groups[0].FindControl("xDateFechaFin") as ASPxDateEdit;
            ASPxDateEdit xDateFechaIni = ASPxNavBar2.Groups[0].FindControl("xDateFechaIni") as ASPxDateEdit;

            var BCtrlProd = new ControlProductosda();
            var oListProd = BCtrlProd.GetCtrlProd(xDateFechaIni.Text, xDateFechaFin.Text, xtxtFolio.Text);
            xgrdProds.DataSource = oListProd;
            xgrdProds.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            xgrdProds.JSProperties["cpAlertMessage"] = string.Empty;
            if (!IsPostBack)
            {
                Session.Remove("ctrlProdsID");
                Form.Attributes.Add("autocomplete", "off");
                ApplyLayout();
                AllowBound = false;

                DateTime fActual = new DateTime();
                fActual = DateTime.Now;
                ASPxDateEdit xDateFechaFin = ASPxNavBar2.Groups[0].FindControl("xDateFechaFin") as ASPxDateEdit;
                ASPxDateEdit xDateFechaIni = ASPxNavBar2.Groups[0].FindControl("xDateFechaIni") as ASPxDateEdit;
                xDateFechaFin.Date = fActual;
                xDateFechaIni.Date = fActual.AddDays(-15);

                foreach (NavBarGroup group in ASPxNavBar2.Groups)
                    group.Expanded = false;
            }
            fillGrid();
        }

        protected void xgrdProds_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("ctrlProdsID");

                e.Cell.Text = string.Format("<input type='checkbox' class='chk' id='chk{0}'>", id);
            }

            if (e.DataColumn.FieldName == "fechaSolicitud")
            {
                e.Cell.Text = e.GetValue("fechaSolicitud").ToString().Substring(0, 10);
            }

            //if (e.DataColumn.FieldName == "sstatus")
            //{
            //    string sstatus = e.GetValue("sstatus").ToString();

            //    switch (sstatus)
            //    {
            //        case "Open":
            //        case "Pending to approve":
            //        case "Waiting Quote":
            //        case "Read":
            //            e.Cell.Style.Add("color", "#0099ff");
            //            break;
            //        case "Cancelled":
            //        case "Rejected":
            //            e.Cell.Style.Add("color", "#ff3300");
            //            break;
            //        case "Quote Done":
            //        case "Done Quote":
            //        case "Assigned":
            //            e.Cell.Style.Add("color", "#009933");
            //            break;
            //    }

            //}
            //if (e.DataColumn.Name == "sstatus")
            //{
            //    string sstatus = e.GetValue("sstatus").ToString();
            //    string background = "background-color:";
            //    switch (sstatus)
            //    {
            //        case "Open":
            //        case "Pending to approve":
            //        case "Waiting Quote":
            //        case "Read":
            //            background += "#0099ff";
            //            break;
            //        case "Cancelled":
            //        case "Rejected":
            //            background += "#ff3300";
            //            break;
            //        case "Quote Done":
            //        case "Done Quote":
            //        case "Assigned":
            //            background += "#009933";
            //            break;
            //    }
            //    e.Cell.Text = "<span class='dot' style='" + background + "'></span>";
            //}
        }

        protected void xgrdProds_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            //if (e.Expanded)
            //{
            //    int indx = e.VisibleIndex;
            //    List<CotizacionesSol> obj = (List<CotizacionesSol>)xgrdCotizacion.DataSource;
            //    CotizacionesSol cS = obj[indx];
            //    ASPxGridView xgrdCotizacion_2 = (ASPxGridView)xgrdCotizacion.FindDetailRowTemplateControl(indx, "xgrdCotizacion_2");
            //    xgrdCotizacion_2.DataSource = cS.productos;
            //    xgrdCotizacion_2.DataBind();
            //}
        }

        protected void CallbackPanelDisable_Callback(object sender, CallbackEventArgsBase e)
        {
        }

        protected void CallbackPanelDisableAll_Callback(object sender, CallbackEventArgsBase e)
        {
        }

        protected void imgNew_Click(object sender, ImageClickEventArgs e)
        {
            Session["ctrlProdsID"] = 0;
            Response.Redirect("ctrolProds_det.aspx");
        }

        protected void imgEditar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgEditar = (ImageButton)sender;
            string arguments = imgEditar.CommandArgument;
            Session["ctrlProdsID"] = arguments;
            Response.Redirect("ctrolProds_det.aspx");
        }
    }
}