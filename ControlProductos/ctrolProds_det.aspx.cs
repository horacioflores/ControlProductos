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
        public List<_tipoArticulo> tiposArticulo
        {
            get
            {
                return (List<_tipoArticulo>)Session["CurrenttiposArticulo"];
            }
            set
            {
                Session["CurrenttiposArticulo"] = value;
            }
        }

        public List<Mantenimiento> mttos
        {
            get
            {
                return (List<Mantenimiento>)Session["CurrentMttos"];
            }
            set
            {
                Session["CurrentMttos"] = value;
            }
        }

        public List<Almacen> almnes
        {
            get
            {
                return (List<Almacen>)Session["CurrentAlmnes"];
            }
            set
            {
                Session["CurrentAlmnes"] = value;
            }
        }

        public List<Aprobacion> aprbnes
        {
            get
            {
                return (List<Aprobacion>)Session["Currentaprbnes"];
            }
            set
            {
                Session["Currentaprbnes"] = value;
            }
        }

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

            var BMaquina = new MaquinaDa();
            cmbMaquina.TextField = "Nombre";
            cmbMaquina.ValueField = "Codigo";
            cmbMaquina.DataSource = BMaquina.GetCombo();
            cmbMaquina.DataBind();

            var BSubcategoria1 = new SubCategoria1Da();
            cmbSubCat1.TextField = "Nombre";
            cmbSubCat1.ValueField = "Codigo";
            cmbSubCat1.DataSource = BSubcategoria1.GetCombo();
            cmbSubCat1.DataBind();

            var BSubcategoria2 = new SubCategoria2Da();
            cmbSubCat2.TextField = "Nombre";
            cmbSubCat2.ValueField = "Codigo";
            cmbSubCat2.DataSource = BSubcategoria2.GetCombo();
            cmbSubCat2.DataBind();

            var BSubcategoria3 = new SubCategoria3Da();
            cmbSubCat3.TextField = "Nombre";
            cmbSubCat3.ValueField = "Codigo";
            cmbSubCat3.DataSource = BSubcategoria3.GetCombo();
            cmbSubCat3.DataBind();

            var BUtilizado = new UtilizadoDa();
            cmbUtilizado.TextField = "Nombre";
            cmbUtilizado.ValueField = "Codigo";
            cmbUtilizado.DataSource = BUtilizado.GetCombo();
            cmbUtilizado.DataBind();

            var BDepartamento = new DepartamentoDa();
            cmbDepa.TextField = "Descripcion";
            cmbDepa.ValueField = "Codigo";
            cmbDepa.DataSource = BDepartamento.GetCombo();
            cmbDepa.DataBind();

            var BPlan = new PlanDa();
            cmbPlan.TextField = "Nombre";
            cmbPlan.ValueField = "Codigo";
            cmbPlan.DataSource = BPlan.GetCombo();
            cmbPlan.DataBind();

            var BMarca = new MarcaDa();
            cmbMarca.TextField = "Nombre";
            cmbMarca.ValueField = "Codigo";
            cmbMarca.DataSource = BMarca.GetCombo();
            cmbMarca.DataBind();

            var BProveedor = new ProveedorDa();
            cmbProveedor.TextField = "Nombre";
            cmbProveedor.ValueField = "Codigo";
            cmbProveedor.DataSource = BProveedor.GetCombo();
            cmbProveedor.DataBind();

            List<Unico> lUnico = new List<Unico>();
            Unico item = new Unico();
            item.value = true;
            item.text = "Si";
            Unico item2 = new Unico();
            item2.value = true;
            item2.text = "Si";
            lUnico.Add(item);
            lUnico.Add(item2);

            cmbUnico.TextField = "text";
            cmbUnico.ValueField = "value";
            cmbUnico.DataSource = lUnico;
            cmbUnico.DataBind();

            var BMoneda = new MonedaDa();
            cmbMoneda.TextField = "Nombre";
            cmbMoneda.ValueField = "Codigo";
            cmbMoneda.DataSource = BMoneda.GetCombo();
            cmbMoneda.DataBind();

            var BUM = new UMedidaDa();
            cmbCodigoUM.TextField = "Nombre";
            cmbCodigoUM.ValueField = "Codigo";
            cmbCodigoUM.DataSource = BUM.GetCombo();
            cmbCodigoUM.DataBind();

            var BPlaneador = new PlaneadorDa();
            cmbPlaneador.TextField = "Nombre";
            cmbPlaneador.ValueField = "Codigo";
            cmbPlaneador.DataSource = BPlaneador.GetCombo();
            cmbPlaneador.DataBind();

            var BComprador = new CompradorDa();
            cmbComprador.TextField = "Nombre";
            cmbComprador.ValueField = "Codigo";
            cmbComprador.DataSource = BComprador.GetCombo();
            cmbComprador.DataBind();

            if (!IsPostBack)
            {
                limpiarModalAprnes();
                int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);
                var BctrlProd = new ControlProductosda();
                List<Entity.ControlProductos> oList = BctrlProd.GetCtrlProducto(ctrlProdsID.ToString());

                if (ctrlProdsID > 0)
                {
                    foreach (Entity.ControlProductos ctrlP in oList)
                    {
                        if (ctrlProdsID == ctrlP.ctrlProdsID)
                        {
                            lblcodigoSts.Text = ctrlP.codigo_sts_Prods;
                            switch (ctrlP.sts_Prods)
                            {
                                case "Abierto":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                                    break;
                                case "Revisado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-eye-open' style='padding-right:5px;'></i>"+ ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Revisado para su proceso</span>";
                                    break;
                                case "Cerrado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-lock' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Cerrado por "+ ctrlP.usuario + " el "+ctrlP.ModFecha+"</span>";
                                    break;
                                case "Cancelado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-warning docEstatus' ><i class='glyphicon glyphicon-ban-circle' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Cancelado por " + ctrlP.usuario + " el " + ctrlP.ModFecha + "</span>";
                                    break;
                                case "Aprobado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-success docEstatus' ><i class='glyphicon glyphicon-ok' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Aprobado por " + ctrlP.usuario + " el " + ctrlP.ModFecha + "</span>";
                                    break;
                                case "Pendiente por aprobación":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-primary docEstatus' ><i class='glyphicon glyphicon-dashboard' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el aprobador</span>";
                                    break;
                                case "Rechazado":
                                    ltlSts.Text = "<span id='spanStatus' class='alert btn-danger docEstatus' ><i class='glyphicon glyphicon-remove' style='padding-right:5px;'></i>" + ctrlP.sts_Prods + "</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Rechazado por " + ctrlP.usuario +"</span>";
                                    break;
                            }

                            lblnDoc.Text = ctrlP.noDocumento;
                            lblDocSolicitante.Text = ctrlP.usuario;
                            lblDocFechaSol.Text = ctrlP.fechaSolicitud;
                            remplazaOtro.Text = ctrlP.remplazaOtro;

                            ListEditItem oItMaquina = cmbMaquina.Items.FindByValue(ctrlP.CodigoMaquina);
                            if (oItMaquina != null)
                            {
                                oItMaquina.Selected = true;
                            }

                            ListEditItem oItmSubCat1 = cmbSubCat1.Items.FindByValue(ctrlP.CodigoSubcategoria1);
                            if (oItmSubCat1 != null)
                            {
                                oItmSubCat1.Selected = true;
                            }

                            ListEditItem oItmSubCat2 = cmbSubCat2.Items.FindByValue(ctrlP.CodigoSubcategoria2);
                            if (oItmSubCat2 != null)
                            {
                                oItmSubCat2.Selected = true;
                            }

                            ListEditItem oItmSubCat3 = cmbSubCat3.Items.FindByValue(ctrlP.CodigoSubcategoria3);
                            if (oItmSubCat3 != null)
                            {
                                oItmSubCat3.Selected = true;
                            }

                            ListEditItem oItmUtilizado = cmbUtilizado.Items.FindByValue(ctrlP.CodigoUtilizado);
                            if (oItmUtilizado != null)
                            {
                                oItmUtilizado.Selected = true;
                            }

                            ListEditItem oItmDepto = cmbDepa.Items.FindByValue(ctrlP.CodigoDepto);
                            if (oItmDepto != null)
                            {
                                oItmDepto.Selected = true;
                            }
                            tiposArticulo = new List<_tipoArticulo>();
                            tiposArticulo = ctrlP.tiposArticulo;
                            xgrdTipoArticulo.DataSource = ctrlP.tiposArticulo;
                            xgrdTipoArticulo.DataBind();

                            txtDescripcion1.Text = ctrlP.descripcionUno;
                            txtDescripcion2.Text = ctrlP.descripcionDos;
                            txtDescripcionLarga.Text = ctrlP.descripcionDos;

                            ListEditItem oItmPlan = cmbPlan.Items.FindByValue(ctrlP.CodigoPlan);
                            if (oItmPlan != null)
                            {
                                oItmPlan.Selected = true;
                            }

                            txtcomoAyudarStockCero.Text = ctrlP.comoAyudarStockCero;
                            txtfuncionMaquina.Text = ctrlP.funcionMaquina;
                            txtCantidad.Text = ctrlP.cantidadOrden.ToString("0.##");
                            txtStockMin.Text = ctrlP.stockMinimo.ToString("0.##");
                            txtStockMax.Text = ctrlP.stockMaximo.ToString("0.##");

                            ListEditItem oItmMarca = cmbMarca.Items.FindByValue(ctrlP.CodigoMarca);
                            if (oItmMarca != null)
                            {
                                oItmMarca.Selected = true;
                            }

                            ListEditItem oItmProveedor = cmbProveedor.Items.FindByValue(ctrlP.CodigoProveedor);
                            if (oItmProveedor != null)
                            {
                                oItmProveedor.Selected = true;
                            }

                            ListEditItem oItmUnico= cmbUnico.Items.FindByValue(ctrlP.esUnico.ToString());
                            if (oItmUnico != null)
                            {
                                oItmUnico.Selected = true;
                            }
                            DateTime fechaCot = new DateTime(Convert.ToInt32(ctrlP.fechaCotizacion.Substring(6, 4)), Convert.ToInt32(ctrlP.fechaCotizacion.Substring(3, 2)), Convert.ToInt32(ctrlP.fechaCotizacion.Substring(0, 2)));
                            xDateFechaCot.Date = fechaCot;
                            txtPrecioU.Text = ctrlP.precioUnitario.ToString("0.##");
                            txtDiasEntrega.Text = ctrlP.diasEntrega.ToString();

                            ListEditItem oItmMoneda = cmbMoneda.Items.FindByValue(ctrlP.Codigomoneda);
                            if (oItmMoneda != null)
                            {
                                oItmMoneda.Selected = true;
                            }
                            lblTotal.Text = ctrlP.total.ToString("0.##");

                            mttos = new List<Mantenimiento>();
                            mttos = ctrlP.mantenimientos;
                            xgrdMtto.DataSource = ctrlP.mantenimientos;
                            xgrdMtto.DataBind();

                            almnes = new List<Almacen>();
                            almnes = ctrlP.almacenes;
                            xgrdAlmacen.DataSource = ctrlP.almacenes;
                            xgrdAlmacen.DataBind();

                            txtFichaSeguridad.Text = ctrlP.fichaDatoSeguridad;

                            ListEditItem oItmUM = cmbCodigoUM.Items.FindByValue(ctrlP.CodigoUM);
                            if (oItmUM != null)
                            {
                                oItmUM.Selected = true;
                            }

                            txtConteoCiclico.Text = ctrlP.conteoCiclico;
                            txtAlmacenamientoExt.Text = ctrlP.almacenamientoExternoPosible;

                            ListEditItem oItmPlaneador = cmbPlaneador.Items.FindByValue(ctrlP.codigoPlaneador);
                            if (oItmPlaneador != null)
                            {
                                oItmPlaneador.Selected = true;
                            }

                            ListEditItem oItmComprador = cmbComprador.Items.FindByValue(ctrlP.codigoComprador);
                            if (oItmComprador != null)
                            {
                                oItmComprador.Selected = true;
                            }

                            txtFichaInv.Text = ctrlP.fichaInventario;
                            txtMultiplo.Text = ctrlP.multiplo;
                            txtHojaSeguridad.Text = ctrlP.hojaSeguridad;
                            txtCodigoArticulo.Text = ctrlP.codigoArticulo;

                            aprbnes = new List<Aprobacion>();
                            aprbnes = ctrlP.aprobaciones;
                            xgrdAprobaciones.DataSource = ctrlP.aprobaciones;
                            xgrdAprobaciones.DataBind();

                            break;
                        }   
                    }
                }
                else
                {
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    lblcodigoSts.Text = "0001";
                    ltlSts.Text = "<span id='spanStatus' class='alert btn-info docEstatus'><i class='glyphicon glyphicon-edit' style='padding-right:5px;'></i>Abierto</span><span style='position: absolute; left: 250px; color:#FBFBFB;padding:2px 0px;'>:Pendiente por el autor para terminar la captura</span>";
                    lblnDoc.Text = "";
                    lblDocSolicitante.Text = LoginInfo.CurrentUsuario.NombreCompleto;
                    lblDocFechaSol.Text = date.ToString("dd/MM/yyyy HH:mm");
                    remplazaOtro.Text = "";
                    tiposArticulo = new List<_tipoArticulo>();
                    mttos = new List<Mantenimiento>();
                    almnes = new List<Almacen>();

                    txtDescripcion1.Text = "";
                    txtDescripcion2.Text = "";
                    txtDescripcionLarga.Text = "";

                    txtcomoAyudarStockCero.Text = "";
                    txtfuncionMaquina.Text = "";
                    txtCantidad.Text = "0";
                    txtStockMin.Text = "0";
                    txtStockMax.Text = "0";

                    txtPrecioU.Text = "0";
                    txtDiasEntrega.Text = "0";

                    lblTotal.Text = "0";

                    txtFichaSeguridad.Text = "";

                    txtConteoCiclico.Text = "";
                    txtAlmacenamientoExt.Text = "";

                    txtFichaInv.Text = "";
                    txtMultiplo.Text = "";
                    txtHojaSeguridad.Text = "";
                    txtCodigoArticulo.Text = "";

                    aprbnes = new List<Aprobacion>();


                    //productos = new List<cotizacion_prod>();
                    //proveedores = new List<cotizacion_proveedor>();
                    //comentarios = new List<Entity.cotizacion_comentarios>();
                    //archivos = new List<cotizacion_archivos>();
                }
                //productosToRel = new List<cotizacion_prodRelProv>();
            }
            else
            {
                xgrdTipoArticulo.DataSource = tiposArticulo;
                xgrdTipoArticulo.DataBind();
                xgrdMtto.DataSource = mttos;
                xgrdMtto.DataBind();
                xgrdAlmacen.DataSource = almnes;
                xgrdAlmacen.DataBind();
                xgrdAprobaciones.DataSource = aprbnes;
                xgrdAprobaciones.DataBind();
            }
        }

        public void limpiarModalAprnes()
        {
            txtCodigoAprobacionAdd.Text = "";
            txtPasoAdd.Text = "";
            txtTituloAdd.Text = "";
            txtAccionAdd.Text = "";
            txtComentarioAdd.Text = "";

            var BPosicion = new PosicionDa();
            cmbPuestoAdd.TextField = "Descripcion";
            cmbPuestoAdd.ValueField = "Codigo";
            List<Posicion> posiciones = BPosicion.GetCombo(true);
            cmbPuestoAdd.DataSource = posiciones;
            cmbPuestoAdd.DataBind();

            if (posiciones.Count > 0)
            {
                ListEditItem iPuesto = cmbPuestoAdd.Items.FindByValue(posiciones[0].Codigo);
                if (iPuesto != null)
                {
                    iPuesto.Selected = true;
                }
                var BEmpleado = new EmpleadosDa();
                cmbEmpleadoAdd.TextField = "NombreCompleto";
                cmbEmpleadoAdd.ValueField = "EmpleadoId";
                cmbEmpleadoAdd.DataSource = BEmpleado.GetCombo(posiciones[0].Codigo);
                cmbEmpleadoAdd.DataBind();

                if (cmbEmpleadoAdd.Items.Count > 0)
                {
                    ListEditItem iEmp = cmbEmpleadoAdd.Items.FindByValue(cmbEmpleadoAdd.Items[0].Value);
                    if (iEmp != null)
                    {
                        iEmp.Selected = true;
                    }
                }
            }

            DateTime fechaActual = DateTime.Now;
            xDateFechaNotAdd.Date = fechaActual;
            xDateFechaAccionAdd.Date = fechaActual;
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ctrolProds.aspx");
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            int ctrlProdsID = Convert.ToInt32(Session["ctrlProdsID"]);
            Entity.ControlProductos ctrlProd = new Entity.ControlProductos();

            ctrlProd.ctrlProdsID = ctrlProdsID;
            ctrlProd.noDocumento = lblnDoc.Text;
            ctrlProd.codigoSolicitante = LoginInfo.CurrentUsuario.Codigo;
            ctrlProd.fechaSolicitud = lblDocFechaSol.Text.Substring(6, 4) + lblDocFechaSol.Text.Substring(3, 2) + lblDocFechaSol.Text.Substring(0, 2) + " " + lblDocFechaSol.Text.Substring(11, 5);
            ctrlProd.remplazaOtro = remplazaOtro.Text;
            ctrlProd.cualArticulo = cualArticulo.Text;
            ctrlProd.CodigoMaquina = cmbMaquina.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria1 = cmbSubCat1.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria2 = cmbSubCat2.SelectedItem.Value.ToString();
            ctrlProd.CodigoSubcategoria3 = cmbSubCat2.SelectedItem.Value.ToString();
            ctrlProd.CodigoUtilizado = cmbUtilizado.SelectedItem.Value.ToString();
            ctrlProd.CodigoDepto = cmbDepa.SelectedItem.Value.ToString();
            ctrlProd.descripcionUno = txtDescripcion1.Text;
            ctrlProd.descripcionDos = txtDescripcion2.Text;
            ctrlProd.descripcionLargo = txtDescripcionLarga.Text;
            ctrlProd.CodigoPlan = cmbPlan.SelectedItem.Value.ToString();
            ctrlProd.comoAyudarStockCero = txtcomoAyudarStockCero.Text;
            ctrlProd.funcionMaquina = txtfuncionMaquina.Text;
            ctrlProd.cantidadOrden = (txtCantidad.Text == "")?0:Convert.ToDecimal(txtCantidad.Text);
            ctrlProd.stockMinimo = (txtStockMin.Text == "") ? 0 : Convert.ToDecimal(txtStockMin.Text);
            ctrlProd.stockMaximo = (txtStockMax.Text == "") ? 0 : Convert.ToDecimal(txtStockMax.Text);
            ctrlProd.CodigoMarca = cmbMarca.SelectedItem.Value.ToString();
            ctrlProd.CodigoProveedor = cmbProveedor.SelectedItem.Value.ToString();
            ctrlProd.esUnico = Convert.ToBoolean(cmbUnico.SelectedItem.Value);
            ctrlProd.fechaCotizacion = (xDateFechaCot.Value.ToString() == "")?DateTime.Now.ToString("yyyyMMdd"): xDateFechaCot.Value.ToString();
            ctrlProd.precioUnitario = (txtPrecioU.Text == "") ? 0 : Convert.ToDecimal(txtPrecioU.Text);
            ctrlProd.diasEntrega = (txtDiasEntrega.Text == "") ? 0 : Convert.ToInt32(txtDiasEntrega.Text);
            ctrlProd.Codigomoneda = cmbMoneda.SelectedItem.Value.ToString();
            ctrlProd.total = (lblTotal.Text == "") ? 0 : Convert.ToDecimal(lblTotal.Text);
            ctrlProd.fichaDatoSeguridad = txtFichaSeguridad.Text;
            ctrlProd.CodigoUM = cmbCodigoUM.SelectedItem.Value.ToString();
            ctrlProd.conteoCiclico = txtConteoCiclico.Text;
            ctrlProd.almacenamientoExternoPosible = txtAlmacenamientoExt.Text;
            ctrlProd.codigoPlaneador = cmbPlaneador.SelectedItem.Value.ToString();
            ctrlProd.codigoComprador = cmbComprador.SelectedItem.Value.ToString();
            ctrlProd.fichaInventario = txtFichaInv.Text;
            ctrlProd.multiplo = txtMultiplo.Text;
            ctrlProd.hojaSeguridad = txtHojaSeguridad.Text;
            ctrlProd.codigoArticulo = txtCodigoArticulo.Text;
            ctrlProd.comentarios = txtComentarios.Text;
            ctrlProd.codigo_sts_Prods = lblcodigoSts.Text;

            ctrlProd.tiposArticulo = tiposArticulo;
            ctrlProd.mantenimientos = mttos;
            ctrlProd.almacenes = almnes;

            List<Aprobacion> aprbInsert = new List<Aprobacion>();

            foreach (Aprobacion prb in aprbnes)
            {
                Aprobacion nuevo = new Aprobacion();
                nuevo = prb;
                nuevo.fechaNotificacion = prb.fechaNotificacion.Substring(6, 4) + prb.fechaNotificacion.Substring(3, 2) + prb.fechaNotificacion.Substring(0, 2);
                nuevo.fechaAccion = prb.fechaAccion.Substring(6, 4) + prb.fechaAccion.Substring(3, 2) + prb.fechaAccion.Substring(0, 2);
                aprbInsert.Add(nuevo);
            }

            ctrlProd.aprobaciones = aprbInsert;

            var BctrlProd = new ControlProductosda();
            string strScript;
            if (ctrlProd.ctrlProdsID == 0)
            {
                var regreso = BctrlProd.InsCtrlP(ctrlProd, LoginInfo.CurrentUsuario.UsuarioId.ToString());
                if (regreso > 0)
                {
                    strScript = "swal('Information', 'The product has been success registered!', 'success');";
                }
                else
                {
                    strScript = "swal('Information', 'There was an error registering the product!', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InsertSucces", strScript, true);
                return;
            }
            else
            {
                var regreso = BctrlProd.UpdCtrlP(ctrlProd, LoginInfo.CurrentUsuario.UsuarioId.ToString());
                if (regreso > 0)
                {
                    strScript = "swal('Information', 'The product has been success updated!', 'success');";
                }
                else
                {
                    strScript = "swal('Information', 'There was an error updating the product!', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateSucces", strScript, true);
                return;
            }
        }

        protected void xgrdTipoArticulo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdTipoArticulo.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            string noDocumento = lblnDoc.Text;
            string codigo = txtCodigoArticuloAdd.Text;
            string tipo = txtTipoArticuloAdd.Text;
            string m = txtMAdd.Text;
            string n = txtNAdd.Text;
            string comentarios = txtComentariosAdd.Text;


            if (pars == "Save" && (tipo != "" && codigo != ""))
            {
                bool bFound = false;
                _tipoArticulo tipoArt = new _tipoArticulo();

                foreach (_tipoArticulo item in tiposArticulo)
                {
                    if (item.codigoTipoArticulo == codigo)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (bFound == true)
                {
                    xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "Exist";
                }
                else
                {

                    tipoArt.tipoArticuloID = 0;
                    tipoArt.noDocumento = noDocumento;
                    tipoArt.codigoTipoArticulo = codigo;
                    tipoArt.tipoArticulo = tipo;
                    tipoArt.M = m;
                    tipoArt.N = n;
                    tipoArt.comentarios = comentarios;
                    tiposArticulo.Add(tipoArt);
                }
            }
            else if (pars == "Save" && (tipo == "" && codigo == ""))
            {
                xgrdTipoArticulo.JSProperties["cpAlertMessage"] = "SelectOne";
            }

            if (pars == "Delete")
            {
                xgrdTipoArticulo.DataSource = null;
                tiposArticulo.Clear();
            }
            else
            {
                if (pars != "Save")
                {
                    var Valores = e.Parameters;
                    string[] data = Valores.Split(',');

                    foreach (string d in data)
                    {
                        string valor = d.Replace("chk", "");
                        string[] datos = valor.Split('-');
                        string idtipoarticulo = datos[0];
                        string folio = datos[1];
                        tiposArticulo = tiposArticulo.FindAll(p => p.codigoTipoArticulo != folio);
                    }
                }

                xgrdTipoArticulo.DataSource = tiposArticulo;
            }

            txtCodigoArticuloAdd.Text = "";
            txtTipoArticuloAdd.Text = "";
            txtMAdd.Text = "";
            txtNAdd.Text = "";
            txtComentariosAdd.Text = "";

            xgrdTipoArticulo.DataBind();
        }

        protected void xgrdTipoArticulo_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("tipoArticuloID").ToString() + "-" + e.GetValue("codigoTipoArticulo").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkArt' id='chk{0}'>", id);
            }
        }

        protected void xgrdMtto_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdMtto.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            string noDocumento = lblnDoc.Text;
            string codigo = txtCodigoMttoAdd.Text;
            string especificacion = txtEspecificacionAdd.Text;
            string responsable = txtResponsableAdd.Text;
            string clasif = txtCasificacionAdd.Text;
            string notas = txtNotasAdd.Text;

            if (pars == "Save" && (especificacion != "" && codigo != ""))
            {
                bool bFound = false;
                Mantenimiento mtto = new Mantenimiento();

                foreach (Mantenimiento item in mttos)
                {
                    if (item.codigoMantenimiento == codigo)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (bFound == true)
                {
                    xgrdMtto.JSProperties["cpAlertMessage"] = "Exist";
                }
                else
                {

                    mtto.MantenimientoID = 0;
                    mtto.noDocumento = noDocumento;
                    mtto.codigoMantenimiento = codigo;
                    mtto.especificacion = especificacion;
                    mtto.notas = notas;
                    mtto.clasificacion = clasif;
                    mtto.responsable = responsable;
                    mttos.Add(mtto);
                }
            }
            else if (pars == "Save" && (especificacion == "" && codigo == ""))
            {
                xgrdMtto.JSProperties["cpAlertMessage"] = "SelectOne";
            }

            if (pars == "Delete")
            {
                xgrdMtto.DataSource = null;
                mttos.Clear();
            }
            else
            {
                if (pars != "Save")
                {
                    var Valores = e.Parameters;
                    string[] data = Valores.Split(',');

                    foreach (string d in data)
                    {
                        string valor = d.Replace("chk", "");
                        string[] datos = valor.Split('-');
                        string idmtto = datos[0];
                        string folio = datos[1];
                        mttos = mttos.FindAll(p => p.codigoMantenimiento != folio);
                    }
                }

                xgrdMtto.DataSource = mttos;
            }

            xgrdMtto.DataBind();
        }

        protected void xgrdMtto_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("MantenimientoID").ToString() + "-" + e.GetValue("codigoMantenimiento").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkMtto' id='chk{0}'>", id);
            }
        }

        protected void xgrdAlmacen_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdAlmacen.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            string noDocumento = lblnDoc.Text;
            string codigo = txtCodigoAlmacenAdd.Text;
            string especificacion = txtEspecificacionAlMAdd.Text;
            string responsable = txtResponsableALMAdd.Text;
            string clasif = txtCasificacionALMAdd.Text;
            string notas = txtNotasALMAdd.Text;

            if (pars == "Save" && (especificacion != "" && codigo != ""))
            {
                bool bFound = false;
                Almacen almn = new Almacen();

                foreach (Almacen item in almnes)
                {
                    if (item.codigoAlmacen == codigo)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (bFound == true)
                {
                    xgrdAlmacen.JSProperties["cpAlertMessage"] = "Exist";
                }
                else
                {

                    almn.AlmacenID = 0;
                    almn.noDocumento = noDocumento;
                    almn.codigoAlmacen = codigo;
                    almn.especificacion = especificacion;
                    almn.notas = notas;
                    almn.clasificacion = clasif;
                    almn.responsable = responsable;
                    almnes.Add(almn);
                }
            }
            else if (pars == "Save" && (especificacion == "" && codigo == ""))
            {
                xgrdAlmacen.JSProperties["cpAlertMessage"] = "SelectOne";
            }

            if (pars == "Delete")
            {
                xgrdAlmacen.DataSource = null;
                almnes.Clear();
            }
            else
            {
                if (pars != "Save")
                {
                    var Valores = e.Parameters;
                    string[] data = Valores.Split(',');

                    foreach (string d in data)
                    {
                        string valor = d.Replace("chk", "");
                        string[] datos = valor.Split('-');
                        string idalmn = datos[0];
                        string folio = datos[1];
                        almnes = almnes.FindAll(p => p.codigoAlmacen != folio);
                    }
                }

                xgrdAlmacen.DataSource = almnes;
            }

            xgrdAlmacen.DataBind();
        }

        protected void xgrdAlmacen_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("AlmacenID").ToString() + "-" + e.GetValue("codigoAlmacen").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkAlmn' id='chk{0}'>", id);
            }
        }

        protected void xgrdAprobaciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            xgrdAprobaciones.JSProperties["cpAlertMessage"] = string.Empty;
            var pars = e.Parameters;

            string EmpleadoId = cmbEmpleadoAdd.SelectedItem.Value.ToString();
            string code = "";
            string user = "";
            var BEmpleado = new EmpleadosDa();
            List<Empleados> empleados = BEmpleado.GetCatalog(cmbPuestoAdd.SelectedItem.Value.ToString(), "", "", "",true);
            foreach (Empleados item in empleados)
            {
                if(item.EmpleadoId.ToString() == EmpleadoId)
                {
                    code = item.Codigo;
                    user = item.NombreCompleto;
                    break;
                }
            }

            TextBox txtCode = (TextBox)ASPxCallbackPanel1.FindControl("txtCodigoAprobacionAdd");

            string noDocumento = lblnDoc.Text;
            string codigo = txtCodigoAprobacionAdd.Text;
            string paso = txtPasoAdd.Text;
            string titulo = txtTituloAdd.Text;
            string codigoEmpleado = code;
            string usuario = user;
            string puesto = cmbPuestoAdd.SelectedItem.Value.ToString();
            string fechaNotificacion = xDateFechaNotAdd.Date.ToString("dd-MM-yyyy");
            string fechaAccion = xDateFechaAccionAdd.Date.ToString("dd-MM-yyyy");
            string accion = txtAccionAdd.Text;
            string coment = txtComentarioAdd.Text;

            if (pars == "Save" && (titulo != "" && codigo != ""))
            {
                bool bFound = false;
                Aprobacion aprb = new Aprobacion();

                foreach (Aprobacion item in aprbnes)
                {
                    if (item.codigoAprobaciones == codigo)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (bFound == true)
                {
                    xgrdAprobaciones.JSProperties["cpAlertMessage"] = "Exist";
                }
                else
                {
                    aprb.AprobacionesID = 0;
                    aprb.noDocumento = noDocumento;
                    aprb.codigoAprobaciones = codigo;
                    aprb.paso = paso;
                    aprb.titulo = titulo;
                    aprb.codigoEmpleado = codigoEmpleado;
                    aprb.usuario = usuario;
                    aprb.puesto = puesto;
                    aprb.fechaNotificacion = fechaNotificacion;
                    aprb.fechaAccion = fechaAccion;
                    aprb.accion = accion;
                    aprb.comentario = coment;
                    aprbnes.Add(aprb);
                }
            }
            else if (pars == "Save" && (titulo == "" && codigo == ""))
            {
                xgrdAprobaciones.JSProperties["cpAlertMessage"] = "SelectOne";
            }

            if (pars == "Delete")
            {
                xgrdAprobaciones.DataSource = null;
                aprbnes.Clear();
            }
            else
            {
                if (pars != "Save")
                {
                    var Valores = e.Parameters;
                    string[] data = Valores.Split(',');

                    foreach (string d in data)
                    {
                        string valor = d.Replace("chk", "");
                        string[] datos = valor.Split('-');
                        string idaprb = datos[0];
                        string folio = datos[1];
                        aprbnes = aprbnes.FindAll(p => p.codigoAprobaciones != folio);
                    }
                }

                xgrdAprobaciones.DataSource = aprbnes;
            }
            xgrdAprobaciones.DataBind();
        }

        protected void xgrdAprobaciones_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.Name == "CheckID")
            {
                var id = e.GetValue("AprobacionesID").ToString() + "-" + e.GetValue("codigoAprobaciones").ToString();

                e.Cell.Text = string.Format("<input type='checkbox' class='chkAprb' id='chk{0}'>", id);
            }
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            var param = e.Parameter;
            var pS = param.Split(',');
            string CodigodPuestoSelected = "";

            if (pS.Length > 0)
            {
                if (pS[0] == "filterPuesto")
                {
                    CodigodPuestoSelected = pS[1];
                    var BEmpleado = new EmpleadosDa();
                    cmbEmpleadoAdd.TextField = "NombreCompleto";
                    cmbEmpleadoAdd.ValueField = "EmpleadoId";
                    cmbEmpleadoAdd.DataSource = BEmpleado.GetCombo(CodigodPuestoSelected);
                    cmbEmpleadoAdd.DataBind();
                }
                if (pS[0] == "Limpiar")
                {
                    limpiarModalAprnes();
                }
            }
            else
            {
                ASPxCallbackPanel1.JSProperties["cpAlertMessage"] = "errror";
            }
        }
    }
}