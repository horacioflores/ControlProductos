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
    public partial class ctrolProds_det : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ctrlProdsID"] == null)
            {
                Response.Redirect("ctrolProds.aspx");
            }
            else
            {
                init();
            }
        }

        private void init()
        {
            if (!IsPostBack)
            {
                int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);


                var BctrlProd = new ControlProductosda();
                List<Entity.ControlProductos> oList = BctrlProd.GetCtrlProducto(ctrlProdsID.ToString());

                //var BEmpleados = new EmpleadosDa();
                //cmbEmpleadoEdit.TextField = "NombreCompleto";
                //cmbEmpleadoEdit.ValueField = "Codigo";
                //cmbEmpleadoEdit.DataSource = BEmpleados.GetCatalog("", "", "", "", true);
                //cmbEmpleadoEdit.DataBind();

                //var BPlantas = new PlantaDa();
                //ASPxCmbPlant.TextField = "Descripcion";
                //ASPxCmbPlant.ValueField = "Codigo";
                //ASPxCmbPlant.DataSource = BPlantas.GetCatalog("", "", true);
                //ASPxCmbPlant.DataBind();

                //var BMonedas = new MonedaDa();
                //cmbMonedaEdit.TextField = "Nombre";
                //cmbMonedaEdit.ValueField = "Codigo";
                //cmbMonedaEdit.DataSource = BMonedas.GetCombo();
                //cmbMonedaEdit.DataBind();

                if (ctrlProdsID > 0)
                {

                    foreach (Entity.ControlProductos ctrlP in oList)
                    {
                        if (ctrlProdsID == ctrlP.ctrlProdsID)
                        {
                            switch (ctrlP.sts_Prods)
                            {
                                case "Abierto":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                                    break;
                                case "Revisado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-eye-open' style='padding-right:5px;'></i>"+ ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Revisado para su proceso</span>";
                                    break;
                                case "Cerrado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-lock' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Cerrado por "+ ctrlP.usuario + " el "+ctrlP.ModFecha+"</span>";
                                    break;
                                case "Cancelado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-warning docEstatus' ><i class='glyphicon glyphicon-ban-circle' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Cancelado por " + ctrlP.usuario + " el " + ctrlP.ModFecha + "</span>";
                                    break;
                                case "Aprobado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-success docEstatus' ><i class='glyphicon glyphicon-ok' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Aprobado por " + ctrlP.usuario + " el " + ctrlP.ModFecha + "</span>";
                                    break;
                                case "Pendiente por aprobación":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-primary docEstatus' ><i class='glyphicon glyphicon-dashboard' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el aprobador</span>";
                                    break;
                                case "Rechazado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-danger docEstatus' ><i class='glyphicon glyphicon-remove' style='padding-right:5px;'></i>" + ctrlP.ctrlProdsID + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Rechazado por " + ctrlP.usuario +"</span>";
                                    break;
                            }

                            lblnDoc.Text = ctrlP.noDocumento;
                            lblDocSolicitante.Text = ctrlP.usuario;
                            lblDocFechaSol.Text = ctrlP.fechaSolicitud;

                            //xtxtFolio.Text = cot.folio_cotizacion;
                            //ASPxStatus.Text = cot.sstatus;

                            //Validacion

                            //if (LoginInfo.CurrentPerfil.Codigo.ToUpper() == "ADMIN")
                            //{
                            //    if (cot.codigoStatus == "002")
                            //    {
                            //        btnSend.Visible = false;
                            //        btnSave.Visible = false;
                            //        btnEnviarProveedor.Visible = false;
                            //        btnRechazar.Visible = false;
                            //        btnRegresar.Visible = true;
                            //        btnCancelar.Visible = false;
                            //    }
                            //    else
                            //    {
                            //        btnSend.Visible = false;
                            //        btnSave.Visible = false;
                            //        btnEnviarProveedor.Visible = false;
                            //        btnRechazar.Visible = false;
                            //        btnRegresar.Visible = true;
                            //        btnCancelar.Visible = false;
                            //    }
                            //}
                            //else if (LoginInfo.CurrentPerfil.Codigo.ToUpper() == "GERENTE")
                            //{
                            //    if (cot.codigoStatus == "002")
                            //    {
                            //        btnSend.Visible = false;
                            //        btnSave.Visible = false;
                            //        btnEnviarProveedor.Visible = true;
                            //        btnRechazar.Visible = true;
                            //        btnRegresar.Visible = true;
                            //        btnCancelar.Visible = true;
                            //    }
                            //    else
                            //    {
                            //        btnSend.Visible = false;
                            //        btnSave.Visible = false;
                            //        btnEnviarProveedor.Visible = false;
                            //        btnRechazar.Visible = false;
                            //        btnRegresar.Visible = true;
                            //        btnCancelar.Visible = false;
                            //    }
                            //}
                            //else
                            //{
                            //    btnSend.Visible = false;
                            //    btnSave.Visible = false;
                            //    btnEnviarProveedor.Visible = false;
                            //    btnRechazar.Visible = false;
                            //    btnRegresar.Visible = true;
                            //    btnCancelar.Visible = false;
                            //}

                            //xtxtProyecto.Text = cot.proyecto;
                            //ASPxtxtUser.Text = cot.usuario;
                            //ASPxtxtNotes.Value = cot.notas;

                            //ListEditItem oItm = cmbEmpleadoEdit.Items.FindByValue(cot.responsableCodigo);
                            //if (oItm != null)
                            //{
                            //    oItm.Selected = true;
                            //}

                            //ListEditItem oItmPlant = ASPxCmbPlant.Items.FindByValue(cot.codigoPlanta);
                            //if (oItmPlant != null)
                            //{
                            //    oItmPlant.Selected = true;
                            //}

                            //DateTime fechaOrig = new DateTime(Convert.ToInt32(cot.fechaOrig.Substring(6, 4)), Convert.ToInt32(cot.fechaOrig.Substring(3, 2)), Convert.ToInt32(cot.fechaOrig.Substring(0, 2)), Convert.ToInt32(cot.fechaOrig.Substring(11, 2)), Convert.ToInt32(cot.fechaOrig.Substring(14, 2)), Convert.ToInt32(cot.fechaOrig.Substring(17, 2)));
                            //DateTime fechareq = new DateTime(Convert.ToInt32(cot.fechaReq.Substring(6, 4)), Convert.ToInt32(cot.fechaReq.Substring(3, 2)), Convert.ToInt32(cot.fechaReq.Substring(0, 2)), Convert.ToInt32(cot.fechaOrig.Substring(11, 2)), Convert.ToInt32(cot.fechaOrig.Substring(14, 2)), Convert.ToInt32(cot.fechaOrig.Substring(17, 2)));
                            //ListEditItem oItmMoneda = cmbMonedaEdit.Items.FindByValue(cot.codigoMoneda);
                            //if (oItmMoneda != null)
                            //{
                            //    oItmMoneda.Selected = true;
                            //}

                            //xDateOriginated.Text = fechaOrig.ToString("dd-MM-yyyy HH:mm");
                            //xDateRequiered.Value = fechareq;

                            //productos = cot.productos;
                            //proveedores = cot.proveedores;
                            //comentarios = cot.comentarios;
                            //archivos = cot.archivos;

                            //xgrdPartes.DataSource = cot.productos;
                            //xgrdPartes.DataBind();
                            //ApplyLayoutParte();

                            //xgrdProvedores.DataSource = cot.proveedores;
                            //xgrdProvedores.DataBind();
                            //ApplyLayoutProv();
                            //ApplyLayoutQuotes();
                            //chat();
                            //fillIndicadores(cot);
                            //break;
                        }
                    }
                }
                else
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>Abierto</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                    lblnDoc.Text = "";
                    lblDocSolicitante.Text = LoginInfo.CurrentUsuario.NombreCompleto;
                    lblDocFechaSol.Text = date.ToString("dd/MM/yyyy HH:mm");

                    //string newFolio = BCotizacion.GetConsecutivosolicitud();
                    //xtxtFolio.Text = newFolio;
                    //ASPxStatus.Text = "Open";
                    //DateTime fechaActual = DateTime.Now;
                    //xDateOriginated.Text = fechaActual.ToString("dd-MM-yyyy HH:mm");
                    //xDateRequiered.Value = fechaActual;
                    //ASPxtxtUser.Text = LoginInfo.CurrentUsuario.NombreCompleto;

                    ////Botones.
                    //btnSave.Visible = true;
                    //btnSend.Visible = true;
                    //btnRegresar.Visible = true;
                    //btnRechazar.Visible = false;
                    //btnCancelar.Visible = false;
                    //btnEnviarProveedor.Visible = false;


                    //productos = new List<cotizacion_prod>();
                    //proveedores = new List<cotizacion_proveedor>();
                    //comentarios = new List<Entity.cotizacion_comentarios>();
                    //archivos = new List<cotizacion_archivos>();
                }
                //productosToRel = new List<cotizacion_prodRelProv>();
            }
            else
            {
                //xgrdPartes.DataSource = productos;
                //xgrdPartes.DataBind();
                //ApplyLayoutParte();

                //xgrdProvedores.DataSource = proveedores;
                //xgrdProvedores.DataBind();
                //ApplyLayoutProv();
                //ApplyLayoutQuotes();

                //ApplyLayoutProdToRel();
                //chat();
            }
            //ParteDa BParte = new ParteDa();
            //if (cmbParteEdit != null)
            //{
            //    cmbParteEdit.TextField = "nombre";
            //    cmbParteEdit.ValueField = "codigo";
            //    cmbParteEdit.DataSource = BParte.GetCombo();
            //    cmbParteEdit.DataBind();
            //}
            //ProveedorDa BProveedor = new ProveedorDa();
            //if (ASPxCmbProv != null)
            //{
            //    ASPxCmbProv.TextField = "nombre";
            //    ASPxCmbProv.ValueField = "codigo";
            //    ASPxCmbProv.DataSource = BProveedor.GetCombo();
            //    ASPxCmbProv.DataBind();
            //}
            //if (ASPcmbFilterProv != null)
            //{
            //    List<Proveedor> filterProv = new List<Proveedor>();
            //    Proveedor all = new Proveedor();
            //    all.Codigo = "ALL";
            //    all.Nombre = "Todos";
            //    filterProv = BProveedor.GetCombo();
            //    filterProv.Add(all);

            //    ASPcmbFilterProv.TextField = "nombre";
            //    ASPcmbFilterProv.ValueField = "codigo";
            //    ASPcmbFilterProv.DataSource = filterProv;
            //    ASPcmbFilterProv.DataBind();

            //    if (proveedores.Count > 0)
            //    {
            //        ListEditItem oItm = ASPcmbFilterProv.Items.FindByValue(proveedores[0].ProveedorCodigo);
            //        if (oItm != null)
            //        {
            //            oItm.Selected = true;
            //        }
            //    }
            //}
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ctrolProds.aspx");
        }
    }
}